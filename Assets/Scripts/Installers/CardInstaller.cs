using UnityEngine;
using Zenject;
using CollectionCardGame.Gameplay;

namespace CollectionCardGame.Infrastructure
{
    public class CardInstaller : MonoInstaller
    {
        [SerializeField] private CardSlot _slotPrefab;
        [SerializeField] private Card _cardPrefab;
        [SerializeField] private HandController _handController;

        [SerializeField] private StackOfCard _healthStackOfCard;
        [SerializeField] private StackOfCard _strengthStackOfCard;
        [SerializeField] private StackOfCard _agilityStackOfCard;
        [SerializeField] private StackOfCard _intellectStackOfCard;
        [SerializeField] private StackOfCard _charismaStackOfCard;
        [SerializeField] private StackOfCard _itemStackOfCard;

        [SerializeField] private Vector3 _cardRecieverStartPoint;
        
        public override void InstallBindings()
        {            
            Container.Bind<CardCreator>().AsSingle().NonLazy();

            BindHand();
            BindStackOfCard();
            BindCardReciever();
            Bindpools();
        }

        private void BindStackOfCard()
        {
            Container.Bind<StackOfCard>().WithId("HealthStack").FromInstance(_healthStackOfCard);
            Container.Bind<StackOfCard>().WithId("StrengthStack").FromInstance(_strengthStackOfCard);
            Container.Bind<StackOfCard>().WithId("AgilityStack").FromInstance(_agilityStackOfCard);
            Container.Bind<StackOfCard>().WithId("IntellectStack").FromInstance(_intellectStackOfCard);
            Container.Bind<StackOfCard>().WithId("CharismaStack").FromInstance(_charismaStackOfCard);
            Container.Bind<StackOfCard>().WithId("ItemStack").FromInstance(_itemStackOfCard);
        }

        private void Bindpools()
        {
            Container.BindMemoryPool<Card, Card.Pool>()
                .FromComponentInNewPrefab(_cardPrefab);

            Container.BindMemoryPool<CardSlot,CardSlot.Pool>()
                .FromComponentInNewPrefab(_slotPrefab);
        }

        private void BindCardReciever()
        {
            Container.Bind<Vector3>().FromInstance(_cardRecieverStartPoint).WhenInjectedInto<CardReciever>();
            Container.Bind<CardReciever>().AsSingle().NonLazy();
        }

        private void BindHand()
        {
            Container.Bind<HandController>().FromInstance(_handController);
        }
    }
}