(ns menu
  (:require [clojure.string :as str]   ; Importing clojure.string namespace and aliasing it as str
            [db :as db]))              ; Importing db namespace and aliasing it as db

(defn showMenu []
  (println "\n\n*** City Information Menu ***")   ; Printing the main menu header
  (println "-----------------------------\n")    ; Printing a separator
  (println "1. List cities")                      ; Printing menu options
  (println "2. Display City Information")
  (println "3. List Provinces")
  (println "4. Display Province Information")
  (println "5. Exit")
  (print "\nEnter an option? ")                   ; Prompting user for input
  (flush)                                          ; Ensuring input prompt is shown immediately
  (read-line))                                     ; Reading user input from console

(defn listCities [cities]
  (let [cityNames (map #(str "\"" (:city %) "\"") (sort-by :city cities))]   ; Mapping city names wrapped in quotes
    (println (str "[" (clojure.string/join " " cityNames) "]"))))            ; Printing city names in a list format

(defn listCitiesByProvince [cities province]
  (let [filtered (filter #(= (:province %) province) cities)          ; Filtering cities by province
        cityMap (map vector (range 1 (inc (count filtered))) filtered)]   ; Creating a map with index and city details
    (doseq [[idx city] cityMap]                                       ; Iterating over each city in cityMap
      (println (str idx ": " [(city :city) (city :size) (city :population)])))))   ; Printing city details

(defn listCitiesByDensity [cities province]
  (let [filtered (filter #(= (:province %) province) cities)          ; Filtering cities by province
        sortedDensity (sort-by :density (map #(assoc % :density (/ (:population %) (:area %))) filtered))]   ; Calculating and sorting by density
    (doseq [[idx city] (map vector (range 1 (inc (count sortedDensity))) sortedDensity)]   ; Iterating over sortedDensity with index
      (println (str idx ": [" "\"" (city :city) "\"" " " (city :density) "]")))))   ; Printing city name and density

(defn option1 [cities]
  (println "1.1 List all cities, ordered by city name (ascending)")   ; Printing sub-options
  (println "1.2 List all cities for a given province, ordered by size (descending) and name (ascending).")
  (println "1.3 List all cities for a given province, ordered by population density in ascending order.")
  (let [option2 (read-line)]    ; Reading user sub-option input
    (cond
      (= option2 "1.1") (listCities cities)   ; Calling listCities if sub-option is 1.1
      (= option2 "1.2") (do (print "\nEnter province name: ") (flush) (let [province (read-line)] (listCitiesByProvince cities province)))   ; Prompting for province and calling listCitiesByProvince
      (= option2 "1.3") (do (print "\nEnter province name: ") (flush) (let [province (read-line)] (listCitiesByDensity cities province)))
      :else (println "Invalid sub-option"))))   ; Handling invalid input

(defn option2 [cities]
  (print "\nPlease enter the city name : ")   ; Prompting for city name input
  (flush)                                       ; Ensuring input prompt is shown immediately
  (let [name (read-line)                        ; Reading user input for city name
        city (first (filter #(= (:city %) name) cities))]   ; Filtering cities by input city name
    (if city
      (println (format "[%s %s %s %d %.2f]"
                       (str "\"" (city :city) "\"")      ; Formatting and printing city details
                       (str "\"" (city :province) "\"")
                       (str "\"" (city :size) "\"")
                       (city :population)
                       (city :area)))
      (println "City not found"))))   ; Printing message if city not found

(defn listProvinces [cities]
  (let [province-counts (frequencies (map :province cities))          ; Counting occurrences of each province in cities
        sorted-provinces (sort-by second > province-counts)           ; Sorting provinces by count in descending order
        indexed-provinces (map-indexed (fn [idx [province count]]    ; Mapping each province to its count with index
                                         (str (inc idx) ": [" province " " count "]"))
                                       sorted-provinces)]
    (doseq [line indexed-provinces]   ; Iterating over indexed-provinces
      (println line))                 ; Printing each line
    (println (str "Total: " (count sorted-provinces) " provinces, " (count cities) " cities on file."))))   ; Printing total provinces and cities

(defn option3 [cities]
  (listProvinces cities))   ; Calling listProvinces function to display province information

(defn displayProvinceInfo [cities]
  (let [grouped (group-by :province cities)    ; Grouping cities by province
        sortedGrouped (sort-by key grouped)]   ; Sorting grouped cities by province name
    (doseq [[idx [province cities]] (map-indexed vector sortedGrouped)]   ; Iterating over sortedGrouped with index
      (let [totalPopulation (reduce + (map :population cities))]   ; Calculating total population for each province
        (println (str (inc idx) ": " [province totalPopulation]))))))   ; Printing province name and total population

(defn option4 [cities]
  (displayProvinceInfo cities))   ; Calling displayProvinceInfo function to display province information

(defn processOption [option cities]
  (cond
    (= option "1") (option1 cities)   ; Handling main menu option 1
    (= option "2") (option2 cities)   ; Handling main menu option 2
    (= option "3") (option3 cities)   ; Handling main menu option 3
    (= option "4") (option4 cities)   ; Handling main menu option 4
    :else (println "Invalid Option, please try again")))   ; Handling invalid main menu option

;; Main menu loop function
(defn menu [cities]
  (loop []
    (let [option (str/trim (showMenu))]   ; Getting and trimming user input option from showMenu
      (if (= option "5")                 ; Checking if user chose to exit
        (println "\nGood Bye\n")         ; Printing exit message
        (do                              ; If not exiting, processing selected option
          (processOption option cities)  ; Processing selected option
          (recur))))))                   ; Repeating menu loop
