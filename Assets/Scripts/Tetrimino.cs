using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//테트리미노 코드
public class Tetrimino : MonoBehaviour
{
    [SerializeField] private TetriminoType tetriminoType;  
    [SerializeField] private GameObject [] tetriminoGroup;    
    [SerializeField] private int[,] batch;
    [SerializeField] private int[,] gridPosition;
    [SerializeField] private float batchGap = 0.7f;
    [SerializeField] private int[,,] baseBatch;


    private void Awake() {
        BaseBatchSet();
        batch = new int[4, 4];
        gridPosition = new int[4, 2];
    }

    void Start()
    {        
        int selected = Random.Range(0, 7);
        tetriminoType = RandomSelect(selected+1);
        //tetriminoSelectedType = tetriminoSample[selected - 1];
        BatchTetrimino(selected);
    }
    private IEnumerator MoveDownTetrimino() {
        while (true) {
            Debug.Log("Y : " + transform.position.y);
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


    //배열 회전 공식 => a[i][j] -> a[j][N - 1 - i];

    //시계방향 회전

    public void TurnCR()
    {
        /*
        for (int i = 0; i < 4; i++) {            
            tetriminoGroup[i].transform.position = new Vector2(tetriminoGroup[i].transform.position.y, -tetriminoGroup[i].transform.position.x);
        }
        */
        transform.Rotate(0, 0, -90);


    }
    //시계 반대방향 회전
    public void TurnCCR()
    {
        /*
        for (int i = 0; i < 4; i++) {
            tetriminoGroup[i].transform.position = new Vector2(-tetriminoGroup[i].transform.position.y, tetriminoGroup[i].transform.position.x);
        }
        */
    }    
    public void MoveXAxis(int side)
    {
         transform.position = (new Vector3(transform.position.x + (side * batchGap), transform.position.y, transform.position.z));
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