using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace lvl_0
{
    public class FlowingWater : MonoBehaviour
    {
        [SerializeField]
        private Vector3 m_flow;

        public Vector3 GetFlow()
        {
            return m_flow;
        }
    }
}
