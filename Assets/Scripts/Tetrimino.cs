using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//테트리미노 코드
public class Tetrimino : MonoBehaviour
{
    [SerializeField] private TetriminoType tetriminoType;
    [SerializeField] private GameObject tetriminoSelectedType = null;
    [SerializeField] private GameObject [] tetriminoSample;



    void Start()
    {
        int selected = Random.Range(1, 8);
        tetriminoType = RandomSelect(selected);
        tetriminoSelectedType = tetriminoSample[selected - 1];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //시계방향 회전
    public void TurnCR()
    {

    }
    //시계 반대방향 회전
    public void TurnCCR()
    {

    }
    public void MoveXAxis(int side)
    {
        //좌측 이동
        if (side == -1)
        {

        }
        //우측 이동
        if (side == 1)
        {

        }
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