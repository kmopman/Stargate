using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour
{
    //Axes
    private enum RotationAxes { MouseXAndY = 0, MouseX = 1, MouseY = 2 }
    private RotationAxes axes = RotationAxes.MouseXAndY;
    //Axes

    //Floats
    [SerializeField]
    private float _sensitivityX = 10f;
    [SerializeField]
    private float _sensitivityY = 10f;

    private float _minimumX = -360f;
    private float _maximumX = 360f;

    private float _minimumY = -60f;
    private float _maximumY = 60f;

    float rotationY = 0f;

    private float _cameraSpeedFloat;
    //Floats

    //Transforms
    Transform cameraTransform;
    //Transforms


    void Start()
    {

        cameraTransform = transform; //Transform of the attached GameObject

        if (GetComponent<Rigidbody>())
        {
            GetComponent<Rigidbody>().freezeRotation = true;
        }
    }

    void Update()
    {
        LookDirection();
        LookMouse();
        AdjustSensitivity();
    }

    private void LookMouse()
    {
        if (axes == RotationAxes.MouseXAndY)
        {
            float rotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * _sensitivityX;

            rotationY += Input.GetAxis("Mouse Y") * _sensitivityY;
            rotationY = Mathf.Clamp(rotationY, _minimumY, _maximumY);

            cameraTransform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);
        }

        else if (axes == RotationAxes.MouseX)
        {
            cameraTransform.Rotate(0, Input.GetAxis("Mouse X") * _sensitivityX, 0);
        }

        else
        {
            rotationY += Input.GetAxis("Mouse Y") * _sensitivityY;
            rotationY = Mathf.Clamp(rotationY, _minimumY, _maximumY);

            cameraTransform.localEulerAngles = new Vector3(-rotationY, transform.localEulerAngles.y, 0);
        }
    }

    private void LookDirection()
    {

        if (axes == RotationAxes.MouseXAndY) 
        {
            float rotationX = transform.localEulerAngles.y + Input.GetAxis("RightStickY") * _sensitivityX;

            rotationY += Input.GetAxis("RightStickX") * _sensitivityY;
            rotationY = Mathf.Clamp(rotationY, _minimumY, _maximumY);

            cameraTransform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);
        }

        else if (axes == RotationAxes.MouseX)
        {
            cameraTransform.Rotate(0, Input.GetAxis("RightStickY") * _sensitivityX, 0);
        }

        else
        {
            rotationY += Input.GetAxis("RightStickX") * _sensitivityY;
            rotationY = Mathf.Clamp(rotationY, _minimumY, _maximumY);

            cameraTransform.localEulerAngles = new Vector3(-rotationY, transform.localEulerAngles.y, 0);
        }

    }

    private void AdjustSensitivity()
    {
        if (Input.GetAxis("Aim") < 0 || (Input.GetKeyDown(KeyCode.Mouse1))) //If the Left Stick has been pressed:
        {
            _sensitivityX = 1.5f;
            _sensitivityY = 1.5f;
        }
        else 
        {
            _sensitivityX = 4f;
            _sensitivityY = 4f;
        }

    }
   
}