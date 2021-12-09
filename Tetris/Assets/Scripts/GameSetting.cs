using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettings : ScriptableObject
{
    public const int BoardSizeX = 4;

    public const int BoardSizeY = 4;

    // Moving piece aniamtion duration
    public const float MovingDuration = 0.1f;

    // spawn or merge piece aniamtion duration
    public const float SpawningDuration = 0.1f;

    //Delay time to check deviceOrientation
    public const float DelayCheck = 1f;

    public class Constants
    {
        public const string Board_Tag_Name = "Board";
    }

    public enum EventID
    {
        TurnPointGain,
        ScoreChange,
        BestScoreChange,
    }

    public class DataFieldString
    {
        public const string BEST_SCORE = "BestScore";
    }

}
