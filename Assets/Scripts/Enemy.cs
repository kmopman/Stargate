using UnityEngine;
using System.Collections;

public class Enemy : LivingEntity//imports the living entity script
{
    Transform theBase;//the base pos
    Transform player; //who would this be o.o
    LivingEntity targetEntity;
    [SerializeField]
    private float _damage;

    [SerializeField]
    private float _attackRange = 1.5f;//the range of his attack...
    [SerializeField]
    private float _attackspeed = 1;//how fast it will attack
    private float _nextAttack;//how much time til it can attack again
    [SerializeField]
    private float _baseHp;//the base hp of this enemy
    [SerializeField]
    private LayerMask _unwalkableMask;//unwalkable layer
    [SerializeField]
    private int _rewardMoney;//the ammount of money the enemy gives
    [SerializeField]
    private int _baseMoney;//the base ammount before the spawner altered it

    private bool _nearPlayer;//is the enemy near the player
    private bool _nearBase;//is the enemy near base

    private GameObject _findSpawner;//finds spawner object
    private SpawnSystem _waveStats;//imports spawnsystem

    private GameObject _findObjectSpawner;//finds objectspawner object
    private ObjectSpawner _resourcesInfo;//we need only the resources

    [SerializeField]
    private AudioSource _punchSFX;
    private bool _isPlayingSFX = false;
    void Awake()
    {
        _findSpawner = GameObject.Find("Spawner");
        _waveStats = _findSpawner.GetComponent<SpawnSystem>();
        _findObjectSpawner = GameObject.Find("ObjectSpawner");
        _resourcesInfo = _findObjectSpawner.GetComponent<ObjectSpawner>();

    }
    protected override void Start()
    {
        base.Start();//gets the start from living entity
        _rewardMoney = _baseMoney + _baseMoney / 100 * _waveStats._percentMoney;
        startingHealth = _baseHp + _baseHp / 100 * _waveStats._percentHP;
        health = _baseHp + _baseHp / 100 * _waveStats._percentHP;
        //Debug.Log(startingHealth + " " + _baseHp + " " + _waveStats._percentHP);

        theBase = GameObject.FindGameObjectWithTag("Base").transform;

        player = GameObject.FindGameObjectWithTag("Player").transform;//player is the thing with the tag player(what a suprise)

    }
    protected override void death()
    {
        base.death();//gets the death from living entity
        _resourcesInfo.recources += _rewardMoney;//on death, lets add some money
    }

    // Update is called once per frame
    void Update () {
        //bool walkable = !(Physics.CheckSphere(transform.position, 2, _unwalkableMask));//checks if it is walkable or not
        bool ableToWalkToPlayer = !(Physics.CheckSphere(player.position, 1, _unwalkableMask));//checks if player is in a walkable space
        float sqrDistancePlayer = (player.position - transform.position).sqrMagnitude;
        float sqrDistanceBase = (theBase.position - transform.position).sqrMagnitude;
        if (sqrDistancePlayer < Mathf.Pow(7, 2) && ableToWalkToPlayer && !_nearPlayer && !_nearBase)
        {
           // Debug.Log("follow");
            float step = 15 * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, player.position, step);
        }

        if (player != null)
        {
            if (sqrDistancePlayer < Mathf.Pow(_attackRange, 2))
            {
                targetEntity = player.GetComponent<LivingEntity>();
                //_nearPlayer = true;
                Attack();
            }
            else
                _nearPlayer = false;
        }


        if (theBase != null)
        {
            if (sqrDistanceBase < Mathf.Pow(_attackRange, 2))
            {
                targetEntity = theBase.GetComponent<LivingEntity>();
                _nearBase = true;
                Attack();
            }
            else
                _nearBase = false;
        }
       
       

       

    }
    void Attack()
    {
        if (Time.time > _nextAttack)//is it time to attack?
        {
            _nextAttack = Time.time + _attackspeed;
            print("pow");
            print(_damage + "TestEnemy");
            targetEntity.TakeDamg(_damage);
            print(targetEntity + "TestEnemy");

            if (!_isPlayingSFX)
            {
                _punchSFX.Play();
                _isPlayingSFX = true;
            }
        }
    }
    //IEnumerator attack
}
