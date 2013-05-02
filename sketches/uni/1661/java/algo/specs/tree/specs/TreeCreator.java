package tree.specs;

import tree.DNode;
import tree.DTree;

public class TreeCreator {

	//0:  1
	//1:    2
	//2:      3
	//3:        4 
	public static DTree<Integer> CreateAbnormalTree() {
		DNode<Integer> child3_4 = new DNode<Integer>(4);
		
		DNode<Integer> child2_3 = new DNode<Integer>(3, null, child3_4);
		
		DNode<Integer> child1_2 = new DNode<Integer>(2, null, child2_3);
		DNode<Integer> root = new DNode<Integer>(1, null, child1_2);
		
		return new DTree<Integer>(root);
	}

	//  0:            1
	//  1:        2       3
	//  2:      4   5   6   7
	//  3:     8 9        10 11 
	public static DTree<Integer> CreateTree() {
		DNode<Integer> child3_1 = new DNode<Integer>(8);
		DNode<Integer> child3_2 = new DNode<Integer>(9);
		DNode<Integer> child3_3 = new DNode<Integer>(10);
		DNode<Integer> child3_4 = new DNode<Integer>(11);
		
		DNode<Integer> child2_1 = new DNode<Integer>(4, child3_1, child3_2);
		DNode<Integer> child2_2 = new DNode<Integer>(5);
		DNode<Integer> child2_3 = new DNode<Integer>(6);
		DNode<Integer> child2_4 = new DNode<Integer>(7, child3_3, child3_4);
		
		DNode<Integer> child1_1 = new DNode<Integer>(2, child2_1, child2_2);
		DNode<Integer> child1_2 = new DNode<Integer>(3, child2_3, child2_4);
		DNode<Integer> root = new DNode<Integer>(1, child1_1, child1_2);
		
		return new DTree<Integer>(root);
	}
}
