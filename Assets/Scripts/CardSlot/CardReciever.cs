using CollectionCardGame.Infrastructure;
using System.Collections.Generic;
using UnityEngine;

namespace CollectionCardGame.Gameplay
{
    public class CardReciever
    {
        private int _maxCardSlots;
        private CardSlot.Pool _slotPool;
        private Vector3 _entryPosition;
        private List<CardSlot> _unfilledSlots = new List<CardSlot>();
        private List<CardSlot> _filledSlots = new List<CardSlot>();

        public CardReciever(CardSlot.Pool slotPool, Vector3 entryPosition)
        {
            _slotPool = slotPool;
            _entryPosition = entryPosition;
        }
        
        public void CreateCardSlot()
        {
            CardSlot currentSlot = _slotPool.Spawn();
            _unfilledSlots.Add(currentSlot);
            currentSlot.transform.position = _entryPosition + new Vector3((_unfilledSlots.Count + _filledSlots.Count) * 2, 0, 0);
        }

        public void CreateCardSlots(int num)
        {
            for (int i = 0; i < num; i++) 
            {
                CreateCardSlot();
            }
        }

        public void HideAllCardSlots()
        {
            foreach (CardSlot slot in _unfilledSlots)
            {
                _slotPool.Despawn(slot);
            }
            _unfilledSlots.Clear();

            foreach (CardSlot slot in _filledSlots)
            {
                _slotPool.Despawn(slot);
            }
            _filledSlots.Clear();
        }
        
        public List<CardSlot> ReadSlots()
        {
            return _filledSlots;
        }
        
    }
}