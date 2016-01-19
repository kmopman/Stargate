using UnityEngine;
using System.Collections;

public class TurretProjectile : MonoBehaviour
{
    //LayerMasks
    [SerializeField]
    private LayerMask _collisionMask;//layer wich the projectile checks for
    //LayerMasks

    //Float

    [SerializeField]
    private float _maxRange = 100;//the max range the bullet will go
    private float _rangeTraveled;//how much the bullet has gone
    [SerializeField]
    private float _projectileSpeed = 10;//the speed of the projectile >_>
    [SerializeField]
    private float _damage = 1;//the ammount of damg this thing does
    //Float
    
    //Vector3
    private Vector3 _turretRotation;
    //Vector3

    //GameObject
    private GameObject _getPlayerRotation;
    [SerializeField]
    private GameObject _getTurretRotation;
    //GameObject

    public void SetSpeed(float newSpeed)//here otherclasses can change the speed of the projectile(incase of more guns or something like that)
    {
        _projectileSpeed = newSpeed;
    }

    void Start()
    {
        _rangeTraveled = 0;
        _turretRotation = _getTurretRotation.gameObject.transform.TransformDirection(Vector3.forward);
    }
    void Update()
    {
        ProjectileTurret();
    }


    void ProjectileTurret()
    {
        float moveDistance = _projectileSpeed * Time.deltaTime;//this calculates the distance it moves before actualy moving
        if (_maxRange > _rangeTraveled)//is the max range still bigger than range traveled?
        {
            CheckCollisions(moveDistance);
            transform.Translate(_turretRotation * moveDistance);//this moves the projectile forward
            _rangeTraveled += moveDistance;
        }
        else
        {
            GameObject.Destroy(gameObject);//destroy this object(the projectile)
        }
    }

    void CheckCollisions(float moveDistance)//checks if the projectile hits something before hitting it
    {
        Ray ray = new Ray(transform.position, transform.forward);//defines a ray that gets a starting pos and a direction
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, moveDistance, _collisionMask, QueryTriggerInteraction.Collide))//actual raycast, queryTriggerInteraction allows me to decide if i want it to collide with triggers to, wich is what i want in this case
        {
            OnHitObject(hit);
        }
    }

    void OnHitObject(RaycastHit hit)//this wil take in information abbout the object hit
    {
        IDamageable damageableObject = hit.collider.GetComponent<IDamageable>();//check for component idamagable on the hit object
        if (damageableObject != null)//"if object has idamagable"
        {
            damageableObject.TakeDamg(_damage);//damage it
        }
        Debug.Log(hit.collider.gameObject.name);
        GameObject.Destroy(gameObject);//destroy this object(the projectile)
    }
}