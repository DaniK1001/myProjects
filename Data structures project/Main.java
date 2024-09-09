/**
 * -----------------------------------------------------
 * Assignment 3, 23-6-2024
 * COMP 352 Section AA
 * Written by: Danial Kouba (40277789)
 * -----------------------------------------------------
 */

public class Main {

    /**
     * Main method to demonstrate the usage of Smarter Priority Queue (SPQ).
     *
     * @param args command-line arguments (not used in this application)
     */
    public static void main(String[] args) {
        // Create a new instance of SmarterPQ
        SPQ<Integer, String> spq = new SPQ<>(2, true);

        System.out.println("top of spq " + spq.top());

        // Insert elements into the SmarterPQ
        spq.insert(5, "A");
        spq.insert(9, "B");
        spq.insert(3, "C");
        spq.insert(7, "D");

        // 1. Check the top element (should be the minimum element since it's a min-heap by default)
        System.out.println("Top element (min-heap): " + spq.top().getKey() + ", " + spq.top().getValue());

        // 2. Toggle to max-heap
        spq.toggle();
        System.out.println("State after toggle: " + spq.state());

        // 3. Check the top element (should be the maximum element now)
        System.out.println("Top element (max-heap): " + spq.top().getKey() + ", " + spq.top().getValue());

        // 4. Insert more elements
        spq.insert(1, "E");
        spq.insert(4, "F");

        // 5. Check the top element again
        System.out.println("Top element (max-heap after insertion): " + spq.top().getKey() + ", " + spq.top().getValue());

        // 6. Remove the top element
        SPQ.Entry<Integer, String> removed = spq.removeTop();
        System.out.println("Removed top element: " + removed.getKey() + ", " + removed.getValue());

        // 7. Check the new top element
        System.out.println("New top element (max-heap): " + spq.top().getKey() + ", " + spq.top().getValue());

        // 8. Toggle back to min-heap
        spq.toggle();
        System.out.println("State after toggle: " + spq.state());

        // 9. Check the top element (should be the minimum element now)
        System.out.println("Top element (min-heap): " + spq.top().getKey() + ", " + spq.top().getValue());

        // 10. Insert a new element and check the top element
        spq.insert(6, "G");
        System.out.println("Top element (min-heap after insertion): " + spq.top().getKey() + ", " + spq.top().getValue());

        // 11. Remove an element by entry reference
        SPQ.Entry<Integer, String> entryToRemove = spq.top(); // the current top entry
        spq.remove(entryToRemove);
        System.out.println("Removed specified element: " + entryToRemove.getKey() + ", " + entryToRemove.getValue());

        // 12. Check the new top element
        System.out.println("New top element (min-heap): " + spq.top().getKey() + ", " + spq.top().getValue());

        // 13. Replace the key of an entry
        SPQ.Entry<Integer, String> entryToReplaceKey = spq.top();
        Integer oldKey = spq.replaceKey(entryToReplaceKey, 8);
        System.out.println("Replaced key of entry: " + oldKey + " with new key: " + entryToReplaceKey.getKey());

        // 14. Check the new top element
        System.out.println("New top element after key replacement (min-heap): " + spq.top().getKey() + ", " + spq.top().getValue());

        // 15. Replace the value of an entry
        SPQ.Entry<Integer, String> entryToReplaceValue = spq.top();
        String oldValue = spq.replaceValue(entryToReplaceValue, "H");
        System.out.println("Replaced value of entry: " + oldValue + " with new value: " + entryToReplaceValue.getValue());

        // 16. Check the top element after value replacement
        System.out.println("New top element after value replacement (min-heap): " + spq.top().getKey() + ", " + spq.top().getValue());

        // 17. Check the size of the SmarterPQ
        System.out.println("Size of SmarterPQ: " + spq.size());

        // 18. Check if the SmarterPQ is empty
        System.out.println("Is SmarterPQ empty? " + spq.isEmpty());

        // 19. Remove all elements and check if the queue is empty
        while (!spq.isEmpty()) {
            SPQ.Entry<Integer, String> entry = spq.removeTop();
            System.out.println("Removed element: " + entry.getKey() + ", " + entry.getValue());
        }
        System.out.println("Is SPQ empty after removing all elements? " + spq.isEmpty());

        // 20. Check the state of the SmarterPQ
        System.out.println("State of SPQ: " + spq.state());
    }
}
