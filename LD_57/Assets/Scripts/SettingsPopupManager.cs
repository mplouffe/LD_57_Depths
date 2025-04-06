using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

namespace lvl_0
{
    public class SettingsPopupManager : SingletonBase<SettingsPopupManager>
    {
        [SerializeField]
        private GameObject m_settingsOverlay;

        [SerializeField]
        private SettingsSliderTrigger m_musicSlider;

        [SerializeField]
        private SettingsSliderTrigger m_sfxSlider;

        [SerializeField]
        private int m_musicAdjustAmount;

        [SerializeField]
        private bool m_isOnMainMenu;

        private InputActions m_inputActions;

        private bool m_settingsShowing;

        protected override void Awake()
        {
            base.Awake();
            m_inputActions = new InputActions();
            m_settingsOverlay.gameObject.SetActive(false);
            m_settingsShowing = false;
        }

        public void ShowSettings()
        {
            if (!m_settingsShowing)
            {
                m_inputActions.Settings.Enable();
                m_settingsOverlay.gameObject.SetActive(true);
                m_inputActions.Settings.ExitSettings.performed += OnExitSettingsPerformed;
                m_inputActions.Settings.MusicDown.performed += OnMusicVolumeDownPerformed;
                m_inputActions.Settings.MusicUp.performed += OnMusicVolumeUpPerformed;
                m_inputActions.Settings.SfxUp.performed += OnSfxVolumeUpPerformed;
                m_inputActions.Settings.SfxDown.performed += OnSfxVolumeDownPerformed;
                m_settingsShowing = true;
            }
        }

        public void HideSettings()
        {
            if (m_settingsShowing)
            {
                m_inputActions.Settings.ExitSettings.performed -= OnExitSettingsPerformed;
                m_inputActions.Settings.Disable();
                m_settingsOverlay.gameObject.SetActive(false);
                m_settingsShowing = false;

                if (m_isOnMainMenu)
                {
                    MenuManager.Instance.HideSettingsMenu();
                }
                else
                {
                    PauseScreenManager.Instance.HideSettingsScreen();
                }
            }
        }

        private void OnExitSettingsPerformed(CallbackContext context)
        {
            HideSettings();
        }

        private void OnMusicVolumeDownPerformed(CallbackContext context)
        {
            m_musicSlider.DecreaseVolume(m_musicAdjustAmount);
        }

        private void OnMusicVolumeUpPerformed(CallbackContext context)
        {
            m_musicSlider.IncreaseVolume(m_musicAdjustAmount);
        }

        private void OnSfxVolumeDownPerformed(CallbackContext context)
        {
            m_sfxSlider.DecreaseVolume(m_musicAdjustAmount);
        }

        private void OnSfxVolumeUpPerformed(CallbackContext context)
        {
            m_sfxSlider.IncreaseVolume(m_musicAdjustAmount);
        }

        private void OnDisable()
        {
            HideSettings();
        }
    }
}
