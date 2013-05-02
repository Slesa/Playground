package list;

public class DPosition<T> {

	private T value;
	private DPosition<T> previous;
	private DPosition<T> next;

	public T getValue() {
		return this.value;
	}
	
	public void setValue(T value) {
		this.value = value;
	}

	public DPosition<T> getPrevious() {
		return this.previous;
	}
	
	public void setPrevious(DPosition<T> previous) {
		this.previous = previous;
	}
	
	public DPosition<T> getNext() {
		return this.next;
	}
	
	public void setNext(DPosition<T> next) {
		this.next = next;
	}
}