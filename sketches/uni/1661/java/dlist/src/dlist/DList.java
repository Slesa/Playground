package dlist;

public class DList<T> {

	DPosition<T> head;
	DPosition<T> tail;
	
	public DList() {
		this.head = null;
		this.tail = null;
	}
	public DPosition<T> getHead() {
		return this.head;
	}
	
	public DPosition<T> getTail() {
		return this.tail;
	}
	
	public DList<T> append(T element) {
		DPosition<T> pos = new DPosition<T>();
		pos.setValue(element);
		
		if(this.head==null) {
			this.head = pos;
		} else {
			this.tail.setNext(pos);
			pos.setPrevious(this.tail);
		}
		tail = pos;
		return this;
	}
	
	public DList<T> prepend(T element) {
		DPosition<T> pos = new DPosition<T>();
		pos.setValue(element);
		
		if(this.head==null) {
			this.tail = pos;
		} else {
			this.head.setPrevious(pos);
			pos.setNext(this.head);
		}
		head = pos;
		return this;
	}
}
