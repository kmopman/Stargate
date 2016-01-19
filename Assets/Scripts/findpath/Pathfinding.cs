using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System;

public class Pathfinding : MonoBehaviour {
	
    //Scripts
    PathRequestManager requestManager;
    Grid grid;//get grid script
    //Scripts


    void Awake() {
		requestManager = GetComponent<PathRequestManager>();
		grid = GetComponent<Grid>();//gets the grit info
    }
	

	public void StartFindPath(Vector3 startPosition, Vector3 targetPosition) {
		StartCoroutine(FindPath(startPosition,targetPosition));
	}

	IEnumerator FindPath(Vector3 startPosition, Vector3 targetPosition)//here we get 2 positions from the game and check on wich node they are, we do that with NodeFromWorldPoint wich we have in the grid script
    {
		Vector3[] waypoints = new Vector3[0];
		bool pathSuccess = false;

		Node startNode = grid.NodeFromWorldPoint(startPosition);//this is going to be the startng point f the path
        Node targetNode = grid.NodeFromWorldPoint(targetPosition);//this wil be the  base or player etc, just the target

        if (startNode.walkable && targetNode.walkable) {
			Heap<Node> openSet = new Heap<Node>(grid.MaxSize);//list of nodes that havent been checked yet
            HashSet<Node> closedSet = new HashSet<Node>();//a list for the nodes that have been checked
            openSet.Add(startNode);//the verry first node that wil be checked, this is the starting position

            while (openSet.Count > 0)//as long as there are nodes to be checked, go on :)
            {
				Node currentNode = openSet.RemoveFirst();//first to be checked a
                closedSet.Add(currentNode);

				if (currentNode == targetNode)
                {
					pathSuccess = true;
					break;
				}

				foreach (Node neighbour in grid.GetNeighbours(currentNode)) {
					if (!neighbour.walkable || closedSet.Contains(neighbour)) {
						continue;
					}

					int newCostToNeighbour = currentNode.gCost + GetDistance(currentNode, neighbour);
					if (newCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour)) {
						neighbour.gCost = newCostToNeighbour;
						neighbour.hCost = GetDistance(neighbour, targetNode);
						neighbour.parent = currentNode;

						if (!openSet.Contains(neighbour))
							openSet.Add(neighbour);
					}
				}
			}
		}
		yield return null;
		if (pathSuccess) {
			waypoints = RetracePath(startNode,targetNode);
		}
		requestManager.FinishedProcessingPath(waypoints,pathSuccess);

	}

	Vector3[] RetracePath(Node startNode, Node endNode)//function to.. retrace its path
    {
		List<Node> path = new List<Node>();
		Node currentNode = endNode;

		while (currentNode != startNode) {
			path.Add(currentNode);
			currentNode = currentNode.parent;
		}
		Vector3[] waypoints = SimplifyPath(path);
		Array.Reverse(waypoints);
		return waypoints;

	}

	Vector3[] SimplifyPath(List<Node> path) {
		List<Vector3> waypoints = new List<Vector3>();
		Vector2 directionOld = Vector2.zero;

		for (int i = 1; i < path.Count; i ++) {
			Vector2 directionNew = new Vector2(path[i-1].gridX - path[i].gridX,path[i-1].gridY - path[i].gridY);
			if (directionNew != directionOld) {
				waypoints.Add(path[i].worldPosition);
			}
			directionOld = directionNew;
		}
		return waypoints.ToArray();
	}

	int GetDistance(Node firstNode, Node secondNode)//here i calculate the distance between 2 nodes(node A and B
    {
		int distanceX = Mathf.Abs(firstNode.gridX - secondNode.gridX);//here i check the x distance
        int distanceY = Mathf.Abs(firstNode.gridY - secondNode.gridY);//here i check the y distance
        //the biggest number - the smaller 1 gives the ammount of horizontal steps that you have to do, like y=7 x = 2 means 5 (5*10)steps horizontal and 2 steps diagonal(2*14)
        if (distanceX > distanceY)
			return 14*distanceY + 10* (distanceX-distanceY);
		return 14*distanceX + 10 * (distanceY-distanceX);
	}


}
