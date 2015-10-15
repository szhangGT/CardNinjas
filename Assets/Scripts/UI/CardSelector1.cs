using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Player;
using Assets.Scripts.Util;
using Assets.Scripts.CardSystem;

namespace Assets.Scripts.UI
{
    public class CardSelector1 : MonoBehaviour
    {
        public delegate void CardSelectorAction();
        public static event CardSelectorAction CardSelectorEnabled;
        public static event CardSelectorAction CardSelectorDisabled;

        private static int playerIndex;
        private int numSelections = 0;
        private const int NUM_SELECTIONS = 8;
        private const int MAX_SELECTIONS = 4;
        private const int CHILD_LABEL_INDEX = 1;

        public Player.Player player;
        private Deck deck;
        private List<Card> selectedCards;
        private List<Card> selectionOptions;
        private Toggle[] selectionButtons;
        private Toggle[] finalButtons;

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

        private bool resize = false;
        private Animator anim;

        void Start()
        {
            anim = this.GetComponent<Animator>();

            //should find players provided they are named in the fashion: "Player 1" or "Player 42"
            player = GameObject.Find("Player " + ++playerIndex).GetComponent<Player.Player>();

            deck = player.Deck;
            selectionOptions = new List<Card>();
            selectedCards = new List<Card>();

            selectionButtons = new Toggle[NUM_SELECTIONS];
            GameObject[] gos = GameObject.FindGameObjectsWithTag("Selection").OrderBy(go => go.name).ToArray();
            for(int i = 0; i < gos.Length; i++)
            {
                selectionButtons[i] = gos[i].GetComponent<Toggle>();
            }
            DrawPossibleSelections();
            InitializeDisplayData();
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
        }

        private void DrawPossibleSelections()
        {
            selectionOptions = deck.DrawHand();
            for(int i = 0; i < NUM_SELECTIONS; i++)
            {
                selectionButtons[i].isOn = false;
                selectionButtons[i].interactable = true;

                if (i < selectionOptions.Count && selectionOptions[i] != null)
                { }
                    //selectionButtons[i].transform.GetChild(CHILD_LABEL_INDEX).GetComponent<Text>().text = selectionOptions[i].Name;
                else
                {
                    //selectionButtons[i].transform.GetChild(CHILD_LABEL_INDEX).GetComponent<Text>().text = "<empty>";
                    //selectionButtons[i].interactable = false;
                }
            }
        }

        public void SelectCard(int index)
        {
            if(!selectionButtons[index].isOn)
            {
                if(numSelections >= MAX_SELECTIONS)
                {
                    for(int i = 0; i < selectionButtons.Length; i++)
                    {
                        if (!selectionButtons[i].interactable && selectionButtons[i].transform.GetChild(CHILD_LABEL_INDEX).GetComponent<Text>().text != "<empty>") selectionButtons[i].interactable = true;
                    }
                }
                Toggle t = selectionButtons[index];
                ColorBlock cb = t.colors;
                cb.highlightedColor = Color.white;
                cb.normalColor = Color.white;
                t.colors = cb;
                selectedCards.Remove(selectionOptions[index]);
                numSelections--;
            }
            else
            {
                Toggle t = selectionButtons[index];
                ColorBlock cb = t.colors;
                cb.normalColor = Color.yellow;
                cb.highlightedColor = Color.yellow;
                t.colors = cb;
                numSelections++;
                selectedCards.Add(selectionOptions[index]);
                if (numSelections >= MAX_SELECTIONS)
                {
                    for (int i = 0; i < selectionButtons.Length; i++)
                    {
                        if (selectionButtons[i].interactable && !selectionButtons[i].isOn) selectionButtons[i].interactable = false;
                    }
                }
            }
        }

        public void EnableCanvas()
        {
            selectedCards.Clear();
            transform.GetComponent<Canvas>().enabled = true;
            DrawPossibleSelections();
            if (CardSelectorEnabled != null) CardSelectorEnabled();
        }

        public void Okay()
        {
            for(int i = 0; i < selectedCards.Count; i++)
            {
                selectionOptions.Remove(selectedCards[i]);
            }
            deck.ReturnUsedCards(selectionOptions);
            player.AddCardsToHand(selectedCards);
            transform.GetComponent<Canvas>().enabled = false;
            if (CardSelectorDisabled != null) CardSelectorDisabled();
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

            displayingImage.sprite = selectionOptions[0].Image;
            displayingName.text = selectionOptions[0].Name;
            displayingDamage.text = selectionOptions[0].Action.Damage.ToString();
            displayingRange.text = selectionOptions[0].Action.Range.ToString();
            displayingType.text = selectionOptions[0].Type.ToString();
            displayingDescription.text = selectionOptions[0].Description;
        }
    }
}