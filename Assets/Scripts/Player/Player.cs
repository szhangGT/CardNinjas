using UnityEngine;
using Assets.Scripts.Util;
using Assets.Scripts.Grid;

namespace Assets.Scripts.Player
{
    public class Player : Character
    {
        [SerializeField]
        private Animator anim;
        [SerializeField]
        private GameObject bullet;
        [SerializeField]
        private Transform barrel;

        private CardSystem.Card[] cards;
        private int currentCard;
        private enum PlayerState { Idle, MoveBegining, Move, MoveEnding, Attacking, Hurt };
        private PlayerState state;
        private GridNode nextNode;

        public delegate void NewSelectedCard(string name, string type, int range, int damage, string description);
        public static event NewSelectedCard NewSelect;

        void Start()
        {
            grid = FindObjectOfType<GridManager>().Grid;
            currentNode = grid[rowStart, colStart];
            currentNode.Owner = this;
            cards = FindObjectOfType<CardSystem.CardList>().Cards;
            currentCard = 0;
            state = PlayerState.Idle;
            CardUIEvent(); //fire event to update card UI
        }

        void Update()
        {
            movementCheck();
            transform.position = currentNode.transform.position;
            Debug.Log(state);
            if (state == PlayerState.Idle)
            {
                if (CustomInput.BoolFreshPress(CustomInput.UserInput.UseCard))
                {
                    switch (cards[currentCard].Type)
                    {
                        case Enums.CardTypes.Sword: anim.SetBool("sword", true); break;
                        default: anim.SetBool("shoot", true); break;
                    }
                    cards[currentCard++].Action.useCard(this);
                    if (currentCard >= cards.Length)
                        currentCard = 0;
                    CardUIEvent();
                    state = PlayerState.Attacking;
                }
                else if(CustomInput.BoolFreshPress(CustomInput.UserInput.Attack))
                {
                    BasicAttack();
                    state = PlayerState.Attacking;
                }
            }
        }

        private void movementCheck()
        {
            bool up = false;
            bool down = false;
            bool left = false;
            bool right = false;

            if (CustomInput.BoolFreshPress(CustomInput.UserInput.Up))
                up = true;
            if (CustomInput.BoolFreshPress(CustomInput.UserInput.Down))
                down = true;
            if (CustomInput.BoolFreshPress(CustomInput.UserInput.Left))
                left = true;
            if (CustomInput.BoolFreshPress(CustomInput.UserInput.Right))
                right = true;

            if ((up || down || left || right) && state == PlayerState.Idle)
            {
                if (up)
                {
                    if (currentNode.panelAllowed(Enums.Direction.Up, Type))
                    {
                        state = PlayerState.MoveBegining;
                        anim.SetBool("startMove", true);
                        nextNode = currentNode.Up;
                    }
                }
                if (down)
                {
                    if (currentNode.panelAllowed(Enums.Direction.Down, Type))
                    {
                        state = PlayerState.MoveBegining;
                        anim.SetBool("startMove", true);
                        nextNode = currentNode.Down;
                    }
                }
                if (left)
                {
                    if (currentNode.panelAllowed(Enums.Direction.Left, Type))
                    {
                        state = PlayerState.MoveBegining;
                        anim.SetBool("startMove", true);
                        nextNode = currentNode.Left;
                    }
                }
                if (right)
                {
                    if (currentNode.panelAllowed(Enums.Direction.Right, Type))
                    {
                        state = PlayerState.MoveBegining;
                        anim.SetBool("startMove", true);
                        nextNode = currentNode.Right;
                    }
                }
            }

            if (state == PlayerState.Move)
            {
                currentNode.clearOccupied();
                currentNode = nextNode;
                currentNode.Owner = (this);
                state = PlayerState.MoveEnding;
                anim.SetBool("endMove", true);
            }
        }

        private void CardUIEvent()
        {
            if (NewSelect != null)
                NewSelect(cards[currentCard].Name, cards[currentCard].Type.ToString(),
                              cards[currentCard].Action.Range, cards[currentCard].Action.Damage, cards[currentCard].Description); //fire event to gui
        }

        private void MovementBeginEnd()
        {
            anim.SetBool("startMove", false);
            state = PlayerState.Move;
        }

        private void MovementEndEnd()
        {
            anim.SetBool("endMove", false);
            state = PlayerState.Idle;
        }

        private void AnimEnd()
        {
            Debug.Log("a");
            for (int i = 0; i < anim.parameters.Length; i++)
                anim.SetBool(anim.parameters[i].nameHash, false);
            state = PlayerState.Idle;
        }

        private void BasicAttack()
        {
            Weapons.Projectiles.Bullet b = Instantiate(bullet).GetComponent<Weapons.Projectiles.Bullet>();
            b.transform.position = barrel.position;
            b.Direction = Direction;
            anim.SetBool("shoot", true);
        }
    }
}
