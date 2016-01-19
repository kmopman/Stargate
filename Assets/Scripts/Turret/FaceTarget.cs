using UnityEngine;
using System.Collections;

public class FaceTarget : MonoBehaviour
{


    //LayerMasks
    [SerializeField]
    private LayerMask enemyLayer;
    //LayerMasks

    //Transforms
    private Transform _targetTransform;
    //Transforms

    //Floats
    private float atan2;
    [SerializeField]
    private float _trackingSpeed = 5f;
    //Floats

    //Floats
    [SerializeField]
    private float _targetRadius = 5f;
    //Floats

    //GameObjects
    private GameObject _closestEnemy;
    private GameObject[] _towerObjects;
    [SerializeField]
    private GameObject[] _enemyObjects;
    //GameObjects

    //Vector3
    private Vector3 v_diff;
    //Vector3


    void Start()
    {
        _enemyObjects = GameObject.FindGameObjectsWithTag("Enemy");
    }


    void Update()
    {
        FindEnemy();
        RotateEnemies();
    }

    void FindEnemy()
    {
        Collider[] hitEnemies = Physics.OverlapSphere(this.gameObject.transform.position, _targetRadius, enemyLayer); 
        //Make a sphere for all colliders that enter.


        float shortestDistance = float.MaxValue;

        for (int i = 0; i < hitEnemies.Length; i++)
        {
            float distance = Vector3.Distance(this.gameObject.transform.position, hitEnemies[i].transform.position); 
            //Calculate distance between attached object and object that enters the circle.

            if (distance < shortestDistance)
            {
                _closestEnemy = hitEnemies[i].gameObject;
                shortestDistance = distance;
            }
        }
    }



    void RotateTowers()
    {

        foreach (GameObject towers in _towerObjects)
        {
            if (_closestEnemy != null)
            {
                if (this.gameObject.tag == "Tower" || (this.gameObject.tag) == "Bullet")
                {
                    _targetTransform = _closestEnemy.transform;
                    v_diff = (_targetTransform.position - transform.position);
                    if (this.gameObject.name == ("Cannon(Clone)") || this.gameObject.name == "Bullet(Clone)")
                        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0f, 0f, (atan2 * Mathf.Rad2Deg) - 90), _trackingSpeed * Time.deltaTime);
                    else
                        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0f, 0f, (atan2 * Mathf.Rad2Deg) - 180), _trackingSpeed * Time.deltaTime);
                    atan2 = Mathf.Atan2(v_diff.y, v_diff.x);

                }
            }
        }
        }

    void RotateEnemies()
    {
        foreach (GameObject enemies in _enemyObjects)
        {
            if (_closestEnemy != null)
            {
                _targetTransform = _closestEnemy.transform;
                v_diff = (_targetTransform.position - transform.position);
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0f, 0f, (atan2 * Mathf.Rad2Deg) -90), _trackingSpeed * Time.deltaTime);
                atan2 = Mathf.Atan2(v_diff.y, v_diff.x);

            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(this.gameObject.transform.position, _targetRadius);
    }
}