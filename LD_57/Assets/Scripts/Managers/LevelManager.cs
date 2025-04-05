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

        [SerializeField]
        private int m_playerLives;

        [SerializeField]
        private Duration m_afterDeathWaitTime;

        [SerializeField]
        private Duration m_levelCompleteWaitTime;

        private CanoeController m_currentPlayer;
        private LevelManagerState m_currentState;

        private void SetState(LevelManagerState newState)
        {
            switch (newState)
            {
                case LevelManagerState.Respawning:
                    m_currentPlayer = Instantiate(m_playerPrefab, m_levelStartingPoint);
                    CinemachineCameraAccessor.Instance.GetCamera().Follow = m_currentPlayer.transform;
                    break;
                case LevelManagerState.WaitingAfterDeath:
                    m_afterDeathWaitTime.Reset();
                    break;
                case LevelManagerState.EndingLevel:
                    m_currentPlayer.Freeze();
                    m_levelCompleteWaitTime.Reset();
                    break;
                case LevelManagerState.LevelComplete:
                    LevelAttendant.Instance.LoadGameState(GameState.LevelComplete);
                    break;
                case LevelManagerState.GameOver:
                    LevelAttendant.Instance.LoadGameState(GameState.GameOver);
                    break;
            }

            m_currentState = newState;
        }

        private void Update()
        {
            switch (m_currentState)
            {
                case LevelManagerState.WaitingAfterDeath:
                    if (m_afterDeathWaitTime.UpdateCheck())
                    {
                        if (m_playerLives >= 0)
                        {
                            SetState(LevelManagerState.Respawning);
                        }
                        else
                        {
                            SetState(LevelManagerState.GameOver);
                        }
                    }
                    break;
                case LevelManagerState.EndingLevel:
                    if (m_levelCompleteWaitTime.UpdateCheck())
                    {
                        SetState(LevelManagerState.LevelComplete);
                    }
                    break;
            }
        }

        private void SpawnPlayer()
        {
            SetState(LevelManagerState.Respawning);
        }

        public void GameSceneLoaded()
        {
            UIManager.Instance.UpdateLivesCount(m_playerLives);
            SpawnPlayer();
        }

        public void EndOfLevelReached()
        {
            SetState(LevelManagerState.EndingLevel);
        }

        public void CanoeKilled()
        {
            m_currentPlayer.KillCanoe();
            m_playerLives--;
            if (m_playerLives >= 0)
            {
                UIManager.Instance.UpdateLivesCount(m_playerLives);
            }
            SetState(LevelManagerState.WaitingAfterDeath);
        }
    }

    public enum LevelManagerState
    {
        None,
        Playing,
        WaitingAfterDeath,
        Respawning,
        EndingLevel,
        GameOver,
        LevelComplete
    }
}
