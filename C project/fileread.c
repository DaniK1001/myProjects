#include <stdio.h>
#include <string.h>
#include "ssort.h"
#include "wordtype.h"

// Function to split a word based on non-alphabetic characters
// This helps separate pure alphabetic segments from a mixed string
void split_alpha_words(const char *word, char **words, int *count, int max_count, char **skipwords, int skipcount) {
    char temp[MAX_WORD_LENGTH]; // Temporary buffer for storing parts of the word
    int temp_index = 0; // Index to keep track of position in the temporary buffer
    
    // Iterate over each character in the input word
    for (int i = 0; i < strlen(word); i++) {
        if (isalpha(word[i])) {
            // If the character is alphabetic, add it to the buffer
            temp[temp_index++] = word[i];
        } else {
            // If the character is not alphabetic, process the current buffer
            if (temp_index > 0) {
                temp[temp_index] = '\0'; // Null-terminate the string
                
                // Check if the word should be skipped
                int skip = 0;
                for (int j = 0; j < skipcount; j++) {
                    if (strcmp(temp, skipwords[j]) == 0) {
                        skip = 1; // Mark the word to be skipped
                        break;
                    }
                }
                
                // If the word is not to be skipped, add it to the words array
                if (!skip && *count < max_count) {
                    strncpy(words[*count], temp, MAX_WORD_LENGTH - 1);
                    words[*count][MAX_WORD_LENGTH - 1] = '\0'; // Ensure null termination
                    (*count)++; // Increment the count of valid words
                }
                temp_index = 0; // Reset the buffer for the next segment
            }
        }
    }
    
    // Process any remaining characters in the buffer
    if (temp_index > 0) {
        temp[temp_index] = '\0'; // Null-terminate the string
        
        // Check if the word should be skipped
        int skip = 0;
        for (int j = 0; j < skipcount; j++) {
            if (strcmp(temp, skipwords[j]) == 0) {
                skip = 1; // Mark the word to be skipped
                break;
            }
        }
        
        // If the word is not to be skipped, add it to the words array
        if (!skip && *count < max_count) {
            strncpy(words[*count], temp, MAX_WORD_LENGTH - 1);
            words[*count][MAX_WORD_LENGTH - 1] = '\0'; // Ensure null termination
            (*count)++; // Increment the count of valid words
        }
    }
}

// Function to split a word based on non-alphanumeric characters
// This helps separate alphanumeric segments from a mixed string
void split_alphanum_words(const char *word, char **words, int *count, int max_count, char **skipwords, int skipcount) {
    char temp[MAX_WORD_LENGTH]; // Temporary buffer for storing parts of the word
    int temp_index = 0; // Index to keep track of position in the temporary buffer
    int has_alpha = 0; // Flag to check if the buffer contains alphabetic characters
    int has_num = 0; // Flag to check if the buffer contains numeric characters
    
    // Iterate over each character in the input word
    for (int i = 0; i < strlen(word); i++) {
        if (isalnum(word[i])) {
            // If the character is alphanumeric, add it to the buffer
            temp[temp_index++] = word[i];
            if (isalpha(word[i])) has_alpha = 1; // Set the alphabetic flag
            if (isdigit(word[i])) has_num = 1; // Set the numeric flag
        } else {
            // If the character is not alphanumeric, process the current buffer
            if (temp_index > 0 && has_alpha && has_num) {
                temp[temp_index] = '\0'; // Null-terminate the string
                
                // Check if the word should be skipped
                int skip = 0;
                for (int j = 0; j < skipcount; j++) {
                    if (strcmp(temp, skipwords[j]) == 0) {
                        skip = 1; // Mark the word to be skipped
                        break;
                    }
                }
                
                // If the word is not to be skipped, add it to the words array
                if (!skip && *count < max_count) {
                    strncpy(words[*count], temp, MAX_WORD_LENGTH - 1);
                    words[*count][MAX_WORD_LENGTH - 1] = '\0'; // Ensure null termination
                    (*count)++; // Increment the count of valid words
                }
                temp_index = 0; // Reset the buffer for the next segment
                has_alpha = 0; // Reset the alphabetic flag
                has_num = 0; // Reset the numeric flag
            } else {
                temp_index = 0; // Reset the buffer if the conditions are not met
                has_alpha = 0; // Reset the alphabetic flag
                has_num = 0; // Reset the numeric flag
            }
        }
    }
    
    // Process any remaining characters in the buffer
    if (temp_index > 0 && has_alpha && has_num) {
        temp[temp_index] = '\0'; // Null-terminate the string
        
        // Check if the word should be skipped
        int skip = 0;
        for (int j = 0; j < skipcount; j++) {
            if (strcmp(temp, skipwords[j]) == 0) {
                skip = 1; // Mark the word to be skipped
                break;
            }
        }
        
        // If the word is not to be skipped, add it to the words array
        if (!skip && *count < max_count) {
            strncpy(words[*count], temp, MAX_WORD_LENGTH - 1);
            words[*count][MAX_WORD_LENGTH - 1] = '\0'; // Ensure null termination
            (*count)++; // Increment the count of valid words
        }
    }
}

// Function to read words from a file and split them based on the specified word type
// This function reads the file line by line and processes each word accordingly
int read_words(const char *filename, char **words, int n, const char *wtype, char **skipwords, int skipcount) {
    FILE *file = fopen(filename, "r"); // Open the file for reading
    if (!file) {
        fprintf(stderr, "Error opening file %s\n", filename); // Print error message if file cannot be opened
        return -1; // Return error code
    }

    char word[MAX_WORD_LENGTH]; // Buffer to store each word read from the file
    int count = 0; // Counter for the number of valid words
    
    // Read words from the file until the end of the file is reached or the limit is reached
    while (fscanf(file, "%s", word) != EOF && count < n) {
        // Tokenize the word by commas and process each token
        char *token = strtok(word, ",");
        while (token != NULL) {
            if (strcmp(wtype, "ALPHA") == 0) {
                // Split the token based on alphabetic characters
                split_alpha_words(token, words, &count, n, skipwords, skipcount);
            } else if (strcmp(wtype, "ALPHANUM") == 0) {
                // Split the token based on alphanumeric characters
                split_alphanum_words(token, words, &count, n, skipwords, skipcount);
            } else if (is_valid_word(token, wtype)) {
                // If the token is valid and doesn't need splitting, check if it should be skipped
                int skip = 0;
                for (int i = 0; i < skipcount; i++) {
                    if (strcmp(token, skipwords[i]) == 0) {
                        skip = 1; // Mark the word to be skipped
                        break;
                    }
                }
                // If the word is not to be skipped, add it to the words array
                if (!skip && count < n) {
                    strncpy(words[count], token, MAX_WORD_LENGTH - 1);
                    words[count][MAX_WORD_LENGTH - 1] = '\0'; // Ensure null termination
                    count++; // Increment the count of valid words
                }
            }
            token = strtok(NULL, ","); // Move to the next token
        }
    }

    fclose(file); // Close the file after reading
    return count; // Return the count of valid words
}
