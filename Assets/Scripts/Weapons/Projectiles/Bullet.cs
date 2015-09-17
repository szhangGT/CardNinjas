using UnityEngine;

namespace Assets.Scripts.Weapons.Projectiles
{
    public class Bullet : Hitbox
    {
        [SerializeField]
        private float speed = 10;

        private Vector3 direction = Vector3.forward;

        public float Speed
        {
            get { return speed; }
            set { speed = value; }
        }

        public Vector3 Direction
        {
            get { return direction; }
            set { direction = value; }
        }

        void Update()
        {
            transform.Translate(direction * speed * Time.deltaTime);
            if ((deathTime -= Time.deltaTime) < 0 || dead)
                Destroy(this.gameObject);
        }
    }
}
