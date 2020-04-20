using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//테트리미노 코드
public class Tetrimino : MonoBehaviour
{
    [SerializeField] private TetriminoType tetriminoType;  
    [SerializeField] private GameObject [] tetriminoGroup = null;    
    [SerializeField] private int[,] batch;
    [SerializeField] private int[,] gridPosition;
    [SerializeField] private float batchGap = 0.7f;
    [SerializeField] private int[,,] baseBatch;


    private void Awake() {
        BaseBatchSet();
        batch = new int[4, 4];
        gridPosition = new int[4, 2];
    }
    public void Initializer(float gap) {
        int selected = Random.Range(0, 7);
        tetriminoType = RandomSelect(selected + 1);
        BatchTetrimino(selected);
        SetColor(tetriminoType);
        batchGap = gap;
    }
    private IEnumerator MoveDownTetrimino() {
        while (true) {
           // Debug.Log("Y : " + transform.position.y);
            yield return new WaitForSeconds(1);
            transform.position = new Vector2(transform.position.x, transform.position.y - 1);

            if (transform.position.y <= 0) {
                Debug.Log("End");
                yield break;
            }
        }
    }
    public void CanMove() {
        StartCoroutine(MoveDownTetrimino());
    }
    public void StopMove()
    {
        Debug.Log("stop");
        //StopCoroutine(MoveDownTetrimino());
        StopAllCoroutines();
    }
    private void SetColor(TetriminoType tetriminoType) {
        Color color;
        Debug.Log("type : " + tetriminoType);
        switch (tetriminoType) {
            case TetriminoType.I_mino: {
                    color = new Color(100.0f / 255.0f, 100.0f / 255.0f, 250.0f / 255.0f);
                    break;
                }
            case TetriminoType.LL_mino: {
                    color = new Color(200.0f / 255.0f, 200.0f / 255.0f, 50.0f / 255.0f);
                    break;
                }
            case TetriminoType.LR_mino: {
                    color = new Color(250.0f / 255.0f, 100.0f / 255.0f, 100.0f / 255.0f);
                    break;
                }
            case TetriminoType.ZL_mino: {
                    color = new Color(200.0f / 255.0f, 200.0f / 255.0f, 200.0f / 255.0f);
                    break;
                }
            case TetriminoType.ZR_mino: {
                    color = new Color(50.0f / 255.0f, 200.0f / 255.0f, 50.0f / 255.0f);
                    break;
                }
            case TetriminoType.O_mino: {
                    color = new Color(50.0f / 255.0f, 50.0f / 255.0f, 150.0f / 255.0f);
                    break;
                }
            case TetriminoType.T_mino: {
                    color = new Color(200.0f / 255.0f, 50.0f / 255.0f, 200.0f / 255.0f);
                    break;
                }
            default: {
                    color = new Color(255.0f / 255.0f, 255.0f / 255.0f, 255.0f / 255.0f);
                    break;
                }
        }
        foreach (Transform child in transform) {
            Debug.Log(child.gameObject.GetComponent<SpriteRenderer>().material.color);
            child.gameObject.GetComponent<SpriteRenderer>().material.color = color;
        }
        Debug.Log("change color : " + color);
    }


    //배열 회전 공식 => a[i][j] -> a[j][N - 1 - i];

    //시계방향 회전

    public void TurnTetrimino()
    {
        transform.Rotate(0, 0, -90);
        if (TetrisManager.IsValidGridPos(transform) == false) {
            transform.Rotate(0, 0, 180);
            if (TetrisManager.IsValidGridPos(transform) == false){
                transform.Rotate(0, 0, -90);
            }
            Debug.Log("fail to rotate Tetrimino");
        }


    }
    //좌우 이동
    public void MoveXAxis(int side) 
    {
        transform.position += new Vector3((side * batchGap), 0, 0);
        if (TetrisManager.IsValidGridPos(transform) == false) {
            transform.position -= new Vector3((side * batchGap), 0, 0);
        }
        Debug.Log("Out of Range");
    }
    //아래 이동
    public void PushToBottom() {
        transform.position += new Vector3(0,(-1 * batchGap), 0);
    }

    private TetriminoType RandomSelect(int selectKey)
    {
     switch(selectKey)
        {
            case 1:
                return TetriminoType.I_mino;
            case 2:
                return TetriminoType.LL_mino;
            case 3:
                return TetriminoType.LR_mino;
            case 4:
                return TetriminoType.ZL_mino;
            case 5:
                return TetriminoType.ZR_mino;
            case 6:
                return TetriminoType.O_mino;
            case 7:
                return TetriminoType.T_mino;
            default:
                return TetriminoType.T_mino;
        }
    }
    private void BatchTetrimino(int tetriminoType) {

        for (int i = 0; i < 4; i++) {
//         gridPosition[i, 0] = (int)transform.position.x + baseBatch[tetriminoType, i, 0]; 
//          gridPosition[i, 1] = (int)transform.position.y + baseBatch[tetriminoType, i, 1];

            tetriminoGroup[i].transform.position = new Vector2(transform.position.x + baseBatch[tetriminoType,i,0] * batchGap, transform.position.y + baseBatch[tetriminoType, i, 1] * batchGap);

        }
    }
    private void BaseBatchSet() =>
        baseBatch = new int[,,] { { { 0, 0 }, { 0, 1 }, { 0, 2 }, { 0, 3 } }, { { 0, 0 }, { 0, 1 }, { 1, 1 }, { 2, 1 } }, { { 3, 0 }, { 3, 1 }, { 2, 1 }, { 1, 1 } }, { { 0, 0 }, { 0, 1 }, { 1, 1 }, { 1, 2 } }, { { 1, 0 }, { 1, 1 }, { 0, 1 }, { 0, 2 } }, { { 0, 0 }, { 0, 1 }, { 1, 0 }, { 1, 1 } }, { { 0, 0 }, { 0, 1 }, { 0 , 2 }, { 1, 1 } } };
}

enum TetriminoType
{
    I_mino,
    LL_mino,            //반대로 된 L (좌상단) 
    LR_mino,            //정상 L (우상단)
    ZL_mino,            //정상 Z (좌상단)
    ZR_mino,            //반대로 된 Z (우상단)
    O_mino,
    T_mino


}