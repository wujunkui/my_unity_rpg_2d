using System.Collections;
using TMPro;
using UnityEngine;


public class EntityFX : MonoBehaviour
{
    private SpriteRenderer sr;

    [Header("Hit Fx")] 
    [SerializeField] private GameObject hitFxPrefab;

    [Header("Flash FX")]
    [SerializeField] private Material hitMat;
    [SerializeField] private float flashDuration = .2f;
    private Material originalMat;


    [SerializeField] private GameObject popUpTextPrefab;


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

    public void CreateHitFx(Transform _target)
    {
        float zRotation = Random.Range(-90, 90);
        float xPosition = Random.Range(-.5f, .5f);
        float yPosition = Random.Range(-.5f, .5f);
        GameObject newHitFX = Instantiate(hitFxPrefab, _target.position + new Vector3(xPosition, yPosition), Quaternion.identity);
        newHitFX.transform.Rotate(new Vector3(0, 0, zRotation));
        
        Destroy(newHitFX, .5f);
    }

    public void CreatePopUpText(string _text)
    {
        CreatePopUpText(_text, Color.white);
    }

    public void CreatePopUpText(string _text, Color _textColor)
    {
        float randomX = Random.Range(-0.5f, 0.5f);
        float randomY = Random.Range(1, 2.5f);
        Vector3 positionOffset = new Vector3(randomX, randomY, 0);
        GameObject newText = Instantiate(popUpTextPrefab, transform.position + positionOffset, Quaternion.identity);

        var newTextScript = newText.GetComponent<TextMeshPro>();
        newTextScript.text = _text;
        newTextScript.color = _textColor;
    }
}