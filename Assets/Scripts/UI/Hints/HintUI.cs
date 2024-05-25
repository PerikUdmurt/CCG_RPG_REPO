using TMPro;
using UnityEngine;
using Zenject;

namespace CollectionCardGame.UI
{
    public class HintUI : MonoBehaviour
    {
        public TextMeshProUGUI hintName;
        public TextMeshProUGUI description;
        public RectTransform rectTransform;

        private void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
        }

        public class Pool: MemoryPool<HintUI> 
        {
            protected override void OnCreated(HintUI item)
            {
                base.OnCreated(item);
                item.gameObject.SetActive(false);
            }
            protected override void OnSpawned(HintUI item)
            {
                base.OnSpawned(item);
                item.gameObject.SetActive(true);
            }
            protected override void OnDespawned(HintUI item)
            {
                base.OnDespawned(item);
                item.gameObject.SetActive(false);
            }
        }
    }
}
