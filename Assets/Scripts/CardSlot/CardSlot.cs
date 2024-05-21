using System;
using UnityEngine;
using Zenject;

namespace CollectionCardGame.Gameplay
{
    public class CardSlot : MonoBehaviour
    {
        public Action<Card> Filled;
        public Action Unfilled;

        [SerializeField]private Card _currentCard;
        public Card CurrentCard
        { 
            get { return _currentCard; }
            set 
            { 
                _currentCard = value;
                if (_currentCard != null) { Filled?.Invoke(_currentCard); }
                else { Unfilled?.Invoke(); }
            }
        }

        private void TakeCard()
        {
            TakeCard(_colCard);
            _colCard = null;
        }

        private void TakeCard(Card card)
        {
            if (_currentCard != null) { ChangeCard(card); return; }
            card.Taken += LoseCard;
            _currentCard = card;
            card.transform.position = this.transform.position;
        }

        private void LoseCard()
        {
            if (_currentCard == null) { return; }
            _currentCard.Taken -= LoseCard;
            _currentCard.Dropped -= TakeCard;
            _colCard = _currentCard;
            _currentCard = null;
        }

        private void ChangeCard(Card newCard)
        {
            
        }

        public Card _colCard;
        private void OnTriggerEnter(Collider collision)
        {
            if (collision.gameObject.TryGetComponent(out Card card))
            {
                if (card != _currentCard)
                {
                    _colCard = card;
                    _colCard.Dropped += TakeCard;
                }
            }
        }

        private void OnTriggerExit(Collider collision)
        {
            if (collision.gameObject.TryGetComponent(out Card card))
            {
                if (card = _colCard)
                {
                    _colCard.Dropped -= TakeCard;
                    _colCard = null;
                }
            }
        }

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
    }
}
