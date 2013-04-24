public class DPosition<T> {
	private T value;
	private DPosition<T> pred;
	private DPosition<T> succ;
	
	public T getValue() {
		return value;
	}
	
	public DPosition<T> getPred() {
		return pred;
	}
	
	public DPosition<T> getSucc() {
		return succ;
	}
}
