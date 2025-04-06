using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace lvl_0
{
    public enum FinishFlagLevel
    {
        Easy,
        Medium,
        Hard
    }

    public class FinishFlag : MonoBehaviour
    {
        [SerializeField]
        private FinishFlagLevel m_finishFlagLevel;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                LevelAttendant.Instance.RaiseFinishFlag(m_finishFlagLevel);
                LevelManager.Instance.EndOfLevelReached();
            }
        }
    }
}
