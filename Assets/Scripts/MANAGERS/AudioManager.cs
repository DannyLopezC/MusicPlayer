using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using Sirenix.OdinInspector;

namespace MusicPlayer
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioManager : MonoBehaviour
    {
        [InlineEditor]
        public List<Playlist> playLists;
        public AudioSource source;

        public int currentPlaylistId = 0;
        [ReadOnly]
        public Playlist currentPlaylist;
        [ReadOnly]
        public Playlist.Song currentSong;


        private void Awake()
        {
            currentPlaylist = playLists[currentPlaylistId];
            currentSong = currentPlaylist.musicList[0];
            source = GetComponent<AudioSource>();
            source.clip = currentSong.audioClip;
            //Play();
        }

        [Button, HorizontalGroup("Buttons")]
        public void Play()
        {

            if (source.clip == null)
            {
                Debug.Log($"Null song");
                return;
            }

            if (!source.isPlaying)
            {
                source.Play();
                CancelInvoke("Next");
                Invoke("Next", source.clip.length - source.time + 1f);
            }
            else
            {
                source.Pause();
                CancelInvoke("Next");
            }
        }

        [Button, HorizontalGroup("Buttons")]
        public void Stop()
        {
            CancelInvoke("Next");
            source.Stop();
        }

        [Button, HorizontalGroup("Buttons")]
        public void Next()
        {
            Debug.Log($"next");
            string currentSongName = source.clip.name;

            for (int i = 0; i < currentPlaylist.musicList.Count; i++)
            {
                if (currentPlaylist.musicList[i].songName == currentSongName)
                {
                    if (i + 1 > currentPlaylist.musicList.Count - 1)
                        currentSong = currentPlaylist.musicList[0];
                    else
                        currentSong = currentPlaylist.musicList[i + 1];
                }
            }

            source.clip = currentSong.audioClip;
            Play();
        }

        [Button, HorizontalGroup("Buttons")]
        public void Back()
        {
            Debug.Log($"back");
            string currentSongName = source.clip.name;

            for (int i = 0; i < currentPlaylist.musicList.Count; i++)
            {
                if (currentPlaylist.musicList[i].songName == currentSongName)
                {
                    if (i - 1 < 0)
                        currentSong = currentPlaylist.musicList[currentPlaylist.musicList.Count - 1];
                    else
                        currentSong = currentPlaylist.musicList[i - 1];
                }
            }

            source.clip = currentSong.audioClip;
            Play();
        }
    }
}
