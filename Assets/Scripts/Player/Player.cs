using UnityEngine;
using System.Collections.Generic;
using Assets.Scripts.Util;
using Assets.Scripts.Grid;
using Assets.Scripts.CardSystem;

namespace Assets.Scripts.Player
{
    public class Player : Character
    {
        public delegate void NewSelectedCard(Card card);
        public static event NewSelectedCard NewSelect;

        [SerializeField]
        private Animator anim;
        [SerializeField]
        private Weapons.Hitbox bullet;
        [SerializeField]
        private GameObject Katana;
        [SerializeField]
        private GameObject Naginata;
        [SerializeField]
        private GameObject Hammer;
        [SerializeField]
        private Transform barrel;
        [SerializeField]
        private int playerNumber = 1;
        [SerializeField]
        private Transform weaponPoint;

        private int damage = 0;
        private bool doOnce = false;
        private bool move = false;
        private bool useCard = false;
        private bool basicAttack = false;
        private bool attack = false;
        private bool takeDamage = false;
        private bool invun = false;
        private float invunTime = .5f;
        private float invunTimer = 0;
        private float hold = 0;//used for delays
        private Enums.Direction directionToMove;
        private GridNode nextNode;

        private float renderTime = .002f;
        private float renderTimer = 0;
        private bool animDone = false;
        private bool hit = false;
        private bool render = false;
        private PlayerStateMachine machine;
        private Enums.PlayerState prevState = 0;
        private Enums.PlayerState currState = 0;
        private Enums.Element damageElement = Enums.Element.None;
        private GameObject weapon;

        private Deck deck;
        private Hand hand;
        private const int HAND_SIZE = 4;

        private bool paused = false;
        private float animSpeed = 0;

        public Deck Deck
        {
            get { return deck; }
            set { deck = value; }
        }

        void Awake()
        {
            deck = new Deck(FindObjectOfType<CardList>().Cards);
        }
        void Start()
        {
            grid = FindObjectOfType<GridManager>().Grid;
            currentNode = grid[rowStart, colStart];
            currentNode.Owner = this;
            transform.position = currentNode.transform.position;
            hand = new Hand();
            //state machine init
            machine = new PlayerStateMachine();
            renderTimer = 0;
            invunTimer = 0;
        }

