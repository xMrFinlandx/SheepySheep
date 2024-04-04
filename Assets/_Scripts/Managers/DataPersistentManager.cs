using System.Collections.Generic;
using _Scripts.Utilities;
using _Scripts.Utilities.Classes;
using _Scripts.Utilities.Interfaces;
using YG;

namespace _Scripts.Managers
{
    public static class DataPersistentManager
    {
        private static IEnumerable<IDataPersistence> _persistentObjects;
        
        public static void LoadData()
        {
            YandexGame.LoadProgress();
            
            _persistentObjects = Extensions.FindObjectsByInterface<IDataPersistence>();

            foreach (var persistentObject in _persistentObjects)
            {
                persistentObject.LoadData();
            }
        }

        public static void SaveData()
        {
            foreach (var persistentObject in _persistentObjects)
            {
                persistentObject.SaveData();
            }
            
            YandexGame.SaveProgress();
        }
    }
}