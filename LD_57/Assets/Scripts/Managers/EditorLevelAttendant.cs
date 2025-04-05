using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace lvl_0
{
    public class EditorLevelAttendant : MonoBehaviour
    {
        [SerializeField]
        private GameObject m_levelAttendant;

        private void Awake()
        {
#if !LEVEL_ATTENDANT
            Instantiate(m_levelAttendant, gameObject.transform.parent);
#endif        
        }
    }
}
