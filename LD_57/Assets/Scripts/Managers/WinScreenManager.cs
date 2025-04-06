using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace lvl_0
{
    public class WinScreenManager : MonoBehaviour
    {
        [SerializeField]
        private Sprite m_bronzeMedal;

        [SerializeField]
        private Sprite m_silverMedal;

        [SerializeField]
        private Sprite m_goldMedal;

        [SerializeField]
        private Sprite m_bronzeText;

        [SerializeField]
        private Sprite m_silverText;

        [SerializeField]
        private Sprite m_goldText;

        [SerializeField]
        private Image m_courseCompletedText;

        [SerializeField]
        private Image m_medalImage;

        private void OnEnable()
        {
            var raisedLevel = LevelAttendant.Instance.GetRaisedLevel();

            switch (raisedLevel)
            {
                case FinishFlagLevel.Easy:
                    m_medalImage.sprite = m_bronzeMedal;
                    m_courseCompletedText.sprite = m_bronzeText;
                    break;
                case FinishFlagLevel.Medium:
                    m_medalImage.sprite = m_silverMedal;
                    m_courseCompletedText.sprite = m_silverText;
                    break;
                case FinishFlagLevel.Hard:
                    m_medalImage.sprite = m_goldMedal;
                    m_courseCompletedText.sprite = m_goldText;
                    break;
            }
        }
    }
}
