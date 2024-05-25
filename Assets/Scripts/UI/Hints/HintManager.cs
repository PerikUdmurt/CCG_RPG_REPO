using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace CollectionCardGame.UI
{
    public class HintManager
    {
        private RectTransform _hintEntryPos;
        private HintUI.Pool _hintPool;
        private List<HintUI> _hintList = new List<HintUI>();

        private RectTransform _currentHintsEntryPoint;

        public HintManager(RectTransform hintsEntryPos, HintUI.Pool hintPool) 
        { 
            _hintEntryPos = hintsEntryPos;
            _currentHintsEntryPoint = _hintEntryPos;
            _hintPool = hintPool;
        }

        public void CreateHint(string name, string hintText, Color color) 
        {
            HintUI currentHint = _hintPool.Spawn();
            
            currentHint.transform.SetParent(_currentHintsEntryPoint.transform, false);
            _currentHintsEntryPoint = currentHint.rectTransform;

            currentHint.hintName.text = name;
            currentHint.description.text = hintText;

            _hintList.Add(currentHint);
        }

        public void DeleteHint() 
        { 
            foreach (var hint in _hintList)
            {
                _hintPool.Despawn(hint);
            }
            _hintList.Clear();
            _currentHintsEntryPoint = _hintEntryPos;
        }
    }
}
