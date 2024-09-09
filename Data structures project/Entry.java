/**
 * Represents an entry in a priority queue, containing a key-value pair.
 *
 * @param <K> the type of the key
 * @param <V> the type of the value
 */
public class Entry<K, V> {
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
