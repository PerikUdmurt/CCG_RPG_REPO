using CollectionCardGame.Infrastructure;
using UnityEngine;
using Zenject;

namespace CollectionCardGame.Gameplay
{
    public class Test : MonoBehaviour
    {
        private CardCreator _cardCreator;
        private CardReciever _reciever;
        [Inject]
        private void Construct(CardCreator cardCreator, CardReciever cardReciever)
        {
            _cardCreator = cardCreator;
            _reciever = cardReciever;
        }

        public CardConfig cardConfig1;
        public int numOfCard1;
        public CardConfig cardConfig2;
        public int numOfCard2;
        public CardConfig cardConfig3;
        public int numOfCard3;
        public CardConfig cardConfig4;
        public int numOfCard4;
        public CardConfig cardConfig5;
        public int numOfCard5;
        public CardConfig cardConfig6;
        public int numOfCard6;

        private void Create(CardConfig cardConfig, int numOfCard)
        {
            for(int i = 0; i < numOfCard; i++)
            {
                _cardCreator.SpawnByConfig(cardConfig);
            }
        }

        private void CreateAllTypeCard()
        {
            Create(cardConfig1, numOfCard1);
            Create(cardConfig2, numOfCard2);
            Create(cardConfig3, numOfCard3);
            Create(cardConfig4, numOfCard4);
            Create(cardConfig5, numOfCard5);
            Create(cardConfig6, numOfCard6);
        }

        private void ShowSlot()
        {
            _reciever.CreateCardSlots(2);
        }

        private void HideSlot()
        {
            _reciever.HideAllCardSlots();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.W)) 
            {
                CreateAllTypeCard();
            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                ShowSlot();
            }

            if (Input.GetKeyDown(KeyCode.D))
            {
                HideSlot();
            }
        }
    }
}
