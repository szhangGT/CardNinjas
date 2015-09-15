using UnityEngine;
using System.Collections;

public class Hitbox : MonoBehaviour {

	public float scaleX = 1f;
	public float scaleY = 1f;
	public float scaleZ = 1f;

	public float floatHeight = 1;

	public int damage = 10;

	public bool visible = false;
	
	public GridNode currentPanel = null;

	public DirectionEnum.Direction direction = DirectionEnum.Direction.NONE;

	public Hitbox(){

	}

	public Hitbox(GridNode panel){currentPanel = panel;}

	public Hitbox(float scaleXIn, float scaleYIn, float scaleZIn, int damageIn, bool visibleIn){
		scaleX = scaleXIn;
		scaleY = scaleYIn;
		scaleZ = scaleZIn;
		damage = damageIn;
		visible = visibleIn;
	}

	int getDamage(){
		return damage;
	}

	public bool isVisibile(){
		return visible;
	}

	public void setVisible(bool inBool){
		visible = inBool;
	}

	public void setCurrentPanel(GridNode node){
		currentPanel = node;
	}

	public GridNode returnCurrentPanel(){
		return currentPanel;
	}

	void OnTriggerEnter (Collider collider)
	{
		Destroy(this.gameObject);
	}

	// Use this for initialization
	void Start () {
	
		transform.localScale = new Vector3(scaleX, scaleY, scaleZ);
		renderer.enabled = visible;

	}



	// Update is called once per frame
	void Update () {
		transform.position = new Vector3(currentPanel.getLocation().x, transform.position.y+floatHeight, currentPanel.getLocation().z);

	}
}
