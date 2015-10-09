using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MainMenuController : MonoBehaviour {

	public GameObject buttonParent;
	public GameObject goalButtonParent;

	private GameObject[] buttons;
	private GameObject[] goalButtons;


	void Start () {
		goalButtonParent.SetActive(true);
		Button[] temp = buttonParent.GetComponentsInChildren<Button>();
		buttons = new GameObject[temp.Length];
		for (int i = 0; i < temp.Length; i++) {
			buttons[i] = temp[i].gameObject;
		}

		temp = goalButtonParent.GetComponentsInChildren<Button>();
		goalButtons = new GameObject[temp.Length];
		for (int i = 0; i < temp.Length; i++) {
			goalButtons[i] = temp[i].gameObject;
		}
		goalButtonParent.SetActive(false);
	}

	// Move buttons to proper placement if they are out of place
	void Update () {
		for (int i = 0; i < buttons.Length; i++) {
			buttons[i].transform.position = Vector3.MoveTowards(buttons[i].transform.position, 
                                            goalButtons[i].transform.position, Time.deltaTime * 100.0f);
		}
	}

	public void MoveButtons() {
		// TODO: Use Jonathan's Custom Input

		// Reassign all button references to proper place after navigation
		if(Input.GetAxis("Vertical") < 0.0f) {
			GameObject temp = buttons[0];
			for (int i = 1; i < buttons.Length; i++) {
				buttons[i-1] = buttons[i];
			}
			buttons[buttons.Length-1] = temp;

			temp.transform.position = goalButtons[buttons.Length-1].transform.position;
		}
		else if(Input.GetAxis("Vertical") > 0.0f) {
			GameObject temp = buttons[buttons.Length-1];
			for (int i = buttons.Length-2; i >= 0; i--) {
				buttons[i+1] = buttons[i];
			}
			buttons[0] = temp;

			temp.transform.position = goalButtons[0].transform.position;
		}
	}
}
