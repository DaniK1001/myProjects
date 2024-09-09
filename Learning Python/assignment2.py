"""
-----------------------------------------------------
# COMP 348 - Assignment 1
# Written by: Danial Kouba (4027789) Section AB
# The main file
# -----------------------------------------------------
"""

import json
import sys
import random
import math

def load_json(filename):
    # Load data from a JSON file
    with open(filename, 'r') as file:
        data = json.load(file)
    return data

def calculate_global_statistics(data):
    # Extract base stations data
    base_stations = data['baseStations']
    total_base_stations = len(base_stations)
    
    total_antennas = 0
    antennas_per_bs = []
    points_coverage = {}
    
    # Iterate through each base station and antenna
    for bs in base_stations:
        num_antennas = len(bs['ants'])
        antennas_per_bs.append(num_antennas)
        total_antennas += num_antennas
        for ant in bs['ants']:
            for pt in ant['pts']:
                point = (pt[0], pt[1])
                if point not in points_coverage:
                    points_coverage[point] = []
                points_coverage[point].append((bs['id'], ant['id'], pt[2]))

    # Calculate statistics
    max_antennas_per_bs = max(antennas_per_bs)
    min_antennas_per_bs = min(antennas_per_bs)
    avg_antennas_per_bs = sum(antennas_per_bs) / total_base_stations

    points_exactly_one = sum(1 for covers in points_coverage.values() if len(covers) == 1)
    points_more_than_one = sum(1 for covers in points_coverage.values() if len(covers) > 1)
    
    min_lat = data['min_lat']
    max_lat = data['max_lat']
    min_lon = data['min_lon']
    max_lon = data['max_lon']
    step = data['step']
    
    total_possible_points = (int(((max_lat - min_lat) / step))+1) * (int((max_lon - min_lon) / step )+2)
    points_no_coverage = total_possible_points - len(points_coverage)
    
    max_antennas_covering_one_point = max(len(covers) for covers in points_coverage.values())
    avg_antennas_covering_point = sum(len(covers) for covers in points_coverage.values()) / len(points_coverage)
    
    percentage_covered_area = (len(points_coverage) / total_possible_points) * 100

    # Calculate antenna coverage count
    antenna_coverage_count = {}
    for covers in points_coverage.values():
        for bs_id, ant_id, _ in covers:
            if (bs_id, ant_id) not in antenna_coverage_count:
                antenna_coverage_count[(bs_id, ant_id)] = 0
            antenna_coverage_count[(bs_id, ant_id)] += 1

    # Find base station and antenna with maximum coverage
    bs_id_max_coverage, ant_id_max_coverage = max(antenna_coverage_count, key=antenna_coverage_count.get)

    return {
        "total_base_stations": total_base_stations,
        "total_antennas": total_antennas,
        "max_min_avg_antennas_per_bs": (max_antennas_per_bs, min_antennas_per_bs, avg_antennas_per_bs),
        "points_exactly_one": points_exactly_one,
        "points_more_than_one": points_more_than_one,
        "points_no_coverage": points_no_coverage,
        "max_antennas_covering_one_point": max_antennas_covering_one_point,
        "avg_antennas_covering_point": avg_antennas_covering_point,
        "percentage_covered_area": percentage_covered_area,
        "bs_id_max_coverage": bs_id_max_coverage,
        "ant_id_max_coverage": ant_id_max_coverage
    }

def calculate_base_station_statistics(data, base_station_id):
    # Extract data for a specific base station
    base_station = next(bs for bs in data['baseStations'] if bs['id'] == base_station_id)
    total_antennas = len(base_station['ants'])
    
    points_coverage = {}
    # Iterate through each antenna of the base station
    for ant in base_station['ants']:
        for pt in ant['pts']:
            point = (pt[0], pt[1])
            if point not in points_coverage:
                points_coverage[point] = []
            points_coverage[point].append((base_station['id'], ant['id'], pt[2]))

    # Calculate statistics
    points_exactly_one = sum(1 for covers in points_coverage.values() if len(covers) == 1)
    points_more_than_one = sum(1 for covers in points_coverage.values() if len(covers) > 1)
    
    min_lat = data['min_lat']
    max_lat = data['max_lat']
    min_lon = data['min_lon']
    max_lon = data['max_lon']
    step = data['step']
    
    total_possible_points = round((((max_lat - min_lat) / step) + 1) * (((max_lon - min_lon) / step) + 2))
    points_no_coverage = total_possible_points - len(points_coverage)
    
    max_antennas_covering_one_point = max(len(covers) for covers in points_coverage.values())
    avg_antennas_covering_point = sum(len(covers) for covers in points_coverage.values()) / len(points_coverage)
    
    percentage_covered_area = (len(points_coverage) / total_possible_points) * 100
    
    # Calculate antenna coverage count
    antenna_coverage_count = {}
    for covers in points_coverage.values():
        for bs_id, ant_id, _ in covers:
            if (bs_id, ant_id) not in antenna_coverage_count:
                antenna_coverage_count[(bs_id, ant_id)] = 0
            antenna_coverage_count[(bs_id, ant_id)] += 1

    # Find antenna with maximum coverage
    _, ant_id_max_coverage = max(antenna_coverage_count, key=antenna_coverage_count.get)

    return {
        "total_antennas": total_antennas,
        "points_exactly_one": points_exactly_one,
        "points_more_than_one": points_more_than_one,
        "points_no_coverage": points_no_coverage,
        "max_antennas_covering_one_point": max_antennas_covering_one_point,
        "avg_antennas_covering_point": avg_antennas_covering_point,
        "percentage_covered_area": percentage_covered_area,
        "ant_id_max_coverage": ant_id_max_coverage
    }

