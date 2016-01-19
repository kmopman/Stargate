using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ObjectSpawner : MonoBehaviour
{

    //Int
    [SerializeField]
    private int _grid;
    [SerializeField]
    private Transform _wall;
    [SerializeField]
    private Transform _turret1;
    [SerializeField]
    private Transform _turret2;
    [SerializeField]
    private LayerMask _occupied;
    [SerializeField]
    private LayerMask _occupiedByTurret;
    private bool _placeOpen;//check is this space is taken by a wall
    private bool _noTurret;//check is this space is taken by a turret
    [SerializeField]
    private bool _wavedone = true;//haal dit nog bij de spawner
    private bool _clearPath = true;//maak in pathfinding wanneer je kan lopen
    public int recources;//the ammount of recources that you have
    [SerializeField]
    private int _wallCost;//the ammount of recources a wall wil cost
    [SerializeField]
    private int _turret1Cost;//the ammount of recources a turrettype1 wil cost
    [SerializeField]
    private int _turret2Cost;//the ammount of recources a turrettype2 wil cost
    [SerializeField]
    private int _blockChoice;//what you pick, a wall(1) or turret(2) or turrettype2(3) etc
    private Vector3 _gridPos;
    //Int

    [SerializeField]
    private GameObject _turretPickIcon;

    private Transform _pickPos;

    [SerializeField]
    private RectTransform _wallPickPos;
    [SerializeField]
    private RectTransform _turretPickPos;
    [SerializeField]
    private RectTransform _hTurretPickPos;

    [SerializeField]
    private AudioSource _switchSFX;

    private GameObject _findSpawner;//finds spawner object
    private SpawnSystem _waveStats;//imports spawnsystem

    private GameObject _recourcesText;//timer object
    // Use this for initialization
    void Start()
    {
        _blockChoice = 1;
        _clearPath = true;
        _findSpawner = GameObject.Find("Spawner");
        _waveStats = _findSpawner.GetComponent<SpawnSystem>();
        _pickPos = _turretPickIcon.GetComponent<RectTransform>();
        _recourcesText = GameObject.Find("RecourcesText");//find timer object
    }

    void ChangePickPos()
    {
        if (_blockChoice == 1)
            _pickPos.position = _wallPickPos.position;
        if (_blockChoice == 2)
            _pickPos.position = _turretPickPos.position;
        if (_blockChoice == 3)
            _pickPos.position = _hTurretPickPos.position;

        
    }
    // Update is called once per frame
    void Update()
    {

        ChangePickPos();

        _wavedone = _waveStats.waveDone;
        _recourcesText.GetComponent<Text>().text = "resources: " + recources;
        //theseif statements are used for switching between blocks to build
        if (Input.GetKeyDown(KeyCode.Q) || Input.GetButtonDown("LeftBumper"))
        {
            _blockChoice--;
            _switchSFX.Play();
        }
            
        if (Input.GetKeyDown(KeyCode.E) || Input.GetButtonDown("RightBumper"))
        {
            _blockChoice++;
            _switchSFX.Play();
        }
            
        if (_blockChoice == 4)
            _blockChoice = 1;
        if (_blockChoice == 0)
            _blockChoice = 3;


        if (_wavedone)
        {
            _placeOpen = !(Physics.CheckSphere(_gridPos, (_grid / 2), _occupied));//checks if the tile is occupied by a wall
            _noTurret = !(Physics.CheckSphere(new Vector3(_gridPos.x,_gridPos.y+_grid,_gridPos.z), (_grid / 2), _occupiedByTurret));//checks if a wall is occupied by a turret
            var pos = new Vector3(Screen.width / 2, Screen.height / 2, 5);//get midle x, midle y, how far it can go
            var position = Camera.main.ScreenToWorldPoint(pos);//use the value's from above actualy in the view of the camera and not the world positions
            _gridPos = new Vector3(Mathf.Round(position.x / _grid) * _grid, 1f, Mathf.Round(position.z / _grid) * _grid);//here we round the position so you get a grid effect
            transform.position = _gridPos;//here we actualy place it
            if (_clearPath)
            {
                if (recources >= _wallCost && _blockChoice == 1 && _placeOpen)
                {
                    if(Input.GetButtonDown("Fire1") || Input.GetButtonDown("Placement"))
                    {
                        Instantiate(_wall, new Vector3(transform.position.x, transform.position.y - -2.5f, transform.position.z), Quaternion.identity);
                        recources -= _wallCost;
                    }
                }
                if (recources >= _turret1Cost && _blockChoice == 2 && !_placeOpen && _noTurret)
                {
                    if (Input.GetButtonDown("Fire1")  || Input.GetButtonDown("Placement"))
                    {
                        Instantiate(_turret1, new Vector3(transform.position.x, transform.position.y - -1f + _grid, transform.position.z), Quaternion.identity);
                        recources -= _turret1Cost;
                    }
                }
                if (recources >= _turret2Cost && _blockChoice == 3 && !_placeOpen && _noTurret)
                {
                    if (Input.GetButtonDown("Fire1")  || Input.GetButtonDown("Placement"))
                    {
                        Instantiate(_turret2, new Vector3(transform.position.x, transform.position.y - -2f + _grid, transform.position.z), Quaternion.identity);
                        recources -= _turret2Cost;
                    }
                }
            }
        }
        else
        {
            transform.position = new Vector3(1000, 0, 0);//where we place it out of the screen
        }
        
    }
}
