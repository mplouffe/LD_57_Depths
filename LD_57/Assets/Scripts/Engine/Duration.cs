using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace lvl_0
{
    public struct Duration
    {
        public AnimationCurve m_easingCurve;
        private float m_currentDuration;
        private float m_totalDuration;

        public Duration(float totalDuration)
        {
            m_currentDuration = 0;
            m_totalDuration = totalDuration;
            m_easingCurve = AnimationCurve.Linear(0.0f, 0.0f, 1.0f, 1.0f);
        }

        public Duration (float totalDuration, AnimationCurve easingCurve)
        {
            m_currentDuration = 0;
            m_totalDuration = totalDuration;
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

        public void Reset(float newDuration = -1f)
        {
            if (newDuration > -1)
            {
                m_totalDuration = newDuration;
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
