#include "ssort.h"
#include <ctype.h>
#include <string.h>

// Function to check if a word is valid based on its type
int is_valid_word(const char *word, const char *wtype) {
    if (strcmp(wtype, "ALPHA") == 0) {
        // Check if the word contains only alphabetic characters
        for (int i = 0; i < strlen(word); i++) {
            if (!isalpha(word[i])) {
                return 0; // Return 0 if any non-alphabetic character is found
            }
        }
    } else if (strcmp(wtype, "ALPHANUM") == 0) {
        int has_alpha = 0, has_num = 0;
        // Check if the word contains both alphabetic and numeric characters
        for (int i = 0; i < strlen(word); i++) {
            if (isalpha(word[i])) {
                has_alpha = 1; // Set the flag for alphabetic characters
            }
            if (isdigit(word[i])) {
                has_num = 1; // Set the flag for numeric characters
            }
            if (!isalnum(word[i])) {
                return 0; // Return 0 if any non-alphanumeric character is found
            }
        }
        // Return 0 if the word does not contain both alphabetic and numeric characters
        if (!(has_alpha && has_num)) {
            return 0;
        }
    } else if (strcmp(wtype, "ALL") == 0) {
        // ALL words are considered valid, no filtering needed
        return 1; // Return 1 to indicate validity
    }
    return 1; // Default to returning 1 for any other word type
}
