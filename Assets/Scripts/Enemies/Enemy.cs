using UnityEngine;
using Assets.Scripts.Grid;

namespace Assets.Scripts.Enemies
{
    abstract class Enemy : Player.Character
    {
        protected abstract void Initialize();
        protected abstract void RunAI();

        protected bool hit = false;
        protected bool animDone = false;
        private bool paused = false;
        private float animSpeed = 0;

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
            if (Managers.GameManager.State == Util.Enums.GameStates.Battle && !stun)
            {
                if (paused)
                {
                    paused = false;
                    if (GetComponent<Animator>() != null)
                        GetComponent<Animator>().speed = animSpeed;
                }
                RunAI();
                if (hit)
                    hit = false;
                if (animDone)
                    animDone = false;
                //if (invulerability > 0)
                //{
                //    render = !render;
                //    GetComponent<Renderer>().enabled = render;
                //    damage = 0;
                //    beingHit = false;
                //    invulerability -= Time.deltaTime;
                //}
                //else
                //    GetComponent<Renderer>().enabled = true;
            }
            else
            {
                if (!paused)
                {
                    if (GetComponent<Animator>() != null)
                    {
                        animSpeed = GetComponent<Animator>().speed;
                        GetComponent<Animator>().speed = 0.0000001f;
                    }
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

        void OnTriggerEnter(Collider col)
        {
            Weapons.Hitbox hitbox = col.gameObject.GetComponent<Weapons.Hitbox>();
            if (hitbox != null)
            {
                hit = true;
                TakeDamage(hitbox.Damage, hitbox.Element);
            }
        }
    }
}
