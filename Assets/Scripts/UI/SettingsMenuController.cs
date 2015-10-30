using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SettingsMenuController : MonoBehaviour {

	public Text subtitleTab;
	public UIHideBehaviour subtitleHide;

	public GameObject Video;
	public GameObject Audio;
	public GameObject Settings;

	public EventSystem es;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ChangeToVideo() {
		subtitleTab.text = "Video";
		subtitleHide.OnScreen = true;

		Settings.SetActive(false);
		Video.SetActive(true);
	}

	public void ChangeToAudio() {
		subtitleTab.text = "Audio";
		subtitleHide.OnScreen = true;

		Settings.SetActive(false);
		Audio.SetActive(true);
	}

	public void ChangeToSettings() {
		subtitleHide.OnScreen = false;


		Video.SetActive(false);
		Audio.SetActive(false);
		Settings.SetActive(true);

		es.UpdateModules();
	}
}
