using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//https://noobtuts.com/unity/2d-tetris-game/
public class TetrisManager : MonoBehaviour
{
    [Header("블럭 배치표")]
    private Transform[,] dataMatrix;
    private int[,] nowPosArray;
    [Header("테트리미노 배치/사용")]
    [SerializeField] private GameObject baseTetrimino = null;
    [SerializeField] private GameObject nowTetrimino = null;
    [SerializeField] private GameObject nextTetrimino = null;
    [SerializeField] private GameObject secondTetrimino = null;

    [Header("자체 변수")]
    [Range(0.0f,1.0f)]
    [SerializeField] private float batchGap = 0.7f;
    [SerializeField] private int[] mapSize = { 9, 20 };

    [Header("조작 컨트롤러")]
    [SerializeField] private TetrisController tetrisController = null;

    

    private void Start() {
        dataMatrix = new Transform[mapSize[0], mapSize[1]];
        //nowPosArray = new int[4, 2];

        nowTetrimino = Instantiate(baseTetrimino, new Vector2(0, 5), transform.rotation);
        nextTetrimino = Instantiate(baseTetrimino, new Vector2(2, 4), transform.rotation);
        secondTetrimino = Instantiate(baseTetrimino, new Vector2(2, 2), transform.rotation);

        nowTetrimino.GetComponent<Tetrimino>().Initializer(batchGap);
        nextTetrimino.GetComponent<Tetrimino>().Initializer(batchGap);
        secondTetrimino.GetComponent<Tetrimino>().Initializer(batchGap);

        nowTetrimino.GetComponent<Tetrimino>().CanMove();
        tetrisController.Tetrimino = nowTetrimino.GetComponent<Tetrimino>();
    }

    // Update is called once per frame
    void Update()
    {
        if(nowTetrimino == null) { //현재 테트리미노가 사용불가가 되었을시
            nowTetrimino.GetComponent<Tetrimino>().CanMove();
            nowTetrimino = nextTetrimino;
            nowTetrimino.transform.position = new Vector2(0, 15);
            nextTetrimino = secondTetrimino;
            nextTetrimino.transform.position = new Vector2(2, 4);
            secondTetrimino = Instantiate(baseTetrimino, new Vector2(2, 2), transform.rotation);
            secondTetrimino.GetComponent<Tetrimino>().Initializer(batchGap);

            tetrisController.Tetrimino = nowTetrimino.GetComponent<Tetrimino>();
        }
    }

    //여기서부터 다 새로 짤것.

    void DeleteRow(int y) {
        for (int x = 0; x < mapSize[0]; x++) {
            Destroy(dataMatrix[x, y].gameObject);
            dataMatrix[x, y] = null;
        }
        DecreaseRow(y+1);
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
        }
    } 
    public static Vector2 roundVec2(Vector2 v) {
        return new Vector2(Mathf.Round(v.x),
                           Mathf.Round(v.y));
    }



}
