using UnityEngine;
using System.Collections;

public class BattleFieldLights : MonoBehaviour {

    //GameObject
    [SerializeField]
    private GameObject _closesDoor;
    [SerializeField]
    private GameObject _battlefieldLights;
    [SerializeField]
    private GameObject _directionalLight;
    [SerializeField]
    private GameObject _intruderMessage;
    //GameObject

    //AudioSource
    [SerializeField]
    private AudioSource _alarmSFX;
    //AudioSource

    //Bool
    public bool startWave = false;
    private bool _playOnce = false;
    //Bool

    //Scripts
    [SerializeField]
    private SpawnSystem _waveCounters;
    //Scripts
	void Start () 
    {
        _battlefieldLights.SetActive(false); //Turn object off
        _closesDoor.SetActive(false); // Turn object off
        _intruderMessage.SetActive(false);
        
	}
    
    void OnTriggerEnter(Collider player)
    {
        if (player.tag == "Player")
        {
            if (!_playOnce)
            {
                _waveCounters._wavesCounter += 1;
                _alarmSFX.Play();
                _playOnce = true;
                _directionalLight.transform.Rotate(45f, 0f, 0f, Space.Self); //Change direction of lightning
                _battlefieldLights.SetActive(true); //Red lights!
                _closesDoor.SetActive(true); //Trapped onto the battlefield.

                _intruderMessage.SetActive(true);
                startWave = true;
                StartCoroutine("TurnLightsOff"); //Start a timer.
            }
        }
    }

    private IEnumerator TurnLightsOff()
    {
        yield return new WaitForSeconds(5);
        _intruderMessage.SetActive(false);
        Destroy(this.gameObject); //Remove object/script.
    }
}
