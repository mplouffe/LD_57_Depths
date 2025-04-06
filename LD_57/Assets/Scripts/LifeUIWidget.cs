using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace lvl_0
{
    public class LifeUIWidget : SingletonBase<LifeUIWidget>
    {
        [SerializeField]
        private List<Image> m_lifeIcons;

        public void UpdateLife(int newLifeAmount)
        {
            for(var i = newLifeAmount + 1; i < m_lifeIcons.Count; i++)
            {
                m_lifeIcons[i].gameObject.SetActive(false);
            }

            for(var i = 0; i < newLifeAmount + 1 && i < m_lifeIcons.Count; i++)
            {
                m_lifeIcons[i].gameObject.SetActive(true);
            }
        }
    }
}
