using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CollectionCardGame.Gameplay
{
    public class StackOfCardAnimation : MonoBehaviour
    {
        private StackOfCard _stk;

        private void OnEnable()
        {
            _stk.Selected += ShowCard;
            _stk.Deselected += HideCard;

        }
        private void Awake()
        {
            _stk = GetComponent<StackOfCard>();
        }
        private void ShowCard()
        {
            Debug.Log("ShowCard");
        }

        private void HideCard()
        {
            Debug.Log("HideCard");
        }
    }
}
