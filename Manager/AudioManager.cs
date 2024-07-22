using System;
using System.Collections.Generic;
using SaveSystem;
using UnityEngine;

namespace Manager
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager instance;
        [SerializeField] private AudioSource[] sfx;
        [SerializeField] private AudioSource[] bgm;

        public bool playBGM;
        
        public AudioManager()
        {
            if (instance == null)
                instance = this;
            else
            {
                Destroy(instance.gameObject);
            }
        }


        private void Update()
        {
            if(!playBGM)
                StopAllBGM();
        }

        public void PlaySFX(int _sfxIndex)
        {
            if (_sfxIndex < sfx.Length)
            {
                sfx[_sfxIndex].Play();
            }
        }

        public void PlaySFX(int _index, Transform _target)
        {
            // todo: check distance to play audio
            PlaySFX(_index);
        }
        
        public void StopSFX(int _index) => sfx[_index].Stop();

        public void PlayBGM(int _bgmIndex)
        {
            StopAllBGM();

            bgm[_bgmIndex].Play();
        }

        private void StopAllBGM()
        {
            foreach (var t in bgm)
            {
                t.Stop();
            }
        }
    }
}