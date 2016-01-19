﻿using UnityEngine;
using System.Collections;

public class HealPickUp : MonoBehaviour
{
    [SerializeField]
    private float _addedHp = -1f;//the ammount of health added(its in minus becouse it uses the damg system but reversed... programming is just like magic)

    [SerializeField]
    private AudioSource _pickupSFX;

    [SerializeField]
    private float _secondsUntilRemoval = 10f;
    private float _blinkSeconds = .5f;

    private bool _isPlayingSFX = false;

    void Update()
    {
        StartCoroutine("RemoveHealthPickUp");
    }
    
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {

            if (!_isPlayingSFX)
            {
                _pickupSFX.Play();
                _isPlayingSFX = true;
            }
            ;
            IDamageable damageableObject = other.GetComponent<IDamageable>();//check for component idamagable on the hit object
            if (damageableObject != null)//"if object has idamagable"
            {
                damageableObject.TakeDamg(_addedHp);//heal it(add negative damg)
            }
            Debug.Log(other.gameObject.name+ other.tag + "halp");
            
            GameObject.Destroy(gameObject);//destroy this object(the projectile)
        }
    }

    IEnumerator RemoveHealthPickUp()
    {
        yield return new WaitForSeconds(_secondsUntilRemoval);
        this.GetComponent<Renderer>().enabled = false;
        yield return new WaitForSeconds(_blinkSeconds);
        this.GetComponent<Renderer>().enabled = true;
        yield return new WaitForSeconds(_blinkSeconds);
        this.GetComponent<Renderer>().enabled = false;
        yield return new WaitForSeconds(_blinkSeconds);
        this.GetComponent<Renderer>().enabled = true;
        yield return new WaitForSeconds(_blinkSeconds);
        this.GetComponent<Renderer>().enabled = false;
        yield return new WaitForSeconds(_blinkSeconds);
        this.GetComponent<Renderer>().enabled = true;
        yield return new WaitForSeconds(_blinkSeconds);
        Destroy(this.gameObject);

    }
}