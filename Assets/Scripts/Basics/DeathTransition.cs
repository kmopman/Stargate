using UnityEngine;
using System.Collections;

public class DeathTransition : MonoBehaviour {

    [SerializeField]
    private GameObject _controllerPlayer;

    [SerializeField]
    private GameObject _deathCam;
    [SerializeField]
    private GameObject _canvasObject;

    [SerializeField]
    private AudioSource _earRingSFX;

	// Use this for initialization
	void Start () {
        
        _deathCam.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
	 if (_controllerPlayer == null)
     {
         _deathCam.SetActive(true);
         _canvasObject.SetActive(false);
         StartCoroutine("WaitForScene");
     }
	}

    IEnumerator WaitForScene()
    {
        _earRingSFX.Play();
        yield return new WaitForSeconds(4);
        Application.LoadLevel("GameOver");
    }
}
