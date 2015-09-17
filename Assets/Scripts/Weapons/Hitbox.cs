using UnityEngine;

namespace Assets.Scripts.Weapons
{
    public class Hitbox : MonoBehaviour
    {
        [SerializeField]
        protected float damage = 10;
        [SerializeField]
        protected float deathTime = 3;

        protected bool dead = false;

        public float Damage
        {
            get { return damage; }
            set { damage = value;  }
        }

        public float DeathTime
        {
            get { return deathTime; }
            set { deathTime = value; }
        }

        void Update()
        {
            if((deathTime -= Time.deltaTime) < 0 || dead)
                Destroy(this.gameObject);
        }

        void OnTriggerEnter(Collider collider)
        {
            dead = true;
        }
    }
}
