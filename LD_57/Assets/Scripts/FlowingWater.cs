using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace lvl_0
{
    public class FlowingWater : SingletonBase<FlowingWater>
    {
        [SerializeField]
        private MeshRenderer m_riverRenderer;

        [SerializeField]
        private Texture2D m_flowMap;

        private float m_flowStrength;

        public float FlowStrength { get { return m_flowStrength;  } private set { m_flowStrength = value;  } }
        public Vector2 FlowMapMin { get; private set; }
        public Vector2 FlowMapMax { get; private set; }

        private void Start()
        {
            Bounds flowmapBounds = m_riverRenderer.bounds;
            FlowMapMin = new Vector2(flowmapBounds.min.x, flowmapBounds.min.y);
            FlowMapMax = new Vector2(flowmapBounds.max.x, flowmapBounds.max.y);
            int flowStrengthIndex = m_riverRenderer.material.shader.FindPropertyIndex("_FlowStrength");
            m_flowStrength = m_riverRenderer.material.shader.GetPropertyDefaultFloatValue(flowStrengthIndex);
        }

        public Texture2D GetFlowMap()
        {
            return m_flowMap;
        }
    }
}
