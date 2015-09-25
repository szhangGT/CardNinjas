using UnityEngine;
using System.Collections.Generic;
using System.Xml;
using System.IO;
using UnityEditor;

namespace Assets.Scripts.CardSystem
{
    class CardList : MonoBehaviour
    {
        [SerializeField]
        private TextAsset xmlCardList;

        private Card[] cards;

        public Card[] Cards
        {
            get
            {
                if (cards == null)
                    ReadList();
                return cards;
            }
        }

        /* XML Expected format
        <library>
            <card name= "Sword"> // The card name.
                <type>Sword</type> // The card type, has to one of the Enums.CardTypes.
                <action range= "1" damage= "3" hitBox= "Assets/Prefabs/HitBox.prefab">Sword</action> // Defines the action class for this card, must implement action and be in the namespace Assets.Scripts.CardSystem.Actions.
                <description>A basic sword.</description> // The string description of the card.
            </card>
        </library>
        */
        public void ReadList()
        {
            List<Card> tempList = new List<Card>();
            Weapons.Hitbox hitbox;
            string name, type, actionType, description;
            int range, damage;
            using (XmlReader reader = XmlReader.Create(new StringReader(xmlCardList.text)))
            {
                while (reader.ReadToFollowing("card"))
                {
                    reader.MoveToAttribute(0);
                    name = reader.Value;
                    reader.ReadToFollowing("type");
                    type = reader.ReadElementContentAsString();
                    reader.ReadToFollowing("action");
                    reader.MoveToFirstAttribute();
                    range = int.Parse(reader.Value);
                    reader.MoveToNextAttribute();
                    damage = int.Parse(reader.Value);
                    reader.MoveToNextAttribute();
                    hitbox = (AssetDatabase.LoadAssetAtPath(reader.Value, typeof(GameObject))as GameObject).GetComponent<Weapons.Hitbox>();
                    reader.MoveToContent();
                    actionType = reader.ReadElementContentAsString();
                    reader.ReadToFollowing("description");
                    description = reader.ReadElementContentAsString();
                    tempList.Add(new Card(name, type, range, damage, actionType, hitbox, description));
                }
            }
            cards = tempList.ToArray();
        }

        //void Start()
        //{
        //    ReadList();
        //    foreach (Card c in cards)
        //    {
        //        Debug.Log(string.Format("name: {0}, type: {1}, action: {2}, description: {3}", c.Name, c.Type, c.Action, c.Description));
        //    }
        //}
    }
}
