using UnityEngine;
using System.Collections;

public class HorizontalRotation : MonoBehaviour {

    [SerializeField]
    private float _rotationSpeed = 5f;

	void Update () 
    {
        this.transform.Rotate(Vector3.up * _rotationSpeed,Space.Self);
	}
}
