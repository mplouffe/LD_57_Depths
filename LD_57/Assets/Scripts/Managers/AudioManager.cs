using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace lvl_0
{
    public class AudioManager : SingletonBase<AudioManager>
    {
        [SerializeField]
        private AudioSource m_sfxSource;

        [SerializeField]
        private AudioSource m_musicSource;

        public void PlayMusic(AudioClip music)
        {
            if (m_musicSource.isPlaying)
            {
                m_musicSource.Stop();
            }
            m_musicSource.clip = music;
            m_musicSource.loop = true;
            m_musicSource.Play();
        }

        public void PlaySfx(AudioClip sfx)
        {
            m_sfxSource.PlayOneShot(sfx);
        }

        public void StopMusic()
        {
            m_musicSource.Stop();
        }
    }
}
