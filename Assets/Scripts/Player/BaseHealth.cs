using UnityEngine;
using System.Collections;

public class BaseHealth : LivingEntity
{
    [SerializeField]
    private GameObject _baseHealthBar;

    [SerializeField]
    private GameObject _deathCam;

    [SerializeField]
    private float _secondsToWait = 4f;


    private float _BaseHpScaler;

    protected override void Start()
    {
        base.Start();//gets the start from living entity
        _deathCam.SetActive(false);
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
    }

    void AdjustHealthBar()
    {
        _baseHealthBar.transform.localScale = new Vector3((health / _BaseHpScaler), 1f, 1f);

    }

    IEnumerator DeathTransition()
    {
        _deathCam.SetActive(true);
        yield return new WaitForSeconds(_secondsToWait);
        Application.LoadLevel("GameOver");
    }
}