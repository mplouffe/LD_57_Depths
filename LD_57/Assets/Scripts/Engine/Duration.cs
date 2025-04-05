using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace lvl_0
{
    [Serializable]
    public struct Duration
    {
        public AnimationCurve m_easingCurve;
        private float m_currentDuration;
        [SerializeField]
        private float m_totalDuration;

        private float m_originalDuration;
        private bool m_overriden;

        public Duration(float totalDuration)
        {
            m_currentDuration = 0;
            m_totalDuration = totalDuration;
            m_originalDuration = totalDuration;
            m_overriden = false;
            m_easingCurve = AnimationCurve.Linear(0.0f, 0.0f, 1.0f, 1.0f);
        }

        public Duration(float totalDuration, AnimationCurve easingCurve)
        {
            m_currentDuration = 0;
            m_totalDuration = totalDuration;
            m_originalDuration = totalDuration;
            m_overriden = false;
            m_easingCurve = easingCurve;
        }

        public void Update(float duration)
        {
            m_currentDuration += duration;
        }

        public float Remaining()
        {
            return m_totalDuration - m_currentDuration;
        }

        public bool Elapsed()
        {
            return m_currentDuration >= m_totalDuration;
        }

        public bool UpdateCheck()
        {
            m_currentDuration += Time.deltaTime;
            return m_currentDuration >= m_totalDuration;
        }

        public void Override(float overrideDuration)
        {
            m_totalDuration = overrideDuration;
            m_overriden = true;
            m_currentDuration = 0;
        }

        public void Reset(float newDuration = -1f)
        {
            if (newDuration > -1)
            {
                m_totalDuration = newDuration;
                m_originalDuration = newDuration;
                m_overriden = false;
            }
            else
            {
                if (m_overriden)
                {
                    m_totalDuration = m_originalDuration;
                    m_overriden = false;
                }
            }
            m_currentDuration = 0;
        }

        public float Delta()
        {
            return m_currentDuration / m_totalDuration;
        }

        public float CurvedDelta()
        {
            return m_easingCurve.Evaluate(Delta());
        }
    }
}
