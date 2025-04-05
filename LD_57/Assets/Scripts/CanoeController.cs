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
        private Sprite m_redPaddleBackstroke;

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

        [SerializeField]
        private Sprite m_bluePaddleBackstroke;

        private InputActions m_inputActions;
        private Rigidbody2D m_rigidbody2D;
        private bool m_isFrozen = false;

        private Vector3 m_frozenVelocity = Vector3.zero;
        private float m_frozenAngularVelocity = 0f;


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
            if (m_isFrozen) return;
            Paddle(m_redPaddlePosition, m_redPaddleForce);
            m_redPaddleRenderer.sprite = m_redPaddleStroke;
        }

        private void OnRedPaddleUpPerformed(CallbackContext context)
        {
            if (m_isFrozen) return;
            Paddle(m_redPaddlePosition, -m_redPaddleForce);
            m_redPaddleRenderer.sprite = m_redPaddleBackstroke;
        }

        private void OnBluePaddleDownPerformed(CallbackContext context)
        {
            if (m_isFrozen) return;
            Paddle(m_bluePaddlePosition, m_bluePaddleForce);
            m_bluePaddlerRenderer.sprite = m_bluePaddleStroke;
        }

        private void OnBluePaddleUpPerformed(CallbackContext context)
        {
            if (m_isFrozen) return;
            Paddle(m_bluePaddlePosition, -m_bluePaddleForce);
            m_bluePaddlerRenderer.sprite = m_bluePaddleBackstroke;
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

        public void KillCanoe()
        {
            Destroy(gameObject);
        }

        public void Freeze()
        {
            OnBluePaddleCanceled(new CallbackContext());
            OnRedPaddleCanceled(new CallbackContext());
            m_rigidbody2D.freezeRotation = true;
            m_frozenVelocity = m_rigidbody2D.velocity;
            m_rigidbody2D.velocity = Vector3.zero;
            m_frozenAngularVelocity = m_rigidbody2D.angularVelocity;
            m_rigidbody2D.angularVelocity = 0f;
            m_rigidbody2D.simulated = false;
            m_isFrozen = true;
        }

        public void UnFreeze()
        {
            m_rigidbody2D.freezeRotation = false;
            m_rigidbody2D.velocity = m_frozenVelocity;
            m_frozenVelocity = Vector3.zero;
            m_rigidbody2D.angularVelocity = m_frozenAngularVelocity;
            m_frozenAngularVelocity = 0f;
            m_rigidbody2D.simulated = true;
            m_isFrozen = false;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.collider.CompareTag("Rock"))
            {
                LevelManager.Instance.CanoeKilled();
            }
        }
    }
}
