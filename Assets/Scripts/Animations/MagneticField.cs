using UnityEngine;
using System.Collections;

public class MagneticField : MonoBehaviour
{

    //GameObject
    [SerializeField]
    private GameObject _MagneticField;

    //AudioSource
    [SerializeField]
    private AudioSource _gateSFX;
    //AudioSource

    //Float
    [SerializeField]
    private float _movementSpeed = 5f;
    [SerializeField]
    private float _waitForDissapear = 15f;

    //Material
    [SerializeField]
    private Material _greenColor;

    //Renderer
    private Renderer _doorColor;

    //Color32
    private Color32 _setDoorColor;

    //Bool
    private bool _isMoving = false;
    private bool _playOnce = false;

    void Start()
    {
        _setDoorColor = new Color(0f, 255f, 118f, 125f); 
        _doorColor = _MagneticField.GetComponent<Renderer>();
    }

    void Update()
    {
        MoveDoor();
    }

    private void MoveDoor()
    {
        if (_isMoving)
        {
            StartCoroutine("RemoveField");
            _MagneticField.transform.Translate((Vector3.left * _movementSpeed) * Time.deltaTime, Space.Self);
            
        }
            
    }

    private void OnTriggerEnter(Collider player)
    {
        if (player.tag == "Player")
        {
            _doorColor.material = _greenColor; //Turn color into green.
            _isMoving = true; //Move door.

            if (!_playOnce)
            {
                _gateSFX.Play();
                _playOnce = true;
            }
            
            
        }
    }

    IEnumerator RemoveField()
    {
        yield return new WaitForSeconds(_waitForDissapear);
        Destroy(this.gameObject);
    }
}
