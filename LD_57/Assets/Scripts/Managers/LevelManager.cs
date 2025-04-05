using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace lvl_0
{
    public class LevelManager : SingletonBase<LevelManager>
    {
        [SerializeField]
        private Transform m_levelStartingPoint;

        [SerializeField]
        private CanoeController m_playerPrefab;

        private CanoeController m_currentPlayer;

        private void SpawnPlayer()
        {
            m_currentPlayer = Instantiate(m_playerPrefab, m_levelStartingPoint);
            CinemachineCameraAccessor.Instance.GetCamera().Follow = m_currentPlayer.transform;
        }

        public void GameSceneLoaded()
        {
            SpawnPlayer();
        }

        public void EndOfLevelReached()
        {
            LevelAttendant.Instance.LoadGameState(GameState.GameOver);
        }
    }
}
