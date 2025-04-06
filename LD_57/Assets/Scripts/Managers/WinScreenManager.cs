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
        private TextMeshProUGUI m_courseCompletedText;

        [SerializeField]
        private Image m_medalImage;

        private void OnEnable()
        {
            var raisedLevel = LevelAttendant.Instance.GetRaisedLevel();

            switch (raisedLevel)
            {
                case FinishFlagLevel.Easy:
                    m_medalImage.sprite = m_bronzeMedal;
                    m_courseCompletedText.text = "Bronze Course";
                    break;
                case FinishFlagLevel.Medium:
                    m_medalImage.sprite = m_silverMedal;
                    m_courseCompletedText.text = "Silver Course";
                    break;
                case FinishFlagLevel.Hard:
                    m_medalImage.sprite = m_goldMedal;
                    m_courseCompletedText.text = "Gold Course";
                    break;
            }
        }
    }
}
