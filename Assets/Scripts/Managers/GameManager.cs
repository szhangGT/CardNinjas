using UnityEngine;
using System.Collections;

namespace Assets.Scripts.Managers
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance;

        public enum GameStates
        {
            Battle,
            CardSelection
        }

        private static GameStates state = GameStates.CardSelection;

        void Awake()
        {
            if(instance == null)
            {
                DontDestroyOnLoad(this.gameObject);
                instance = this;
            }
            else if(this != instance)
            {
                Destroy(this.gameObject);
            }
        }

        void Update()
        {
            if(Input.GetKeyDown(KeyCode.O))
            {
                GameObject.Find("Card Selection Canvas").GetComponent<UI.CardSelector>().EnableCanvas();
            }
        }

        private void CardSelectorStateEnable()
        {
            state = GameStates.CardSelection;
        }
        private void CardSelectorStateDisable()
        {
            state = GameStates.Battle;
        }

        public static GameStates State
        {
            get { return state; }
        }

        void OnEnable()
        {
            UI.CardSelector.CardSelectorEnabled += CardSelectorStateEnable;
            UI.CardSelector.CardSelectorDisabled += CardSelectorStateDisable;
        }
        void OnDisable()
        {
            UI.CardSelector.CardSelectorEnabled -= CardSelectorStateEnable;
            UI.CardSelector.CardSelectorDisabled -= CardSelectorStateDisable;
        }
    }
}
