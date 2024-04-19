using System.Collections.Generic;

namespace YG
{
    [System.Serializable]
    public class SavesYG
    {
        #region Technical Saves

        // "Технические сохранения" для работы плагина (Не удалять)
        public int idSave;
        public bool isFirstSession = true;
        public string language = "ru";
        public bool promptDone;

        #endregion

        public string NextScene = "_SheepyA1";
        public int Coins = 0;

        public Dictionary<string, bool> PassedScenes = new();

        public Dictionary<string, bool> Collectables = new();
        
        public SavesYG()
        {
        }

        public void MakeScenePassed(string sceneName) => AddDataToDictionary(PassedScenes, sceneName, true);

        public void AddCollectable(string guid, bool value) => AddDataToDictionary(Collectables, guid, value);

        public bool IsScenePassed(string sceneName) => PassedScenes.ContainsKey(sceneName);
        
        public void TrySetNextScene(string sceneName)
        {
            if (IsScenePassed(sceneName))
                return;

            NextScene = sceneName;
        }
        
        public bool IsCollectableEnabled(string guid)
        {
            Collectables.TryGetValue(guid, out var value);
            return value;
        }

        private static void AddDataToDictionary(IDictionary<string, bool> dictionary, string guid, bool value)
        {
            if (dictionary.ContainsKey(guid))
                dictionary.Remove(guid);

            dictionary.Add(guid, value);
        }
    }
}
