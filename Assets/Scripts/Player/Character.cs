using UnityEngine;
using Assets.Scripts.Grid;

namespace Assets.Scripts.Player
{
    public abstract class Character : MonoBehaviour
    {
        [SerializeField]
        protected int health = 100;
        protected const int MAX_HEALTH = 100;
        [SerializeField]
        protected int rowStart = 1;
        [SerializeField]
        protected int colStart = 1;
        [SerializeField]
        private Util.Enums.Direction direction = Util.Enums.Direction.Left;
        [SerializeField]
        private Util.Enums.FieldType type = Util.Enums.FieldType.Red;


        protected GridNode[,] grid;
        protected GridNode currentNode;

        public GridNode CurrentNode
        {
            get { return currentNode; }
        }

        public Util.Enums.Direction Direction
        {
            get { return direction; }
        }

        public Util.Enums.FieldType Type
        {
            get { return type; }
        }

        bool invincible = false;

        void Start()
        {
            grid = FindObjectOfType<GridManager>().Grid;
        }
        
        public void takeDamage(int damage)
        {
            if (!invincible)
            {
                health = health - damage;
            }
        }

        public void addHealth(int health)
        {
            this.health = Mathf.Clamp(this.health + health, 0, MAX_HEALTH);
            Debug.Log("Healing by " + health.ToString() + " points. Health is " + this.health.ToString());
        }
    }
}