using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Assets.Scripts.UI
{
    public class SelectionTimer : MonoBehaviour
    {
        public float timeToSelect = 10f;
        private float timer = 0;

        private float xScale = 1f;

        private Image timerBar;

        void Start()
        {
            timerBar = GameObject.Find("Card Selection Timer").GetComponent<Image>();
        }

        void Update()
        {
            timer += Time.deltaTime;

            if(timer >= timeToSelect)
            {
                timer = 0f;

            }
        }
    }
}
