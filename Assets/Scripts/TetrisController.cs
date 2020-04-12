using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrisController : MonoBehaviour
{

    [SerializeField] private Tetrimino tetrimino;       //조작될 테트리미노.


    // Update is called once per frame
    void Update()
    { 
        if (tetrimino != null)
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                tetrimino.TurnCR();
            }
            if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                tetrimino.TurnCCR();
            }
            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                tetrimino.MoveXAxis(-1);
            }
            if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                tetrimino.MoveXAxis(1);
            }
        }
    }
    public Tetrimino Tetrimino { get { return tetrimino; } set { tetrimino = value; } }


}
