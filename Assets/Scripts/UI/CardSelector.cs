using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Player;
using Assets.Scripts.Util;
using Assets.Scripts.CardSystem;

namespace Assets.Scripts.UI
{
    public class CardSelector : MonoBehaviour
    {
        public delegate void CardSelectorAction();
        public static event CardSelectorAction CardSelectorEnabled;
        public static event CardSelectorAction CardSelectorDisabled;

        private static int playerIndex;
        private int thisPlayerIndex;
        private int numSelections = 0;
        private const int NUM_SELECTIONS = 8;
        private const int MAX_SELECTIONS = 4;
        private const int CHILD_IMAGE_INDEX = 0;

        public Player.Player player;
        private Deck deck;
        private List<Card> selectedCards;
        private List<Card> selectionOptions;
        private List<int> finalMap;
        private Toggle[] selectionButtons;
        private Toggle[] finalButtons;
        private Button okayButton;

        private Image displayingImage;
        private Image nextImage;
        private Text displayingName;
        private Text nextName;
        private Text displayingDamage;
        private Text nextDamage;
        private Text displayingRange;
        private Text nextRange;
        private Text displayingType;
        private Text nextType;
        private Text displayingDescription;
        private Text nextDescription;

        private int lastTransition = 0;
        private bool resize = false;
        private Animator anim;

        void Start()
        {
            anim = this.GetComponent<Animator>();

            //should find players provided they are named in the fashion: "Player 1" or "Player 42"
            thisPlayerIndex = ++playerIndex;
            player = GameObject.Find("Player " + playerIndex).GetComponent<Player.Player>();

            deck = player.Deck;
            selectionOptions = new List<Card>();
            selectedCards = new List<Card>();
            finalMap = new List<int>();

            selectionButtons = new Toggle[NUM_SELECTIONS];
            finalButtons = new Toggle[MAX_SELECTIONS];
            GameObject[] gos = GameObject.FindGameObjectsWithTag("Selection").OrderBy(go => go.name).ToArray();
            for(int i = 0; i < gos.Length; i++)
            {
                gos[i].tag = "Selection " + thisPlayerIndex.ToString();
                selectionButtons[i] = gos[i].GetComponent<Toggle>();
            }

            gos = GameObject.FindGameObjectsWithTag("Final").OrderBy(go => go.name).ToArray();
            for (int i = 0; i < gos.Length; i++)
            {
                gos[i].tag = "Final " + thisPlayerIndex.ToString();
                finalButtons[i] = gos[i].GetComponent<Toggle>();
            }

            okayButton = GameObject.Find("Okay").GetComponent<Button>();
            okayButton.transform.name = "Okay " + thisPlayerIndex.ToString();

            DrawPossibleSelections();
            InitializeDisplayData();
            UpdateFinalDisplayData();
        }

        void Update()
        {
            if (Managers.GameManager.State == Enums.GameStates.CardSelection)
            {
                if (CustomInput.BoolFreshPress(CustomInput.UserInput.Attack))
                {
                    DrawPossibleSelections();
                }
            }

            if (CustomInput.BoolFreshPress(CustomInput.UserInput.Up, thisPlayerIndex)) Navigate(CustomInput.UserInput.Up);
            if (CustomInput.BoolFreshPress(CustomInput.UserInput.Down, thisPlayerIndex)) Navigate(CustomInput.UserInput.Down);
            if (CustomInput.BoolFreshPress(CustomInput.UserInput.Right, thisPlayerIndex)) Navigate(CustomInput.UserInput.Right);
            if (CustomInput.BoolFreshPress(CustomInput.UserInput.Left, thisPlayerIndex)) Navigate(CustomInput.UserInput.Left);
        }

        private void DrawPossibleSelections()
        {
            selectionOptions = deck.DrawHand();
            for (int i = 0; i < NUM_SELECTIONS; i++)
            {
                if (selectionOptions != null && i < selectionOptions.Count && selectionOptions[i] != null)
                {
                    selectionButtons[i].transform.GetChild(CHILD_IMAGE_INDEX).GetComponent<Image>().sprite = selectionOptions[i].Image;
                    selectionButtons[i].isOn = false;
                    selectionButtons[i].interactable = true;
                }
                else
                {
                    selectionButtons[i].transform.GetChild(CHILD_IMAGE_INDEX).GetComponent<Image>().sprite = null;
                    selectionButtons[i].interactable = false;
                    selectionButtons[i].isOn = false;
                }
            }
        }

