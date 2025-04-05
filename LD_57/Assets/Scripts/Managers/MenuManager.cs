using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEngine.InputSystem.InputAction;

namespace lvl_0
{
    public class MenuManager : MonoBehaviour
    {
        [SerializeField]
        private RectTransform m_pointerArrow;

        [SerializeField]
        private float m_inputCooldown;

        [SerializeField]
        private List<Vector2> m_pointerPositions;

        private Duration m_inputCooldownDuration;

        private InputActions m_inputActions;

        private MenuItem m_selectedMenuItem;

        private bool m_itemSelected = false;

        private void Awake()
        {
            m_inputCooldownDuration = new Duration(m_inputCooldown);
            m_inputActions = new InputActions();
            m_selectedMenuItem = MenuItem.Start;
            m_pointerArrow.anchoredPosition = m_pointerPositions[0];
        }

        private void OnEnable()
        {
            m_inputActions.MainMenu.Enable();
            m_inputActions.MainMenu.Select.performed += OnSelectPerformed;
            m_inputActions.MainMenu.Escape.performed += OnEscapePerformed;
            m_itemSelected = false;
        }

        private void OnDisable()
        {
            m_inputActions.MainMenu.Select.performed -= OnSelectPerformed;
            m_inputActions.MainMenu.Escape.performed -= OnEscapePerformed;
            m_inputActions.MainMenu.Disable();
        }

        private void Update()
        {
            if (!m_itemSelected)
            {
                m_inputCooldownDuration.Update(Time.deltaTime);
                if (m_inputCooldownDuration.Elapsed())
                {
                    var moveInput = m_inputActions.MainMenu.Move.ReadValue<Vector2>();
                    if (moveInput.y < 0 && m_selectedMenuItem < MenuItem.Quit)
                    {
                        m_selectedMenuItem++;
                        m_pointerArrow.anchoredPosition = m_pointerPositions[(int)m_selectedMenuItem];
                        m_inputCooldownDuration.Reset();
                    }
                    else if (moveInput.y > 0 && m_selectedMenuItem > MenuItem.Start)
                    {
                        m_selectedMenuItem--;
                        m_pointerArrow.anchoredPosition = m_pointerPositions[(int)m_selectedMenuItem];
                        m_inputCooldownDuration.Reset();
                    }
                }
            }
        }

        private void OnSelectPerformed(CallbackContext context)
        {
            if (m_inputCooldownDuration.Elapsed())
            {
                switch (m_selectedMenuItem)
                {
                    case MenuItem.Start:
                        LevelAttendant.Instance.LoadGameState(GameState.GameStart);
                        break;
                    case MenuItem.Settings:
                        LevelAttendant.Instance.LoadGameState(GameState.Settings);
                        break;
                    case MenuItem.Quit:
                        Application.Quit();
                        break;
                }
                m_itemSelected = true;
            }
        }

        private void OnEscapePerformed(CallbackContext context)
        {
            if (m_inputCooldownDuration.Elapsed())
            {
                Application.Quit();
            }
        }

    }

    public enum MenuItem
    {
        Start,
        Settings,
        Quit
    }
}


