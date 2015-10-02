using UnityEngine;

namespace Assets.Scripts.Enemies
{
    class SpawnScript : MonoBehaviour
    {
        [SerializeField]
        private Wave[] waves;

        private float wait = -1;
        private int currWave = 0;

        void Update()
        {
            if ((FindObjectsOfType(typeof(Enemy)) as Enemy[]).Length == 0 && wait < 0)
                wait = 0;
            if(wait > -1)
                wait += Time.deltaTime;
            if(wait > 1f)
            {
                if (currWave >= waves.Length)
                    Application.Quit();
                waves[currWave].SpawnWave();
                wait = -1;
                currWave++;
            }

            //if (currWave >= waves.Length)
            //    Destroy(this.gameObject);
        }
    }
}
