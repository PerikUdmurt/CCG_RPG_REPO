using CollectionCardGame.Gameplay;
using System.Collections.Generic;
using UnityEngine;

namespace CollectionCardGame.Infrastructure
{
    public class Combinator
    {
        [SerializeField] List<Combination> combinations = new List<Combination>();
        private CardReciever _cardReciever;

        public Combinator(CardReciever cardReciever) 
        {
            _cardReciever = cardReciever;
        }


    }
}
