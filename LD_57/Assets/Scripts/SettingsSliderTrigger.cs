using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace lvl_0
{
    public class SettingsSliderTrigger : MonoBehaviour
    {
        public bool IsSfx;

        [SerializeField]
        private Slider m_slider;

        private bool m_init = false;

        public void OnSettingChanged(float newValue)
        {
            if (IsSfx)
            {
                AudioManager.Instance.UpdateSfxLevel((int)newValue);
            }
            else
            {
                AudioManager.Instance.UpdateMusicLevel((int)newValue);
            }
        }

        public void Update()
        {
            if (!m_init)
            {
                if (IsSfx)
                {

                    m_slider.value = AudioManager.Instance.SfxLevel;
                }
                else
                {
                    m_slider.value = AudioManager.Instance.MusicLevel;
                }
                m_init = true;
            }
        }

        public void IncreaseVolume(int increaseAmount)
        {
            var newVolume = Mathf.Min(m_slider.value + increaseAmount, m_slider.maxValue);
            m_slider.value = newVolume;
        }

        public void DecreaseVolume(int increaseAmount)
        {
            var newVolume = Mathf.Max(m_slider.value - increaseAmount, m_slider.minValue);
            m_slider.value = newVolume;
        }
    }
}
