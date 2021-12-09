using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    private int boardIndex;
    public int BoardIndex 
    { 
        get => boardIndex;
        set { this.boardIndex = value; } 
    }
    public int XDimIndex { get; set; }

    public int YDimIndex { get; set; }

    
}
