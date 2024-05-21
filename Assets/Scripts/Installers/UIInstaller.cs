using UnityEngine;
using Zenject;
using CollectionCardGame.UI;
using CollectionCardGame.Infrastructure;

namespace CollectionCardGame.Infrastructure
{
    public class UIInstaller : MonoInstaller
    {
        [SerializeField] private RectTransform _HintsEntryPos;
        [SerializeField] private HintUI _hintPrefab;

        public override void InstallBindings()
        {
            BindPool();
            BindHints();
        }

        private void BindPool() 
        {
            Container.BindMemoryPool<HintUI, HintUI.Pool>()
                .WithInitialSize(2)
                .FromComponentInNewPrefab(_hintPrefab);
        }


        private void BindHints()
        {
            Container.Bind<HintManager>().AsSingle().NonLazy();
            Container.Bind<RectTransform>().FromInstance(_HintsEntryPos).WhenInjectedInto<HintManager>();
        }
    }
}