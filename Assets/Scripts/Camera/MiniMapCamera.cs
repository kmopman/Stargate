using UnityEngine;
using System.Collections;

public class MiniMapCamera : MonoBehaviour
{
    //GameObject
    [SerializeField]
    private GameObject _controllerPlayer;
    //GameObject


    /*
     * Drag and drop the object (in this case, the player).
     * Serialized so that the object could be dragged and dropped in the editor.
     */

    //Vector3
    private Vector3 offset;
    //Vector3
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
            this.transform.position = _controllerPlayer.transform.position - offset;

        /*
         * If the player still exists in the game...
         * Adapt the camera's position to the player's position.
         * Camera follows player!
         */
    }
}