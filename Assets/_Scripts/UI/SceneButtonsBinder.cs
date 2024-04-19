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
            
            for (int i = 0; i < scenes.Count; i++)
            {
                var button = Instantiate(_prefab, _grid.transform);
                var isButtonEnabled = YandexGame.savesData.IsScenePassed(scenes[i]) || scenes[i] == YandexGame.savesData.NextScene;
                
                button.Init(i, isButtonEnabled, OnButtonClick);
            }
        }

        private void OnButtonClick(int id)
        {
            LevelSelectedAction?.Invoke(_scenesHandler.Scenes[id]);
        }

        private void Start()
        {
            CreateButtons(_scenesHandler);
        }
    }
}