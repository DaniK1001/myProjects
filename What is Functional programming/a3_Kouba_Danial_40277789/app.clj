; -----------------------------------------------------
; Assignment 3, 24-6-2024
; COMP 352 Section AA
; Written by: Danial Kouba (40277789)
; -----------------------------------------------------


(ns app
  (:require [menu :as menu]   ; Importing the 'menu' namespace and aliasing it as 'menu'
            [db :as db]))     ; Importing the 'db' namespace and aliasing it as 'db'

(def citiesDB (db/loadData "cities.txt"))  ; Loading data from "cities.txt" using db/loadData function and storing it in citiesDB

(defn -main []
  (menu/menu citiesDB))   ; Main function that calls menu/menu function with citiesDB as argument

;; Call the main function
(-main)   ; Invoking the main function when the namespace is loaded
