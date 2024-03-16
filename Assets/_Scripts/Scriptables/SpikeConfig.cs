using UnityEngine;

namespace _Scripts.Scriptables
{
    [CreateAssetMenu(fileName = "New Spike Config", menuName = "Gameplay/Spike Config", order = 0)]
    public class SpikeConfig : InteractableConfig
    {
        [SerializeField, Range(0, 10)] private int _triggerId;
        
        public int TriggerId => _triggerId;
    }
}