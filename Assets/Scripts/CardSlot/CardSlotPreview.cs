using CollectionCardGame.Gameplay;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace CollectionCardGame.UI
{
    public class CardSlotPreview : MonoBehaviour
    {
        public float alpha;
        public float duration;
        private SpriteRenderer _spriteRenderer;

        private void Awake()
        {
            if (_spriteRenderer == null) 
            {_spriteRenderer = GetComponent<SpriteRenderer>();}
        }
        public void ShowSetCardPreview(Card card)
        {
            _spriteRenderer.sprite = card.CardConfiguration.sprite;
            CardPreviewAnimation(alpha);
        }

        public void ShowSwapCardPreview(Card card)
        {
            //Сделать стрелочки по кругу, которые показывают, что мы меняем карты
            _spriteRenderer.sprite = card.CardConfiguration.sprite;
            CardPreviewAnimation(alpha);
        }

        public void CardPreviewAnimation(float alpha)
        {
            _spriteRenderer.DOFade(alpha, duration);
        }
    }
}
