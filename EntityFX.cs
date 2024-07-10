using System;
using System.Collections;
using UnityEngine;


public class EntityFX : MonoBehaviour
{

    private SpriteRenderer sr;
    [Header("Flash FX")]
    [SerializeField] private Material hitMat;

    [SerializeField] private float flashDuration = .2f;
    private Material originalMat;
    

    private void Start()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
        originalMat = sr.material;
    }

    private IEnumerator FlashFX()
    {
        sr.material = hitMat;
        yield return new WaitForSeconds(flashDuration);
        sr.material = originalMat;
    }

    private void RedColorBlink()
    {
        if (sr.color != Color.white)
            sr.color = Color.white;
        else
            sr.color = Color.red;

    }

    private void CancelRedBlink()
    {
        CancelInvoke();
        sr.color = Color.white;
    }
    
}