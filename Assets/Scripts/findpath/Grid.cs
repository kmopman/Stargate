using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Grid : MonoBehaviour {

    //Bool
    [SerializeField]
    private bool _showGrid;//do you want to see the gizmo's in the editor?
    //Bool

    //LayerMasks
    [SerializeField]
    private LayerMask unwalkableMask;//the layermask that the obstacles will have where you cant walk.
    //LayerMasls
    
    //Vector2
    [SerializeField]
    private Vector2 _gridSize;//a vector 2 that decides the size of the grid
    //Vector2

    //Float
    [SerializeField]
    private float _nodeRadius;//the radius of the node
    private float _nodeDiameter;//the diameter of the nodes
    //Float

    //Scripts
    private Node[,] _grid;//a 2d array of nodes that will make the grid
    private SpawnSystem _waveStats;//imports spawnsystem
    //Scripts
   
    
    //Int
    private int _gridSizeX, _gridSizeY;//the amount of nodes that wil be in a x line and a y line in the grid(magic trick: do x*y and you get all the nodes in the grid! :D)
    //Int
    
    //GameObjects
    private GameObject _findSpawner;//finds spawner object
    //GameObjects
   

    void Awake() {
        _findSpawner = GameObject.Find("Spawner");
        _waveStats = _findSpawner.GetComponent<SpawnSystem>();

        _nodeDiameter = _nodeRadius*2;//radius = from midle to end, diameter is from end to end
        _gridSizeX = Mathf.RoundToInt(_gridSize.x/_nodeDiameter);//takes the grid and divides it by the diameter of a node to find the ammount if nodes, rounded to a int becouse we cant have hal nodes here
        _gridSizeY = Mathf.RoundToInt(_gridSize.y/_nodeDiameter);//takes the grid and divides it by the diameter of a node to find the ammount if nodes, rounded to a int becouse we cant have hal nodes here
        CreateGrid();//create the grid
        InvokeRepeating("gridRefresh", 0, .25f);//the grid gets refreshed every .25 sec, becouse every frome would be a waste of resources
	}
    void gridRefresh()
    {
        if (_waveStats.waveDone)
        {
           CreateGrid();
        }

    }

	public int MaxSize {
		get {
			return _gridSizeX * _gridSizeY;
		}
	}

	void CreateGrid() {
		_grid = new Node[_gridSizeX,_gridSizeY];//here we draw the grid to check what is walkabl
        Vector3 worldBottomLeft = transform.position - Vector3.right * _gridSize.x/2 - Vector3.forward * _gridSize.y/2;

		for (int x = 0; x < _gridSizeX; x ++)//2 for loops that go thru every node in the grid
        {
			for (int y = 0; y < _gridSizeY; y ++)
            {
				Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * _nodeDiameter + _nodeRadius) + Vector3.forward * (y * _nodeDiameter + _nodeRadius);//actual thing that has the node position
                bool walkable = !(Physics.CheckSphere(worldPoint,_nodeRadius,unwalkableMask));//checks for each worldpoint(node) if it is walkable or not
                _grid[x,y] = new Node(walkable,worldPoint, x,y);//creates final grid with the info if a node is walkable or not
            }
		}
	}

	public List<Node> GetNeighbours(Node node)//list with the neighbours of the node, cant be a array becouse the ammount of neighbours may varry
    {
		List<Node> neighbours = new List<Node>();

		for (int x = -1; x <= 1; x++)
        {
			for (int y = -1; y <= 1; y++)//loop who will search a 3by3 block(except for the middle node)
            {
				if (x == 0 && y == 0)//if it is on the current node it wil continue(skip the rest of this code) 
                    continue;// skip!

				int checkX = node.gridX + x;//the xpos of the neighbour node
                int checkY = node.gridY + y;//the ypos of the neighbour node

                if (checkX >= 0 && checkX < _gridSizeX && checkY >= 0 && checkY < _gridSizeY)//if its in the grid add it to the neighbours list
                {
					neighbours.Add(_grid[checkX,checkY]);//here add it to the neighbour list
                }
			}
		}

		return neighbours;
	}
	

	public Node NodeFromWorldPoint(Vector3 worldPosition)//this method gets a position from the world(wich becomes the target)
    {
		float percentX = (worldPosition.x + _gridSize.x/2) / _gridSize.x;//the place of the target wil be a percentage, if it is of the left it wil be 0, middle 0,5 and right 1.
        float percentY = (worldPosition.z + _gridSize.y/2) / _gridSize.y;
		percentX = Mathf.Clamp01(percentX);//these two mathf make sure that if for somereason the target is outside the grid it wont give error etc
        percentY = Mathf.Clamp01(percentY);//the pos is always between 1 and 0.

        int x = Mathf.RoundToInt((_gridSizeX-1) * percentX);
		int y = Mathf.RoundToInt((_gridSizeY-1) * percentY);
		return _grid[x,y];
	}
	
	void OnDrawGizmos()//a function to draw the gizmo's this wont be visable ingame but in the editor to show me stuff works
    {
		Gizmos.DrawWireCube(transform.position,new Vector3(_gridSize.x,1,_gridSize.y));//here i draw a wirecube the size of the grid itself. why a vector 3 for a vector 2 value? the game is 3d so the y wil go up instead of forward wich is why we put the vector2 y valye on the vector3 z.
        if (_grid != null && _showGrid)
        {
			foreach (Node n in _grid)
            {
				Gizmos.color = (n.walkable)?Color.white:Color.red;
				Gizmos.DrawCube(n.worldPosition, Vector3.one * (_nodeDiameter-.1f));
			}
		}
	}
}