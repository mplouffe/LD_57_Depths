using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace lvl_0
{
    public class AudioManager : SingletonBase<AudioManager>
    {
        [SerializeField]
        private AudioSource m_playerSfxSource;

        [SerializeField]
        private AudioSource m_worldSfxSource;

        [SerializeField]
        private AudioSource m_musicSource;

        [SerializeField]
        private AudioSettings m_defaultSettings;

        private AudioSettings m_currentSettings;

        public float MusicLevel => m_currentSettings.MusicLevel;
        public float SfxLevel => m_currentSettings.SfxLevel;

        protected override void Awake()
        {
            base.Awake();
            m_currentSettings = m_defaultSettings;
            m_musicSource.volume = m_currentSettings.MusicLevel / 100f;
            m_worldSfxSource.volume = m_currentSettings.SfxLevel / 100f;
            m_playerSfxSource.volume = m_currentSettings.SfxLevel / 100f;
        }

        public void PlayMusic(AudioClip music)
        {
            PlayMusic(music, true);
        }

        public void PlayMusicOneShot(AudioClip music)
        {
            PlayMusic(music, false);
        }

        private void PlayMusic(AudioClip music, bool loop)
        {
            if (m_musicSource.isPlaying) m_musicSource.Stop();
            m_musicSource.clip = music;

            if (m_currentSettings.MusicLevel == 0) return;

            m_musicSource.volume = m_currentSettings.MusicLevel / 100f;
            m_musicSource.clip = music;
            m_musicSource.loop = loop;
            m_musicSource.Play();
        }

        public void PlaySfx(AudioClip sfx)
        {
            m_playerSfxSource.volume = m_currentSettings.SfxLevel / 100f;
            m_playerSfxSource.PlayOneShot(sfx);
        }

        public void PlayWorldSfx(AudioClip sfx)
        {
            m_worldSfxSource.volume = m_currentSettings.SfxLevel / 100f;
            m_worldSfxSource.PlayOneShot(sfx);
        }

        public void StopMusic()
        {
            m_musicSource.Stop();
        }

        public void UpdateMusicLevel(int newMusicLevel)
        {
            m_currentSettings.MusicLevel = newMusicLevel;
            m_musicSource.volume = m_currentSettings.MusicLevel / 100f;
        }

        public void UpdateSfxLevel(int newSfxLevel)
        {
            m_currentSettings.SfxLevel = newSfxLevel;
            m_playerSfxSource.volume = m_currentSettings.SfxLevel / 100f;
            m_worldSfxSource.volume = m_currentSettings.SfxLevel / 100f;
        }
    }
}
