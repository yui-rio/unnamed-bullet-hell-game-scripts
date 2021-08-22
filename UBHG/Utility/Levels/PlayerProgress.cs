using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace UBHG.Utility.Levels {
#nullable enable
    [Serializable]
    public class PlayerProgress {
        private const string SAVE_FILE = "save.dat";

        private static readonly string saveFilePath =
            Path.Combine(Path.GetFullPath(Application.persistentDataPath), SAVE_FILE);

        private static readonly PlayerProgress startProgress =
            new PlayerProgress(new List<LevelUtility.Level> {LevelUtility.Level.StartLevel}, null);

        private PlayerProgress(List<LevelUtility.Level> availableLevels, LevelUtility.Level? lastPlayedLevel) =>
            (AvailableLevels, LastPlayedLevel) = (availableLevels, lastPlayedLevel);

        public List<LevelUtility.Level> AvailableLevels { get; private set; }
        public LevelUtility.Level? LastPlayedLevel { get; private set; }

        private bool LevelAvailable(LevelUtility.Level level) => AvailableLevels.Contains(level);

        private void Save() {
            var formatter = new BinaryFormatter();
            try {
                using var saveFile = new FileStream(saveFilePath, FileMode.Create);
                formatter.Serialize(saveFile, this);
            } catch (IOException e) {
                Debug.LogError($"Failed to create save file '{saveFilePath}': {e.Message}");
            }
        }

        public static void Update() {
            if (false == LevelUtility.IsOnValidLevel) {
                return;
            }

            var currentLevel = LevelUtility.CurrentLevel;
            var progress = Load();

            if (false == progress.LevelAvailable(currentLevel)) {
                progress.AvailableLevels.Add(currentLevel);
            }

            progress.LastPlayedLevel = currentLevel;
            progress.Save();
        }

        public static void Reset() => startProgress.Save();

        public static PlayerProgress Load() {
            if (false == File.Exists(saveFilePath)) {
                return startProgress;
            }

            var formatter = new BinaryFormatter();
            try {
                using var saveFile = new FileStream(saveFilePath, FileMode.Open);
                if (formatter.Deserialize(saveFile) is PlayerProgress progress) {
                    return progress;
                }

                File.Delete(saveFilePath);
                return startProgress;
            } catch (IOException e) {
                Debug.LogError($"Failed to load save file '{saveFilePath}': {e.Message}");
                return startProgress;
            } catch (SerializationException) {
                File.Delete(saveFilePath);
                return startProgress;
            }
        }
    }
#nullable restore
}