def check_coverage(data, lat, lon):
    # Create a dictionary to store points coverage
    points_coverage = {}
    for bs in data['baseStations']:
        for ant in bs['ants']:
            for pt in ant['pts']:
                point = (pt[0], pt[1])
                if point not in points_coverage:
                    points_coverage[point] = []
                points_coverage[point].append((bs['id'], ant['id'], pt[2]))
    
    # Check if the input point is covered
    if (lat, lon) in points_coverage:
        return points_coverage[(lat, lon)], None
    else:
        # Find the nearest point with coverage
        nearest_point = min(points_coverage.keys(), key=lambda p: math.hypot(p[0] - lat, p[1] - lon))
        return None, (nearest_point, points_coverage[nearest_point])

def display_menu():
    # Display menu options
    print("\n")
    print("===========================MENU================================")
    print("1. Display Global Statistics")
    print("2. Display Base Station Statistics")
    print("3. Check Coverage")
    print("4. Exit")
    print("===========================MENU================================")

def main():

    # Ensure correct usage
    if len(sys.argv) != 2:
        print("Usage: python3 assignment2.py <test_file.json>")
        sys.exit(1)

    filename = sys.argv[1]
    data = load_json(filename)

    # Main loop to display the menu and process user input
    while True:
        display_menu()
        choice = input("Enter your choice: ")

        if choice == '1':
            print("++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++")
            global_stats = calculate_global_statistics(data)
            print(f"Total number of base stations = {global_stats['total_base_stations']}")
            print(f"Total number of antennas = {global_stats['total_antennas']}")
            max_min_avg = global_stats['max_min_avg_antennas_per_bs']
            print(f"The max, min and average of antennas per BS = {max_min_avg[0]}, {max_min_avg[1]}, {max_min_avg[2]}")
            print(f"Total number of points covered by exactly one antenna = {global_stats['points_exactly_one']}")
            print(f"Total number of points covered by more than one antenna = {global_stats['points_more_than_one']}")
            print(f"Total number of points not covered by any antenna = {global_stats['points_no_coverage']}")
            print(f"The maximum number of antennas that cover one point = {global_stats['max_antennas_covering_one_point']}")
            print(f"The average number of antennas covering a point = {global_stats['avg_antennas_covering_point']:.2f}")
            print(f"The percentage of the covered area = {global_stats['percentage_covered_area']:.2f}%")
            print(f"The id of the base station and antenna covering the maximum number of points = base station {global_stats['bs_id_max_coverage']}, antenna {global_stats['ant_id_max_coverage']}")
            print("++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++ \n")
        
        elif choice == '2':
            print("1. Statistics for a random station")
            print("2. Choose a station by Id.")
            sub_choice = input("Enter your choice: ")

            if sub_choice == '1':
                # Randomly select a base station ID
                bs_id = random.choice([bs['id'] for bs in data['baseStations']])
                print(f"Randomly chosen base station ID : {bs_id}")
            elif sub_choice == '2':
                # User input for base station ID
                bs_id = int(input("Enter base station ID: "))
            
            if bs_id:
                # Calculate and display statistics for the chosen base station
                bs_stats = calculate_base_station_statistics(data, bs_id)
                print("++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++")
                print(f"Total number of antennas = {bs_stats['total_antennas']}")
                print(f"Total number of points covered by exactly one antenna = {bs_stats['points_exactly_one']}")
                print(f"Total number of points covered by more than one antenna = {bs_stats['points_more_than_one']}")
                print(f"Total number of points not covered by any antenna = {bs_stats['points_no_coverage']}")
                print(f"The maximum number of antennas that cover one point = {bs_stats['max_antennas_covering_one_point']}")
                print(f"The average number of antennas covering a point = {bs_stats['avg_antennas_covering_point']:.2f}")
                print(f"The percentage of the covered area = {bs_stats['percentage_covered_area']:.2f}%")
                print(f"The id of the antenna that covers the maximum number of points = antenna {bs_stats['ant_id_max_coverage']}")
                print("++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++")


        elif choice == '3':
            # Check coverage for a specific point
            lat = float(input("Enter latitude: "))
            lon = float(input("Enter longitude: "))
            coverage, nearest = check_coverage(data, lat, lon)
            if coverage:
                for bs_id, ant_id, power in coverage:
                    print(f"Base station {bs_id}, Antenna {ant_id}, Received power {power} dBm")
            else:
                # Display nearest antenna if point is not covered
                nearest_point, nearest_coverage = nearest
                nearest_bs_id, nearest_ant_id, _ = nearest_coverage[0]
                print(f"The point ({lat}, {lon}) is not covered by any antenna.")
                print(f"The nearest antenna is at ({nearest_point[0]}, {nearest_point[1]}) by base station {nearest_bs_id}, antenna {nearest_ant_id}.")

        elif choice == '4':
            # Exit the program
            print("Exiting...")
            break
        
        else:
            print("Invalid choice. Please try again.")

if __name__ == "__main__":
    main()
