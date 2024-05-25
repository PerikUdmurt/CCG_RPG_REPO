using System;
using System.Collections.Generic;
using UnityEngine;
using CollectionCardGame.Configurations;
using Zenject;
using DG.Tweening;

namespace CollectionCardGame.Gameplay
{
    [RequireComponent(typeof(Transform))]
    [RequireComponent(typeof(SpriteRenderer))]
    public class Card : MonoBehaviour, IDragable, ISelectable, IUsable
    {


        #region Fields
        #region InterfaceRealisation
        public event Action Used;
        public Action Taken { get; set; }
        public Action Dropped { get; set; }
        public Action Selected { get; set; }
        public Action Deselected { get; set; }
        public Transform tf {  get; set; }
        public bool isUsable { get; set; } = true;
        public bool isDragable { get; set; } = true;
        public bool isSelectable { get; set; } = true;
        #endregion

        [HideInInspector] public bool inCardSlot;
        [HideInInspector] public string cardName;
        [HideInInspector] public string description;
        [SerializeField] private float _moveSpeed;

        private SpriteRenderer _spriteRenderer;
        private CardConfig _cardConfiguration;
        private StackOfCard _stack;
        private int _value;
        private CardConfig.CardType _type;
        private List<CardEffect> _cardEffects;
        
        #endregion

        #region Properties

        public CardConfig CardConfiguration
        {
            get { return _cardConfiguration; }
            set
            {
                _cardConfiguration = value;
                UpdateInfo(value);
            }
        }

        public StackOfCard Stack
        {
            get { return _stack; }
            set { _stack = value; }
        }

        public CardConfig.CardType Type
        {
            get { return _type; }
            private set { _type = value; }
        }

        public int Value
        {
            get { return _value; }
            set
            {
                _value = value;
                if (_value < 0) _value = 0;
            }
        }

        public List<CardEffect> Effects
        {
            get { return _cardEffects; }
            set { _cardEffects = value; }
        }
        #endregion

        #region Methods
        private void Awake()
        {
            
            tf = GetComponent<Transform>();
            _spriteRenderer = GetComponent<SpriteRenderer>();

            if (_cardConfiguration != null)
            { UpdateInfo(_cardConfiguration); }

            Used += Use;
        }

        public void MoveTo(Vector3 endPoint)
        {
            DOTween.Sequence().
                Append(transform.DOMove(endPoint, _moveSpeed, false).
                SetEase(Ease.OutQuint));
        }

        public void ReturnToStack()
        {
            if (Stack != null)
            {
                MoveTo(new Vector3(Stack.transform.position.x, Stack.transform.position.y, Stack.transform.position.z - 1));
                Stack.AddToStack(this);
            }
        }

        public void Use()
        {
            Debug.Log("Used");
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
        #endregion

        #region ObjectPool
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
        }
        #endregion
    }

    public interface IDragable
    {
        public Transform tf { get; set; }
        public Action Taken { get; set; }
        public Action Dropped { get; set; }
        public bool isDragable { get; set; }
    }

    public interface ISelectable
    {
        public Action Selected { get; set; }
        public Action Deselected { get; set; }
        public bool isSelectable { get; set; }
    }

    public interface IUsable
    {
        public void Use();
        public event Action Used;
        public bool isUsable { get; set; }
    }

    
}
