using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace lvl_0
{
    public class BearTubeController : MonoBehaviour
    {
        [SerializeField]
        private Rigidbody2D m_rigidbody;

        [SerializeField]
        private Duration m_pulseInterval;

        [SerializeField]
        private float m_pulseIntervalMinimum;

        [SerializeField]
        private float m_pulseIntervalMaximum;

        [SerializeField]
        private float m_pulseForceMinimum;

        [SerializeField]
        private float m_pulseForceMaximum;

        [SerializeField]
        private bool m_isChasingPrey;
        [SerializeField]
        private Transform m_prey;

        [SerializeField]
        private Animator m_animator;

        [SerializeField]
        private AudioClip m_rageRoar;

        [SerializeField]
        private AudioClip m_wakingUpRoar;

        [SerializeField]
        private AudioClip m_findingPreyRoar;

        private bool m_hibernating = true;
        private int m_activeEntities = 0;

        private void FixedUpdate()
        {
            if (m_hibernating)
            {
                return;
            }

            if (m_pulseInterval.UpdateCheck())
            {
                var direction = GetPulseDirection();
                var forceAmount = GetForceAmount();
                m_rigidbody.AddForce(forceAmount * direction);
                m_pulseInterval.Reset(Random.Range(m_pulseIntervalMinimum, m_pulseIntervalMaximum));
            }
        }

        private Vector3 GetPulseDirection()
        {
            if (m_isChasingPrey)
            {
                var directionOfPrey = m_prey.position - transform.position;
                directionOfPrey.Normalize();
                return directionOfPrey;
            }

            float randomX = Random.Range(-1, 1);
            float randomY = Random.Range(-1, 1);

            Vector3 randomDirection = new Vector3(randomX, randomY, 0);
            randomDirection.Normalize();
            return randomDirection;
        }

        private float GetForceAmount()
        {
            if (m_isChasingPrey) return m_pulseIntervalMaximum;
            
            return Random.Range(m_pulseForceMinimum, m_pulseForceMinimum + 10);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.collider.CompareTag("Player"))
            {
                AudioManager.Instance.PlayWorldSfx(m_rageRoar);
                m_animator.SetTrigger("Rage");
            }
        }

        public void OnPreyCircleEntered()
        {
            AudioManager.Instance.PlayWorldSfx(m_findingPreyRoar);
            m_prey = LevelManager.Instance.GetPlayerTransform();
            m_isChasingPrey = true;
        }

        public void OnPreyCircleExited()
        {
            m_isChasingPrey = false;
            m_prey = null;
        }

        public void OnActivityCircleExited()
        {
            m_activeEntities--;
            if (m_activeEntities == 0)
            {
                m_hibernating = true;
                m_rigidbody.velocity = Vector2.zero;
            }
        }

        public void OnActivityCircleEntered()
        {
            AudioManager.Instance.PlayWorldSfx(m_wakingUpRoar);
            m_activeEntities++;
            m_hibernating = false;
            m_pulseInterval.Reset();
        }
    }
}
