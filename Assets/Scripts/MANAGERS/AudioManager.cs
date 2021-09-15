﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using Sirenix.OdinInspector;

namespace MusicPlayer
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager instance;

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
            if (AudioManager.instance == null)
            {
                AudioManager.instance = this;
                DontDestroyOnLoad(AudioManager.instance);
            }
            else Destroy(AudioManager.instance);


            currentPlaylist = playLists[currentPlaylistId];
            currentSong = currentPlaylist.musicList[0];
            source = GetComponent<AudioSource>();
            source.clip = currentSong.audioClip;
        }

        [Button, HorizontalGroup("Buttons")]
        public void Play()
        {
            if (source.clip == null) return;

            if (!source.isPlaying) source.Play();
            else source.Pause();
        }

        [Button, HorizontalGroup("Buttons")]
        public void Next()
        {
            source.time = 0f;

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
        }

        [Button, HorizontalGroup("Buttons")]
        public void Back()
        {
            source.time = 0f;

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
        }
    }
}
