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
        private SpriteRenderer m_canoeRenderer;

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

        [SerializeField]
        private AudioClip m_paddleUpSFX;

        [SerializeField]
        private AudioClip m_paddleDownSFX;

        [SerializeField]
        private AudioClip m_deathSound;

        private InputActions m_inputActions;
        private Rigidbody2D m_rigidbody2D;
        private CapsuleCollider2D m_collider;
        private bool m_isFrozen = false;

        private Vector3 m_frozenVelocity = Vector3.zero;
        private float m_frozenAngularVelocity = 0f;

        private Vector3 m_currentFlow = Vector3.zero;
        private List<Vector3> m_flowQueue = new(){ Vector3.zero };

        private int m_deathAnimationFrame;
        private bool m_isDying;
        [SerializeField]
        private List<Sprite> m_deathAnimation;
        [SerializeField]
        private Duration m_deathAnimationDuration;

        private Transform m_parent;

        private void Awake()
        {
            m_inputActions = new InputActions();
            m_rigidbody2D = GetComponent<Rigidbody2D>();
            m_collider = GetComponent<CapsuleCollider2D>();
        }

        private void Update()
        {
            if (!m_isFrozen)
            {
                m_rigidbody2D.AddForce(m_currentFlow * Time.deltaTime);
            }

            if (m_isDying)
            {
                if (m_deathAnimationDuration.UpdateCheck())
                {
                    m_deathAnimationFrame++;
                    if (m_deathAnimationFrame < m_deathAnimation.Count)
                    {
                        m_canoeRenderer.sprite = m_deathAnimation[m_deathAnimationFrame];
                        m_deathAnimationDuration.Reset();
                    }
                    else
                    {
                        m_isDying = false;
                        Destroy(gameObject);
                    }
                }
            }
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
            AudioManager.Instance.PlaySfx(m_paddleDownSFX);
        }

        private void OnRedPaddleUpPerformed(CallbackContext context)
        {
            if (m_isFrozen) return;
            Paddle(m_redPaddlePosition, -m_redPaddleForce);
            m_redPaddleRenderer.sprite = m_redPaddleBackstroke;
            AudioManager.Instance.PlaySfx(m_paddleUpSFX);
        }

        private void OnBluePaddleDownPerformed(CallbackContext context)
        {
            if (m_isFrozen) return;
            Paddle(m_bluePaddlePosition, m_bluePaddleForce);
            m_bluePaddlerRenderer.sprite = m_bluePaddleStroke;
            AudioManager.Instance.PlaySfx(m_paddleDownSFX);
        }

        private void OnBluePaddleUpPerformed(CallbackContext context)
        {
            if (m_isFrozen) return;
            Paddle(m_bluePaddlePosition, -m_bluePaddleForce);
            m_bluePaddlerRenderer.sprite = m_bluePaddleBackstroke;
            AudioManager.Instance.PlaySfx(m_paddleUpSFX);
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
            m_inputActions.Game.Disable();
            m_deathAnimationFrame = 0;
            m_bluePaddlerRenderer.enabled = false;
            m_redPaddleRenderer.enabled = false;
            m_collider.enabled = false;
            m_canoeRenderer.sprite = m_deathAnimation[0];
            m_deathAnimationDuration.Reset();

            m_isDying = true;
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

        public InputActions GetInputActions()
        {
            return m_inputActions;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.collider.CompareTag("Rock"))
            {
                AudioManager.Instance.PlaySfx(m_deathSound);
                LevelManager.Instance.CanoeKilled();
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Water"))
            {
                var newFlow = collision.GetComponent<FlowingWater>().GetFlow();
                m_flowQueue.Add(newFlow);
                m_currentFlow = newFlow;
            }
            else if (collision.CompareTag("Whirlpool"))
            {
                m_parent = transform.parent;
                transform.parent = collision.gameObject.transform.parent;
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Water"))
            {
                var exitingFlow = collision.GetComponent<FlowingWater>().GetFlow();
                var exitingFlowIndex = -1;
                for(var i = 0; i < m_flowQueue.Count; i++)
                {
                    if (m_flowQueue[i] == exitingFlow)
                    {
                        exitingFlowIndex = i;
                        break;
                    }
                }
                
                if (exitingFlowIndex > -1)
                {
                    m_flowQueue.RemoveAt(exitingFlowIndex);
                }
                m_currentFlow = m_flowQueue[m_flowQueue.Count - 1];
            }
            else if (collision.CompareTag("Whirlpool"))
            {
                transform.parent = m_parent;
            }
        }
    }
}
