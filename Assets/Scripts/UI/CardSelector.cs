using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Linq;
using Assets.Scripts.Player;
using Assets.Scripts.Util;

namespace Assets.Scripts.UI
{
    public class CardSelector : MonoBehaviour
    {
        public delegate void CardSelectorAction();
        public static event CardSelectorAction CardSelectorEnabled;
        public static event CardSelectorAction CardSelectorDisabled;

        private static int playerIndex;
        private const int NUM_SELECTIONS = 8;
        private const int MAX_SELECTIONS = 4;
        private int numSelections = 0;
        private const int CHILD_LABEL_INDEX = 1;

        public Player.Player player;
        private CardSystem.Card[] cards;
        private CardSystem.Card[] selectedCards;
        private CardSystem.Card[] selectionOptions;
        private Toggle[] selectionButtons;

        void Start()
        {
            //should find players provided they are named in the fashion: "Player 1" or "Player 42"
            player = GameObject.Find("Player " + ++playerIndex).GetComponent<Player.Player>();

            cards = player.Cards;
            ArrayHandler.Shuffle<CardSystem.Card>(cards);

            selectionOptions = ArrayHandler.Remove<CardSystem.Card>(ref cards, 0, NUM_SELECTIONS);

            selectionButtons = new Toggle[NUM_SELECTIONS];
            GameObject[] gos = GameObject.FindGameObjectsWithTag("Selection").OrderBy(go => go.name).ToArray();
            for(int i = 0; i < gos.Length; i++)
            {
                selectionButtons[i] = gos[i].GetComponent<Toggle>();
                if (i < selectionOptions.Length && selectionOptions[i] != null)
                    selectionButtons[i].transform.GetChild(CHILD_LABEL_INDEX).GetComponent<Text>().text = selectionOptions[i].Name;
                else
                {
                    selectionButtons[i].transform.GetChild(CHILD_LABEL_INDEX).GetComponent<Text>().text = "<empty>";
                    selectionButtons[i].interactable = false;
                }
            }

            selectedCards = new CardSystem.Card[MAX_SELECTIONS];
        }

        void Update()
        {
            if(Input.GetKeyDown(KeyCode.P))
            {
                DrawPossibleSelections();
            }
        }

        private void DrawPossibleSelections()
        {
            
            selectionOptions = ArrayHandler.Remove<CardSystem.Card>(ref cards, 0, NUM_SELECTIONS);
            for(int i = 0; i < NUM_SELECTIONS; i++)
            {
                selectionButtons[i].isOn = false;
                selectionButtons[i].interactable = true;

                if (i < selectionOptions.Length && selectionOptions[i] != null)
                    selectionButtons[i].transform.GetChild(CHILD_LABEL_INDEX).GetComponent<Text>().text = selectionOptions[i].Name;
                else
                {
                    selectionButtons[i].transform.GetChild(CHILD_LABEL_INDEX).GetComponent<Text>().text = "<empty>";
                    selectionButtons[i].interactable = false;
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
                ArrayHandler.RemoveFromEnd<CardSystem.Card>(ref selectedCards);
                --numSelections;
                Toggle t = selectionButtons[index];
                ColorBlock cb = t.colors;
                cb.highlightedColor = Color.white;
                cb.normalColor = Color.white;
                t.colors = cb;
            }
            else
            {
                Toggle t = selectionButtons[index];
                ColorBlock cb = t.colors;
                cb.normalColor = Color.yellow;
                cb.highlightedColor = Color.yellow;
                t.colors = cb;
                selectedCards[numSelections++] = selectionOptions[index];
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
            transform.GetComponent<Canvas>().enabled = true;
            DrawPossibleSelections();
            if (CardSelectorEnabled != null) CardSelectorEnabled();
        }

        public void Okay()
        {
            player.AddCardsToHand(selectedCards);
            transform.GetComponent<Canvas>().enabled = false;
            if (CardSelectorDisabled != null) CardSelectorDisabled();
        }

        void OnLevelWasLoaded(int i)
        {
            playerIndex = 0;
        }
    }
}