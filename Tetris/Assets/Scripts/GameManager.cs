using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum eGameStateS
    {
        SETUP,
        PLAYING,
        PAUSING,
        GAMEOVER,
    }

    public static GameManager Instance
    {
        get;
        private set;
    }

    private eGameStateS currentGameSates;

    private eGameStateS lastGameStates;

    public eGameStateS CurrentGameStates
    {
        get => this.currentGameSates;
        set
        {
            if (value != currentGameSates)
            {
                lastGameStates = currentGameSates;
                this.currentGameSates = value;
                OnGameStateChange.Invoke(currentGameSates);
                Debug.Log(currentGameSates);
            }
        }
    }

    private DeviceOrientation orientation;

    public delegate void OnGameStateChangeEvent(eGameStateS currentGameState);

    public OnGameStateChangeEvent OnGameStateChange;

    public delegate void OnDeviceOrientationChangeEvent(DeviceOrientation deviceOrientation);

    public OnDeviceOrientationChangeEvent DeviceOrientationChangeHandler;

    private void Awake()
    {
        if(Instance!=null && Instance != this)
        {
            Destroy(Instance.gameObject);
        }
        Instance = this;

        this.CurrentGameStates = eGameStateS.SETUP;
    }

    // Start is called before the first frame update
    void Start()
    {
        orientation = Input.deviceOrientation;
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(CheckForChange());
    }

    public void StartGame()
    {
        this.CurrentGameStates = GameManager.eGameStateS.PLAYING;
    }

    IEnumerator CheckForChange()
    {
        // Check for an Orientation Change
        switch (Input.deviceOrientation)
        {
            case DeviceOrientation.Unknown:            // Ignore
            case DeviceOrientation.FaceUp:            // Ignore
            case DeviceOrientation.FaceDown:        // Ignore
                break;
            default:
                if (orientation != Input.deviceOrientation)
                {
                    orientation = Input.deviceOrientation;

                    DeviceOrientationChangeHandler.Invoke(orientation);
                }
                break;
        }

        yield return new WaitForSeconds(GameSettings.DelayCheck);

    }



}
