using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultRecord : MonoBehaviour
{
    private int lastGameScore;

    private float startRecordTime;

    public float StartRecordTime
    {
        get => this.startRecordTime;
        set { this.startRecordTime = value; }
    }

    private float endRecordTime;

    public float EndRecordTime
    {
        get => this.endRecordTime;
        set { this.endRecordTime = value; }
    }

    private float lastGameTimeRecord;

    public float LastGameTimeRecord
    {
        get => this.lastGameTimeRecord;
    }

    private void Start()
    {
        EventDispatcher.Instance.RegisterListener(GameSettings.EventID.ScoreChange, (param) => UpdateBestScore((int)param));

        lastGameScore = 0;
    }

    private void GameStartingRecord()
    {
        this.startRecordTime = Time.time;
    }

    private void GameEndingRecord()
    {
        this.EndRecordTime = Time.time;
        this.lastGameTimeRecord = endRecordTime - lastGameTimeRecord;
    }

    private void OnGameOverHandler(object param)
    {
        //Compare with best score!
    }

    private void UpdateBestScore(int newScore)
    {
        lastGameScore = newScore;

        if (lastGameScore > PlayerPrefs.GetInt(GameSettings.DataFieldString.BEST_SCORE, 0))
        {
            PlayerPrefs.SetInt(GameSettings.DataFieldString.BEST_SCORE, lastGameScore);
            EventDispatcher.Instance.PostEvent(GameSettings.EventID.BestScoreChange, lastGameScore);
        }
    }
    



    }
