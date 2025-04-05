using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.EventSystems;

namespace lvl_0
{
    public abstract class SingletonBase<T> : MonoBehaviour
        where T : class
    {
        protected static T m_instance = null;
        
        public static T Instance
        {
            get
            {
                if (m_instance == null)
                {
                    m_instance = GameObject.FindObjectOfType(typeof(T)) as T;
                    if (m_instance == null)
                    {
                        Debug.LogError("SingletonBase<T>: Could not find GameObject of type: {" + typeof(T).Name + "}");
                    }
                }
                return m_instance;
            }
        }

        protected virtual void Awake()
        {
            if (m_instance != null)
            {
                var instance = GameObject.FindObjectOfType(typeof(T)) as T;
                if (m_instance != instance)
                {
                    Destroy(gameObject);
                    return;
                }
            }
        }

        protected virtual void OnDestroy()
        {
            if (m_instance != null && m_instance == this as T)
            {
                m_instance = null;
            }
        }
    }
}
