using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Linq;
using Assets.Scripts.Util;

namespace Assets.Scripts.UI
{
    public class CardInfoUI : MonoBehaviour
    {
        private static int playerIndex;
        private int thisPlayerIndex;

        private const int MAX_HAND = 4;
        private const int CHILD_IMAGE_INDEX = 0;

        private int cardToUse = 0;
        private Image[] hand;
        private string[] cards;
        private Text currentCardName;
        private Canvas canvas;
        private Player.Player player;

        void OnEnable()
        {
            Player.Player.NewSelect += UpdateCardInfo;
            CardSelector.CardSelectorDisabled += Show;
            SelectionTimer.TimerFinish += Hide;
        }
        void OnDisable()
        {
            Player.Player.NewSelect -= UpdateCardInfo;
            CardSelector.CardSelectorDisabled -= Show;
            SelectionTimer.TimerFinish -= Hide;
        }

        void Awake()
        {
            hand = new Image[MAX_HAND];
            cards = new string[MAX_HAND];

            thisPlayerIndex = ++playerIndex;
            player = GameObject.Find("Player " + playerIndex).GetComponent<Player.Player>();
            GameObject[] gos = GameObject.FindGameObjectsWithTag("Hand").OrderBy(go => go.name).ToArray();
            for (int i = 0; i < gos.Length; i++)
            {
                gos[i].tag = "Hand " + thisPlayerIndex.ToString();
                hand[i] = gos[i].GetComponent<Image>();
            }
            currentCardName = GameObject.Find("Current Hand Card").GetComponent<Text>();

            canvas = this.GetComponent<Canvas>();
            Hide();
        }

        private void UpdateCardInfo(CardSystem.Card card)
        {
            if (cards[cardToUse] == "")
            {
                currentCardName.text = "None";
            }
            else
            {
                currentCardName.text = cardToUse+1 >= MAX_HAND ? "None" : cards[cardToUse+1];
                hand[cardToUse].transform.GetChild(CHILD_IMAGE_INDEX).GetComponent<Image>().sprite = null;
                hand[cardToUse].transform.GetChild(CHILD_IMAGE_INDEX).GetComponent<Image>().color = Color.black;
                hand[cardToUse].color = new Color(128.0f / 255, 128.0f / 255, 128.0f / 255);
            }
            cardToUse++;
        }

        private void Show()
        {
            canvas.enabled = true;
            for(int i = 0; i < MAX_HAND; i++)
            {
                if(i < player.Hand.PlayerHand.Count)
                {
                    cards[i] = player.Hand.PlayerHand[i].Name;
                    hand[i].transform.GetChild(CHILD_IMAGE_INDEX).GetComponent<Image>().color = Color.white;
                    hand[i].transform.GetChild(CHILD_IMAGE_INDEX).GetComponent<Image>().sprite = player.Hand.PlayerHand[i].Image;
                    hand[i].color = GetElementDisplay(player.Hand.PlayerHand[i].Element);
                }
                else
                {
                    cards[i] = "";
                }
            }
            currentCardName.text = cards[0] == null ?  "" : cards[0];
        }

        private void Hide()
        {
            canvas.enabled = false;
            for(int i = 0; i < hand.Length; i++)
            {
                hand[i].transform.GetChild(CHILD_IMAGE_INDEX).GetComponent<Image>().sprite = null;
                hand[i].transform.GetChild(CHILD_IMAGE_INDEX).GetComponent<Image>().color = Color.black;
                hand[i].color = new Color(128.0f / 255, 128.0f / 255, 128.0f / 255);
            }
            currentCardName.text = "None";
            cardToUse = 0;
        }

        public Color GetElementDisplay(Enums.Element element)
        {
            switch (element)
            {
                case Enums.Element.Fire:
                    return new Color(175.0f / 255, 30.0f / 255, 30.0f / 255);
                case Enums.Element.Water:
                    return new Color(30.0f / 255, 30.0f / 255, 175.0f / 255);
                case Enums.Element.Thunder:
                    return new Color(225.0f / 255, 225.0f / 255, 30.0f / 255);
                case Enums.Element.Earth:
                    return new Color(85.0f / 255, 50.0f / 255, 15.0f / 255);
                case Enums.Element.Wood:
                    return new Color(30.0f / 255, 175.0f / 255, 30.0f / 255);
                default:
                    return new Color(128.0f / 255, 128.0f / 255, 128.0f / 255);
            }
        }
    }
}
