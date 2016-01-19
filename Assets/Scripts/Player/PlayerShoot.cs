using UnityEngine;
using System.Collections;

public class PlayerShoot : MonoBehaviour
{
    //GameObjects
    [SerializeField]
    private GameObject _bulletToShoot; //Serialized Field for the bullet.
    [SerializeField]
    private GameObject _gunParticle;
    //GameObjects

    //AudioSource
    [SerializeField]
    private AudioSource _gunSFX;
    //AudioSource



    //Bools
    private bool _shooting = false; //Decides if the gun is either shooting or not.
    private bool _isPlayingSFX = false;
    //Bools

    void Start()
    {
        _gunParticle.SetActive(false);
    }

    void Update()
    {
        
        ShootingButton();
        Shooting();
    }

    void ShootingButton()
    {

        if (Input.GetAxisRaw("Shoot") != 0 || (Input.GetButton("Fire1"))) // If the Right Trigger has been pressed down...
        {
            if (!_isPlayingSFX)
            {
                _gunSFX.Play();
                _isPlayingSFX = true;
            }

            _shooting = true; //Shoot!
        }
        else
        {
            _gunSFX.Stop();
            _gunParticle.SetActive(false);
            _isPlayingSFX = false;
            _shooting = false; //Whenever this button has not been pressed, do not shoot at all!
        }
            
    }

    void Shooting()
    {
        if (_shooting) //While shooting...
        {
            _gunParticle.SetActive(true);
            Instantiate(_bulletToShoot, transform.position, Quaternion.identity); //Spawn bullets!
            _shooting = false;
        }
           
    }

  
}