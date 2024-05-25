using System;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using Zenject;
using UnityEngine.Events;

namespace CollectionCardGame.UI
{
    public class MyButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        public TextMeshProUGUI buttonText;
        public RectTransform rectTransform;

        public UnityEvent<MyButton> clicked;
        public UnityEvent<MyButton> unclicked;

        public void OnPointerDown(PointerEventData eventData)
        {
            clicked?.Invoke(this);
            Debug.Log("Click");
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            unclicked?.Invoke(this);
            Debug.Log("unclicked");
        }

        public class Pool : MemoryPool<MyButton>
        {
            protected override void OnCreated(MyButton item)
            {
                base.OnCreated(item);
                item.gameObject.SetActive(false);
            }
            protected override void OnSpawned(MyButton item)
            {
                base.OnSpawned(item);
                item.gameObject.SetActive(true);
            }
            protected override void OnDespawned(MyButton item)
            {
                base.OnDespawned(item);
                item.gameObject.SetActive(false);
            }
        }
    }
}
