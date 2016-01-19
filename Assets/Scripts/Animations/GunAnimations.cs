using UnityEngine;
using System.Collections;

public class GunAnimations : MonoBehaviour {

    //GameObjects
    [SerializeField]
    private GameObject _mainCamera; //Drags the camera
    //GameObjects

    //Floats
    [SerializeField]
    private float _maxCamZoom = 30f; //Max the camera can zoom in.

    [SerializeField]
    private float _zoomSpeed = 5f; //Speed the camera zooms in with.
    //Floats

    //Camera
    private Camera _cameraFOV; //Field Of View.
    //Camera

    //Bools
    private bool _cameraZoomIn = false;
    //Bools

    //Animator
    Animator anim;
    Animator camAnim;
    //Animator

    //AudioSource
    [SerializeField]
    private AudioSource _zoomOutSFX;
    [SerializeField]
    private AudioSource _zoomInSFX;
    //AudioSource

    void Start()
    {
        anim = GetComponent<Animator>(); //Grabs the Animator component of the attached GameObject.
        camAnim = _mainCamera.GetComponent<Animator>(); //Grabs the Camera's animator.
       _cameraFOV = _mainCamera.GetComponent<Camera>(); //Grabs the Camera for it's FOV.
    }

	void Update () 
    {
        Zooming();
        AimAnimation();
        SprintAnimation();
        JumpAnimation();

  
	}

    void AimAnimation()
    {
        if (Input.GetAxis("Aim") < 0 || (Input.GetKey(KeyCode.Mouse1))) //If Left Trigger has been pressed down...
        {
            _zoomOutSFX.Play();
            _cameraZoomIn = true; //Camera zooming in!
            anim.SetBool("FocusGun", true);
            anim.SetBool("ReturnGun", false);
            anim.SetBool("NeutralGun", false);
            anim.SetBool("SprintGun", false);
        }

        else //If Left Trigger has been let go of...
        {
            _zoomInSFX.Play();
            _cameraZoomIn = false; //Zooms out camera.
            anim.SetBool("FocusGun", false);
            anim.SetBool("ReturnGun", true);
            anim.SetBool("NeutralGun", true);
        }
    }

    void SprintAnimation()
    {
        if (Input.GetButtonDown("Sprint") || (Input.GetKeyDown(KeyCode.LeftShift))) //If Left Stick has been pressed down...
        {
            anim.SetBool("SprintGun", true);
            anim.SetBool("FocusGun", false);
            anim.SetBool("NeutralGun", false);
            anim.SetBool("ReturnGun", false);
            anim.SetBool("NeutralGun", false);
            camAnim.SetBool("NeutralCam", false);
            camAnim.SetBool("SprintCam", true);

        }


        if (Input.GetButtonUp("Sprint") || (Input.GetKeyUp(KeyCode.LeftShift))) //Left stick has been let go of...
        {
            anim.SetBool("FocusGun", false);
            anim.SetBool("NeutralGun", true);
            anim.SetBool("ReturnGun", false);
            anim.SetBool("SprintGun", false);
            camAnim.SetBool("SprintCam", false);
            camAnim.SetBool("NeutralCam", true);
        } 
    }

    void JumpAnimation()
    {
        if (Input.GetButton("Jump"))
        {
            anim.SetBool("FocusGun", false);
            anim.SetBool("NeutralGun", true);
            anim.SetBool("ReturnGun", false);
            anim.SetBool("SprintGun", false);
            camAnim.SetBool("SprintCam", false);
            camAnim.SetBool("NeutralCam", true);
        }
    }

    void Zooming()
    {
        if (_cameraZoomIn) //If boolean is true...
        {
            if (_cameraFOV.fieldOfView >= _maxCamZoom)
            {
                _cameraFOV.fieldOfView -= _zoomSpeed; //Zoom in!
            }
        }
        else
        {
            if (_cameraFOV.fieldOfView <= 59)
            {
                _cameraFOV.fieldOfView += _zoomSpeed; //Zoom out...
            }
        }
    }
}
