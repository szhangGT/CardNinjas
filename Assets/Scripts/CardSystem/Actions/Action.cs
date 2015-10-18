using UnityEngine;

namespace Assets.Scripts.CardSystem.Actions
{
    public abstract class Action
    {
        protected Weapons.Hitbox hitbox;
        protected int range, damage;
        protected GameObject prefab;

        public Weapons.Hitbox HitBox
        {
            set { hitbox = value; }
        }

        public GameObject Prefab
        {
            set { prefab = value; }
        }

        public int Range
        {
            get { return range; }
            set { range = value; }
        }
        public int Damage
        {
            get { return damage; }
            set { damage = value; }
        }

        public abstract void useCard(Player.Character actor);

        protected void spawnObjectUsingPrefabAsModel(int damage, int distance, float deathTime, bool piercing, Util.Enums.Direction direction, float speed, int timesCanPierce, bool isFlying, Grid.GridNode spawnPosition)
        {
            Weapons.Hitbox temp = MonoBehaviour.Instantiate(hitbox);
            GameObject model = MonoBehaviour.Instantiate(prefab);
            model.transform.parent = temp.transform;
            temp.Damage = damage;
            temp.Distance = distance;
            temp.DeathTime = deathTime;
            temp.Piercing = piercing;
            temp.Direction = direction;
            temp.Speed = speed;
            temp.TimesCanPierce = timesCanPierce;
            temp.IsFlying = isFlying;
            temp.CurrentNode = spawnPosition;
            temp.transform.position = spawnPosition.transform.position;
        }
    }
}
