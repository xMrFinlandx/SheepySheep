using System;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.UI
{
    public class PopupController : MonoBehaviour
    {
        [SerializeField] private CanvasGroupController _canvasGroupController;
        [Space]
        [SerializeField] private Button _okButton;
        [SerializeField] private Button _cancelButton;
        public void Show(Action onOk, Action onCancel, Action callback)
        {
            _canvasGroupController.gameObject.SetActive(true);
            _canvasGroupController.Show();
            
            _okButton.onClick.AddListener(() =>
            {
                onOk?.Invoke();
                callback?.Invoke();
                Hide();
            });
            _cancelButton.onClick.AddListener(() =>
            {
                onCancel?.Invoke();
                callback?.Invoke();
                Hide();
            });
        }

        public void Hide()
        {
            _okButton.onClick.RemoveAllListeners();
            _cancelButton.onClick.RemoveAllListeners();
            
            _canvasGroupController.Hide();
            _canvasGroupController.gameObject.SetActive(false);
        }
    }
}