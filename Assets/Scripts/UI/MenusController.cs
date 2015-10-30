using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Util;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenusController : MonoBehaviour {

	
	public UIHideBehaviour MainMenu1;
	public UIHideBehaviour MainMenu2;
	public GameObject MenuSelected;

	public UIHideBehaviour Settings;
	public GameObject SettingsSelected;

	public UIHideBehaviour LevelSelect1;
	public UIHideBehaviour LevelSelect2;
	public GameObject LevelSelected;


	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.B) && (Settings.OnScreen || LevelSelect1.OnScreen)) {
			GoToMainMenu();
		}

		Debug.Log(Input.GetKeyDown(KeyCode.DownArrow));
		if (Input.GetKeyDown(KeyCode.UpArrow)) Navigate(CustomInput.UserInput.Up);
		if (Input.GetKeyDown(KeyCode.DownArrow)) Navigate(CustomInput.UserInput.Down);
		if (Input.GetKeyDown(KeyCode.RightArrow)) Navigate(CustomInput.UserInput.Right);
		if (Input.GetKeyDown(KeyCode.LeftArrow)) Navigate(CustomInput.UserInput.Left);
	}


	public void GoToSettings() {
		if (MainMenu1.OnScreen) MenuSelected = EventSystem.current.currentSelectedGameObject;
		MainMenu1.OnScreen = false;
		MainMenu2.OnScreen = false;
		
		LevelSelect1.OnScreen = false;
		LevelSelect2.OnScreen = false;

		Settings.OnScreen = true;
		EventSystem.current.SetSelectedGameObject(SettingsSelected);
	}

	public void GoToMainMenu() {
		if (LevelSelect1.OnScreen) LevelSelected = EventSystem.current.currentSelectedGameObject;
		Settings.OnScreen = false;

		LevelSelect1.OnScreen = false;
		LevelSelect2.OnScreen = false;
		
		MainMenu1.OnScreen = true;
		MainMenu2.OnScreen = true;
		EventSystem.current.SetSelectedGameObject(MenuSelected);
	}

	public void GoToLevelSelect() {
		if (MainMenu1.OnScreen) MenuSelected = EventSystem.current.currentSelectedGameObject;
		Settings.OnScreen = false;
		
		MainMenu1.OnScreen = false;
		MainMenu2.OnScreen = false;

		LevelSelect1.OnScreen = true;
		LevelSelect2.OnScreen = true;
		EventSystem.current.SetSelectedGameObject(LevelSelected);
	}

	#region NAVIGATION
	private void Navigate(CustomInput.UserInput direction)
	{
		GameObject next = EventSystem.current.currentSelectedGameObject;

		switch(direction)
		{
		case CustomInput.UserInput.Up:
			next = EventSystem.current.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnUp().gameObject;
			break;
		case CustomInput.UserInput.Down:
			next = EventSystem.current.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnDown().gameObject;
			break;
		case CustomInput.UserInput.Left:
			next = EventSystem.current.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnLeft().gameObject;
			break;
		case CustomInput.UserInput.Right:
			next = EventSystem.current.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnRight().gameObject;
			break;
		}
		EventSystem.current.SetSelectedGameObject(next);
	}
	#endregion
}
