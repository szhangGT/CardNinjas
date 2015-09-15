using UnityEngine;
using System.Collections;

public class HexArray : MonoBehaviour {



	public int ArrayWidth = 1;
	public int ArrayHeight = 1;

	private GridNode[, ] hexArray;

	// Use this for initialization
	void Awake () {
		hexArray = new GridNode[ArrayWidth, ArrayHeight];
	}

	public GridNode[, ] getArray(){
		return hexArray;
	}

	public void setArray(GridNode[, ] nuArr){
		hexArray = nuArr;
	}

	public GridNode getSpot(int i, int j){
		return hexArray [i, j];
	}

	// Update is called once per frame
	void Update () {
	
	}
}
