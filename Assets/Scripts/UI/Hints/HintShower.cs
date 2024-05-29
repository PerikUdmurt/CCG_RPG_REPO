using CollectionCardGame.Gameplay;
using UnityEngine;
using Zenject;

namespace CollectionCardGame.UI
{
    public class HintShower : MonoBehaviour
    {
        private ISelectable _selectableObj;
        private HintManager _hintManager;

        [SerializeField] private string _name;
        [SerializeField] private string _hintText;
        [SerializeField] private Color _hintColor;

        [Inject]
        private void Construct(HintManager hintManager)
        {
            _hintManager = hintManager;
        }

        private void Awake()
        {
            _selectableObj = GetComponent<ISelectable>();
        }

        private void OnEnable()
        {
            _selectableObj.Selected += ShowHint;
            _selectableObj.Deselected += HideHint;
        }

        private void OnDisable()
        {
            _selectableObj.Selected -= ShowHint;
            _selectableObj.Deselected -= HideHint;
        }

        private void ShowHint()
        {
            _hintManager.CreateHint(_name, _hintText, Color.blue);
        }

        private void HideHint()
        {
            _hintManager.DeleteHint();
        }
    }
}
