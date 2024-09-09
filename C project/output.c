#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include "ssort.h"

// Comparison function for ascending order
int compare_asc(const void *a, const void *b) {
    return strcmp(*(const char **)a, *(const char **)b);
}

// Comparison function for descending order
int compare_desc(const void *a, const void *b) {
    return strcmp(*(const char **)b, *(const char **)a);
}

// Function to sort words in ascending or descending order
void sort_words(char **words, int count, const char *sorttype) {
    // Check the sort type and call the appropriate qsort function
    if (strcmp(sorttype, "ASC") == 0) {
        qsort(words, count, sizeof(char *), compare_asc);
    } else {
        qsort(words, count, sizeof(char *), compare_desc);
    }
}

// Function to print words with line breaks every 10 words
void print_words(char **words, int count) {
    for (int i = 0; i < count; i++) {
        printf("%s", words[i]);
        // Add a line break every 10 words
        if ((i + 1) % 10 == 0) {
            printf("\n");
        } else {
            printf(" ");
        }
    }
    // Add a final line break if the total number of words is not a multiple of 10
    if (count % 10 != 0) {
        printf("\n");
    }
}
