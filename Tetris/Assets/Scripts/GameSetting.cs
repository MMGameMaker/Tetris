using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettings : ScriptableObject
{

    public class Constants
    {
        public const int BoardSizeX = 12;

        public const int BoardSizeY = 20;

        //Slowest falling piece duration
        public const float SlowestDuration = 0.8f;

        //Fastest falling piece duration
        public const float FastestDuration = 0.08f;

        public const int ScoreToFatest = 600;

        //Delay time to check deviceOrientation
        public const float DelayCheck = 1f;

        public const string Grid_Tag_Name = "Grid";
    }

    public enum EventID
    {
        GameStart,
        ScoreChange,
        BestScoreChange,
        GameOver,
        Restart,
    }

    public class DataFieldString
    {
        public const string BEST_SCORE = "TetrisBestScore";
    }

}
