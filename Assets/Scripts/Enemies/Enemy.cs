using UnityEngine;
using Assets.Scripts.Grid;

namespace Assets.Scripts.Enemies
{
    abstract class Enemy : Player.Character
    {
        protected abstract void Initialize();
        protected abstract void RunAI();

		protected bool hit = false;

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
            if (Managers.GameManager.State == Util.Enums.GameStates.Battle)
            {
                RunAI();
                transform.position = currentNode.transform.position;
				if(hit){
					hit = false;
				}
            }
        }

        void OnTriggerEnter(Collider col)
        {
            Weapons.Hitbox hitbox = col.gameObject.GetComponent<Weapons.Hitbox>();
            if (hitbox != null)
            {
				hit=true;
                TakeDamage(hitbox.Damage , hitbox.Element);
            }
        }
    }
}
