using UnityEngine;
using Assets.Scripts.Util;
using Assets.Scripts.Grid;

namespace Assets.Scripts.Weapons
{
    public class Hitbox : MonoBehaviour
    {
        [SerializeField]
        protected int damage = 10;
        [SerializeField]
        protected int distance = 3;
        [SerializeField]
        protected float deathTime = 3;
        [SerializeField]
        protected bool piercing = true;
        [SerializeField]
        protected Enums.Direction direction = Enums.Direction.None;
        [SerializeField]
        private float speed = 20;
        [SerializeField]
        private int timesCanPierce = 2;

        protected bool dead = false;
        private bool moveCompleted = false;
        private float pos = 0;
        private GridNode currentNode;
        private GridNode target;

        public int Damage
        {
            get { return damage; }
            set { damage = value; }
        }

        public int Distance
        {
            get { return distance; }
            set { distance = value; }
        }

        public float DeathTime
        {
            get { return deathTime; }
            set { deathTime = value; }
        }

        public bool Piercing
        {
            get { return piercing; }
            set { piercing = value; }
        }

        public Enums.Direction Direction
        {
            get { return direction; }
            set { direction = value; }
        }

        public float Speed
        {
            get { return speed; }
            set { speed = value; }
        }
        
        public int TimesCanPierce
        {
            get { return timesCanPierce; }
            set { timesCanPierce = value; }
        }

        protected bool MoveCompleted
        {
            get { return moveCompleted; }
        }

        void Update()
        {
            if (moveCompleted)
            {
                switch (direction)
                {
                    case Enums.Direction.Up:
                        if (currentNode.panelExists(Enums.Direction.Up))
                            target = currentNode.Up;
                        else
                            Destroy(this.gameObject);
                        break;
                    case Enums.Direction.Down:
                        if (currentNode.panelExists(Enums.Direction.Down))
                            target = currentNode.Down;
                        else
                            Destroy(this.gameObject);
                        break;
                    case Enums.Direction.Left:
                        if (currentNode.panelExists(Enums.Direction.Left))
                            target = currentNode.Left;
                        else
                            Destroy(this.gameObject);
                        break;
                    case Enums.Direction.Right:
                        if (currentNode.panelExists(Enums.Direction.Right))
                            target = currentNode.Right;
                        else
                            Destroy(this.gameObject);
                        break;
                    default: deathTime -= Time.deltaTime; break;
                }
            }
            if (deathTime < 0 || dead || distance == 0)
                Destroy(this.gameObject);
            Move();
        }

        void OnTriggerEnter(Collider collider)
        {
            if (!piercing || timesCanPierce == 0)
                dead = true;
            else
                timesCanPierce--;
        }

        private void Move()
        {
            if(!moveCompleted)
                transform.position = Vector3.Lerp(currentNode.transform.position, target.transform.position, pos = pos + Time.deltaTime);
            if(pos > 1)
            {
                moveCompleted = true;
                transform.position = target.transform.position;
                currentNode = target;
                distance--;
            }
        }
    }
}
