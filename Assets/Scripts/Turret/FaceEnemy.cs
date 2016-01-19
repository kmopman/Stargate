using UnityEngine;
using System.Collections;

public class FaceEnemy : MonoBehaviour
{
    [SerializeField]
    private float _trackingSpeed = 80f;
    [SerializeField]
    private GameObject targetToLookAt;
    Vector3 m_lastKnownPosition = Vector3.zero;
    Quaternion m_lookAtRotation;

    //LayerMasks
    [SerializeField]
    private LayerMask enemyLayer;
    //LayerMasks

    //Floats
    [SerializeField]
    private float _targetRadius = 5f;
    //Floats

    //GameObjects
    private GameObject _closestEnemy;
    //GameObjects


    void Update()
    {
        

        if (this.gameObject.tag == "Enemy")
            LookAtTarget();
        else
            FindEnemy();
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
                targetToLookAt = hitEnemies[i].gameObject;
                shortestDistance = distance;

                TargetSwitcher();
                
            }
        }
    }

    void TargetSwitcher()
    {
        LookAtTarget();    
    }

    void LookAtTarget()
    {
        if (targetToLookAt != null)
        {
            if (m_lastKnownPosition != targetToLookAt.transform.position)
            {
                m_lastKnownPosition = targetToLookAt.transform.position;
                m_lookAtRotation = Quaternion.LookRotation(m_lastKnownPosition - transform.position);
            }

            if (transform.rotation != m_lookAtRotation)
            {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, m_lookAtRotation, _trackingSpeed * Time.deltaTime);
            }
        }
    }

    bool SetTarget(GameObject target)
    {
        if (!target)
        {
            return false;
        }

        targetToLookAt = target;

        return true;
    }
}