using CollectionCardGame.Gameplay;
using System.Collections.Generic;
using UnityEngine;
using CollectionCardGame.Configurations;


namespace CollectionCardGame
{
    [CreateAssetMenu(fileName ="EmptyCard",menuName = "Card")]
    public class CardConfig: ScriptableObject
    {        
        public Sprite sprite;

        private string _name;
        public string Name
        {
            get { return _name; }
            private set { _name = value; }
        }

        [SerializeField] private string _cardDescription;
        public string CardDescription
        {
            get { return _cardDescription; }
            private set { _cardDescription = value; }
        }

        public enum CardType
        {
            Health, Strenght, Agility, Intellect, Charisma, Item
        }

        [SerializeField]private CardType _cardType;

        public CardType Type
        {
            get { return _cardType; }
            private set { _cardType = value; }
        }

        [SerializeField] private int _valueOfCard;

        public int ValueOfCard
        {
            get { return _valueOfCard; }
            private set 
            {
                _valueOfCard = value;
                if (_valueOfCard < 0) _valueOfCard = 0; 
            }
        }

        public List<CardEffect> _effects;

        
    }
}
