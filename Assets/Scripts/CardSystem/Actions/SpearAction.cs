using UnityEngine;
using Assets.Scripts.Player;

namespace Assets.Scripts.CardSystem.Actions
{
    class SpearAction : Action
    {
        public override void useCard(Character actor)
        {
            Weapons.Hitbox temp = MonoBehaviour.Instantiate(hitbox);
            temp.Damage = damage;
            temp.DeathTime = 3f;
            if (actor.Direction == Util.Enums.Direction.Left)
            {
                temp.transform.position = actor.CurrentNode.Left.transform.position;
                
            }

            if (actor.Direction == Util.Enums.Direction.Right)
            {
                temp.transform.position = actor.CurrentNode.Right.transform.position;
                
            }
        }
    }
}
