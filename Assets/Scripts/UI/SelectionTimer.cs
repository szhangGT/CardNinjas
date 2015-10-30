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

        private Transform timerBar;

        private Canvas canvas;

        #region Events
        public delegate void SelectionTimerAction();
        public static event SelectionTimerAction TimerFinish;

        void OnEnable()
        {
            CardSelector.CardSelectorDisabled += Show;
        }
        void OnDisable()
        {
            CardSelector.CardSelectorDisabled -= Show;
        }
        #endregion

        void Start()
        {
            timerBar = GameObject.Find("Card Selection Timer").transform;
            canvas = this.GetComponent<Canvas>();

            canvas.enabled = false;
        }

        void Update()
        {
            if (canvas.enabled)
            {
                timer += Time.deltaTime;
                xScale = Mathf.Clamp((timeToSelect - timer) / timeToSelect, 0, 1);

                timerBar.localScale = new Vector3(xScale, 1, 1);

                if (timer >= timeToSelect)
                {
                    Hide();
                }
            }
        }

        private void Show()
        {
            canvas.enabled = true;
        }

        private void Hide()
        {
            timer = 0f;
            canvas.enabled = false;
            if (TimerFinish != null) TimerFinish();
        }
    }
}
