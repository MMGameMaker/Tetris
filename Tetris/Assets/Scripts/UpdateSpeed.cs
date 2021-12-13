using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateSpeed : MonoBehaviour
{
    private int currentScore;

    private float fastest = GameSettings.Constants.FastestDuration;
    private float slowest = GameSettings.Constants.SlowestDuration;
    private int scoreTofastest = GameSettings.Constants.ScoreToFatest;
    private float currentDuration;

    private float fallDuration;

    private TetrisBlock block;

    private void UpdateFallDuration(int newScore)
    {
        currentScore = newScore;

        if(currentScore <= scoreTofastest)
        {
            currentDuration = fastest + (slowest - fastest) * (scoreTofastest - currentScore) / scoreTofastest;
        }
        else
        {
            currentDuration = fastest;
        }

        GridControl.Instance.FallDuration = currentDuration;

        Debug.Log(currentDuration);
    }

    // Start is called before the first frame update
    void Start()
    {
        EventDispatcher.Instance.RegisterListener(GameSettings.EventID.ScoreChange, (param) => UpdateFallDuration((int)param));
    }

    
}
