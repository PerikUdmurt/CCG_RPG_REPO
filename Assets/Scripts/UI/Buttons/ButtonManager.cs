using CollectionCardGame.Gameplay;
using System.Collections.Generic;
using UnityEngine;

namespace CollectionCardGame.UI
{
    public class ButtonManager
    {
        private MyButton.Pool _buttonPool;
        private RectTransform _buttonEntryPos;
        private RectTransform _currentButtonEntryPos;
        private List<MyButton> _buttons = new List<MyButton>();
        
        public ButtonManager(MyButton.Pool buttonPool, RectTransform buttonEntryPos, CardReciever cardReciever)
        {
            _buttonPool = buttonPool;
            _buttonEntryPos = buttonEntryPos;
            _currentButtonEntryPos = buttonEntryPos;
        }

        public void CreateButton(string buttonText)
        {
            MyButton currentButton = _buttonPool.Spawn();

            currentButton.transform.SetParent(_currentButtonEntryPos.transform, false);
            _currentButtonEntryPos = currentButton.rectTransform;

            currentButton.buttonText.text = buttonText;

            _buttons.Add(currentButton);
        }

        public void DeleteButton()
        {
            foreach (var hint in _buttons)
            {
                _buttonPool.Despawn(hint);
            }
            _buttons.Clear();
            _currentButtonEntryPos = _buttonEntryPos;
        }

        private void Click()
        {

        }
    }
}
