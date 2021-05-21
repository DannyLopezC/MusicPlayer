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

        public int currentPlaylistId = 0;
        Playlist currentPlaylist;
        Playlist.Song currentSong;

        public Text title, timer;

        private void Start()
        {
            currentPlaylist = playLists[currentPlaylistId];
            currentSong = currentPlaylist.musicList[0];
            source = GetComponent<AudioSource>();
            source.clip = currentSong.audioClip;
            title.text = currentSong.songName;
            //Play();
        }

        private void Update()
        {
            SetTimer();


        }

        public void SetTimer()
        {
            if (source != null)
            {
                int minutes = (int)source.time / 60;
                int seconds = (int)source.time % 60;

                timer.text = $"{minutes.ToString("00")}:{seconds.ToString("00")}";
            }
        }

        public void Play()
        {
            Debug.Log($"play");

            if (source.clip == null)
            {
                Debug.Log($"Null song");
                title.text = "Null song";
                return;
            }

            title.text = currentSong.songName;
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

        public void Stop()
        {
            CancelInvoke("Next");
            source.Stop();
        }

        public void Next()
        {
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

        public void Back()
        {
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
