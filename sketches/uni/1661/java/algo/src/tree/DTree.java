package tree;

public class DTree<T> {

	private DNode<T> root;
	
	public DTree() {
	}
	
	public DTree(T value) {
		this.root = new DNode<T>(value);
	}
	
	public DTree(DNode<T> root) {
		this.root = root;
	}
	
	public DNode<T> getRoot() {
		return this.root;
	}
	
	public int getNodeCount() {
		return calculateNodeCount(this.root);
	}
	
	public int getLeafCount() {
		return calculateLeafCount(this.root);
	}
	
	public int getInnerCount() {
		return calculateInnerCount(this.root);
	}
	
	public int getDepthOf(T value) {
		return calculateDepthOf(this.root, value);
	}
	
	int calculateNodeCount(DNode<T> node) {
		if(node==null) return 0;
		int result = 1;
		result += calculateNodeCount(node.getLeft());
		result += calculateNodeCount(node.getRight());
		return result;
	}

	int calculateLeafCount(DNode<T> node) {
		if(node==null) return 0;
		if(node.getLeft()==null && node.getRight()==null) return 1;
		return calculateLeafCount(node.getLeft()) + calculateLeafCount(node.getRight());
	}

	int calculateInnerCount(DNode<T> node) {
		if(node==null) return 0;
		if(node.getLeft()==null && node.getRight()==null) return 0;
		return 1 + calculateInnerCount(node.getLeft()) + calculateInnerCount(node.getRight());
	}

	int calculateDepthOf(DNode<T> node, T value) {
		if(node==null) return -1;
		if(node.getValue()==value) return 0;
		
		int found = calculateDepthOf(node.getLeft(), value);
		if( found<0)
			found = calculateDepthOf(node.getRight(), value);
		if(found<0 ) return found;
		return 1 + found;
	}
}
