using System.IO;
using UnityEngine;

namespace ESDM.ScriptableObjects
{
    [CreateAssetMenu(fileName = "GlobalGameState", menuName = "ESDM/Global Game State", order = 0)]
    public class GlobalGameState : ScriptableObject
    {
        public GameData[] savedGames;
        [SerializeField] private GameData currentGameState;

        // Json Paths
        private static string _gameStatePath;

        // Singleton pattern
        private static GlobalGameState _globalGameState;

        public static GlobalGameState Instance
        {
            get
            {
                _gameStatePath = Application.persistentDataPath + Path.DirectorySeparatorChar + "gamestate.json";
                System.IO.Directory.CreateDirectory(Path.GetDirectoryName(_gameStatePath));
                                
                if (!_globalGameState)
                    _globalGameState = Restore();
                if (!_globalGameState)
                    _globalGameState = ScriptableObject.CreateInstance<GlobalGameState>();
                return _globalGameState;
            }
        }
        
        public GameData CurrentGameState
        {
            get
            {
                if (currentGameState == null)
                {
                    currentGameState = new GameData();
                }
                
                return currentGameState;
            }
            set { currentGameState = value; }
        }

        public void ResetCurrentGame()
        {
            currentGameState.Reset();
            Save();
        }

        public void Save()
        {
            string savedGamesJson = JsonUtility.ToJson(this, true);
            File.WriteAllText(_gameStatePath, savedGamesJson);
        }

        private static GlobalGameState Restore()
        {
            GlobalGameState gameState = null;

            if (File.Exists(_gameStatePath))
            {
                gameState = ScriptableObject.CreateInstance<GlobalGameState>();
                string json = File.ReadAllText(_gameStatePath);
                JsonUtility.FromJsonOverwrite(json, gameState);
            }

            return gameState;
        }
    }
}