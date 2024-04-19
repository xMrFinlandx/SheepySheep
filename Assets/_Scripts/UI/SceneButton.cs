using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.UI
{
    public class SceneButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private TextMeshProUGUI _textMesh;

        private int _buttonID; 
        
        private Action<int> _onClickAction;

        public void Init(int id, bool isEnabled, Action<int> onClickAction)
        {
            _buttonID = id;
            _button.interactable = isEnabled;
            
            _textMesh.text = $"{id + 1}";
            
            if (!isEnabled)
                return;
            
            _onClickAction = onClickAction;
            _button.onClick.AddListener(OnClick);
        }

        private void OnClick()
        {
            _onClickAction?.Invoke(_buttonID);
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveAllListeners();
        }
    }
}