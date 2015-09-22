using Assets.Scripts.Util;
using Assets.Scripts.Grid;

namespace Assets.Scripts.Player
{
    public class Player : Character
    {

        private bool hasStart = false;
        private CardSystem.Card[] cards;
        private int currentCard;

        void Start()
        {
            grid = FindObjectOfType<GridManager>().Grid;
            currentNode = grid[rowStart, colStart];
            currentNode.Owner = this;
            cards = FindObjectOfType<CardSystem.CardList>().Cards;
            currentCard = 0;

        }

        void Update()
        {
            movementCheck();
            transform.position = currentNode.transform.position;
            if (CustomInput.BoolFreshPress(CustomInput.UserInput.UseCard))
            {
                cards[currentCard++].Action.useCard(this);
                if (currentCard >= cards.Length)
                    currentCard = 0;
            }
        }

        //MovementCheck does all the necessary keyPress checks and counts which ones are true. 
        //Then it calculates the moved hex in batch form, and returns the end result.
        void movementCheck()
        {
            bool up = false;
            bool down = false;
            bool left = false;
            bool right = false;

            if (!hasStart)
            {
                currentNode = grid[rowStart, colStart];
                hasStart = true;
            }
            if (CustomInput.BoolFreshPress(CustomInput.UserInput.Up))
                up = true;
            if (CustomInput.BoolFreshPress(CustomInput.UserInput.Down))
                down = true;
            if (CustomInput.BoolFreshPress(CustomInput.UserInput.Left))
                left = true;
            if (CustomInput.BoolFreshPress(CustomInput.UserInput.Right))
                right = true;


            if (up || down || left || right)
            {
                if (up)
                {
                    if (currentNode.panelAllowed(Enums.Direction.Up, Type))
                    {
                        currentNode.clearOccupied();
                        currentNode = currentNode.Up;
                    }
                }
                if (down)
                {
                    if (currentNode.panelAllowed(Enums.Direction.Down, Type))
                    {
                        currentNode = currentNode.Down;
                    }
                }
                if (left)
                {
                    if (currentNode.panelAllowed(Enums.Direction.Left, Type))
                    {
                        currentNode.clearOccupied();
                        currentNode = currentNode.Left;
                    }
                }
                if (right)
                {
                    if (currentNode.panelAllowed(Enums.Direction.Right, Type))
                    {
                        currentNode.clearOccupied();
                        currentNode = currentNode.Right;
                    }
                }
                currentNode.Owner = (this);
            }
        }
    }
}
