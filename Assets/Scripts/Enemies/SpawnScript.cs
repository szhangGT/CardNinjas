using UnityEngine;

namespace Assets.Scripts.Enemies
{
    class SpawnScript : MonoBehaviour
    {
        [SerializeField]
        private Wave[] waves;

        private float wait = -1;
        private int currWave = 0;
        private GameObject[] enemies;
        private bool done;

        void Start()
        {
            enemies = new GameObject[0];
            done = true;
        }

        void Update()
        {
            done = true;
            foreach (GameObject g in enemies)
                if (g != null)
                    done = false;
            if (done && wait < 0)
                wait = 0;
            if(wait > -1)
                wait += Time.deltaTime;
            if(wait > 1f)
            {
                if (currWave >= waves.Length)
                    Application.Quit();
                enemies = waves[currWave].SpawnWave();
                wait = -1;
                currWave++;
            }

            //if (currWave >= waves.Length)
            //    Destroy(this.gameObject);
        }
    }
}
