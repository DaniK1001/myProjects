// -----------------------------------------------------
// COMP 348 - Assignment 1
// Written by: Danial Kouba (4027789) Section AB
// The main file
// -----------------------------------------------------
//
#include <stdlib.h>
#include <stdio.h>
#include <string.h>
#include "wordtype.h"
#include "fileread.h"
#include "output.h"
#include "ssort.h"

int main(int argc, char *argv[]) {
    // Check if the minimum number of arguments is provided
    if (argc < 4) {
        fprintf(stderr, "Usage: %s <inputfile> <n> <wtype> [<sorttype>] [<skipword1> <skipword2> ...]\n", argv[0]);
        return 1;
    }

    // Get the input file name from the arguments
    const char *inputfile = argv[1]; ///!!!--- need to remeber what argv is for the exam ---!!!!///
    
    // Get the number of words to read from the arguments and convert to integer
    int n = atoi(argv[2]);
    if (n <= 0) {
        fprintf(stderr, "Error: Invalid number of words specified (n must be > 0).\n");
        return 1;
    }
    
    // Get the word type from the arguments
    const char *wtype = argv[3];
    
    // Get the sort type from the arguments, default to "ASC" if not provided or invalid
    const char *sorttype = (argc > 4 && (strcmp(argv[4], "ASC") == 0 || strcmp(argv[4], "DESC") == 0)) ? argv[4] : "ASC";
    
    // Get the skip words from the arguments if provided
    char **skipwords = (argc > 5) ? &argv[5] : NULL;
    int skipcount = (argc > 5) ? argc - 5 : 0;

    // Allocate memory for the words array
    char **words = (char **)malloc(n * sizeof(char *));
    for (int i = 0; i < n; i++) {
        words[i] = (char *)malloc(MAX_WORD_LENGTH * sizeof(char));
    }

    // Read words from the input file
    int count = read_words(inputfile, words, n, wtype, skipwords, skipcount);
    if (count == -1) {
        // Free allocated memory if reading words failed
        for (int i = 0; i < n; i++) {
            free(words[i]);
        }
        free(words);
        return 1;
    }

    // Sort the words based on the sort type
    sort_words(words, count, sorttype);
    
    // Print the sorted words
    print_words(words, count);

    // Free allocated memory for the words array
    for (int i = 0; i < n; i++) {
        free(words[i]);
    }
    free(words);

    return 0;
}
