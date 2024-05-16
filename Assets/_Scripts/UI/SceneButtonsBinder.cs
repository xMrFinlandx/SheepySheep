using System;
using _Scripts.Scriptables.UI;
using _Scripts.Utilities;
using UnityEngine;
using UnityEngine.UI;
using YG;

namespace _Scripts.UI
{
    public class SceneButtonsBinder : MonoBehaviour
    {
        [SerializeField] private GridLayoutGroup _grid;
        [SerializeField] private SceneButton _prefab;
        [Space]
        [SerializeField] private ScenesHandler _scenesHandler;

        public Action<string> LevelSelectedAction;
        
        public void CreateButtons(ScenesHandler scenesHandler)
        {
            _grid.transform.KillChildren();
            var scenes = scenesHandler.Scenes;
            
            for (int buttonId = 0; buttonId < scenes.Count; buttonId++)
            {
                var button = Instantiate(_prefab, _grid.transform);
                var isButtonEnabled = YandexGame.savesData.IsScenePassed(scenes[buttonId]) || scenes[buttonId] == YandexGame.savesData.NextScene;

                button.Init(buttonId, isButtonEnabled, OnButtonClick, $"{buttonId + 1}");
            }
        }

        private void OnButtonClick(int id)
        {
            LevelSelectedAction?.Invoke(_scenesHandler.Scenes[id]);
        }

        private void OnEnable()
        {
            CreateButtons(_scenesHandler);
        }
    }
}