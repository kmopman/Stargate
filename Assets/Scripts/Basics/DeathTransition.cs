using UnityEngine;
using System.Collections;

public class DeathTransition : MonoBehaviour {

    //GameObject
    [SerializeField]
    private GameObject _controllerPlayer;
    [SerializeField]
    private GameObject _deathCam;
    [SerializeField]
    private GameObject _canvasObject;
    [SerializeField]
    private AudioSource _earRingSFX;
	//GameObject
	void Start ()
    {
        _deathCam.SetActive(false); // Set camera false.
	}
	

	void Update () 
    {
        CheckIfAlive();
	}

    void CheckIfAlive()
    {
        if (_controllerPlayer == null)
        {
            _deathCam.SetActive(true); 
            _canvasObject.SetActive(false);
            StartCoroutine("WaitForScene");

            /*
             * If the player doesn't exist in the game anymore...
             * Turn the death cam on.
             * Remove the UI.
             * Start running a function.
             */
        }
    }

    IEnumerator WaitForScene()
    {
        _earRingSFX.Play();
        yield return new WaitForSeconds(4); //Wait four seconds.
        Application.LoadLevel("GameOver"); //Load game over screen.
    }
}
