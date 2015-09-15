using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour {



	public int health = 100;

	public int rowStart = 1;
	public int colStart = 1;
	public HexArray hexArray = new HexArray();

	bool randStart = false;

	bool invincible = false;

	public GameObject model = new GameObject();

	Vector3 position;


	public Character(){
		position = hexArray.getSpot(rowStart, colStart).getLocation();
	}


	public Vector3 getPosition(){
		return position;
	}

	public void setPosition(Vector3 place){
		model.transform.position = place;
		position = place;
	}

	//Decrements health by a number
	public void takeDamage(int damage){
		if(!invincible){
			health = health-damage;
		}
	}


	// Use this for initialization
	void Start () {
		if (randStart) {
			//Finish Later		
		}
	}

	// Update is called once per frame
	void Update () {
		model.transform.position = position;
	}
}
