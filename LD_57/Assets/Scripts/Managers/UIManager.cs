using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace lvl_0
{
    public class UIManager : SingletonBase<UIManager>
    {
        [SerializeField]
        private TextMeshProUGUI m_livesCounter;

        public void UpdateLivesCount(int newLivesCount)
        {
            m_livesCounter.text = newLivesCount.ToString();
        }
    }
}
