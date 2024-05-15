using UnityEngine;
using YG;

namespace _Scripts.Managers
{
    public class PhoneControls : MonoBehaviour
    {
        private void OnEnable()
        {
            gameObject.SetActive(!YandexGame.EnvironmentData.isDesktop);
        }
    }
}