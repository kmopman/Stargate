using UnityEngine;
using System.Collections;

public class BaseHealth : LivingEntity
{
    [SerializeField]
    private GameObject _baseHealthBar; //Drag and drop the Base's health bar.

    [SerializeField]
    private GameObject _deathCam; //Drag and drop the camera that gets triggered.

    [SerializeField]
    private float _secondsToWait = 4f; //Seconds to wait... editable in editor.


    private float _BaseHpScaler; 

    protected override void Start()
    {
        base.Start();// Grabs the start from the LivingEntity.cs script.
        _deathCam.SetActive(false); //Sets the camera false.
        _BaseHpScaler = (health / 1);

    }


    void Update()
    {
        AdjustHealthBar();
        ToDeath();
        
    }

    void ToDeath()
    {
        if (health <= 0 && !dead)

            StartCoroutine("DeathTransition");


        /* If your health is under 0, and you're not dead yet...
         * Start running a function.
         */
    }

    void AdjustHealthBar()
    {
        _baseHealthBar.transform.localScale = new Vector3((health / _BaseHpScaler), 1f, 1f); //Adjust the health bar to it's damage.

    }

    IEnumerator DeathTransition()
    {
        _deathCam.SetActive(true);
        yield return new WaitForSeconds(_secondsToWait);
        Application.LoadLevel("GameOver");

        /*
         * Whenever you're dead, the Main Camera gets removed, and the Death Camera gets activated.
         * The amount of seconds you entered start running until the next line of code gets to run...
         * After that, load another scene, which is in this case, a Game Over! You lost.
         */
    }
}