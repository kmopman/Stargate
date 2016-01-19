using UnityEngine;
using System.Collections;

public class MiniMapCamera : MonoBehaviour
{
    //GameObject
    [SerializeField]
    private GameObject _controllerPlayer;

    //Vector3
    private Vector3 offset;

    void Start()
    {
        offset = _controllerPlayer.transform.position - transform.position;
    }

    void Update()
    {
        FollowSomething();
    }

    void FollowSomething()
    {
        if (_controllerPlayer != null)
            this.transform.position = _controllerPlayer.transform.position - offset; //Follows the player.
    }
}