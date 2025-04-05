using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

namespace lvl_0
{
    public class BasicRotator : MonoBehaviour
    {

        [SerializeField]
        private float m_waitTime;

        [SerializeField]
        private Vector3 m_targetRotation;

        [SerializeField]
        private bool m_loop;
        
        private Duration m_waitDuration;
        private Vector3 m_originalRotation;

        private bool m_rotated = false;

        private void Awake()
        {
            m_waitDuration = new Duration(m_waitTime);
            m_originalRotation = transform.localEulerAngles;
        }

        private void Update()
        {
            if (!m_rotated)
            {
                m_waitDuration.Update(Time.deltaTime);
                if (m_waitDuration.Elapsed())
                {
                    transform.localEulerAngles = m_targetRotation;
                    if (m_loop)
                    {
                        m_targetRotation = m_originalRotation;
                        m_originalRotation = transform.localEulerAngles;
                        m_waitDuration.Reset();
                    }
                    else
                    {
                        m_rotated = true;
                    }
                }
            }
        }
    }
}
