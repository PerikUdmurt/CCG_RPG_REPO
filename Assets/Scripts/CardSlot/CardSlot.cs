using System;
using UnityEngine;
using Zenject;
using CollectionCardGame.UI;

namespace CollectionCardGame.Gameplay
{
    public class CardSlot : MonoBehaviour
    {
        #region Fields
        public Action<Card> Changed;

        [HideInInspector] public CardSlotPreview preview;
        private Card _currentCard;
        public Card CurrentCard
        { 
            get { return _currentCard; }
            set 
            { 
                _currentCard = value;
                if (_currentCard != null) { Changed?.Invoke(_currentCard); }
                else { Changed?.Invoke(null); }
            }
        }
        #endregion

        #region Methods
        private void Awake()
        {
            if (preview == null) { preview = GetComponentInChildren<CardSlotPreview>(); }
        }
        public void SetDefaultState()
        {
            LoseCard();
            if (preview != null)
            {
                preview.CardPreviewAnimation(0);
            }
        }


        public void TakeCard(Card card)
        {
            card.MoveTo(this.transform.position);
            CurrentCard = card;
            card.inCardSlot = true;
            card.Taken += LoseCard;
        }

        public void LoseCard()
        {
            if (CurrentCard != null)
            {
                CurrentCard.Taken -= LoseCard;
                CurrentCard.inCardSlot = false;
                CurrentCard = null;
            }
        }

        public void SwapCard(Card newCard)
        {
            CurrentCard.Taken -= LoseCard;
            CurrentCard.inCardSlot = false;
            CurrentCard.ReturnToStack();
            CurrentCard = null;

            TakeCard(newCard);
        }
        #endregion

        #region ObjectPool
        public class Pool : MemoryPool<CardSlot> 
        {
            protected override void OnCreated(CardSlot item)
            {
                base.OnCreated(item);
                item.gameObject.SetActive(false);
            }

            protected override void OnSpawned(CardSlot item)
            {
                base.OnSpawned(item);
                item.gameObject.SetActive(true);
            }

            protected override void OnDespawned(CardSlot item)
            {
                base.OnDespawned(item);
                item.gameObject.SetActive(false);
            }
        }
        #endregion
    }
}
