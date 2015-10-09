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

        protected bool dead = false;
        private GridNode currentNode;

        public int Damage
        {
            get { return damage; }
            set { damage = value;  }
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

        void Update()
        {
            if (direction == Enums.Direction.None)
                deathTime -= Time.deltaTime;
            if (deathTime < 0 || dead || distance == 0)
                Destroy(this.gameObject);
        }

        void OnTriggerEnter(Collider collider)
        {
            if(!piercing)
                dead = true;
        }
    }
}
