using UnityEngine;
using System.Collections;

public class GridNode : MonoBehaviour {

	public GridNode left;
	public GridNode right;
	public GridNode up;
	public GridNode down;
	public GameObject locationNode;

	public bool occupied = false;

	public Character panelOwner;

	private Vector3 location;

	public bool panelExists(DirectionEnum.Direction direction){
		if(direction == DirectionEnum.Direction.LEFT){
			return getLeft() != null;
		}
		if(direction == DirectionEnum.Direction.RIGHT){
			return getRight() != null;
		}
		if(direction == DirectionEnum.Direction.UP){
			return getUp() != null;
		}
		if(direction == DirectionEnum.Direction.DOWN){
			return getDown() != null;
		}
		return false;
	}

	//Getters and Setters for Node Locations

	public GridNode getLeft(){
		return left;
	}

	public void setLeft(GridNode nuLeft){
		//left = new GridNode();
		left = nuLeft as GridNode;
	}

	public GridNode getRight(){
		return right;
	}
	
	public void setRight(GridNode nuRight){

		right = nuRight as GridNode;
	}

	public GridNode getUp(){
		return up;
	}
	
	public void setUp(GridNode nuUp){
		up = nuUp as GridNode;
	}

	public GridNode getDown(){
		return down;
	}
	
	public void setDown(GridNode nuDown){
		down = nuDown as GridNode;
	}


	//Returns the vector3 location of the LocationNode game object.

	public Vector3 getLocation(){
		//return locationNode.transform.position;
		return transform.position;
	}


	//Returns whether or not something is currently using the panel as a resting place.
	public bool isOccupied(){
		return occupied;
	}

	public Character getOwner(){
		return panelOwner;
	}

	//Sets the occupancy status of the panel.
	public void setOccupied(Character newOwner){
		occupied = true;
		panelOwner = newOwner; 
	}

	//Resets all occupancy options.
	public void clearOccupied(){
		occupied = false;
		panelOwner = null;
	}

	// Use this for initialization
	void Start () {
		//location = locationNode.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
