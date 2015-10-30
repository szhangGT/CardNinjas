using UnityEngine;
using System.Collections.Generic;

namespace Assets.Scripts.Enemies
{
    class TenguBoss : Enemy
    {
        [SerializeField]
        private GameObject bullet;
        [SerializeField]
        private Transform holdPos;

        private Vector3 TeleportPos;
        private GameObject player;
        private int state, prevState;
        private bool doOnce, moveToHold, moveFailed;
        private float waitTime, moveFailedWait;

        private TenguBossStateMachine machine;

        protected override void Initialize()
        {
            player = FindObjectOfType<Player.Player>().gameObject;
            Random.seed = System.DateTime.Today.Millisecond;
            state = 0;
            prevState = 0;
            waitTime = Random.Range(0, 2);
            doOnce = false;
            moveToHold = false;
            moveFailed = false;
            moveFailedWait = 1f;
            machine = new TenguBossStateMachine();
        }

        protected override void RunAI()
        {
            if (state != prevState)
            {
                doOnce = false;
                prevState = state;
                waitTime = Random.Range(0, 2);
            }
            if (animDone)
            {
                if (moveToHold)
                {
                    transform.position = holdPos.position;
                    moveToHold = false;
                }
                if(state == (int)TenguBossStateMachine.State.Move)
                    doOnce = false;
            }            
            if(moveFailed)
            {
                if ((moveFailedWait -= Time.deltaTime) < 0)
                {
                    moveFailedWait = 1f;
                    moveFailed = false;
                }
            }

            state = machine.Run(animDone, waitTime < 0, moveFailed);

            switch (state)
            {
                case (int)TenguBossStateMachine.State.Wait: Wait(); break;
                case (int)TenguBossStateMachine.State.Move: Move(); break;
                case (int)TenguBossStateMachine.State.TeleportPrep: TeleportPrep(); break;
                case (int)TenguBossStateMachine.State.WaitToAppear: WaitToAppear(); break;
                case (int)TenguBossStateMachine.State.Attack: Attack(); break;
                case (int)TenguBossStateMachine.State.Return: Return(); break;
                case (int)TenguBossStateMachine.State.Tornado: Tornado(); break;
            }

        }

        private void Wait()
        {
        }

        private void Move()
        {
            if(!doOnce)
            {
                GameObject[] tiles = GameObject.FindGameObjectsWithTag("Red");
                List<Grid.GridNode> usableTiles = new List<Grid.GridNode>();
                Grid.GridNode n;
                foreach (GameObject g in tiles)
                {
                    n = g.GetComponent<Grid.GridNode>();
                    if (!n.Occupied)
                        usableTiles.Add(n);
                }
                int tile = usableTiles.Count > 0 ? Random.Range(0, usableTiles.Count - 1) : -1;
                if (tile == -1)
                    moveFailed = true;
                else
                {
                    currentNode.clearOccupied();
                    currentNode = usableTiles[tile];
                    currentNode.Owner = (this);
                }

            }
        }

        private void TeleportPrep()
        {
            if(!doOnce)
            {
                doOnce = true;
                moveToHold = true;
            }
        }

        private void WaitToAppear()
        {
            waitTime -= Time.deltaTime;
            if(!doOnce && waitTime < .1f)
            {
                doOnce = true;
                TeleportPos = player.transform.position;
            }
        }

        private void Attack()
        {
        }

        private void Return()
        {
            if(!doOnce)
            {
                doOnce = true;
                transform.position = currentNode.transform.position;
            }
        }

        private void Tornado()
        {
            Weapons.Hitbox b = Instantiate(bullet).GetComponent<Weapons.Hitbox>();
            b.transform.position = currentNode.Left.transform.position;
            b.CurrentNode = currentNode.Left;
        }
    }
}
