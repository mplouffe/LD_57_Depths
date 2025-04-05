using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace lvl_0
{
    public class BasicAnimator : MonoBehaviour
    {

        [SerializeField]
        private float m_startWaitDuration;

        [SerializeField]
        private float m_animationDuration;

        [SerializeField]
        private AnimationCurve m_moveEasingCurve;

        [SerializeField]
        private Vector3 m_endPosition;
        private Vector3 m_startPosition;

        [SerializeField]
        private bool m_animateRelativePosition;

        [SerializeField]
        private Vector3 m_endScale;
        private Vector3 m_startScale;

        [SerializeField]
        private Transform m_animatedTransform;

        [SerializeField]
        private bool m_loopAnimation;

        private AnimatorState m_currentState;

        private Duration m_waitDuration;
        private Duration m_animateDuration;

        private void Awake()
        {
            m_waitDuration = new Duration(m_startWaitDuration);
            m_animateDuration = new Duration(m_animationDuration, m_moveEasingCurve);
            m_startPosition = m_animateRelativePosition ? m_animatedTransform.localPosition : m_animatedTransform.position;
            m_startScale = m_animatedTransform.localScale;
        }

        void OnEnable()
        {
            m_currentState = m_startWaitDuration > 0 ? AnimatorState.Waiting : AnimatorState.Animating;
        }

        // Update is called once per frame
        void Update()
        {
            switch (m_currentState)
            {
                case AnimatorState.Waiting:
                    m_waitDuration.Update(Time.deltaTime);
                    if (m_waitDuration.Elapsed())
                    {
                        m_currentState = AnimatorState.Animating;
                    }
                    break;
                case AnimatorState.Animating:
                    m_animateDuration.Update(Time.deltaTime);
                    if (m_animateRelativePosition)
                    {
                        m_animatedTransform.localPosition = Vector3.Lerp(m_startPosition, m_endPosition, m_animateDuration.CurvedDelta());
                    }
                    else
                    {
                        m_animatedTransform.position = Vector3.Lerp(m_startPosition, m_endPosition, m_animateDuration.CurvedDelta());
                    }
                    m_animatedTransform.localScale = Vector3.Lerp(m_startScale, m_endScale, m_animateDuration.CurvedDelta());
                    if (m_animateDuration.Elapsed())
                    {
                        if (m_loopAnimation)
                        {
                            if (m_animateRelativePosition)
                            {
                                m_animatedTransform.localPosition = m_endPosition;
                                m_endPosition = m_startPosition;
                                m_startPosition = m_animatedTransform.localPosition;
                            }
                            else
                            {
                                m_animatedTransform.position = m_endPosition;
                                m_endPosition = m_startPosition;
                                m_startPosition = m_animatedTransform.position;
                            }

                            m_animatedTransform.localScale = m_endScale;
                            m_endScale = m_startScale;
                            m_startScale = m_animatedTransform.localScale;

                            m_animateDuration.Reset();
                            m_waitDuration.Reset();
                            m_currentState = AnimatorState.Waiting;
                        }
                        else
                        {
                            m_currentState = AnimatorState.FinishedMoving;
                        }
                    }
                    break;
            }
        }
    }

    public enum AnimatorState
    {
        Waiting,
        Animating,
        FinishedMoving
    }
}