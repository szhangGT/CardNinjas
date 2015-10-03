using UnityEngine;
using System.Collections.Generic;
namespace Assets.Scripts.CardSystem
{
    class Deck
    {
        public const int DRAW_SIZE = 8;
        private List<Card> deck;

        public Deck(List<Card> deck)
        {
            this.deck = deck;
            Random.seed = System.DateTime.Today.Millisecond;
            Card temp;
            for (int i = 0; i < this.deck.Count; i++)
            {
                int r = i + (int)(Random.Range(0f, 1f) * (this.deck.Count - i));
                temp = this.deck[r];
                this.deck[r] = this.deck[i];
                this.deck[i] = temp;
            }
        }

        public List<Card> DrawHand()
        {
            if (deck.Count == 0)
                return null;
            List<Card> selection = new List<Card>();
            for (int i = 0; i < DRAW_SIZE; i++)
                selection.Add(deck[i]);
            deck.RemoveRange(0, 8);
            return selection;
        }

        public void ReturnUsedCards(List<Card> cards)
        {
            deck.AddRange(cards);
        }
    }
}
