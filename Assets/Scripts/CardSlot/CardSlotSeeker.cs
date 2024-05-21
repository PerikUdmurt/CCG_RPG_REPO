using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

namespace CollectionCardGame.Gameplay
{
    [RequireComponent(typeof(Card))]
    public class CardSlotSeeker: MonoBehaviour
    {
        #region Fields
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
        }

        private void OnDisable()
        {
            _card.Dropped -= CheckCardSlot;
        }

        private void Update()
        {
            if (!_card.inCardSlot&&_slots.Count > 0) 
            { 
                _nearestSlot = FindNearestSlot(_slots); 
            }
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