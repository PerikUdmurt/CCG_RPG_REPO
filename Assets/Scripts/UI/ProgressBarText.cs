using UnityEngine.UI;
using UnityEngine;
using TMPro;

namespace CollectionCardGame.UI
{
    public class ProgressBarText : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text;
        private Image _image;

        private void Awake()
        {
            _image = GetComponent<Image>();
        }

        private void Update()
        {
            int result = (int)(_image.fillAmount * 100);
            _text.text = $"Loading {result}%";
        }
    }
}
