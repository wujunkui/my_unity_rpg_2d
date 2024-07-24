using System;
using UnityEngine;

namespace FX
{
    public class AfterImageFX : MonoBehaviour
    {
        private SpriteRenderer sr;
        private float colorLoseRate;

        public void SetupAfterImage(float _loseSpeed, Sprite _spriteImage)
        {
            sr = GetComponent<SpriteRenderer>();
            sr.sprite = _spriteImage;
            colorLoseRate = _loseSpeed;
        }

        private void Update()
        {
            float alpha = sr.color.a - colorLoseRate * Time.deltaTime;
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, alpha);
            
            if(sr.color.a <= 0)
                Destroy(gameObject);
        }
    }
}