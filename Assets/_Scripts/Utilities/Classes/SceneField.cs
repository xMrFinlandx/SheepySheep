using System;
using UnityEditor;
using UnityEngine;

namespace _Scripts.Utilities.Classes
{
    [Serializable]
    public class SceneField
    {
        [SerializeField] private SceneAsset _sceneAsset;
        [SerializeField] private string _sceneName;

        public string SceneName => _sceneName;

        public static implicit operator string(SceneField sceneField) => sceneField.SceneName;
    }
}