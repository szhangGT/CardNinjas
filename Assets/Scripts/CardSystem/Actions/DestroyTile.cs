using UnityEngine;
using Assets.Scripts.Player;

namespace Assets.Scripts.CardSystem.Actions
{
	class DestroyTile : Action
	{
		public override void useCard(Character actor)
		{

			//Create a reference to the node we're trying to destroy. Since we do not know the
			//entire grid map, we will start at the actor origin and work from there.
			Grid.GridNode targetNode = actor.CurrentNode;

			//Keep track of the hitbox in case we want to do damage when we destroy a tile.
			Weapons.Hitbox temp = MonoBehaviour.Instantiate(hitbox);
			temp.Damage = damage;
			temp.DeathTime = .2f;
			temp.transform.position = targetNode.transform.position;


			if (actor.Direction == Util.Enums.Direction.Left)
			{
				for (int i = 0; i < range; i++)
				{
					if (actor.CurrentNode.Left.panelExists(Util.Enums.Direction.Left))
					{
						temp.transform.position = targetNode.Left.transform.position;
						targetNode = targetNode.Left;
					}
				}
				//Destroy the furthest tile within range.
				targetNode.Type = Assets.Scripts.Util.Enums.FieldType.Destroyed;

			}

			if (actor.Direction == Util.Enums.Direction.Right)
			{
				for (int i = 0; i < range; i++)
				{
					if (actor.CurrentNode.Right.panelExists(Util.Enums.Direction.Right))
					{
						temp.transform.position = targetNode.Right.transform.position;
						targetNode = targetNode.Right;
					}
				}
				//Destroy the furthest tile within range.
				targetNode.Type = Assets.Scripts.Util.Enums.FieldType.Destroyed;
			}
		}
	}
}
