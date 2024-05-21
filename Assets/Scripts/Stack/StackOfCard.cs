using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace CollectionCardGame.Gameplay
{
    public class StackOfCard : MonoBehaviour, ISelectable, IUsable
    {
        public Action Used {  get; set; }
        public Action Selected { get; set; }
        public Action Deselected { get; set; }
        [SerializeField] private CardConfig.CardType _type;
        private Dictionary<Card, int> _cards = new Dictionary<Card, int>();

        private void AddToStack(Card card)
        {
            _cards.Add(card, card.Value);
            var sortedCard = from entry in _cards orderby entry.Value select entry;
            for (int i = 0; i < _cards.Count; i++)
            {
                Debug.Log(_cards.ElementAt(i));
            }
        }

        private void RemoveFromStack(Card card) 
        {  
            _cards.Remove(card); 
        }

        public void Select(){}

        public void Deselect(){}

        public void Use(){}

        private void ShowCard()
        {
            Debug.Log("ShowCard");
        }

        private void HideCard()
        {
            Debug.Log("HideCard");
        }
    }

}