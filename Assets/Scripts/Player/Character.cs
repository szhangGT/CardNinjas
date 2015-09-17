using UnityEngine;
using Assets.Scripts.Grid;

namespace Assets.Scripts.Player
{
    public abstract class Character : MonoBehaviour
    {
        public int health = 100;

        public int rowStart = 1;
        public int colStart = 1;
        protected GridNode[,] grid;

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