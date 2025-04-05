using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace lvl_0
{
    public class TimeLoaderManager : MonoBehaviour
    {
        [SerializeField]
        private float m_loadingScreenTime;

        [SerializeField]
        private GameState m_gameToLoad;

        private Duration m_loadingScreenDuration;

        private bool m_loaded = false;

        private void Start()
        {
            m_loadingScreenDuration = new Duration(m_loadingScreenTime);
        }

        private void Update()
        {
            m_loadingScreenDuration.Update(Time.deltaTime);
            if (m_loadingScreenDuration.Elapsed() && !m_loaded)
            {
                m_loaded = true;
                LevelAttendant.Instance.LoadGameState(m_gameToLoad);
            }
        }
    }
}
