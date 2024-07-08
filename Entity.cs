using System;
using System.Collections;
using UnityEngine;


public class Entity : MonoBehaviour
{ 
    public Animator anim { get; private set; }
    protected SpriteRenderer sr { get; private set; }

    public EntityFX fx; 

    protected virtual void Start()
    {
        fx = GetComponent<EntityFX>();
        anim = GetComponentInChildren<Animator>();
        sr = GetComponentInChildren<SpriteRenderer>();
    }

    protected virtual void Update()
    {
        
    }


    public virtual void OnDamage()
    {
        fx.StartCoroutine("FlashFX");
        Debug.Log(gameObject.name + "was onDamaged!");
    }
    
}
