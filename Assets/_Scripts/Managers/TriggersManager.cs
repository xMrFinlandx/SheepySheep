using System;
using System.Collections.Generic;
using System.Linq;
using _Scripts.Gameplay.Tilemaps.Modifier;
using _Scripts.Utilities.Interfaces;
using UnityEngine;

namespace _Scripts.Managers
{
    public class TriggersManager : MonoBehaviour, IRestartable
    {
        public static Action<int> AllTriggersActivatedAction;

        private readonly Dictionary<int, int> _counter = new();
        private readonly Dictionary<int, int> _triggers = new();

        public void Restart() => ResetProgress();
        
        private void Start()
        {
            Plate.TriggerEnabledAction += UpdateCounter;
            
            InitDictionaries();
        }

        private void OnDisable()
        {
            Plate.TriggerEnabledAction -= UpdateCounter;
        }

        private void UpdateCounter(int index)
        {
            _counter[index]++;
            
            if (_counter[index] != _triggers[index])
                return;
            
            AllTriggersActivatedAction?.Invoke(index);
        }

        private void InitDictionaries()
        {
            var triggers = FindObjectsByType<Plate>(FindObjectsSortMode.None);
            var indexes = triggers.Select(item => item.CallId);
            
            foreach (var triggerIndex in indexes)
            {
                if (!_triggers.TryAdd(triggerIndex, 1))
                {
                    _triggers[triggerIndex]++;
                }
                else
                {
                    _counter.Add(triggerIndex, 0);
                }
            }
        }
        
        private void ResetProgress()
        {
            for (var index = 0; index < _counter.Count; index++)
            {
                _counter[index] = 0;
            }
        }
    }
}