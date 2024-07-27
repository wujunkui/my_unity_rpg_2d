using System;
using System.Collections;
using TMPro;
using UnityEngine;
using Cinemachine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;


public class EntityFX : MonoBehaviour
{
    private SpriteRenderer sr;
    private Entity entity;

    [Header("After Image FX")] 
    [SerializeField] private GameObject afterImagePrefab;
    [SerializeField] private float colorLoseRate;
    [FormerlySerializedAs("lifeTime")] [SerializeField] private float afterImageLifeTime;
    private float afterImageCooldownTimer;

    [Header("Screen shake FX")] 
    private CinemachineImpulseSource screenShake;

    [SerializeField] private float shakeMultiplier = 0.2f;
    public Vector3 swordCatchShakePower;
    public Vector3 hitShakePower = new(0.5f,0);
    
    [Header("Hit Fx")] 
    [SerializeField] private GameObject hitFxPrefab;
    [SerializeField] private GameObject criticalHitFxPrefab;
    

    [Header("Flash FX")]
    [SerializeField] private Material hitMat;
    [SerializeField] private float flashDuration = .2f;
    private Material originalMat;
    
    [SerializeField] private GameObject popUpTextPrefab;
    
    [SerializeField] private ParticleSystem dustFx;

    private void Start()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
        originalMat = sr.material;
        screenShake = GetComponent<CinemachineImpulseSource>();
        entity = GetComponent<Entity>();
    }

    private void Update()
    {
        afterImageCooldownTimer -= Time.deltaTime;
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

    public void CreateHitFx(Transform _target)
    {
        float zRotation = Random.Range(-90, 90);
        float xPosition = Random.Range(-.5f, .5f);
        float yPosition = Random.Range(-.5f, .5f);
        GameObject newHitFX = Instantiate(hitFxPrefab, _target.position + new Vector3(xPosition, yPosition), Quaternion.identity);
        newHitFX.transform.Rotate(new Vector3(0, 0, zRotation));
        
        Destroy(newHitFX, .5f);
    }

    public void CreateCriticalHitFx(Transform _target)
    {
        float zRotation = Random.Range(-30, 30);
        float xPosition = Random.Range(-.5f, .5f);
        float yPosition = Random.Range(-.5f, .5f);
        GameObject newHitFX = Instantiate(criticalHitFxPrefab, _target.position + new Vector3(xPosition, yPosition), Quaternion.identity);
        var rotateVector = new Vector3(0, 0, zRotation);
        if (entity.facingDirection == 1)
            rotateVector.y = 180;
        //     newHitFX.transform.Rotate(new Vector3(0, 180, zRotation));
        // else
        // {
        //     newHitFX.transform.Rotate(new Vector3(0, 0, zRotation));
        // }
        newHitFX.transform.Rotate(rotateVector);
        
        Destroy(newHitFX, .5f);
    }

    public void CreatePopUpText(string _text)
    {
        CreatePopUpText(_text, Color.white, 0);
    }

    public void CreatePopUpText(string _text, Color _textColor, float _incrFontSize = 0)
    {
        float randomX = Random.Range(-0.5f, 0.5f);
        float randomY = Random.Range(1, 2.5f);
        Vector3 positionOffset = new Vector3(randomX, randomY, 0);
        GameObject newText = Instantiate(popUpTextPrefab, transform.position + positionOffset, Quaternion.identity);

        var newTextScript = newText.GetComponent<TextMeshPro>();
        newTextScript.text = _text;
        newTextScript.color = _textColor;
        newTextScript.fontSize += _incrFontSize;
    }
    
    public void PlayDustFX()
    {
        if (dustFx != null)
            dustFx.Play();
    }

    public void ScreenShake(Vector3 shakePower)
    {
        var player = PlayerManager.instance.player;
        screenShake.m_DefaultVelocity =
            new Vector3(shakePower.x * player.facingDirection, shakePower.y) * shakeMultiplier;
        screenShake.GenerateImpulse();
    }

    public void CreateAfterImage()
    {
        if (afterImageCooldownTimer < 0)
        {
            GameObject newAfterImage = Instantiate(afterImagePrefab, transform.position, transform.rotation);
            newAfterImage.GetComponent<FX.AfterImageFX>().SetupAfterImage(colorLoseRate, sr.sprite);
            afterImageCooldownTimer = afterImageLifeTime;
        }
    }
}