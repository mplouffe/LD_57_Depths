using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace lvl_0
{
    public class BackgroundMusicTrigger : MonoBehaviour
    {
        [SerializeField]
        private AudioClip m_autoBackgroundTrack;

        private void Start()
        {
            AudioManager.Instance.PlayMusicOneShot(m_autoBackgroundTrack);
        }
    }
}
