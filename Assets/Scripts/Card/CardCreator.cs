using UnityEngine;
using CollectionCardGame.Gameplay;
using Zenject;

namespace CollectionCardGame.Infrastructure
{
    public class CardCreator
    {
        [Inject(Id = "HealthStack")]
        private StackOfCard _healthStackOfCard;
        [Inject(Id = "StrengthStack")]
        private StackOfCard _strengthStackOfCard;
        [Inject(Id = "AgilityStack")]
        private StackOfCard _agilityStackOfCard;
        [Inject(Id = "IntellectStack")]
        private StackOfCard _intellectStackOfCard;
        [Inject(Id = "CharismaStack")]
        private StackOfCard _charismaStackOfCard;
        [Inject(Id = "ItemStack")]
        private StackOfCard _itemStackOfCard;

        private Card.Pool _cardPool;

        public CardCreator(Card.Pool pool)
        {
            _cardPool = pool;
        }

        public void SpawnByConfig(CardConfig config)
        {
            Card card = _cardPool.Spawn();
            card.CardConfiguration = config;
            card.Stack = SetStackOfCard(config.Type);
            card.ReturnToStack();
        }

        private StackOfCard SetStackOfCard(CardConfig.CardType type)
        {
            switch (type)
            {
                case CardConfig.CardType.Health: return _healthStackOfCard;
                case CardConfig.CardType.Strenght: return _strengthStackOfCard;
                case CardConfig.CardType.Agility: return _agilityStackOfCard;
                case CardConfig.CardType.Intellect: return _intellectStackOfCard;
                case CardConfig.CardType.Charisma: return _charismaStackOfCard;
                case CardConfig.CardType.Item: return _itemStackOfCard;
            }
            return null;
        }
        
    }
}
