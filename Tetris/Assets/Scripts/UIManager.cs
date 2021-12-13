using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class UIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject WaitingStartUIPanel;

    [SerializeField]
    private GameObject GameOverUIPanel;

    [SerializeField]
    private Text scoreText;

    [SerializeField]
    private Text bestScoreText;

    [SerializeField]
    private Button newGame;

    [SerializeField]
    private Button playAgain;

    private Vector2 popupStartPos;

    [SerializeField]
    private float popupMoveSpeed;

    [SerializeField]
    private GameObject gameOverBoardCover;

    private void Awake()
    {
        GameManager.Instance.OnGameStateChange += UIHandlerOnGameStateChange;

        //Register for score change and best score change event
        EventDispatcher.Instance.RegisterListener(GameSettings.EventID.ScoreChange, (param) => ChangeScoreText((int)param));
        EventDispatcher.Instance.RegisterListener(GameSettings.EventID.BestScoreChange, (param) => ChangeBestScoreText((int)param));
    }

    // Start is called before the first frame update
    void Start()
    {
        ChangeScoreText(0);
        ChangeBestScoreText(PlayerPrefs.GetInt(GameSettings.DataFieldString.BEST_SCORE, 0));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartBtnClieckHandler()
    {
        GameManager.Instance.CurrentGameStates = GameManager.eGameStateS.PLAYING;
    }


    private void UIHandlerOnGameStateChange(GameManager.eGameStateS currentGameSate)
    {
        switch (currentGameSate)
        {
            case GameManager.eGameStateS.SETUP:
                gameOverBoardCover.SetActive(true);
                WaitingStartUIPanel.SetActive(true);
                GameOverUIPanel.SetActive(false);
                break;
            case GameManager.eGameStateS.PLAYING:
                gameOverBoardCover.SetActive(false);
                WaitingStartUIPanel.SetActive(false);
                GameOverUIPanel.SetActive(false);
                break;
            case GameManager.eGameStateS.PAUSING:
                gameOverBoardCover.SetActive(false);
                WaitingStartUIPanel.SetActive(false);
                GameOverUIPanel.SetActive(false);
                break;
            case GameManager.eGameStateS.GAMEOVER:
                gameOverBoardCover.SetActive(true);
                WaitingStartUIPanel.SetActive(false);
                GameOverUIPanel.SetActive(true);
                break;
        }
    }

    private void ChangeScoreText(int newScore)
    {
        this.scoreText.text = newScore.ToString();
    }

    private void ChangeBestScoreText(int newBestScore)
    {
        this.bestScoreText.text = newBestScore.ToString();
    }

    public void LoadNewGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}