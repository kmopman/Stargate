using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    //Floats
    [SerializeField]
    private float _speed = 6f;
    [SerializeField]
    private float _maxSpeed = 12f;
    [SerializeField]
    private float _jumpSpeed = 8f;
    [SerializeField]
    private float _gravity = 20f;
    [SerializeField]
    private float _sprintTimer = 5f;
    //Floats

    //AudioSource
    [SerializeField]
    private AudioSource _footstepSFX;
    [SerializeField]
    private AudioSource _fastFootstepSFX;
    [SerializeField]
    private AudioSource _jumpSFX;
    //AudioSource

    


    //Vector3
    private Vector3 _moveDirection = Vector3.up; //Direction the player needs to go to.
    //Vector3
    
    //CharacterController
    private CharacterController _characterController; //Player uses a character controller.
    //CharacterController

    //Bool
    private bool _tiredPlayer = false;
    private bool _isPlayingSFX = false;
    //Bool


    void Start()
    {
        _characterController = GetComponent<CharacterController>(); //Grabs the Character Controller.
    }

    void Update()
    {
        PlayerMoving();
        PlayerSprint();
    }

    void PlayerMoving()
    {
        if (_characterController.isGrounded) //If the Character is on the ground.
        {
            _moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            _moveDirection = transform.TransformDirection(_moveDirection);
            _moveDirection *= _speed;
            


            if (Input.GetButton("Jump")) //If the Jump button has been pressed...
            {
                _moveDirection.y = _jumpSpeed; //Jump!
                _jumpSFX.Play();
                _fastFootstepSFX.Stop();
            } 

          
        }

        _moveDirection.y -= _gravity * Time.deltaTime;
        _characterController.Move(_moveDirection * Time.deltaTime);
    }

    void PlayerSprint()
    {
        if (Input.GetButtonDown("Sprint") || Input.GetKeyDown(KeyCode.LeftShift)) //If the Left Stick has been pressed:
        {

            _speed = _maxSpeed; //Make your speed faster!
            
            while (_sprintTimer >= 0)
                _sprintTimer -= 0.1f;

            if (!_isPlayingSFX)
            {
                _fastFootstepSFX.Play();
                _isPlayingSFX = true;
            }

        }
        else if (Input.GetButtonUp("Sprint") || Input.GetKeyUp(KeyCode.LeftShift)) //If Left Stick has been let go of:
        {
            _speed = 8f; //Turn speed back to normal.
            _fastFootstepSFX.Stop();
            _isPlayingSFX = false;
            while (_sprintTimer <= 5)
            _sprintTimer+= 0.1f;
        }

        if (_sprintTimer <= 0.1f)
            _tiredPlayer = true;
        else
            _tiredPlayer = false;
    }
}