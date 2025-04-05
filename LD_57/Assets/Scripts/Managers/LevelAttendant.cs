#define LEVEL_ATTENDANT

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace lvl_0
{
    public class LevelAttendant : SingletonBase<LevelAttendant>
    {
        [SerializeField]
        private List<GameStateDictionaryEntry> m_gameDictionaryEntries;

        private Dictionary<GameState, int> m_gameStateDictionary;
        private Dictionary<int, GameState> m_gameStateReverseSearchIndex;

        private GameState m_currentGame;

        protected override void Awake()
        {


            if (m_gameDictionaryEntries == null)
            {
                Debug.LogError("ArcadeAttendant has an empty dictionary! (?°?°)?? ???");
                return;
            }
            base.Awake();
            DontDestroyOnLoad(gameObject);
            m_gameStateDictionary = new Dictionary<GameState, int>(m_gameDictionaryEntries.Count);
            m_gameStateReverseSearchIndex = new Dictionary<int, GameState>(m_gameDictionaryEntries.Count);

            foreach(var gameDictionaryEntry in m_gameDictionaryEntries)
            {
                m_gameStateDictionary.Add(gameDictionaryEntry.Game, gameDictionaryEntry.LoaderSceneIndex);
                m_gameStateReverseSearchIndex.Add(gameDictionaryEntry.LoaderSceneIndex, gameDictionaryEntry.Game);
            }
            m_currentGame = GameState.LogoScreen;
        }

        public void LoadGameState(GameState newGame)
        {
            if (m_currentGame == newGame)
            {
                // soft reset?
                // Log error?
            }
            else
            {
                SceneManager.sceneLoaded += OnSceneLoaded;
                SceneManager.LoadScene(m_gameStateDictionary[newGame]);
                m_currentGame = newGame;  
            }
        }

        private void OnSceneLoaded(Scene loadedScene, LoadSceneMode loadSceneMode)
        {
            if (m_gameStateReverseSearchIndex.ContainsKey(loadedScene.buildIndex))
            {
                if (m_gameStateReverseSearchIndex[loadedScene.buildIndex] == m_currentGame)
                {
                    SceneManager.sceneLoaded -= OnSceneLoaded;
                    return;
                }
            }

            Debug.LogError("OnSceneLoaded called from ArcadeAttendent when it wasn't expected.");
        }
    }

    [Serializable]
    public struct GameStateDictionaryEntry
    {
        public GameState Game;
        public int LoaderSceneIndex;
    }

    public enum GameState
    {
        LogoScreen,
        Menu,
        Settings,
        GameStart,
        GameOver
    }
}
