using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;

namespace CollectionCardGame.Gameplay
{
    [RequireComponent(typeof(Card))]
    public class CardSlotSeeker: MonoBehaviour
    {
        #region Fields
        public Action<CardSlot> SlotChanged;

        private Card _card;

        private HashSet<CardSlot> _slots = new HashSet<CardSlot>();
        private CardSlot _nearestSlot;
        
        #endregion


        #region Methods
        private void Awake()
        {
            _card = GetComponent<Card>();
        }

        private void OnEnable()
        {
            _card.Dropped += CheckCardSlot;
            SlotChanged += SwitchPrevew;
        }

        private void OnDisable()
        {
            _card.Dropped -= CheckCardSlot;
        }

        private void Update()
        {
            if (!_card.inCardSlot) 
            { 
                _nearestSlot = FindNearestSlot(_slots);
            }
        }

        private void SwitchPrevew(CardSlot newSlot)
        {
            if (_nearestSlot != null) { HidePreview(_nearestSlot); }
            if (newSlot != null) { ShowPreview(newSlot); }
        }

        private void ShowPreview(CardSlot slot)
        {
             slot.preview.ShowSetCardPreview(_card);
        }

        private void HidePreview(CardSlot slot)
        {
            slot.preview.CardPreviewAnimation(0);
        }


        private void CheckCardSlot()
        {
            if (_slots.Count == 0) { _card.ReturnToStack(); return; }
            if (_nearestSlot.CurrentCard==null) { _nearestSlot.TakeCard(_card); }
            else {  _nearestSlot.SwapCard(_card);}
        }

        private CardSlot FindNearestSlot(HashSet<CardSlot> slots)
        {
            CardSlot nearestSlot = null;
            float minDistance = float.MaxValue;
            for (int i = 0; i < slots.Count; i++)
            {
                float currentDistance = (this.transform.position - slots.ElementAt(i).transform.position).magnitude;
                if (currentDistance < minDistance) { minDistance = currentDistance; nearestSlot = slots.ElementAt(i); }
            }
            if (nearestSlot != _nearestSlot) { SlotChanged?.Invoke(nearestSlot); }
            return nearestSlot;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.attachedRigidbody.TryGetComponent(out CardSlot cardSlot))
            {
                _slots.Add(cardSlot);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.attachedRigidbody.TryGetComponent(out CardSlot cardSlot))
            {
                _slots.Remove(cardSlot);
            }
        }
        #endregion
    }
}