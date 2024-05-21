using System;
using System.Collections.Generic;
using UnityEngine;
using CollectionCardGame.Configurations;
using Zenject;
using CollectionCardGame.UI;

namespace CollectionCardGame.Gameplay
{
    [RequireComponent(typeof(Transform))]
    [RequireComponent(typeof(SpriteRenderer))]
    public class Card : MonoBehaviour, IDragable, ISelectable, IUsable
    {
        #region InterfaceRealisation
        public Action Used {  get;  set; }
        public Action Taken { get; set; }
        public Action Dropped { get; set; }
        public Action Selected { get; set; }
        public Action Deselected { get; set; }
        public Transform tf {  get; set; }
        #endregion

        private SpriteRenderer _spriteRenderer;
        [SerializeField] private CardConfig _cardConfiguration;

        public CardConfig CardConfiguration
        {
            get { return _cardConfiguration; }
            set
            {
                _cardConfiguration = value;
                UpdateInfo(value);
            }
        }
        private StackOfCard _stack;
        public StackOfCard Stack
        {
            get { return _stack; }
            set { _stack = value; }
        }

        [HideInInspector] public string cardName;
        [HideInInspector] public string description;

        private int _value;
        public int Value
        {
            get { return _value; }
            set 
            { 
                _value = value;
                if (_value < 0) _value = 0;
            }
        }

        private CardConfig.CardType _type;
        public CardConfig.CardType Type
        {
            get { return _type; }
            private set { _type = value; }
        }
        
        private List<CardEffect> _cardEffects;
        public List<CardEffect> Effects
        { 
            get { return _cardEffects; } 
            set { _cardEffects = value; }
        }

        private void Awake()
        {
            tf = GetComponent<Transform>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            if (_cardConfiguration != null)
            { UpdateInfo(_cardConfiguration); }
        }

        public void ReturnToStack()
        {
            if (Stack != null)
            {
                this.transform.position = new Vector3(Stack.transform.position.x, Stack.transform.position.y, Stack.transform.position.z - 1);
            }
        }

        private void UpdateInfo(CardConfig config)
        {
            cardName = config.name;
            description = config.CardDescription;
            _value = config.ValueOfCard;
            _type = config.Type;
            _cardEffects = config._effects;
            _spriteRenderer.sprite = config.sprite;
        }

        public void AddEffect()
        {

        }

        public class Pool : MemoryPool<Card> 
        {
            protected override void OnCreated(Card item)
            {
                base.OnCreated(item);
                item.gameObject.SetActive(false);
            }
            protected override void OnSpawned(Card item)
            {
                base.OnSpawned(item);
                item.gameObject.SetActive(true);
            }
            protected override void OnDespawned(Card item)
            {
                base.OnDespawned(item);
                item.gameObject.SetActive(false);
            }
        };
    }

    public interface IDragable
    {
        public Transform tf { get; set; }
        public Action Taken {  get; set; }
        public Action Dropped {  get; set; }
    }

    public interface ISelectable
    {
        public Action Selected { get; set; }
        public Action Deselected { get; set; }
    }

    public interface IUsable
    {
        public Action Used { get; set; }
    }

    
}
