using System;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using Zenject;
using UnityEngine.Events;
using System.Threading.Tasks;
using System.Threading;
using Unity.VisualScripting.Antlr3.Runtime;

namespace CollectionCardGame.UI
{
    public class MyButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        public TextMeshProUGUI buttonText;
        public bool isInteractable = true;
        public int longPressTime;
        public RectTransform rectTransform;

        public event Action clicked;
        public event Action unclicked;
        public event Action longPressed;
        public event Action longPressedCancelled;

        private CancellationTokenSource _cts = new CancellationTokenSource();
        private CancellationToken _token;
        private bool _pressed;

        public void OnPointerDown(PointerEventData eventData)
        {
            
            if (!isInteractable) return;
            clicked?.Invoke();
            _cts.Dispose();
            _cts = new CancellationTokenSource();
            _token = _cts.Token;
            _token.Register(() => { CancelLongPress(); });
            ConfirmLongPress();
            _pressed = true;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (!isInteractable) return;
            unclicked?.Invoke();
            _cts.Cancel();
        }

        private async Task<bool> LongPress(CancellationToken token)
        {
            if (!isInteractable) return false;
            await Task.Delay(longPressTime);
            if (token.IsCancellationRequested) return false;
            return true;
        }

        private async void ConfirmLongPress()
        {
            bool isConfirmed = await LongPress(_token);
            if (isConfirmed)
            {
                Debug.Log("LONGPRESSED");
                longPressed?.Invoke();
                _pressed = false;
            }
        }

        private void CancelLongPress()
        {
            if (!_pressed) return;
            Debug.Log("CANCELLED");
            longPressedCancelled?.Invoke();
            _pressed = false;
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