        void Update()
        {
            if (Managers.GameManager.State == Enums.GameStates.Battle && !stun)
            {
                if (paused)
                {
                    paused = false;
                    anim.speed = animSpeed;
                }
                if (CustomInput.BoolFreshPress(CustomInput.UserInput.Up, playerNumber))
                {
                    if (currentNode.panelAllowed(Enums.Direction.Up, Type))
                    {
                        directionToMove = Enums.Direction.Up;
                        nextNode = currentNode.Up;
                    }
                }
                else if (CustomInput.BoolFreshPress(CustomInput.UserInput.Down, playerNumber))
                {
                    if (currentNode.panelAllowed(Enums.Direction.Down, Type))
                    {
                        directionToMove = Enums.Direction.Down;
                        nextNode = currentNode.Down;
                    }
                }
                else if (CustomInput.BoolFreshPress(CustomInput.UserInput.Left, playerNumber))
                {
                    if (currentNode.panelAllowed(Enums.Direction.Left, Type))
                    {
                        directionToMove = Enums.Direction.Left;
                        nextNode = currentNode.Left;
                    }
                }
                else if (CustomInput.BoolFreshPress(CustomInput.UserInput.Right, playerNumber))
                {
                    if (currentNode.panelAllowed(Enums.Direction.Right, Type))
                    {
                        directionToMove = Enums.Direction.Right;
                        nextNode = currentNode.Right;
                    }
                }
                else
                    directionToMove = Enums.Direction.None;
                //get next state
                currState = machine.update(hit, animDone, directionToMove, hand.GetCurrentType(), hand.Empty(), playerNumber);

                //state clean up
                if (prevState != currState)
                {
                    doOnce = false;
                    animDone = false;
                    attack = false;
                    basicAttack = false;
                    move = false;
                    hit = false;
                    if (weapon != null)
                        Destroy(weapon);
                    anim.SetInteger("state", (int)currState);
                }
                if (invunTimer > 0)
                {
                    if (renderTimer > renderTime)
                    {
                        render = !render;
                        renderTimer = 0;
                        //GetComponent<Renderer>().enabled = render;
                    }
                    hit = false;
                    renderTimer += Time.deltaTime;
                    invunTimer -= Time.deltaTime;
                }
                else
                {
                    //GetComponent<Renderer>().enabled = true;
                    invun = false;
                }

                //run state
                switch (currState)
                {
                    case Enums.PlayerState.Idle:Idle(); break;
                    case Enums.PlayerState.MoveBegining: MoveBegining(); break;
                    case Enums.PlayerState.MoveEnding: MoveEnding(); break;
                    case Enums.PlayerState.Hit: Hit(); break;
                    case Enums.PlayerState.Dead: Dead(); break;
                    case Enums.PlayerState.BasicAttack: CardAnim(); break;
                    case Enums.PlayerState.HoriSwingMid: CardAnim(); break;
                    case Enums.PlayerState.VertiSwingHeavy: CardAnim(); break;
                    case Enums.PlayerState.ThrowLight: CardAnim(); break;
                    case Enums.PlayerState.ThrowMid: CardAnim(); break;
                    case Enums.PlayerState.Shoot: CardAnim(); break;
                    case Enums.PlayerState.ChiAttack: CardAnim(); break;
                    case Enums.PlayerState.ChiStationary: CardAnim(); break;
                    case Enums.PlayerState.TauntGokuStretch: Taunt(); break;
                    case Enums.PlayerState.TauntPointPoint: Taunt(); break;
                    case Enums.PlayerState.TauntThumbsDown: Taunt(); break;
                    case Enums.PlayerState.TauntWrasslemania: Taunt(); break;
                    case Enums.PlayerState.TauntYaMoves: Taunt(); break;
                }

                if (move)
                {
                    move = false;
                    currentNode.clearOccupied();
                    currentNode = nextNode;
                    currentNode.Owner = (this);
                    transform.position = currentNode.transform.position;
                }

                if (useCard)
                {
                    if (!hand.Empty())
                    {
                        Enums.CardTypes type = hand.GetCurrentType();
                        if (type == Enums.CardTypes.SwordHori || type == Enums.CardTypes.SwordVert)
                        {
                            weapon = Instantiate(Katana);
                            weapon.transform.position = weaponPoint.position;
							weapon.transform.localScale = weaponPoint.localScale;
                            weapon.transform.parent = weaponPoint;
							weapon.transform.localEulerAngles = new Vector3(0,0,0);

                        }
                        else if (type == Enums.CardTypes.NaginataHori || type == Enums.CardTypes.NaginataVert)
                        {
                            weapon = Instantiate(Naginata);
                            weapon.transform.position = weaponPoint.position;
                            weapon.transform.localRotation = weaponPoint.localRotation;
                            weapon.transform.localScale = weaponPoint.localScale / 1.5f;
                            weapon.transform.parent = weaponPoint;
                        }
                        else if (type == Enums.CardTypes.HammerHori || type == Enums.CardTypes.HammerVert)
                        {
                            weapon = Instantiate(Hammer);
							weapon.transform.position = weaponPoint.position;
							weapon.transform.localScale = weaponPoint.localScale;
							weapon.transform.parent = weaponPoint;
							weapon.transform.localEulerAngles = new Vector3(0,0,0);
                        }
                        useCard = false;
                        hand.UseCurrent(this);
                        CardUIEvent();
                    }
                }

                if (basicAttack)
                {
                    basicAttack = false;
                    Weapons.Hitbox b = Instantiate(bullet);
                    b.Owner = this.gameObject;
                    b.transform.position = Direction == Enums.Direction.Left ? currentNode.Left.transform.position : currentNode.Right.transform.position;
                    b.CurrentNode = Direction == Enums.Direction.Left ? currentNode.Left : currentNode.Right;
                    b.Direction = Direction;
                }

                if (damage > 0 && takeDamage)
                {
                    takeDamage = false;
                    TakeDamage(damage, damageElement);
                    damage = 0;
                    damageElement = Enums.Element.None;
                }
                prevState = currState;
            }
            else
            {
                if (!paused)
                {
                    animSpeed = anim.speed;
                    anim.speed = 0;
                    paused = true;
                }
                if (stun)
                {
                    if ((stunTimer += Time.deltaTime) > stunTime)
                    {
                        stunTimer = 0f;
                        stun = false;
                    }
                }
            }
        }

        public void AnimDetector()
        {
            animDone = true;
        }

        public void Attack()
        {
            attack = true;
        }

        private void CardUIEvent()
        {
            if (NewSelect != null)
                NewSelect(hand.getCurrent()); //fire event to gui
        }

        public Hand Hand
        {
            get { return hand; }
        }

        public void AddCardsToHand(List<Card> cards)
        {
            hand.PlayerHand = cards;
        }

        void OnTriggerEnter(Collider col)
        {
            Weapons.Hitbox hitbox = col.gameObject.GetComponent<Weapons.Hitbox>();
            if (hitbox != null && !invun)
            {
                hit = true;
                damage = hitbox.Damage;
                damageElement = hitbox.Element;
            }
        }

        private void Idle()
        {
        }

        private void MoveBegining()
        {
        }

        private void MoveEnding()
        {
            if (!doOnce)
            {
                doOnce = true;
                move = true;
            }
        }

        private void Hit()
        {
            if (!doOnce)
            {
                doOnce = true;
                invunTimer = invunTime;
                invun = true;
                takeDamage = true;
            }
        }

        private void Dead()
        {
        }

        private void BasicAttack()
        {
            if (attack)
            {
                attack = false;
                basicAttack = true;
            }
        }

        private void CardAnim()
        {
            if (!doOnce)
            {
                doOnce = true;
                useCard = true;
            }
        }

        private void Taunt()
        {
        }
    }
}
