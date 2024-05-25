using CollectionCardGame.Gameplay;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CollectionCardGame.UI
{
    public class MenuButtonModel
    {
        private CardReciever _cardReciever;
        public MenuButtonModel(CardReciever cardReciever)
        {
            _cardReciever = cardReciever;
            _cardReciever.Updated += ReadCombination;
        }

        private void ReadCombination()
        {
            List<Card> cards = _cardReciever.GetCombination();

            if (cards.ElementAt(0) == null) { HideSolution(); return; }
            var cardType = cards.ElementAt(0).Type;
            switch (cardType)
            {
                case CollectionCardGame.CardConfig.CardType.Health:
                    ShowStartSolution(); break;
                case CollectionCardGame.CardConfig.CardType.Strenght:
                    ShowStartSolution(); break;
                case CollectionCardGame.CardConfig.CardType.Agility:
                    ShowStartSolution(); break;
            }
        }

        private void ShowStartSolution()
        {

        }

        private void HideSolution()
        {
            Debug.Log("HideSolution");
        }

    }
}
