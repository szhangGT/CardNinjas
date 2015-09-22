using UnityEngine;

namespace Assets.Scripts.CardSystem.Actions
{
    abstract class Action
    {
        protected Weapons.Hitbox hitbox;
        protected int range, damage;

        public Weapons.Hitbox HitBox
        {
            set { hitbox = value; }
        }
        public int Range
        {
            set { range = value; }
        }
        public int Damage
        {
            set { damage = value; }
        }

        public abstract void useCard(Player.Character actor);
    }
}
