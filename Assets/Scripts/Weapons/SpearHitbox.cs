using UnityEngine;
using System.Collections;

namespace Assets.Scripts.Weapons
{
    public class SpearHitbox : Hitbox
    {
        private float speed = 20;

        private int numHits = 2;

        private Util.Enums.Direction direction = Util.Enums.Direction.Right;

        public float Speed
        {
            get { return speed; }
            set { speed = value; }
        }

        public Util.Enums.Direction Direction
        {
            get { return direction; }
            set { direction = value; }
        }

        void Update()
        {
            transform.Translate(new Vector3(direction == Util.Enums.Direction.Left ? 1 : -1, 0, 0) * speed * Time.deltaTime);
            if ((deathTime -= Time.deltaTime) < 0 || dead)
                Destroy(this.gameObject);
        }

        void OnTriggerEnter(Collider collider)
        {
            if((numHits -= 1) == 0)
                dead = true;
        }
    }
}