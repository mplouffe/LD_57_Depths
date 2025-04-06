using UnityEngine;
using UnityEngine.Events;

namespace lvl_0
{
    public class TriggerColliderEventDispatcher : MonoBehaviour
    {

        [SerializeField]
        private UnityEvent m_onTriggerEnterEvent;

        [SerializeField]
        private UnityEvent m_onTriggerExitEvent;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                m_onTriggerEnterEvent.Invoke();
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                m_onTriggerExitEvent.Invoke();
            }
        }    
    }
}
