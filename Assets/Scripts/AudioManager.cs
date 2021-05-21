using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace MusicPlayer
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioManager : MonoBehaviour
    {
        public List<Playlist> playLists;
        public AudioSource source;

        private void Start()
        {
            source = GetComponent<AudioSource>();
            source.clip = playLists[0].musicList[0].audioClip;
            //Play();
        }

        public void Play()
        {
            Debug.Log($"play");
            if (source.clip == null) return;
            else
                Debug.Log($"Null song");

            if (!source.isPlaying)
                source.Play();
            else
                source.Pause();
        }

        public void Stop()
        {

        }
    }
}
