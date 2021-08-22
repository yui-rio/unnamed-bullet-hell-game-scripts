using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UBHG.Utility.Levels {
    public static class LevelUtility {
        private const string UNKNOWN_LEVEL = nameof(UNKNOWN_LEVEL);
        private const string LEVEL_PREFIX = "Level";

        private static readonly string startLevelScene = $"{LEVEL_PREFIX}0";

        private static readonly string sceneNameFormatWarning =
            $"Invalid scene name format. Should be '{LEVEL_PREFIX}<index>'.";

        internal static bool IsOnValidLevel => IsLevel(SceneManager.GetActiveScene().name);

        internal static Level CurrentLevel => new Level(SceneManager.GetActiveScene());

        private static bool IsLevel(string sceneName) => sceneName.StartsWith(LEVEL_PREFIX, StringComparison.Ordinal);

        private static string LevelName(string sceneName) {
            if (int.TryParse(sceneName.Substring(LEVEL_PREFIX.Length), out var levelIndex)) {
                return $"LEVEL {levelIndex}";
            }

            Debug.LogWarning(sceneNameFormatWarning);
            return sceneName;
        }

        private static string LevelName(Scene scene) {
            if (null == scene.name) {
                Debug.LogError($"{nameof(scene.name)} is {scene.name}");
                return UNKNOWN_LEVEL;
            }

            if (IsLevel(scene.name)) {
                return LevelName(scene.name);
            }

            Debug.LogWarning(sceneNameFormatWarning);
            return scene.name;
        }

        [Serializable]
        public readonly struct Level {
            public static readonly Level StartLevel = new Level(startLevelScene, LevelName(startLevelScene));

            public Level(Scene scene) => (SceneName, Name) = (scene.name, LevelName(scene));

            public Level(string sceneName, string levelName) => (SceneName, Name) = (sceneName, levelName);

            public string SceneName { get; }

            public string Name { get; }

            public override string ToString() => $"{Name} (scene {SceneName})";

            public override bool Equals(object other) =>
                other is Level otherLevel
                && otherLevel.SceneName == SceneName
                && otherLevel.Name == Name;

            public override int GetHashCode() => SceneName.GetHashCode() ^ Name.GetHashCode();
        }
    }
}