package dlist;

public class DPosition<T> {

	private T value;
	private DPosition<T> succ;
	private DPosition<T> pred;

	public T getValue() {
		return this.value;
	}

	public DPosition<T> getSucc() {
		return this.succ;
	}
	
	public DPosition<T> getPred() {
		return this.pred;
	}
}
