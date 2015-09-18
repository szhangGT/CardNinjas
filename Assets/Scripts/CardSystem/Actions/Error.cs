using UnityEngine;

namespace Assets.Scripts.CardSystem.Actions
{
    class Error : Action
    {
        public override void useCard(int range, int damage)
        {
            Debug.Log("I am Error.");
        }
    }
}
