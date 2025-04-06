using lvl_0;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class PauseScreenManager : SingletonBase<PauseScreenManager>
{
    [SerializeField]
    private GameObject m_pauseScreen;

    private InputActions m_inputActions;

    private bool m_pauseScreenShowing;

    [SerializeField]
    private Duration m_inputBufferDuration;

    protected override void Awake()
    {
        base.Awake();
        m_inputActions = new InputActions();
        m_pauseScreen.gameObject.SetActive(false);
        m_pauseScreenShowing = false;
    }

    private void Update()
    {
        if (m_pauseScreenShowing)
        {
            m_inputBufferDuration.Update(Time.deltaTime);
        }
    }

    public void ShowPauseScreen()
    {
        if (!m_pauseScreenShowing)
        {
            m_inputActions.PauseMenu.Enable();
            m_pauseScreen.gameObject.SetActive(true);
            m_inputActions.PauseMenu.UnPause.performed += OnUnpausePerformed;
            m_inputActions.PauseMenu.ShowSettings.performed += OnShowSettingsPerformed;
            m_inputActions.PauseMenu.Quit.performed += OnQuitPerformed;
            m_pauseScreenShowing = true;
            m_inputBufferDuration.Reset();
        }
    }

    public void HidePauseScreen()
    {
        if (m_pauseScreenShowing)
        {
            m_inputActions.PauseMenu.Disable();
            m_pauseScreen.gameObject.SetActive(false);
            m_inputActions.PauseMenu.UnPause.performed -= OnUnpausePerformed;
            m_inputActions.PauseMenu.ShowSettings.performed -= OnShowSettingsPerformed;
            m_inputActions.PauseMenu.Quit.performed -= OnQuitPerformed;
            m_pauseScreenShowing = false;
            LevelManager.Instance.UnPauseGame();
        }
    }

    private void OnUnpausePerformed(CallbackContext context)
    {
        if (m_inputBufferDuration.Elapsed())
        {
            m_inputBufferDuration.Reset();
            HidePauseScreen();
        }
    }

    private void OnShowSettingsPerformed(CallbackContext context)
    {
        if (m_inputBufferDuration.Elapsed())
        {
            m_inputBufferDuration.Reset();
            m_inputActions.PauseMenu.Disable();
            SettingsPopupManager.Instance.ShowSettings();
        }
    }

    public void HideSettingsScreen()
    {
        m_inputBufferDuration.Reset();
        m_inputActions.PauseMenu.Enable();
    }

    private void OnQuitPerformed(CallbackContext context)
    {
        if (m_inputBufferDuration.Elapsed())
        {
            m_inputBufferDuration.Reset();
            LevelAttendant.Instance.LoadGameState(GameState.Menu);
        }
    }

    private void OnDisable()
    {
        HidePauseScreen();
    }
}
