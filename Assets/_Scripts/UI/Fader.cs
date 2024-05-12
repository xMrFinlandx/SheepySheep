using System;
using System.Collections;
using _Scripts.Utilities;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using YG;

namespace _Scripts.UI
{
    public class Fader : Singleton<Fader>
    {
        [SerializeField] private Image _image;
        [Space]
        [SerializeField] private float _adCloseTime = 7f;
        [SerializeField] private float _showTime = .3f;
        [SerializeField] private float _hideTime = 1f;

        private bool _isShowed = false;
        
        private Coroutine _coroutine;

        public void Show(Action onCompleteAction = null) => Show(onCompleteAction, 0);
        
        public void Show(Action onCompleteAction, float delay) => Show(onCompleteAction, null, delay);
        
        public void Show(Action onCompleteAction, Func<bool> hideCondition, float delay = 0)
        {
            _isShowed = true;
            _image.DOFade(1f, _showTime)
                .OnComplete(() =>
                {
                    DOVirtual.DelayedCall(delay, () =>
                    {
                        onCompleteAction?.Invoke();

                        if (hideCondition != null && hideCondition.Invoke())
                            Hide();
                    });
                });
        }
        
        private void Start()
        {
            YandexGame.onAdNotification += OnAdNotification;
            YandexGame.OpenFullAdEvent += OnOpenAd;
            
            Hide();
        }
        
        private void Hide()
        {
            _isShowed = false;
            _image.DOFade(0f, _hideTime);
        }

        private void OnOpenAd()
        {
            if (_coroutine == null) 
                return;
            
            StopCoroutine(_coroutine);
            Hide();
        }

        private void OnAdNotification()
        {
            if (!_isShowed)
            {
                Show(CallCloseCoroutine);
            }
            else
            {
                CallCloseCoroutine();
            }
        }

        private void CallCloseCoroutine()
        {
            YandexGame.OpenFullAdEvent?.Invoke();
            _coroutine = StartCoroutine(CloseAdNotification());
        }

        private IEnumerator CloseAdNotification()
        {
            yield return new WaitForSeconds(_adCloseTime);
            Hide();
            YandexGame.CloseFullAdEvent?.Invoke();
        }

        private void OnDestroy()
        {
            YandexGame.onAdNotification -= OnAdNotification;
            YandexGame.OpenFullAdEvent -= OnOpenAd;
        }
    }
}