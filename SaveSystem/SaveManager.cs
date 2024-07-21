using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace SaveSystem
{
    public class SaveManager : MonoBehaviour
    {
        private GameData gameData;
        public static SaveManager instance;
        private List<ISaveManager> saveManagers;
        private void Awake()
        {
            // singleton
            if (instance == null)
                instance = this;
            else
            {
                Destroy(instance.gameObject);
            }
        }

        private void Start()
        {
            saveManagers = FinAllSaveManagers();
            LoadGame();
        }

        public void NewGame()
        {
            gameData = new GameData();
        }

        public void LoadGame()
        {
            if (gameData == null)
            {
                NewGame();
            }

            foreach (var saveManager in saveManagers)
            {
                saveManager.LoadData(gameData);
            }
        }

        public void SaveGame()
        {
            foreach (var saveManger in saveManagers)
            {
                saveManger.SaveData(ref gameData);
            }
        }

        private void OnApplicationQuit()
        {
            SaveGame();
        }

        private List<ISaveManager> FinAllSaveManagers()
        {
            IEnumerable<ISaveManager> _saveManagers = FindObjectsOfType<MonoBehaviour>().OfType<ISaveManager>();
            return new List<ISaveManager>(_saveManagers);
        }
    }
}