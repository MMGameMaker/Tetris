using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    
    public BoardController boardController;

    private Touch touch;

    private Vector2 startTouchPos;

    private Vector2 endTouchPos;

    private Vector2 touchDirection;

    private BoardController.eInputDirection inputdirection;
    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
