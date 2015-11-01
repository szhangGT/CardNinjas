using UnityEngine;
using Assets.Scripts.Player;
using Assets.Scripts.Grid;

namespace Assets.Scripts.CardSystem.Actions
{
    class Lightning : Action
    {
        public override void useCard(Character actor)
        {
            GameObject[] strikenNodes = new GameObject[range];
            int randHolder;
            int strikeTracker = 0;
            bool repeater = false;
            if (actor.Direction == Util.Enums.Direction.Left)
            {
                GameObject[] enemyNodes = GameObject.FindGameObjectsWithTag("Red");
                while (strikenNodes[range-1] == null)
                {
                    randHolder = (int)Random.Range(0, enemyNodes.Length);
                    foreach(GameObject node in strikenNodes)
                    {
                        repeater = repeater||(node == enemyNodes[randHolder]);
                    }
                    if (repeater == false)
                    {
                        strikenNodes[strikeTracker] = enemyNodes[randHolder];
                        strikeTracker++;
                    }
                    else
                    {
                        repeater = false;
                    }
                }
                foreach(GameObject node in strikenNodes)
                {
                    spawnObjectUsingPrefabAsModel(damage, 9, .2f, false, Util.Enums.Direction.None, 10, 0, true, node.GetComponent<GridNode>(), actor);
                }
            }
            if (actor.Direction == Util.Enums.Direction.Right)
            {
                GameObject[] enemyNodes = GameObject.FindGameObjectsWithTag("Blue");
                while (strikenNodes[range-1] == null)
                {
                    randHolder = (int)Random.Range(0, enemyNodes.Length);
                    Debug.Log(randHolder);
                    foreach (GameObject node in strikenNodes)
                    {
                        repeater = repeater||(node == enemyNodes[randHolder]);
                    }
                    if (repeater == false)
                    {
                        strikenNodes[strikeTracker] = enemyNodes[randHolder];
                        strikeTracker++;
                    }
                    else
                    {
                        repeater = false;
                    }
                }
                foreach (GameObject node in strikenNodes)
                {
                    spawnObjectUsingPrefabAsModel(damage, 9, .2f, false, Util.Enums.Direction.None, 10, 0, true, node.GetComponent<GridNode>(), actor);
                }
            }
        }
    }
}