using UnityEngine;
using System.Collections;

namespace Assets.Scripts.Weapons
{
    public class SpearHitbox : Hitbox
    {

        void Update()
        {
            transform.Translate(new Vector3(direction == Util.Enums.Direction.Left ? 1 : -1, 0, 0) * Speed * Time.deltaTime);
            if ((deathTime -= Time.deltaTime) < 0 || dead)
                Destroy(this.gameObject);
        }

        void OnTriggerEnter(Collider collider)
        {
            if((TimesCanPierce -= 1) == 0)
                dead = true;
        }
    }
}