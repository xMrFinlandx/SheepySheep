using UnityEngine;

namespace _Scripts.Scriptables
{
    [CreateAssetMenu(fileName = "New Plate Config", menuName = "Gameplay/Plate Config", order = 0)]
    public class PlateConfig : InteractableConfig
    {
        [SerializeField, Range(0, 10)] private int _callId;
        
        public int CallId => _callId;
    }
}