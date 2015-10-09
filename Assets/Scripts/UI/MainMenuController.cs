using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MainMenuController : MonoBehaviour {

	public GameObject buttonParent;
	public GameObject goalButtonParent;

	private GameObject[] buttons;
	private GameObject[] goalButtons;

	
	public GameObject cardParent;
	public GameObject goalCardParent;
	public GameObject[] cards;
	public GameObject[] goalCards;

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

		// Now do the cards
		goalCardParent.SetActive(true);
		Image[] temp1 = cardParent.GetComponentsInChildren<Image>();
		cards = new GameObject[temp1.Length];
		for (int i = 0; i < temp1.Length; i++) {
			cards[i] = temp1[i].gameObject;
		}
		
		temp1 = goalCardParent.GetComponentsInChildren<Image>();
		goalCards = new GameObject[temp1.Length];
		for (int i = 0; i < temp1.Length; i++) {
			goalCards[i] = temp1[i].gameObject;
		}
		goalCardParent.SetActive(false);
	}

	// Move buttons to proper placement if they are out of place
	void Update () {
		for (int i = 0; i < buttons.Length; i++) {
			buttons[i].transform.position = Vector3.MoveTowards(buttons[i].transform.position, 
                                            goalButtons[i].transform.position, Time.deltaTime * 500.0f);
		}

		for (int i = 0; i < cards.Length; i++) {
			cards[i].transform.position = Vector3.MoveTowards(cards[i].transform.position, 
			                                                    goalCards[i].transform.position, Time.deltaTime * 500.0f);
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

		
		
		GameObject temp1 = cards[cards.Length-1];
		for (int i = cards.Length-2; i >= 0; i--) {
			cards[i+1] = cards[i];
			cards[i+1].transform.SetSiblingIndex(i+1);
		}
		cards[0] = temp1;
	}
}
