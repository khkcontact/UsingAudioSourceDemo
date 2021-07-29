using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SoundPlayerDemo
{
    public class SoundPlayer : MonoBehaviour
    {
        [SerializeField]
        AudioSource audioSource;

        [Header("rate of total lenght to fast forward")]
        [SerializeField]
        float FastForwardStepRate = 0.1f;
        // Start is called before the first frame update
        void Start()
        {
            if (audioSource == null)
                audioSource = GetComponent<AudioSource>();
            _isPaused = false;

        }

        public float TotalSeconds()
        {
            float ret = 0;
            if(audioSource?.clip !=null)
                ret = audioSource.clip.length;
            return ret;
        }

        public int TotalSamples()
        {
            int ret = 0;
            if (audioSource?.clip != null)
                ret = audioSource.clip.samples;

            return ret;
        }

        public void ToStart()
        {
            audioSource.timeSamples = 0;
        }
        public void SetPositionInRate(float newRate)
        {
            if (audioSource.clip == null)
            {
                Debug.Log("SetPositionInRate: clip not assigned");
                return;
            }
            int newVal = Mathf.RoundToInt( newRate * audioSource.clip.samples);
            Debug.Log($" Set position for rate to {newRate} as {newVal} of {audioSource.clip.samples}");
            audioSource.timeSamples = newVal;
        }
        public int GetPositionInSamples()
        {
           int ret = audioSource.timeSamples;
            
            return ret;
        }

        public float GetPositionInSeconds()
        {
            return audioSource.time;
        }

        public int StepLenght() => Mathf.RoundToInt(FastForwardStepRate*TotalSamples());

        public void FastBackOnce()
        {
            int newSamples = audioSource.timeSamples - StepLenght();
            if (newSamples < 0)
                newSamples = 0;
             audioSource.timeSamples = newSamples;
            // FastForwardStepRate

        }

        public void FastForwardOnce()
        {
            int newSamples = audioSource.timeSamples + StepLenght();
            if (newSamples > TotalSamples()-1)
                newSamples = TotalSamples() - 1;
            audioSource.timeSamples = newSamples;
        }

        public void Stop()
        {
            _isPaused = false;
            audioSource.Stop();
            ToStart();
        }
        public void Pause()
        {
            if (audioSource.isPlaying)
            {
                _isPaused = true;
                audioSource.Pause();
            }

        }

        public void Resume()
        {
            if (_isPaused)
            {
                audioSource.UnPause();
                _isPaused = false;
            }
        }

        [SerializeField]
        bool _isPaused;
        public bool IsPaused => _isPaused;



        public void Play(AudioClip newClip)
        {
            Stop();
            audioSource.clip = newClip;
            audioSource.Play();
        }



    }
}