using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;

namespace CollectionCardGame.UI
{
    public class MyButtonAnimation : MonoBehaviour
    {
        private MyButton _myButton;
        private Image _Image;
        private int _PropertyID;
        private Tween _standartTween;
        private Tween _cancelTween;
        private Tween _unclickTween;

        private void Awake()
        {
            _myButton = GetComponent<MyButton>();
            _Image = GetComponent<Image>();
            _PropertyID = Shader.PropertyToID("_FullGlowDissolveFade");
            _standartTween = _Image.material.DOFloat(0, _PropertyID, _myButton.longPressTime/1000f);
            _cancelTween = _Image.material.DOFloat(1, _PropertyID, _myButton.longPressTime/1000f);
            _unclickTween = _Image.material.DOFloat(1, _PropertyID, _myButton.longPressTime / 1000f);
        }

        private void OnEnable()
        {
            _myButton.clicked += PlayClickAnimation;
            _myButton.longPressedCancelled += PlayCancelAnimation;
            _myButton.unclicked += PlayCancelAnimation;
        }

        private void PlayClickAnimation()
        {
            _Image.material.DOPause();
            _standartTween = _Image.material.DOFloat(0, _PropertyID, _myButton.longPressTime / 1000f);
        }

        private void PlayCancelAnimation()
        {
            _Image.material.DOPause();
            _cancelTween = _Image.material.DOFloat(1, _PropertyID, _myButton.longPressTime / 1000f);
        }
    }
}
