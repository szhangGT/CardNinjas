using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MenusController : MonoBehaviour {

	public Stack<GameObject> MenuStack = new Stack<GameObject>();
	public GameObject InitialMenu;

	// Use this for initialization
	void Start () {
		this.MenuStack.Push(InitialMenu);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
