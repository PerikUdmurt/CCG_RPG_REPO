using UnityEngine;
using Zenject;
using CollectionCardGame.UI;

namespace CollectionCardGame.Infrastructure
{
    public class UIInstaller : MonoInstaller
    {
        [SerializeField] private RectTransform _ButtonEntryPos;
        [SerializeField] private MyButton _myButtonPrefab;
        [SerializeField] private RectTransform _HintsEntryPos;
        [SerializeField] private HintUI _hintPrefab;


        public override void InstallBindings()
        {
            BindPools();
            BindHints();
            BindButtons();
        }

        private void BindPools() 
        {
            Container.BindMemoryPool<HintUI, HintUI.Pool>()
                .WithInitialSize(2)
                .FromComponentInNewPrefab(_hintPrefab);

            Container.BindMemoryPool<MyButton, MyButton.Pool>()
                .WithInitialSize(2)
                .FromComponentInNewPrefab(_myButtonPrefab);
        }


        private void BindHints()
        {
            Container.Bind<HintManager>().AsSingle().NonLazy();
            Container.Bind<RectTransform>().FromInstance(_HintsEntryPos).WhenInjectedInto<HintManager>();
        }

        private void BindButtons()
        {
            Container.Bind<RectTransform>().FromInstance(_ButtonEntryPos).WhenInjectedInto<ButtonManager>();
            Container.Bind<MenuButtonModel>().AsSingle().NonLazy();
        }
    }
}