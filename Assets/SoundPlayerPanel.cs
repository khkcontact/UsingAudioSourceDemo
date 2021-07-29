using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace SoundPlayerDemo
{
    public class SoundPlayerPanel : MonoBehaviour
    {
        // Start is called before the first frame update
        [SerializeField]
        SoundPlayer soundPlayer;

        [SerializeField]
        AudioClip audioFile;

        [SerializeField]
        Slider slider;

        [SerializeField]
        HandleDragDetector handleDragDetector;

        [SerializeField]
        Text endLabel;

        [SerializeField]
        Text curposText;

        [SerializeField]
        Button fastBackwardButton;

        [SerializeField]
        Button fastForwardButton;

        [SerializeField]
        Button playButton;

        [SerializeField]
        Button stopButton;

        [SerializeField]
        Button pauseButton;

        void Start()
        {
            if (soundPlayer == null)
                soundPlayer = FindObjectOfType<SoundPlayer>();

            stopButton.onClick.AddListener(soundPlayer.Stop);
            playButton.onClick.AddListener(OnPlayClicked);
            pauseButton.onClick.AddListener(onPauseClicked);
            fastForwardButton.onClick.AddListener(OnFastForwardClicked);
            fastBackwardButton.onClick.AddListener(OnFastBackClicked);

            slider.onValueChanged.AddListener(OnSliderValueChanged);

            handleDragDetector.Subscribe(OnHandleDragging);

            slider.value = 0;
            StartCoroutine(UpdateRoutine());
        }

        public bool isSliderDragging = false;
        

    void OnFastBackClicked() => soundPlayer.FastBackOnce();
        void OnFastForwardClicked() => soundPlayer.FastForwardOnce();
        void onPauseClicked() => soundPlayer.Pause();

        void OnHandleDragging(bool isPressed)
        {
            isSliderDragging = isPressed;
        }
        void OnSliderValueChanged(float newVal)
        {
            Debug.Log($"OnSliderValueChanged {newVal}");
            soundPlayer.SetPositionInRate(newVal);
        }

        void OnPlayClicked()
        {
            Debug.Log("OnPlayClicked");
            if (soundPlayer.IsPaused)
                soundPlayer.Resume();
            else
                soundPlayer.Play(audioFile);

        }

        void UpdatePosition()
        {
            endLabel.text = $"{soundPlayer.TotalSeconds():F3}";
            curposText.text = $"{soundPlayer.GetPositionInSeconds():F3}";
            if (soundPlayer.GetPositionInSamples() >= soundPlayer.TotalSamples()-1)
            {
                soundPlayer.Stop();
            }
           
            if(!isSliderDragging)// handleDragDetector.IsDragging )
                slider.value = soundPlayer.TotalSeconds()>0.1f?soundPlayer.GetPositionInSeconds()/ soundPlayer.TotalSeconds():1;
        }

        IEnumerator UpdateRoutine()
        {
            yield return null;
            while (true)
            {
                UpdatePosition();
                yield return 0.1f;
            }
        
        }



    }
}
