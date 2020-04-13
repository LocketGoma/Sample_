using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//https://noobtuts.com/unity/2d-tetris-game/
public class TetrisManager : MonoBehaviour
{
    [Header("블럭 배치표")]
    [SerializeField] private Transform[,] dataMatrix;
    private int[,] nowPosArray;
    [Header("테트리미노 배치/사용")]
    [SerializeField] private GameObject baseTetrimino = null;
    [SerializeField] private GameObject nowTetrimino = null;
    [SerializeField] private GameObject nextTetrimino = null;
    [SerializeField] private GameObject secondTetrimino = null;

    [Header("자체 변수")]
    [Range(0.0f, 1.0f)]
    [SerializeField] private float batchGap = 0.7f;
    [SerializeField] private int[] mapSize = { 9, 20 };
    [SerializeField] private int[] batchPosition = { 5, 15 };

    [Header("조작 컨트롤러")]
    [SerializeField] private TetrisController tetrisController = null;
    [SerializeField] private bool endOfMove = false;
    [SerializeField] private bool playState = true;



    private void Start() {
        dataMatrix = new Transform[mapSize[0], mapSize[1]];
        //nowPosArray = new int[4, 2];

        nowTetrimino = Instantiate(baseTetrimino, new Vector2(0, 15), transform.rotation);
        //nextTetrimino = Instantiate(baseTetrimino, new Vector2(5, 16), transform.rotation);
        //secondTetrimino = Instantiate(baseTetrimino, new Vector2(5, 2), transform.rotation);

        nowTetrimino.GetComponent<Tetrimino>().Initializer(batchGap);
        //nextTetrimino.GetComponent<Tetrimino>().Initializer(batchGap);
        //secondTetrimino.GetComponent<Tetrimino>().Initializer(batchGap);

        nowTetrimino.GetComponent<Tetrimino>().CanMove();
        tetrisController.Tetrimino = nowTetrimino.GetComponent<Tetrimino>();
    }

    // Update is called once per frame
    void Update()
    {
        if (endOfMove == true) { //현재 테트리미노가 사용불가가 되었을시
            Debug.Log("Next");
            foreach (Transform child in nowTetrimino.transform)
            {
                Vector2 v = roundVec2(child.position);
                dataMatrix[(int)v.x, (int)v.y].name = "Rock";
                playState = CheckOverflow(v) == false;
            }

                nowTetrimino.GetComponent<Tetrimino>().StopMove();
            if (playState == true)
            {
                nowTetrimino = nextTetrimino = Instantiate(baseTetrimino, new Vector2(0, 15), transform.rotation);
                nowTetrimino.GetComponent<Tetrimino>().Initializer(batchGap);
                //nowTetrimino = nextTetrimino;
                //nowTetrimino.transform.position = new Vector2(0, 15);
                //nextTetrimino = secondTetrimino;
                //nextTetrimino.transform.position = new Vector2(5, 16);
                //secondTetrimino = Instantiate(baseTetrimino, new Vector2(5, 2), transform.rotation);
                //secondTetrimino.GetComponent<Tetrimino>().Initializer(batchGap);

                tetrisController.Tetrimino = nowTetrimino.GetComponent<Tetrimino>();
                nowTetrimino.GetComponent<Tetrimino>().CanMove();

                endOfMove = false;
            }
        }
        UpdateData();
    }

    //여기서부터 다 새로 짤것.

    void DeleteRow(int y) {
        for (int x = 0; x < mapSize[0]; x++) {
            Destroy(dataMatrix[x, y].gameObject);
            dataMatrix[x, y] = null;
        }
        DecreaseRow(y + 1);
    }
    void DecreaseRow(int y) {
        bool transCheck = false;
        if (y == mapSize[1]) {
            return;
        }
        for (int x = 0; x < mapSize[0]; x++) {
            if (dataMatrix[x, y] != null) {
                dataMatrix[x, y - 1] = dataMatrix[x, y];
                dataMatrix[x, y] = null;

                transCheck = true;
            }
        }
        if (transCheck == true)
            DecreaseRow(y + 1);
    }

    void UpdateData() {
        for (int y = 0; y < mapSize[1]; ++y)
            for (int x = 0; x < mapSize[0]; ++x)
                if (dataMatrix[x, y] != null)
                    if (dataMatrix[x, y].parent == nowTetrimino.transform)
                        dataMatrix[x, y] = null;

        foreach (Transform child in nowTetrimino.transform) {
            Vector2 v = roundVec2(child.position);
            dataMatrix[(int)v.x, (int)v.y] = child;
            if ((dataMatrix[(int)v.x, (int)v.y - 1] != null && dataMatrix[(int)v.x, (int)v.y - 1].name != child.name) || (int)v.y <= 1)
            {
                endOfMove = true;
            }
            Debug.Log("now Check :" + v.x + "," + v.y);
        }
        Debug.Log("-----------");
    }
    public static Vector2 roundVec2(Vector2 v) {
        return new Vector2(Mathf.Round(v.x),
                           Mathf.Round(v.y));
    }
    //꽉 차서 게임 종료 되는 경우.
    bool CheckOverflow(Vector2 checker)
    {
        if (checker.y >= batchPosition[1]) { 
            return true;
        } else {
        return false;
        }
    }


}