        public void SelectCard(int index)
        {
            if (!selectionButtons[index].isOn)
            {
                if (numSelections == 0) return;
                if (numSelections >= MAX_SELECTIONS)
                {
                    for (int i = 0; i < selectionButtons.Length; i++)
                    {
                        if (!selectionButtons[i].interactable && selectionButtons[i].transform.GetChild(CHILD_IMAGE_INDEX).GetComponent<Image>().sprite != null) selectionButtons[i].interactable = true;
                    }
                }
                selectedCards.Remove(selectionOptions[index]);
                finalMap.Remove(index);
                UpdateFinalDisplayData();
                if(--numSelections < 0) numSelections = 0;
            }
            else
            {
                numSelections++;
                selectedCards.Add(selectionOptions[index]);
                finalMap.Add(index);
                UpdateFinalDisplayData();
                if (numSelections >= MAX_SELECTIONS)
                {
                    for (int i = 0; i < selectionButtons.Length; i++)
                    {
                        if (!selectionButtons[i].isOn) selectionButtons[i].interactable = false;
                    }
                }
            }
        }

        public void RemoveCard(int index)
        {
            if (numSelections >= MAX_SELECTIONS)
            {
                for (int i = 0; i < selectionButtons.Length; i++)
                {
                    if (!selectionButtons[i].interactable && selectionButtons[i].transform.GetChild(CHILD_IMAGE_INDEX).GetComponent<Image>().sprite != null) selectionButtons[i].interactable = true;
                }
            }
            selectionButtons[finalMap[index]].isOn = false;
            FinalTransition(index);
            UpdateFinalDisplayData();
            numSelections--;
        }

        public void EnableCanvas()
        {
            selectedCards.Clear();
            finalMap.Clear();
            numSelections = 0;
            transform.GetComponent<Canvas>().enabled = true;
            Navigate(CustomInput.UserInput.Up);
            DrawPossibleSelections();
            UpdateDisplayData();
            UpdateFinalDisplayData();
            if (CardSelectorEnabled != null) CardSelectorEnabled();
        }

        public void Okay()
        {
            if (selectionOptions != null)
            {
                for (int i = 0; i < selectedCards.Count; i++)
                {
                    selectionOptions.Remove(selectedCards[i]);
                }
                deck.ReturnUsedCards(selectionOptions);
                player.AddCardsToHand(selectedCards);
            }
            transform.GetComponent<Canvas>().enabled = false;
            if (CardSelectorDisabled != null) CardSelectorDisabled();
            EventSystem.current.SetSelectedGameObject(null);
        }

        void OnLevelWasLoaded(int i)
        {
            playerIndex = 0;
        }

        public void Resize()
        {
            resize = !resize;
            anim.SetBool("Resize", resize);
        }

        #region DISPLAY_DATA
        public void UpdateFinalDisplayData()
        {
            for(int i = 0; i < MAX_SELECTIONS; i++)
            {
                if(i < finalMap.Count)
                {
                    finalButtons[i].transform.GetChild(CHILD_IMAGE_INDEX).GetComponent<Image>().color = Color.white;
                    finalButtons[i].transform.GetChild(CHILD_IMAGE_INDEX).GetComponent<Image>().sprite = selectionOptions[finalMap[i]].Image;
                    finalButtons[i].interactable = true;
                }
                else
                {
                    finalButtons[i].transform.GetChild(CHILD_IMAGE_INDEX).GetComponent<Image>().color = Color.black;
                    finalButtons[i].transform.GetChild(CHILD_IMAGE_INDEX).GetComponent<Image>().sprite = null;
                    finalButtons[i].interactable = false;
                }
            }
        }

        public void InitializeDisplayData()
        {
            displayingImage = GameObject.Find("Displayed Card").GetComponent<Image>();
            displayingName = GameObject.Find("Displayed Name").GetComponent<Text>();
            displayingDamage = GameObject.Find("Displayed Damage").GetComponent<Text>();
            displayingRange = GameObject.Find("Displayed Range").GetComponent<Text>();
            displayingType = GameObject.Find("Displayed Type").GetComponent<Text>();
            displayingDescription = GameObject.Find("Displayed Description").GetComponent<Text>();

            nextImage = GameObject.Find("Next Card").GetComponent<Image>();
            nextName = GameObject.Find("Next Name").GetComponent<Text>();
            nextDamage = GameObject.Find("Next Damage").GetComponent<Text>();
            nextRange = GameObject.Find("Next Range").GetComponent<Text>();
            nextType = GameObject.Find("Next Type").GetComponent<Text>();
            nextDescription = GameObject.Find("Next Description").GetComponent<Text>();

            UpdateDisplayData();
        }

