import java.util.NoSuchElementException;

/**
 * Implements a Smarter Priority Queue using a binary heap.
 *
 * @param <K> the type of keys stored in the priority queue, must extend Comparable
 * @param <V> the type of values stored in the priority queue
 */
public class SPQ<K extends Comparable<K>, V> {
    private Entry<K, V>[] heap;
    private boolean isMinHeap;
    private int size;
    private static final int DEFAULT_CAPACITY = 10;

    /**
     * Constructs a Smarter Priority Queue with the specified initial capacity and heap type.
     *
     * @param initialCapacity the initial capacity of the priority queue
     * @param isMinHeap       true if the priority queue should be a min-heap, false for max-heap
     */
    @SuppressWarnings("unchecked")
    public SPQ(int initialCapacity, boolean isMinHeap) {
        this.heap = (Entry<K, V>[]) new Entry[initialCapacity];
        this.isMinHeap = isMinHeap;
        this.size = 0;
    }

    /**
     * Constructs a Smarter Priority Queue with default capacity and specified heap type.
     *
     * @param isMinHeap true if the priority queue should be a min-heap, false for max-heap
     */
    public SPQ(boolean isMinHeap) {
        this(DEFAULT_CAPACITY, isMinHeap);
    }

    /**
     * Toggles the priority queue between min-heap and max-heap.
     */
    public void toggle() {
        isMinHeap = !isMinHeap;
        buildHeap();
    }

    /**
     * Inserts a new key-value pair into the priority queue.
     *
     * @param key   the key of the entry
     * @param value the value associated with the key
     * @return the entry object representing the inserted pair
     */
    public Entry<K, V> insert(K key, V value) {
        if (size == heap.length) {
            resize();
        }
        Entry<K, V> entry = new Entry<>(key, value);
        heap[size] = entry;
        upHeap(size);
        size++;
        return entry;
    }

    /**
     * Removes and returns the entry with the top key from the priority queue.
     *
     * @return the entry with the top key, or null if the priority queue is empty
     */
    public Entry<K, V> removeTop() {
        if (isEmpty()) return null;
        swap(0, size - 1);
        Entry<K, V> top = heap[--size];
        heap[size] = null;
        downHeap(0);
        return top;
    }

    /**
     * Returns the entry with the top key from the priority queue without removing it.
     *
     * @return the entry with the top key, or null if the priority queue is empty
     */
    public Entry<K, V> top() {
        if (isEmpty()) return null;
        return heap[0];
    }

    /**
     * Removes a specific entry from the priority queue.
     *
     * @param entry the entry to be removed
     * @return the removed entry, or null if the entry was not found
     */
    public Entry<K, V> remove(Entry<K, V> entry) {
        int index = indexOf(entry);
        if (index == -1) return null;
        swap(index, size - 1);
        Entry<K, V> removed = heap[--size];
        heap[size] = null;
        if (index < size) {
            downHeap(index);
            upHeap(index);
        }
        return removed;
    }

    /**
     * Replaces the key of a specific entry in the priority queue and adjusts its position.
     *
     * @param entry  the entry whose key should be replaced
     * @param newKey the new key to set
     * @return the old key of the entry
     * @throws NoSuchElementException if the entry is not found in the priority queue
     */
    public K replaceKey(Entry<K, V> entry, K newKey) {
        int index = indexOf(entry);
        if (index == -1) throw new NoSuchElementException();
        K oldKey = entry.getKey();
        entry.setKey(newKey);
        if (compare(newKey, oldKey) < 0) {
            upHeap(index);
        } else {
            downHeap(index);
        }
        return oldKey;
    }

    /**
     * Replaces the value of a specific entry in the priority queue.
     *
     * @param entry    the entry whose value should be replaced
     * @param newValue the new value to set
     * @return the old value of the entry
     * @throws NoSuchElementException if the entry is not found in the priority queue
     */
    public V replaceValue(Entry<K, V> entry, V newValue) {
        int index = indexOf(entry);
        if (index == -1) throw new NoSuchElementException();
        V oldValue = entry.getValue();
        entry.setValue(newValue);
        return oldValue;
    }

    /**
     * Returns the current state of the priority queue (Min or Max).
     *
     * @return the current state of the priority queue as a string ("Min" or "Max")
     */
    public String state() {
        return isMinHeap ? "Min" : "Max";
    }

    /**
     * Checks if the priority queue is empty.
     *
     * @return true if the priority queue is empty, false otherwise
     */
    public boolean isEmpty() {
        return size == 0;
    }

