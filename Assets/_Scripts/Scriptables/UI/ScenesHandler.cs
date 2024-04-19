using System.Collections.Generic;
using _Scripts.Utilities.Classes;
using UnityEngine;

namespace _Scripts.Scriptables.UI
{
    [CreateAssetMenu(fileName = "New Scenes Handler", menuName = "UI/Scenes Handler", order = 0)]
    public class ScenesHandler : ScriptableObject
    {
        [SerializeField] private SceneField[] _scenes;

        public IReadOnlyList<SceneField> Scenes => _scenes;
    }
}