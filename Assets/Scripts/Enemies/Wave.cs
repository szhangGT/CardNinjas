using UnityEngine;

namespace Assets.Scripts.Enemies
{
    class Wave : MonoBehaviour
    {
        [SerializeField]
        private Enemy[] enemies;
        [SerializeField]
        private Vector2[] spawnPositions;

        internal void SpawnWave()
        {
            Enemy temp;
            for( int i = 0; i< enemies.Length; i++)
            {
                temp = Instantiate(enemies[i]);
                temp.RowStart = (int)spawnPositions[i].x;
                temp.ColStart = (int)spawnPositions[i].y;
            }
        }
    }
}
