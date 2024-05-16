using System;
using _Scripts.Player.Controls;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using YG;

namespace _Scripts.Managers
{
    public class PhoneControls : MonoBehaviour
    {
        [SerializeField] private InputReader _inputReader;
        [SerializeField] private float _duration = .3f;
        [Space]
        [SerializeField] private Image[] _addButtonImages;
        [SerializeField] private Image[] _removeButtonImages;
        [Space] 
        [SerializeField] private Color[] _disabledColors;
        [SerializeField] private Color[] _addButtonColors;
        [SerializeField] private Color[] _removeButtonColors;

        private bool _previousState;
         
        private void OnEnable()
        {
            gameObject.SetActive(!YandexGame.EnvironmentData.isDesktop);
            
            _inputReader.SetAdditive();
            OnSetAdditiveMode(true);
        }

        private void Start()
        {
            _inputReader.SetAdditiveModeEvent += OnSetAdditiveMode;
        }

        private void OnSetAdditiveMode(bool isAdditive)
        {
            if (_previousState == isAdditive)
                return;
            
            _previousState = isAdditive;

            if (!isAdditive)
                ChangeColors(_addButtonImages, _removeButtonImages, _addButtonColors);
            else
                ChangeColors(_removeButtonImages, _addButtonImages, _removeButtonColors);
        }

        private void ChangeColors(Image[] toEnable, Image[] toDisable, Color[] enabledColors)
        {
            for (var i = 0; i < toEnable.Length; i++)
            {
                var j = i;
                toEnable[i].DOColor(Color.white, .1f).OnComplete(() => toEnable[j].DOColor(enabledColors[j], _duration)).SetLink(gameObject);
                toDisable[i].DOColor(_disabledColors[j], _duration).SetLink(gameObject);
            }
        }

        private void OnDisable()
        {
            _previousState = false;
        }

        private void OnDestroy()
        {
            _inputReader.SetAdditiveModeEvent -= OnSetAdditiveMode;
        }
    }
}