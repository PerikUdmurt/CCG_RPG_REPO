using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CollectionCardGame.Gameplay
{
    public class CardReciever
    {
        public event Action Updated;

        private int _maxCardSlots;
        private CardSlot.Pool _slotPool;
        private Card.Pool _cardPool;
        private Vector3 _entryPosition;
        private HashSet<CardSlot> _slots = new HashSet<CardSlot>();

        public CardReciever(Card.Pool cardPool,CardSlot.Pool slotPool, Vector3 entryPosition)
        {
            _cardPool = cardPool;
            _slotPool = slotPool;
            _entryPosition = entryPosition;
        }
        
        public void CreateCardSlot()
        {
            CardSlot currentSlot = _slotPool.Spawn();
            _slots.Add(currentSlot);
            currentSlot.Changed += UpdateInfo;
            currentSlot.transform.position = _entryPosition + new Vector3((_slots.Count - 1) * 2, 0, 0);
        }

        private void UpdateInfo()
        {
            Updated?.Invoke();
        }

        public List<Card> GetCombination()
        {
            List<Card> result = new List<Card>();
            foreach (CardSlot slot in _slots)
            {
                result.Add(slot.CurrentCard);
            }
            return result;
        }

        public void CreateCardSlots(int num)
        {
            for (int i = 0; i < num; i++) 
            {
                CreateCardSlot();
            }
        }

        public void HideCardSlot(CardSlot slot) 
        {
            if (slot.CurrentCard != null)
            { _cardPool.Despawn(slot.CurrentCard); }
            slot.SetDefaultState();
            slot.Changed -= UpdateInfo;
            _slotPool.Despawn(slot);
            _slots.Remove(slot);
        }
        public void HideAllCardSlots()
        {
            int slotsCount = _slots.Count;
            for (int i = 0; i< slotsCount; i++)
            {
                HideCardSlot(_slots.First());
            }
        }
    }
}