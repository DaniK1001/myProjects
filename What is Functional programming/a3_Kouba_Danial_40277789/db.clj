(ns db
  (:require [clojure.string :as str])) ; Importing the clojure.string library with alias 'str'

;; Function to parse a line of text into a map of data
(defn parse-line [line]
  ;; Destructure the line into individual components using "|" as delimiter
  (let [[city province size population area] (str/split line #"\|")]
    ;; Return a map with keys :city, :province, :size, :population, :area
    {:city city                            ; Store the city name
     :province province                    ; Store the province name
     :size size                            ; Store the size (e.g., Large urban)
     :population (read-string population)  ; Convert population string to a number
     :area (read-string area)}))           ; Convert area string to a number

;; Function to load data from a file and parse each line
(defn loadData [filename]
  ;; Read the entire file as a string, split into lines
  (let [lines (str/split-lines (slurp filename))]
    ;; Map each line through the parse-line function to parse and transform into maps
    (map parse-line lines)))
