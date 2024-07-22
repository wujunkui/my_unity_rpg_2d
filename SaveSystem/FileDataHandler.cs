using UnityEngine;
using System;
using System.IO;

namespace SaveSystem
{
    public class FileDataHandler
    {
        private string dataDirPath = "";
        private string dataFileName = "";
        private string fullPath => Path.Combine(dataDirPath, dataFileName);

        public FileDataHandler(string _dataDirPath, string _dataFileName)
        {
            dataDirPath = _dataDirPath;
            dataFileName = _dataFileName;
        }

        public void Save(GameData _data)
        {
            string fullPath = Path.Combine(dataDirPath, dataFileName);
            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(fullPath));
                string dataToStore = JsonUtility.ToJson(_data, true);

                using (FileStream stream = new FileStream(fullPath, FileMode.Create))
                {
                    using (StreamWriter writer = new StreamWriter(stream))
                    {
                        writer.Write(dataToStore);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public GameData Load()
        {
            GameData loadData = null;
            if (File.Exists(fullPath))
            {
                try
                {
                    string dataToLoad = "";
                    using FileStream stream = new FileStream(fullPath, FileMode.Open);
                    using StreamReader reader = new StreamReader(stream);
                    dataToLoad = reader.ReadToEnd();
                    loadData = JsonUtility.FromJson<GameData>(dataToLoad);
                }
                catch (Exception e)
                {
                    Debug.LogError(e);
                    throw;
                }
            }

            return loadData;
        }

        public void Delete()
        {
            if(File.Exists(fullPath))
                File.Delete(fullPath);
        }
    }
}