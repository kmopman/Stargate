using UnityEngine;
using System.Collections;

public class HorizontalRotation : MonoBehaviour {

    [SerializeField]
    private float _rotationSpeed = 5f; 

    /*
     * Decides how fast the object will horizontally rotate.
     * Serialized so that it's value can be edited in the editor.
     */

	void Update () 
    {
        RotateHorizontal();
	}

    void RotateHorizontal()
    {
        this.transform.Rotate(Vector3.up * _rotationSpeed, Space.Self); //Rotates THIS Game Object.
    }
}
