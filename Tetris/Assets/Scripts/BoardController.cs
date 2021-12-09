using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BoardController : MonoBehaviour
{
    public static BoardController Instance;
    public enum eInputDirection
    {
        Up,
        Down,
        Left,
        Right,
    }

    private InputController inputController;

    private int gamePoint;
    public int GamePoint 
    { 
        get => gamePoint;
        private set { this.gamePoint = value; }
    }

    private int turnPoint;

    public int TurnPoint
    { 
        get => this.turnPoint;
        private set { this.turnPoint = value; } 
    }

    private bool isMoving = false;

    public bool IsMoving { get => this.isMoving; }

    public int movingCount { get; set; }

    public int beingMergeCount { get; set; }

    private bool isBusy;

    public bool IsBusy
    {
        get => this.isBusy;
        set { this.isBusy = value; }
    }

    //This field win be set "true" if has any move avaiable (set in fuction MovetoCell at Piece script)
    //call in coroutine "OnGetInputHandler" to check if be able to 
    private bool couldSpawn = false;

    public bool CouldSpawn 
    { 
        get => this.couldSpawn;
        set { this.couldSpawn = value; }
    }

    private bool isGetTouchInput;

    public bool IsGetTouchInput 
    { 
        get => this.isGetTouchInput; 
        set { this.isGetTouchInput = value; }
    }

    public static BoardController boardInstance;

    private int xDim;

    private int yDim;

    private int boardSize;

    [SerializeField]
    private GameObject CellBG;

    [SerializeField]
    private GameObject piecePrefabs;

    private Cell[] boardCells;

    public List<Cell> emptyCellsList = new List<Cell>();

    private SetRandomValue setRandomComponent;

    private void Awake()
    {
        GameManager.Instance.OnGameStateChange += BoardHandlerOnStateChange;

        if (Instance != null && Instance != this)
        {
            Destroy(Instance.gameObject);
        }
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        this.GamePoint = 0;

        this.TurnPoint = 0;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void BoardHandlerOnStateChange(GameManager.eGameStateS currentGameSate)
    {
        switch (currentGameSate)
        {
            case GameManager.eGameStateS.SETUP:
                
                break;
            case GameManager.eGameStateS.PLAYING:
                
                break;
            case GameManager.eGameStateS.PAUSING:
            case GameManager.eGameStateS.GAMEOVER:
                break;
        }
    }
    public void OnGetInputHandler(eInputDirection temDirection)
    {
        
    }

   
}