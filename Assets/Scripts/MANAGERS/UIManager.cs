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
        [BoxGroup("UI")]
        public TMP_Text title;
        [BoxGroup("UI/Slider")]
        public Slider slider;
        [BoxGroup("UI/Slider")]
        public TMP_Text currentTimer, lengthTimer;
        [BoxGroup("UI/PlayOrPause")]
        public Button play, pause;

        bool played;

        private void Awake()
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
                AudioManager.instance.source.time = Mathf.Min(slider.value * AudioManager.instance.source.clip.length, AudioManager.instance.source.clip.length - 0.01f);

            if (slider != null && fromUpdate) slider.value = AudioManager.instance.source.time / AudioManager.instance.source.clip.length;

            SetTimer();

        }

        public void SetTimer()
        {
            int currentMinutes = (int)AudioManager.instance.source.time / 60;
            int currentSeconds = (int)AudioManager.instance.source.time % 60;

            int lengthMinutes = (int)AudioManager.instance.source.clip.length / 60;
            int lengthSeconds = (int)AudioManager.instance.source.clip.length % 60;

            currentTimer.text = $"{currentMinutes.ToString("0")}:{currentSeconds.ToString("00")}";
            lengthTimer.text = $"{lengthMinutes.ToString("00")}:{lengthSeconds.ToString("00")}";
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

        public void OnNext() { AudioManager.instance.Next(); OnPlay(); }

        public void OnBack() { AudioManager.instance.Back(); OnPlay(); }
    }
}
