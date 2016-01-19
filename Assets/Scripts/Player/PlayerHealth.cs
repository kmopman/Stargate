using UnityEngine;
using System.Collections;

public class PlayerHealth : LivingEntity//imports the living entity script
{
    [SerializeField]
    private GameObject _deathCam;

    [SerializeField]
    private GameObject _HealthBar;

    [SerializeField]
    private GameObject _baseObject;


    [SerializeField]
    private float _secondsToWait = 4f;
    private float _HpScaler;

    protected override void Start()
    {
        base.Start();//gets the start from living entity
        _deathCam.SetActive(false);
        _HpScaler = (health / 1);
    }

    void Update()
    {
        AdjustHealthBar();
        ToDeath();
      
    }

    void ToDeath()
    {
        if (health <= 0 && !dead || _baseObject == null)
            StartCoroutine("DeathTransition");
    }


    void AdjustHealthBar()
    {
        _HealthBar.transform.localScale = new Vector3((health / _HpScaler), 1f, 1f);
    }

    IEnumerator DeathTransition()
    {
        _deathCam.SetActive(true);
        yield return new WaitForSeconds(_secondsToWait);
        Application.LoadLevel("GameOver");
    }
}