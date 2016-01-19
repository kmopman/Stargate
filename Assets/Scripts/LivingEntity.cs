using UnityEngine;
using System.Collections;

public class LivingEntity : MonoBehaviour, IDamageable
{
    public float startingHealth;//how much health at the start
    protected float health;//how much health
    protected bool dead;//to be or not to be :)

    public event System.Action OnDeath;

    [SerializeField]
    private GameObject _healthPickup;
    [SerializeField]
    private GameObject _explosionObject;
    [SerializeField]
    private AudioSource _explosionSF;

    protected virtual void Start()
    {
        health = startingHealth;//sets health
       
    }

    public void TakeDamg(float damage)
    {
        health -= damage;
        if (health <= 0 && !dead)
        {
            death();

            if(gameObject.tag == "Player" || gameObject.tag == "Base")
            {
                print("killed by mayro minion");
            }
        }
    }

    virtual protected void death()//when would this be used >_>
    {
        dead = true;
        if (OnDeath != null)
            OnDeath();
        
        Instantiate(_explosionObject, gameObject.transform.position, Quaternion.identity);
        Instantiate(_healthPickup, gameObject.transform.position, Quaternion.identity);
        _explosionSF.Play();

        GameObject.Destroy(gameObject);
    }

   
}