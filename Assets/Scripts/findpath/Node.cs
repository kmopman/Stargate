using UnityEngine;
using System.Collections;

public class Node : IHeapItem<Node> {
	
	public bool walkable;//can you walk on this node or not
    public Vector3 worldPosition;//the position of the node, kinda importand
    public int gridX;//xpos of the current node
    public int gridY;//ypos of the current node

    public int gCost;//the ammount of units its from the starting point
    public int hCost;//the ammount of units its from the target
    public Node parent;
	private int _heapIndex;
	
	public Node(bool _walkable, Vector3 _worldPos, int _gridX, int _gridY) {
		walkable = _walkable;
		worldPosition = _worldPos;
		gridX = _gridX;
		gridY = _gridY;
	}

	public int fCost {
		get {
			return gCost + hCost;
		}
	}

	public int HeapIndex {
		get {
			return _heapIndex;
		}
		set {
			_heapIndex = value;
		}
	}

	public int CompareTo(Node nodeToCompare) {
		int compare = fCost.CompareTo(nodeToCompare.fCost);
		if (compare == 0) {
			compare = hCost.CompareTo(nodeToCompare.hCost);
		}
		return -compare;
	}
}
