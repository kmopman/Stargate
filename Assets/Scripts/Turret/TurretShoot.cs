using UnityEngine;
using System.Collections;

public class TurretShoot : MonoBehaviour
{

    [SerializeField]
    private LayerMask enemyLayer;

    //GameObjects
    [SerializeField]
    private GameObject bulletObj;
    private GameObject _closestEnemy;
    //GameObjects

    //Floats
    [SerializeField]
    private float _targetRadius = 5f;
    //Floats

    //Bool
    [SerializeField]
    private bool spotEnemy = false;
    //Bool

    //Floats
    [SerializeField]
    private float _trackingSpeed = 5f;
    //Floats

    
    [SerializeField]
    private GameObject targetToLookAt;
    Vector3 m_lastKnownPosition = Vector3.zero;
    Quaternion m_lookAtRotation;


    void Start()
    {
        InvokeRepeating("FindEnemy", 0, 1f);
    }


    void FindEnemy()
    {
        Collider[] hitEnemies = Physics.OverlapSphere(this.gameObject.transform.position, _targetRadius, enemyLayer);


        float shortestDistance = float.MaxValue;

        for (int i = 0; i < hitEnemies.Length; i++)
        {
            float distance = Vector3.Distance(this.gameObject.transform.position, hitEnemies[i].transform.position);

            if (distance < shortestDistance)
            {
                _closestEnemy = hitEnemies[i].gameObject;
                shortestDistance = distance;
                spotEnemy = true;
            }
        }

        if (_closestEnemy != null)
        {

            ShootBullet();
                AimNozzle();
        }

    }

    void AimNozzle()
    {
        if (_closestEnemy != null)
        {
            if (m_lastKnownPosition != _closestEnemy.transform.position)
            {
                m_lastKnownPosition = _closestEnemy.transform.position;
                m_lookAtRotation = Quaternion.LookRotation(m_lastKnownPosition - transform.position);
            }

            if (transform.rotation != m_lookAtRotation)
            {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, m_lookAtRotation, _trackingSpeed * Time.deltaTime);
            }
        }
    }

    void SpawnBullets()
    {
        GameObject bulletToSpawn = Instantiate(bulletObj, transform.position, transform.rotation) as GameObject;
    }

    void OnDrawGizmosSelected()
    {
        if (this.gameObject.name == "TurretHeavy")
            Gizmos.color = Color.green;
        else
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(this.gameObject.transform.position, _targetRadius);
    }

    void ShootBullet()
    {
        SpawnBullets();
    }

}