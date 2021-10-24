using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MusicPlayer
{
    public class PlaylistManager : MonoBehaviour
    {
        public static PlaylistManager instance;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(PlaylistManager.instance);
            }
            else Destroy(instance);
        }
    }
}