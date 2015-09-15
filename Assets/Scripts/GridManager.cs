using UnityEngine;
using System.Collections;

public class GridManager: MonoBehaviour{
	//following public variable is used to store the hex model prefab;
	//instantiate it by dragging the prefab on this variable using unity editor;
	public GridNode Hex;

	//next two variables can also be instantiated using unity editor
	public static int gridWidthInHexes = 10;
	public static int gridHeightInHexes = 10;

	public HexArray field = null;

	private GridNode[, ] hexArray;
	private bool[, ] filledArray;

	//Hexagon tile width and height in game world
	private float hexWidth;
	private float hexHeight;


	//Returns array version of 
	GridNode[, ] getArray(){
		return hexArray;
	}

	void setGridSize(){
		gridWidthInHexes = hexArray.GetLength (0);
		gridHeightInHexes = hexArray.GetLength (1);

	}

	//Method to initialise Hexagon width and height
	void setSizes()
	{
		//renderer component attached to the Hex prefab is used to get the current width and height
		hexWidth = Hex.renderer.bounds.size.x;
		hexHeight = Hex.renderer.bounds.size.z;
	}
	
	//Method to calculate the position of the first hexagon tile
	//The center of the hex grid is (0,0,0)
	Vector3 calcInitPos()
	{
		Vector3 initPos;
		//the initial position will be in the left upper corner
		initPos = new Vector3(-hexWidth * gridWidthInHexes / 2f + hexWidth / 2, 0,
		                      gridHeightInHexes / 2f * hexHeight - hexHeight / 2);
		
		return initPos;
	}
	
	//method used to convert hex grid coordinates to game world coordinates
	public Vector3 calcWorldCoord(Vector2 gridPos)
	{
		//Position of the first hex tile
		Vector3 initPos = calcInitPos();
		//Every second row is offset by half of the tile width
		float offset = 0;
		if (gridPos.y % 2 != 0)
			offset = hexWidth;
		
		float x =  initPos.x /*+ offset*/ + gridPos.x * hexWidth;
		//Every new line is offset in z direction by 3/4 of the hexagon height
		float z = initPos.z - gridPos.y * hexHeight;// * 0.75f;
		return new Vector3(x, 0, z);
	}
	
	//Finally the method which initialises and positions all the tiles
	void createGrid()
	{
		hexArray = new GridNode[gridWidthInHexes, gridHeightInHexes];
		filledArray = new bool[gridWidthInHexes, gridHeightInHexes];
		setGridSize();
		//Game object which is the parent of all the hex tiles
		//GameObject hexGridGO = new GameObject("HexGrid");
		
		for (float x = 0; x < gridWidthInHexes; x++)
		{
			for (float y = 0; y < gridHeightInHexes; y++)
			{
				//GameObject assigned to Hex public variable is cloned
				GridNode hex = (GridNode)Instantiate(Hex);
				//Current position in grid
				Vector2 gridPos = new Vector2(x, y);
				hex.transform.position = calcWorldCoord(gridPos);
				//hex.transform.parent = hexGridGO.transform;
				hexArray[(int)x, (int)y] = hex as GridNode;

			}
		}
		//field.setArray(hexArray);
	}

	//Creates a grid with the help of a given HexArray
	void createGridInput()
	{
		hexArray = field.getArray();
		filledArray = new bool[hexArray.GetLength (0), hexArray.GetLength (1)];
		setGridSize();
		//Game object which is the parent of all the hex tiles
		//GameObject hexGridGO = new GameObject("HexGrid");
		
		for (float y = 0; y < gridHeightInHexes; y++)
		{
			for (float x = 0; x < gridWidthInHexes; x++)
			{
				//GameObject assigned to Hex public variable is cloned
				GridNode hex = (GridNode)Instantiate(Hex);
				//Current position in grid
				Vector2 gridPos = new Vector2(x, y);
				hex.transform.position = calcWorldCoord(gridPos);
				//hex.transform.parent = hexGridGO.transform;
				hexArray[(int)x, (int)y] = hex as GridNode;
				filledArray[(int)x, (int)y] = false;
			}
		}


		//NOTES on how the grid system works.
		//It's a HACK. Just how it is. Nevertheless:
		//UP: 			y--
		//DOWN:			y++
		//RIGHT:		x--
		//LEFT:			x++
		//Input FORMAT:	[y,x]
		for (float x = 0; x < gridHeightInHexes; x++)
		{
			print("X: "+x);
			for (float y = 0; y < gridWidthInHexes; y++)
			{
				print("Y: "+y);
				if(y>0){
					hexArray[(int)y, (int)x].setUp(hexArray[(int)y-1, (int)x]);
					print("UP SET");
				}
				if(y<gridWidthInHexes-1){
					hexArray[(int)y, (int)x].setDown(hexArray[(int)y+1, (int)x]);
				}
				//print("MIDBREAK1");
				if(x>0){
					hexArray[(int)y, (int)x].setRight (hexArray[(int)y, (int)x-1]);
					print("RIGHT SET");
				}
				//print("MIDBREAK2");
				if(x<gridHeightInHexes-1){
					hexArray[(int)y, (int)x].setLeft (hexArray[(int)y, (int)x+1]);

				}
					
				
			}
		}

		field.setArray(hexArray);
	}
	
	//The grid should be generated on game start
	void Start()
	{
		setSizes();
		/*
		if (!(field != null)) {//If field == null
			createGridInput();
			Debug.Log("FIELD GIVEN");
		}
		else{
			Debug.Log("NO FIELD GIVEN");
			createGrid();
		}*/

		createGridInput();
	}
}