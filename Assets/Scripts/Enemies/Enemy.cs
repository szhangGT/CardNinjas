using UnityEngine;
using Assets.Scripts.Grid;

namespace Assets.Scripts.Enemies
{
    abstract class Enemy : Player.Character
    {
        protected abstract void Initialize();
        protected abstract void RunAI();

        void Start()
        {
            grid = FindObjectOfType<GridManager>().Grid;
            currentNode = grid[rowStart, colStart];
            currentNode.Owner = this;
            transform.position = currentNode.transform.position;
            Initialize();
        }

        void Update()
        {
            RunAI();
        }

        void OnTriggerEnter(Collider col)
        {
            Weapons.Hitbox hitbox = col.gameObject.GetComponent<Weapons.Hitbox>();
            if (hitbox != null)
            {
                TakeDamage(hitbox.Damage);
            }
        }
    }
}
