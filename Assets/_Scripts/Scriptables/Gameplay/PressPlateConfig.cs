using UnityEngine;

namespace _Scripts.Scriptables.Gameplay
{
    [CreateAssetMenu(fileName = "New Press Plate Config", menuName = "Gameplay/Press Plate Config", order = 0)]
    public class PressPlateConfig : InteractableConfig
    {
        [SerializeField, Range(0, 10)] private int _callId;
        
        public int CallId => _callId;
    }
}