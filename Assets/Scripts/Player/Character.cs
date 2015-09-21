using UnityEngine;
using Assets.Scripts.Grid;

namespace Assets.Scripts.Player
{
    public abstract class Character : MonoBehaviour
    {
        [SerializeField]
        protected int health = 100;
        [SerializeField]
        protected int rowStart = 1;
        [SerializeField]
        protected int colStart = 1;
        [SerializeField]
        private Util.Enums.Direction direction = Util.Enums.Direction.Left;


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
    }
}