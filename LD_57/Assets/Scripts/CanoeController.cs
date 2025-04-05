using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

namespace lvl_0
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class CanoeController : MonoBehaviour
    {
        [SerializeField]
        private Transform m_redPaddlePosition;

        [SerializeField]
        private float m_redPaddleForce;

        [SerializeField]
        private SpriteRenderer m_redPaddleRenderer;

        [SerializeField]
        private Sprite m_redPaddleIdle;

        [SerializeField]
        private Sprite m_redPaddleStroke;

        [SerializeField]
        private Transform m_bluePaddlePosition;

        [SerializeField]
        private float m_bluePaddleForce;

        [SerializeField]
        private SpriteRenderer m_bluePaddlerRenderer;

        [SerializeField]
        private Sprite m_bluePaddleIdle;

        [SerializeField]
        private Sprite m_bluePaddleStroke;

        private InputActions m_inputActions;
        private Rigidbody2D m_rigidbody2D;

        private void Awake()
        {
            m_inputActions = new InputActions();
            m_rigidbody2D = GetComponent<Rigidbody2D>();
        }

        private void OnEnable()
        {
            m_inputActions.Game.Enable();
            m_inputActions.Game.RedPaddleDown.performed += OnRedPaddleDownPerformed;
            m_inputActions.Game.RedPaddleUp.performed += OnRedPaddleUpPerformed;
            m_inputActions.Game.BluePaddeDown.performed += OnBluePaddleDownPerformed;
            m_inputActions.Game.BluePaddeUp.performed += OnBluePaddleUpPerformed;
            
            m_inputActions.Game.RedPaddleDown.canceled += OnRedPaddleCanceled;
            m_inputActions.Game.RedPaddleUp.canceled += OnRedPaddleCanceled;
            m_inputActions.Game.BluePaddeDown.canceled += OnBluePaddleCanceled;
            m_inputActions.Game.BluePaddeUp.canceled += OnBluePaddleCanceled;
        }

        private void OnRedPaddleDownPerformed(CallbackContext context)
        {
            Paddle(m_redPaddlePosition, m_redPaddleForce);
            m_redPaddleRenderer.sprite = m_redPaddleStroke;
        }

        private void OnRedPaddleUpPerformed(CallbackContext context)
        {
            Paddle(m_redPaddlePosition, -m_redPaddleForce);
            m_redPaddleRenderer.sprite = m_redPaddleStroke;
        }

        private void OnBluePaddleDownPerformed(CallbackContext context)
        {
            Paddle(m_bluePaddlePosition, m_bluePaddleForce);
            m_bluePaddlerRenderer.sprite = m_bluePaddleStroke;
        }

        private void OnBluePaddleUpPerformed(CallbackContext context)
        {
            Paddle(m_bluePaddlePosition, -m_bluePaddleForce);
            m_bluePaddlerRenderer.sprite = m_bluePaddleStroke;
        }

        private void OnRedPaddleCanceled(CallbackContext context)
        {
            m_redPaddleRenderer.sprite = m_redPaddleIdle;
        }

        private void OnBluePaddleCanceled(CallbackContext context)
        {
            m_bluePaddlerRenderer.sprite = m_bluePaddleIdle;
        }

        private void Paddle(Transform m_paddlePosition, float m_paddleForce)
        {
            var paddleForce = -m_paddlePosition.transform.up * m_paddleForce;
            m_rigidbody2D.AddForceAtPosition(paddleForce, m_paddlePosition.position);
        }

        private void OnDisable()
        {
            m_inputActions.Game.RedPaddleDown.performed -= OnRedPaddleDownPerformed;
            m_inputActions.Game.RedPaddleUp.performed -= OnRedPaddleUpPerformed;
            m_inputActions.Game.BluePaddeDown.performed -= OnBluePaddleDownPerformed;
            m_inputActions.Game.BluePaddeUp.performed -= OnBluePaddleUpPerformed;
            m_inputActions.Game.Disable();
        }
    }
}
