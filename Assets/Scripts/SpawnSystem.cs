using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SpawnSystem : MonoBehaviour {
    [SerializeField]
    private Wave[] _waves;//ammount of waves
    [SerializeField]
    private EnemyKind[] _enemies;//import enemy class

    private Enemy _enemy;//the enemy that gets spawned

    [SerializeField]
    private float _maxTimeBetweenWaves;//the max ammount of time you get between waves
    private float _maxTimeCounter;//actual vallue that counts down to 0
    [SerializeField]
    private bool _112233;//volgorde soort ennemies, eerst een dan ander
    [SerializeField]
    private bool _123123;//volgorde soort ennemies, om de beurd
    [SerializeField]
    private bool _232113;////volgorde soort ennemies random
    private EnemyKind _currentEnemy;//has the difrend kinds of enemies
    private Wave _currentWave;//has the info from current wave

    private float _nextSpawnTime;//for checking if if it is time for the next enemy to spawn
    [SerializeField]
    public float _wavesCounter;//for counting waves

    public bool waveDone;//checks if wave is done

    private int _currentWaveNum;//which number the current wave is
    private int _enemiesToSpawn;//how much are yet to spawn, if 0 then no enemies wil be spawned for this wave
    private int _enemiesAlive;//the ammount of enemies that are still alive(includes enemies yet to be spawned
    [HideInInspector]
    public int _percentHP;//percentage hp wat er bij komt
    public int _percentSpeed;//percentage movementspeed wat er bij komt
    public int _percentMoney;//percentage money(recources)

    private GameObject _timerText;//timer object
    private GameObject _wavesText;
    [SerializeField]
    private GameObject _waveDoneText;
    [SerializeField]
    private AudioSource _waveSFX;

    private bool _isPlayingSFX = false;

    void Awake()
    {
        _wavesCounter = 0;
        NextWave();
        _timerText = GameObject.Find("TimeText");//find timer object
        _wavesText = GameObject.Find("WaveText");
        _waveDoneText.SetActive(false);
    }

    void Update()
    {
        if(_wavesCounter>= 1)
        {
            spawnCheck();
            timerCheck();
            WavesCheck();
        }
        
    }

    void WavesCheck()
    {
        _wavesText.GetComponent<Text>().text = "Waves : " + _wavesCounter;

        if (_wavesCounter >= 10)
        {
            Application.LoadLevel("Win");
        }

        if (waveDone)
        {
            if (!_isPlayingSFX)
            {
                StartCoroutine("ShowWaveMessage");
                _isPlayingSFX = true;
            }
            
        }
    }

    void timerCheck()
    {
        _timerText.GetComponent<Text>().text = "Time until next wave: " + (Mathf.Round(_maxTimeCounter) - 1);

        if (Mathf.Round(_maxTimeCounter) <= 0)
        {
            _timerText.SetActive(false);
        }
        else
        {
            _timerText.SetActive(true);
        }
    }
    void spawnCheck()
    {
        if (_enemiesToSpawn > 0 && Time.time > _nextSpawnTime)//checks if there are enemies left to spawn this wave and if it is time to spawn a enemy, if it executes it ajusts the amount of enemies that need to spawn, resets the timer and spawns a enemy
        {
            _enemiesToSpawn--;
            _nextSpawnTime = Time.time + _currentWave.spawnTime;

            if(_112233)//functie die er voor zorgt dat als 112233 true is dat de eerste eerst worden gespawned dan de 2e en dan de 3e
            {
                if(_currentWave.enemyCount1 != 0)
                {
                    _currentEnemy = _enemies[0];
                    _enemy = _currentEnemy.enemy;
                    _currentWave.enemyCount1--;
                }
                else if (_currentWave.enemyCount2 != 0)
                {
                    _currentEnemy = _enemies[1];
                    _enemy = _currentEnemy.enemy;
                    _currentWave.enemyCount2--;
                }
                else if (_currentWave.enemyCount3 != 0)
                {
                    _currentEnemy = _enemies[2];
                    _enemy = _currentEnemy.enemy;
                    _currentWave.enemyCount3--;
                }
                else if (_currentWave.enemyCount4 != 0)
                {
                    _currentEnemy = _enemies[3];
                    _enemy = _currentEnemy.enemy;
                    _currentWave.enemyCount4--;
                }
                else if (_currentWave.enemyCount5 != 0)
                {
                    _currentEnemy = _enemies[4];
                    _enemy = _currentEnemy.enemy;
                    _currentWave.enemyCount5--;
                }
            }
            if(_123123)//wip
            {


            }
            if(_232113)//wip
            {

            }
            Enemy spawnedEnemy = Instantiate(_enemy, transform.position, Quaternion.identity) as Enemy;
            spawnedEnemy.OnDeath += OnEnemyDeath;//when ondeath is called, onenemydeath wil be called to
        }
        if (waveDone == true)
        {
            _maxTimeCounter -= Time.deltaTime;
            //Debug.Log(Mathf.Round(_maxTimeCounter));
            if (_maxTimeCounter <= 0)
            {
                NextWave();
                _wavesCounter++;
                if(_wavesCounter == 10)
                {
                    print("hello there wave 10");
                }
                waveDone = false;
            }
        }
    }

    IEnumerator ShowWaveMessage()
    {
        
        if (!_isPlayingSFX)
        {
            _waveSFX.Play();
            _isPlayingSFX = true;
        }
        _waveDoneText.SetActive(true);
        yield return new WaitForSeconds(5);
        _waveDoneText.SetActive(false);
    }

    void OnEnemyDeath()//decreases the int enemies alive and checks if next wave can start
    {
        //print("i dieded");
        _enemiesAlive--;
        if(_enemiesAlive == 0)
        {
            waveDone = true;
            _maxTimeCounter = _maxTimeBetweenWaves;
        }
    }

    void NextWave()//ads 1 to the wave number and sets the currentwave to a new wave and sets the value's given by the new wave  
    {
        _currentWaveNum++;
        if (_currentWaveNum - 1 < _waves.Length)
        {
            _currentWave = _waves[_currentWaveNum - 1];
            _currentWave.enemyCountTotal = _currentWave.enemyCount1 + _currentWave.enemyCount2 + _currentWave.enemyCount3 + _currentWave.enemyCount4 ;
            _enemiesToSpawn = _currentWave.enemyCountTotal;
            _enemiesAlive = _enemiesToSpawn;
            _percentHP = _currentWave.addedPercentageHealth;
            _percentSpeed = _currentWave.addedPercentageMovespeed;
            _percentMoney = _currentWave.addedPercentageDeathMoney; 
            
        }
    }

    [System.Serializable]
    public class EnemyKind
    {
        public Enemy enemy;
    }

    [System.Serializable]
    public class Wave//class for edditing the waves
    {
        public int enemyCount1;//how much nr 1 enemies
        public int enemyCount2;//how much nr 2 enemies
        public int enemyCount3;//how much nr 3 enemies
        public int enemyCount4;//how much nr 4 enemies
        public int enemyCount5;//how much nr 5 enemies
        public int enemyCountTotal;//how much enemies in the wave total
        public float spawnTime;//how much time inbetween enemy spawns
        public int addedPercentageHealth;//each ennemy has a static(not yet static this version) base health that wont be changed, to make each wave harder the health that the enemy starts with is baseHealth + (basehealth/100 * addedPercentageHealth). 
        public int addedPercentageMovespeed;//each ennemy has a static(not yet static this version) base movespeed that wont be changed, to make each wave harder the speed that the enemy starts with is baseHealth + (basespeed/100 * addedPercentageMovespeed).
        public int addedPercentageDeathMoney;//each ennemy has a static(not yet static this version) base gold reward for when de enemy gets killed .
        //why percentage? if i add +2 hp each round it makes later on the difrense between tanks and other units insignificant, round 1 tank 10hp speedster 3hp, round 15 tank 40hp speedster 33hp now we have a tanky speedster wich is op or a weak tank. percentage fixes that and same goes for th speed.
    }
}
