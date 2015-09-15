using UnityEngine;
using System.Collections;

public class ControlledCharacter : Character {

	//INHERITS

	/*
	public int health = 100;

	public int rowStart = 1;
	public int colStart = 1;
	public HexArray hexArray = new HexArray();

	bool randStart = false;

	bool invincible = false;

	public GameObject model = new GameObject();

	Vector3 position;

	*/

	public string upButton = "up";
	public string downButton = "down";
	public string leftButton = "left";
	public string rightButton = "right";

	DirectionEnum.Direction direction = DirectionEnum.Direction.LEFT;

	bool randStart = false;

	bool hasStart = false;

	private Vector3 position;
	public GridNode currHex;

	public Hitbox myHitbox;

	/*
	//Constructor
	public ControlledCharacter(){
		position = hexArray.getSpot (rowStart,colStart).getLocation();
		print("POSITION SET 1!");
		currHex = hexArray.getSpot (rowStart,colStart);
		print("CURRHEX SET 1!");
	}*/



	//Sets the location of our ControlledCharacter

	

	// Use this for initialization
	void Start () {
		
		currHex = hexArray.getSpot (rowStart,colStart);
		currHex.setOccupied (this);
		print("CURRHEX SET 2!");


		//GridNode node = hexArray.getSpot(rowStart,colStart);
		//position = node.getLocation();
		//print("POSITION SET 2!");



		/*
		if (randStart) {
			//Finish Later		
		}
		else{
			//currHex = hexArray.getSpot (rowStart,colStart);
			//position = currHex.getLocation();

		}*/
	}

	//MovementCheck does all the necessary keyPress checks and counts which ones are true. 
	//Then it calculates the moved hex in batch form, and returns the end result.
	void movementCheck(){
		bool up = false;
		bool down = false;
		bool left = false;
		bool right = false;

		if(!hasStart){
		currHex = hexArray.getSpot (rowStart,colStart);
			hasStart = true;
		}
		if(Input.GetKeyDown(upButton)){
			up = true;
			print("UP PRESSED");
			print(currHex.getUp ());
		}
		if(Input.GetKeyDown(downButton)){
			down = true;
		}
		if(Input.GetKeyDown(leftButton)){
			left = true;
		}
		if(Input.GetKeyDown(rightButton)){
			right = true;
		}
	

		if(up || down || left || right){
			currHex.clearOccupied();
			if(up){
				if(currHex.getUp()!=null){
					currHex = currHex.getUp();
					//currHex.getDown ().clearOccupied();
					//currHex.getDown ().getUp ().setOccupied (this);
					//print("UP SET");
				}
			}
			if(down){
				if(currHex.getDown()!=null){
					currHex = currHex.getDown ();
				}
			}
			if(left){
				if(currHex.getLeft ()!=null){
					currHex = currHex.getLeft ();
				}
			}
			if(right){
				if(currHex.getRight ()!=null){
					currHex=currHex.getRight ();
				}
			}
			currHex.setOccupied(this);
		}
	}

	void attackCheck(){

	}

	// Update is called once per frame
	void Update () {
		//currHex = hexArray.getSpot (rowStart,colStart);
		movementCheck();
		attackCheck();
		transform.position = currHex.getLocation();//position;
	}
}
