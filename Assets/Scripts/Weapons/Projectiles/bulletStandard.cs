using UnityEngine;
using System.Collections;

public class bulletStandard : Hitbox {


	//INHERITS

	/*
	public float scaleX = 1f;
	public float scaleY = 1f;
	public float scaleZ = 1f;

	public int damage = 10;

	public bool visible = false;
	
	public GridNode currentPanel = null;

	DirectionEnum.Direction direction = DirectionEnum.Direction.NONE;
	*/


	//DirectionEnum.Direction direction = DirectionEnum.Direction.NONE;

	//Directions are:
	//"UP"
	//"DOWN"
	//"LEFT"
	//"RIGHT"
	//"NONE"


	//Low Delay, Hi Speed
	public float delay = 1f;

	float tempDelay = 1f;

	public bulletStandard(GridNode node){
		currentPanel = node;
	}

	public bulletStandard(bool isVisible, GridNode givenPanel, DirectionEnum.Direction newDirec, float delay, float scale, int damage){
		currentPanel = givenPanel;
		visible = isVisible;
		direction = newDirec;
		this.delay = delay;
		scaleX = scale;
		scaleY = scale;
		scaleZ = scale;
		this.damage = damage;
	}


	// Use this for initialization
	void Start () {
		tempDelay = delay;
	}
	
	// Update is called once per frame
	void Update () {
		moveCheck(direction);
		transform.position = new Vector3(currentPanel.getLocation().x, currentPanel.getLocation ().y+floatHeight, currentPanel.getLocation().z);

	}

	void moveCheck(DirectionEnum.Direction myDirec){
		{
			tempDelay-=Time.deltaTime;
			if(tempDelay<0){
				tempDelay = delay;

				if(myDirec == DirectionEnum.Direction.NONE){
					if(currentPanel.isOccupied()){
						currentPanel.getOwner().takeDamage(damage);
						Destroy(this.gameObject);
					}

				}
				if(myDirec == DirectionEnum.Direction.LEFT){
					if(currentPanel.getLeft().isOccupied()){
						currentPanel.getLeft().getOwner().takeDamage(damage);
						Destroy(this.gameObject);
					}
					else if(currentPanel.panelExists(myDirec) == false){
						Destroy(this.gameObject);
					}
					else{
						currentPanel = currentPanel.getLeft ();
					}
				}
				if(myDirec == DirectionEnum.Direction.RIGHT){
					if(currentPanel.getRight().isOccupied()){
						currentPanel.getRight().getOwner().takeDamage(damage);
						Destroy(this.gameObject);
					}
					else if(!(currentPanel.getRight() != null)){
						Destroy(this.gameObject);
					}
					else{
						currentPanel = currentPanel.getRight();
					}
				}
				if(myDirec == DirectionEnum.Direction.UP){
					if(currentPanel.getUp().isOccupied()){
						currentPanel.getUp().getOwner().takeDamage(damage);
						Destroy(this.gameObject);
					}
					else if(!(currentPanel.getUp() != null)){
						Destroy(this.gameObject);
					}
					else{
						currentPanel = currentPanel.getUp();
					}
				}
				if(myDirec == DirectionEnum.Direction.DOWN){
					if(currentPanel.getDown().isOccupied()){
						currentPanel.getDown().getOwner().takeDamage(damage);
						Destroy(this.gameObject);
					}
					else if(!(currentPanel.getDown() != null)){
						Destroy(this.gameObject);
					}
					else{
						currentPanel = currentPanel.getDown();
					}
				}
			}
				
		}
	}
}
