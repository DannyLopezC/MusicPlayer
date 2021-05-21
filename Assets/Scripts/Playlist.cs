using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace MusicPlayer
{
    [System.Serializable, CreateAssetMenu(fileName = "Playlist_", menuName = "MusicPlayer/Playlists")]
    public class Playlist : ScriptableObject
    {
        [System.Serializable]
        public class Song
        {
            public AudioClip audioClip;
            public int id;
            public string songName;
        }

        public string playListName;
        public int id;
        public List<Song> musicList;

        [Button]
        public void SetValues()
        {
            for (int i = 0; i < musicList.Count; i++)
            {
                musicList[i].songName = musicList[i].audioClip.name;
                musicList[i].id = i;
            }
        }
    }
}
