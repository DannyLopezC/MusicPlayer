using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;
using TMPro;

namespace MusicPlayer
{
    public class UIManager : MonoBehaviour
    {
        public AudioManager audioManager;

        [BoxGroup("UI")]
        public TMP_Text title;
        [BoxGroup("UI/Slider")]
        public Slider slider;
        [BoxGroup("UI/Slider")]
        public TMP_Text currentTimer, lengthTimer;

        private void Start()
        {
            slider.onValueChanged.AddListener(delegate { UpdateSlider(); });
            if (title != null) title.text = audioManager.currentSong.songName;
            UpdateSlider();
        }

        private void Update()
        {
            UpdateSlider(true);
        }

        private void UpdateSlider(bool fromUpdate = false)
        {
            if (!fromUpdate && audioManager.source.clip.length - 0.1 > slider.value) audioManager.source.time = slider.value * audioManager.source.clip.length;
            if (slider != null && fromUpdate) slider.value = audioManager.source.time / audioManager.source.clip.length;

            SetTimer();
        }

        public void SetTimer()
        {
            if (audioManager.source != null)
            {
                int currentMinutes = (int)audioManager.source.time / 60;
                int currentSeconds = (int)audioManager.source.time % 60;

                int lengthMinutes = (int)audioManager.source.clip.length / 60;
                int lengthSeconds = (int)audioManager.source.clip.length % 60;

                currentTimer.text = $"{currentMinutes.ToString("0")}:{currentSeconds.ToString("00")}";
                lengthTimer.text = $"{lengthMinutes.ToString("00")}:{lengthSeconds.ToString("00")}";
            }
        }

        public void OnPlay()
        {
            if (title != null)
            {
                if (audioManager.source.clip == null) title.text = "Null song";
                else title.text = audioManager.currentSong.songName;
            }
        }
    }
}