    /**
     * Returns the current size of the priority queue.
     *
     * @return the number of entries currently in the priority queue
     */
    public int size() {
        return size;
    }

    /**
     * Moves the entry at the specified index up the heap to maintain heap order.
     *
     * @param index the index of the entry to move up
     */
    private void upHeap(int index) {
        while (index > 0) {
            int parentIndex = (index - 1) / 2;
            if (compare(heap[index].getKey(), heap[parentIndex].getKey()) < 0) {
                swap(index, parentIndex);
                index = parentIndex;
            } else {
                break;
            }
        }
    }

    /**
     * Moves the entry at the specified index down the heap to maintain heap order.
     *
     * @param index the index of the entry to move down
     */
    private void downHeap(int index) {
        while (true) {
            int leftChildIndex = 2 * index + 1;
            int rightChildIndex = 2 * index + 2;
            int targetIndex = index;

            if (leftChildIndex < size &&
                    compare(heap[leftChildIndex].getKey(), heap[targetIndex].getKey()) < 0) {
                targetIndex = leftChildIndex;
            }

            if (rightChildIndex < size &&
                    compare(heap[rightChildIndex].getKey(), heap[targetIndex].getKey()) < 0) {
                targetIndex = rightChildIndex;
            }

            if (targetIndex == index) break;

            swap(index, targetIndex);
            index = targetIndex;
        }
    }

    /**
     * Builds a heap from the current elements in the heap array.
     */
    private void buildHeap() {
        for (int i = size / 2 - 1; i >= 0; i--) {
            downHeap(i);
        }
    }

    /**
     * Swaps two entries in the heap array.
     *
     * @param i the index of the first entry
     * @param j the index of the second entry
     */
    private void swap(int i, int j) {
        Entry<K, V> temp = heap[i];
        heap[i] = heap[j];
        heap[j] = temp;
    }

    /**
     * Finds the index of a specific entry in the heap.
     *
     * @param entry the entry to find in the heap
     * @return the index of the entry in the heap, or -1 if not found
     */
    private int indexOf(Entry<K, V> entry) {
        for (int i = 0; i < size; i++) {
            if (heap[i].equals(entry)) {
                return i;
            }
        }
        return -1;
    }

    /**
     * Resizes the heap array to double its current capacity.
     */
    @SuppressWarnings("unchecked")
    private void resize() {
        Entry<K, V>[] newHeap = (Entry<K, V>[]) new Entry[heap.length * 2];
        System.arraycopy(heap, 0, newHeap, 0, size);
        heap = newHeap;
    }

    /**
     * Compares two keys based on the current state of the priority queue (min or max).
     *
     * @param key1 the first key to compare
     * @param key2 the second key to compare
     * @return a negative integer, zero, or a positive integer as the first key is less than, equal to, or greater than the second
     */
    private int compare(K key1, K key2) {
        return isMinHeap ? key1.compareTo(key2) : key2.compareTo(key1);
    }

    /**
     * Represents an entry in the priority queue containing a key-value pair.
     *
     * @param <K> the type of the key
     * @param <V> the type of the value
     */
    public static class Entry<K, V> {
        private K key;
        private V value;

        /**
         * Constructs a new Entry with the specified key and value.
         *
         * @param key   the key of the entry
         * @param value the value associated with the key
         */
        public Entry(K key, V value) {
            this.key = key;
            this.value = value;
        }

        /**
         * Retrieves the key of this entry.
         *
         * @return the key of the entry
         */
        public K getKey() {
            return key;
        }

        /**
         * Sets a new key for this entry.
         *
         * @param key the new key to set
         */
        public void setKey(K key) {
            this.key = key;
        }

        /**
         * Retrieves the value associated with this entry.
         *
         * @return the value of the entry
         */
        public V getValue() {
            return value;
        }

        /**
         * Sets a new value for this entry.
         *
         * @param value the new value to set
         */
        public void setValue(V value) {
            this.value = value;
        }

        /**
         * Checks whether this entry is equal to another object.
         *
         * @param o the object to compare with
         * @return {@code true} if the objects are equal, {@code false} otherwise
         */
        @Override
        public boolean equals(Object o) {
            if (this == o) return true;
            if (o == null || getClass() != o.getClass()) return false;
            Entry<?, ?> entry = (Entry<?, ?>) o;
            return key.equals(entry.key) && value.equals(entry.value);
        }

        /**
         * Computes the hash code of this entry.
         *
         * @return the hash code of the entry
         */
        @Override
        public int hashCode() {
            int result = key.hashCode();
            result = 31 * result + value.hashCode();
            return result;
        }
    }
}
