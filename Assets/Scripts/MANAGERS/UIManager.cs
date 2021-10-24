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
        [BoxGroup("UI")] public TMP_Text title;
        [BoxGroup("UI/Slider")] public Slider slider;
        [BoxGroup("UI/Slider")] public TMP_Text currentTimer, lengthTimer;
        [BoxGroup("UI/PlayOrPause")] public Button play, pause;

        private bool played;

        private void Start()
        {
            if (title != null) title.text = AudioManager.instance.currentSong.songName;
            UpdateSlider();
        }

        private void Update()
        {
            UpdateSlider(true);

            if (played)
            {
                if (AudioManager.instance.source.isPlaying)
                {
                }
                else
                {
                    OnNext();
                }
            }
        }

        public void UpdateSlider(bool fromUpdate = false)
        {
            if (AudioManager.instance.source == null) return;

            if (!fromUpdate && AudioManager.instance.source.clip.length - 0.1 > slider.value)
            {
                var clip = AudioManager.instance.source.clip;
                AudioManager.instance.source.time = Mathf.Min(slider.value * clip.length,
                    clip.length - 0.01f);
            }

            if (slider != null && fromUpdate)
                slider.value = AudioManager.instance.source.time / AudioManager.instance.source.clip.length;

            SetTimer();
        }

        private void SetTimer()
        {
            var time = AudioManager.instance.source.time;
            int currentMinutes = (int) time / 60;
            int currentSeconds = (int) time % 60;

            var clip = AudioManager.instance.source.clip;
            int lengthMinutes = (int) clip.length / 60;
            int lengthSeconds = (int) clip.length % 60;

            currentTimer.text = $"{currentMinutes:0}:{currentSeconds:00}";
            lengthTimer.text = $"{lengthMinutes:00}:{lengthSeconds:00}";
        }

        public void OnPlay()
        {
            if (title != null)
            {
                if (AudioManager.instance.source.clip == null) title.text = "Null song";
                else title.text = AudioManager.instance.currentSong.songName;
            }

            play.gameObject.SetActive(false);
            pause.gameObject.SetActive(true);

            AudioManager.instance.Play();
            played = true;
        }

        public void OnPause()
        {
            play.gameObject.SetActive(true);
            pause.gameObject.SetActive(false);

            played = false;
            AudioManager.instance.Play();
        }

        public void OnNext()
        {
            AudioManager.instance.Next();
            OnPlay();
        }

        public void OnBack()
        {
            AudioManager.instance.Back();
            OnPlay();
        }
    }
}