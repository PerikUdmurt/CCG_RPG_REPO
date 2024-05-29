using System;
using UnityEngine;
using UnityEngine.UI;

namespace CollectionCardGame.UI
{
    public class ProgressBarUI : MonoBehaviour
    {
        public event Action Changed;

        [SerializeField] private float _animDuration;
        private float _value;
        private Image _image;

        private void Awake()
        {
            _image = GetComponent<Image>();
        }

        public float Value
        {
            get { return _value; }
            set
            {
                _value = value;
                Changed?.Invoke();
            }
        }

        private void Update()
        {
            float currentValue = _image.fillAmount;
            _image.fillAmount = Mathf.Lerp(currentValue, _value, _animDuration);

        }
    }
}
