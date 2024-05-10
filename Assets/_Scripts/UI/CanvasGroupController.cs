using System;
using System.Collections;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;

namespace _Scripts.UI
{
    [RequireComponent(typeof(CanvasGroup))]
    public class CanvasGroupController : MonoBehaviour
    {
        [SerializeField] private bool _interactable = false;
        [Space] 
        [SerializeField, Range(0, 1)] private float _endAlpha = 0;
        [SerializeField, Range(0, 1)] private float _startAlpha = 1;
        [Space] 
        [SerializeField] private float _fadeInTime = .1f;
        [SerializeField] private float _fadeOutTime = 1;
        [SerializeField] private float _hangTime = 3;

        [SerializeField, HideInInspector] private CanvasGroup _canvasGroup;

        private TweenerCore<float, float, FloatOptions> _tweener;

        public Action<bool> ControllerIsFreeAction;

        public void Show(bool autoDisable = false)
        {
            enabled = true;

            if (_interactable)
            {
                _canvasGroup.interactable = true;
                _canvasGroup.blocksRaycasts = true;
            }

            ControllerIsFreeAction?.Invoke(false);

            _tweener.Kill();
            _tweener = _canvasGroup.DOFade(_startAlpha, _fadeInTime).OnComplete(() =>
            {
                if (autoDisable)
                    StartCoroutine(HangBeforeHide());
                
            }).SetLink(gameObject);
        }

        public void SetInteractionState(bool canInteract)
        {
            _canvasGroup.interactable = canInteract;
            _canvasGroup.blocksRaycasts = canInteract;
        }

        public void Hide()
        {
            _tweener.Kill();
            _tweener = _canvasGroup.DOFade(_endAlpha, _fadeOutTime)
                .OnComplete(() => ControllerIsFreeAction?.Invoke(true)).SetLink(gameObject);

            _canvasGroup.interactable = false;
            _canvasGroup.blocksRaycasts = false;
            enabled = false;
        }

        public void InstantHide()
        {
            ControllerIsFreeAction?.Invoke(true);

            _canvasGroup.alpha = _endAlpha;
            _canvasGroup.interactable = false;
            _canvasGroup.blocksRaycasts = false;
            _tweener.Kill();

            enabled = false;
        }

        private IEnumerator HangBeforeHide()
        {
            yield return new WaitForSeconds(_hangTime);
            Hide();
        }

        private void OnValidate()
        {
            _canvasGroup = GetComponent<CanvasGroup>();

            _canvasGroup.alpha = _endAlpha;
            _canvasGroup.interactable = _interactable;
            _canvasGroup.blocksRaycasts = _interactable;
        }
    }
}