using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace CollectionCardGame.Gameplay
{
    public class StackOfCard : MonoBehaviour, ISelectable, IUsable
    {
        #region Fields
        #region InterfaceRealisation
        public Action Used {  get; set; }
        public Action Selected { get; set; }
        public Action Deselected { get; set; }
        public bool isUsable { get; set; } = true;
        public bool isSelectable { get; set; } = true;
#endregion

        [SerializeField] private CardConfig.CardType _type;
        private Dictionary<Card, int> _cards = new Dictionary<Card, int>();
        #endregion

        #region Methods
        private void OnEnable()
        {
            Used += ShowCard;
        }


        public void AddToStack(Card card)
        {
            /*
            _cards.Add(card, card.Value);
            var sortedCard = from entry in _cards orderby entry.Value select entry;
            for (int i = 0; i < _cards.Count; i++)
            {
                Debug.Log(_cards.ElementAt(i));
            }
            */
        }

        public void RemoveFromStack(Card card) 
        {  
            _cards.Remove(card); 
        }

        private void ShowCard()
        {
            Debug.Log("ShowCard");
        }

        private void HideCard()
        {
            Debug.Log("HideCard");
        }
        #endregion
    }

}