        public void UpdateDisplayData()
        {
            for (int i = 0; i < NUM_SELECTIONS; i++)
            {
                if (selectionOptions != null && i < selectionOptions.Count)
                {
                    selectionButtons[i].transform.GetChild(CHILD_IMAGE_INDEX).GetComponent<Image>().color = Color.white;
                    selectionButtons[i].transform.GetChild(CHILD_IMAGE_INDEX).GetComponent<Image>().sprite = selectionOptions[i].Image;
                    selectionButtons[i].interactable = true;
                }
                else
                {
                    selectionButtons[i].transform.GetChild(CHILD_IMAGE_INDEX).GetComponent<Image>().color = Color.black;
                    selectionButtons[i].transform.GetChild(CHILD_IMAGE_INDEX).GetComponent<Image>().sprite = null;
                    selectionButtons[i].interactable = false;
                }
            }
            if (selectionOptions != null)
            {
                displayingImage.sprite = selectionOptions[0].Image;
                displayingName.text = selectionOptions[0].Name;
                displayingDamage.text = selectionOptions[0].Action.Damage.ToString();
                displayingRange.text = selectionOptions[0].Action.Range.ToString();
                displayingType.text = selectionOptions[0].Type.ToString();
                displayingDescription.text = selectionOptions[0].Description;
            }
            else
            {
                displayingImage.sprite = null;
                displayingName.text = "None";
                displayingDamage.text = "0";
                displayingRange.text = "0";
                displayingType.text = "None";
                displayingDescription.text  = "No more cards in deck.";
            }
        }
        #endregion

        #region TRANSITIONS
        public void Transition(int index)
        {
            if (selectionOptions == null) return;
            if (index >= selectionOptions.Count || index == lastTransition) return;
            lastTransition = index;

            nextImage.sprite = selectionOptions[index].Image;
            nextName.text = selectionOptions[index].Name;
            nextDamage.text = selectionOptions[index].Action.Damage.ToString();
            nextRange.text = selectionOptions[index].Action.Range.ToString();
            nextType.text = selectionOptions[index].Type.ToString();
            nextDescription.text = selectionOptions[index].Description;

            anim.SetBool("Transition", true);
        }

        public void FinalTransition(int index)
        {
            if (index >= finalMap.Count || finalMap[index] == lastTransition || index >= MAX_SELECTIONS) return;
            lastTransition = finalMap[index];

            nextImage.sprite = selectionOptions[finalMap[index]].Image;
            nextName.text = selectionOptions[finalMap[index]].Name;
            nextDamage.text = selectionOptions[finalMap[index]].Action.Damage.ToString();
            nextRange.text = selectionOptions[finalMap[index]].Action.Range.ToString();
            nextType.text = selectionOptions[finalMap[index]].Type.ToString();
            nextDescription.text = selectionOptions[finalMap[index]].Description;

            anim.SetBool("Transition", true);
        }

        public void EndTransition()
        {
            anim.SetBool("Transition", false);

            displayingImage.sprite = nextImage.sprite;
            displayingName.text = nextName.text;
            displayingDamage.text = nextDamage.text;
            displayingRange.text = nextRange.text;
            displayingType.text = nextType.text;
            displayingDescription.text = nextDescription.text;
        }
        #endregion

        #region NAVIGATION

        private void Navigate(CustomInput.UserInput direction)
        {
            GameObject next = EventSystem.current.currentSelectedGameObject;
            if(next == null)
            {
                if(deck.GetDeck.Count > 0)
                {
                    Transition(0);
                    EventSystem.current.SetSelectedGameObject(selectionButtons[0].gameObject);
                }
                else
                {
                    EventSystem.current.SetSelectedGameObject(okayButton.gameObject);
                }
                return;
            }

            bool nextIsValid = false;
            while (!nextIsValid)
            {
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
                nextIsValid = next.GetComponent<Selectable>().interactable;
            }
        }
        #endregion
    }
}