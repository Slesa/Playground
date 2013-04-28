package tree;

public class DNode<T> {

	private T value;
	private DNode<T> left;
	private DNode<T> right;
	
	public DNode(T value) {
		this.value = value;
	}
	
	public DNode(T value, DNode<T> left, DNode<T> right){
		this.value = value;
		setLeft(left);
		setRight(right);
	}
	
	public T getValue() {
		return this.value;
	}

	public DNode<T> getLeft() {
		return this.left;
	}
	
	public void setLeft(DNode<T> left) {
		this.left = left;
	}
	
	public DNode<T> getRight() {
		return this.right;
	}
	
	public void setRight(DNode<T> right) {
		this.right = right;
	}
}
