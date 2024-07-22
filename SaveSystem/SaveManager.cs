using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace SaveSystem
{
    public class SaveManager : MonoBehaviour
    {
        public static SaveManager instance;
        [SerializeField] private string fileName;
        private GameData gameData;
        private List<ISaveManager> saveManagers;

        private FileDataHandler dataHandler;
        
        [ContextMenu("Delete save file")]
        private void DeleteSaveFile()
        {
            dataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
            dataHandler.Delete();
        }
        
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
            dataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
            saveManagers = FindAllSaveManagers();
            LoadGame();
        }

        public void NewGame()
        {
            gameData = new GameData();
        }

        public void LoadGame()
        {
            gameData = dataHandler.Load();
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
            dataHandler.Save(gameData);
        }

        private void OnApplicationQuit()
        {
            SaveGame();
        }

        private List<ISaveManager> FindAllSaveManagers()
        {
            IEnumerable<ISaveManager> _saveManagers = FindObjectsOfType<MonoBehaviour>().OfType<ISaveManager>();
            return new List<ISaveManager>(_saveManagers);
        }
        
        
    }
}