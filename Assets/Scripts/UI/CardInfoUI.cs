using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Assets.Scripts.UI
{
    public class CardInfoUI : MonoBehaviour
    {
        private Text nameText, typeText, descriptionText, rangeText, damageText;

        void OnEnable()
        {
            Player.Player.NewSelect += UpdateCardInfo;
        }
        void OnDisable()
        {
            Player.Player.NewSelect -= UpdateCardInfo;
        }

        void Start()
        {
            nameText = GameObject.Find("Name Text").GetComponent<Text>();
            typeText = GameObject.Find("Type Text").GetComponent<Text>();
            descriptionText = GameObject.Find("Description Text").GetComponent<Text>();
            rangeText = GameObject.Find("Range Text").GetComponent<Text>();
            damageText = GameObject.Find("Damage Text").GetComponent<Text>();

            HideDescription();
        }

        private void UpdateCardInfo(string name, string type, int range, int damage, string description)
        {
            nameText.text = "Name: " + name;
            typeText.text = "Type: " + type;
            descriptionText.text = "Description: "+ description;
            rangeText.text = "Range: " + range.ToString();
            damageText.text = "Damage: " + damage.ToString();
        }

        public void DisplayDescription()
        {
            if(!descriptionText.IsActive()) descriptionText.gameObject.SetActive(true);
        }

        public void HideDescription()
        {
            if (descriptionText.IsActive()) descriptionText.gameObject.SetActive(false);
        }
    }
}
