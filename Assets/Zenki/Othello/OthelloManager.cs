using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OthelloManager : MonoBehaviour
{
    GameObject target;                          //クリック先のブロックを得るための変数
    [SerializeField] int raw;                //横のブロック数
    [SerializeField] int col;                //縦のブロック数

    OthelloField field;                                //fieldコンポーネントの変数
    [SerializeField] GameObject fieldCellObj = default;
    [SerializeField] GameObject stone = default;
    GameObject[,] fieldCells = null;
    const float spaceSize = 1f;                                //ブロック同士の間隔

    State[,] stoneState = null;
    GameObject[,] stones = null;
    Point putPos;
    Point beginPos;
    Point endPos;
    State turn = State.Black;

    int bCount = 0;
    int wCount = 0;

    void Start()
    {
        field = FindObjectOfType<OthelloField>();
        CreateField(raw, col);
        turn = State.Black;
    }

    void Update()
    {
        Put();
        for(int r = 0; r < raw; r++)
        {
            for(int c = 0; c < col; c++)
            {
                if (stones[r, c] != null)
                {
                    stones[r, c].GetComponent<Stone>().SetState(stoneState[r, c]);
                }
            }
        }
    }
    //ステージ作成
    public void CreateField(int _raw, int _col)
    {
        fieldCells = new GameObject[_raw, _col];
        stones = new GameObject[_raw, _col];
        stoneState = new State[_raw, _col];
        InitiaLizedField(_raw, _col);
    }

    //ステージの初期化
    void InitiaLizedField(int raw, int col)
    {

        for (int r = 0; r < raw; r++)
        {
            for (int c = 0; c < col; c++)
            {
                fieldCells[r, c] = Instantiate(fieldCellObj, new Vector3(c * spaceSize, 0, (9 - r) * spaceSize), fieldCellObj.transform.rotation);
                fieldCells[r, c].name = $"{r},{c}";
                stoneState[r, c] = State.Empty;
                if ((r == 4 && c == 4) || (r == 5 && c == 5))
                {
                    var obj = Instantiate(stone, new Vector3(c * spaceSize, spaceSize, (9 - r) * spaceSize), Quaternion.Euler(-180, 0, 0));
                    obj.AddComponent<Stone>().SetState(State.Black);
                    stoneState[r, c] = State.Black;
                    stones[r, c] = obj;
                }
                if((r == 5 && c == 4) || (r == 4 && c == 5))
                {
                    var obj = Instantiate(stone, new Vector3(c * spaceSize, spaceSize, (9 - r) * spaceSize), Quaternion.Euler(0, 0, 0));
                    obj.AddComponent<Stone>().SetState(State.White);
                    stoneState[r, c] = State.White;
                    stones[r, c] = obj;
                }
            }
        }
        beginPos = new Point((int)fieldCells[0, 0].transform.position.x, (int)fieldCells[0, 0].transform.position.z);
        endPos = new Point((int)fieldCells[0, col - 1].transform.position.x, (int)fieldCells[raw - 1, 0].transform.position.z);
        bCount = 2;
        wCount = 2;
    }

    void Put()
    {
        if (Input.GetMouseButtonDown(0))
        {
            target = null;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit = new RaycastHit();

            if (Physics.Raycast(ray, out hit))
            {
                target = hit.collider.gameObject;
                if (target.GetComponent<Stone>() != null || target == null)
                {
                    return;
                }
                else
                {
                    putPos = new Point((int)target.transform.position.x, (int)target.transform.position.z);
                    if (putPos.x >= beginPos.x && putPos.x <= endPos.x && putPos.z >= endPos.z && putPos.z <= beginPos.z && stoneState[9 - putPos.z, putPos.x] == State.Empty)
                    {

                        if (turn == State.Black)
                        {
                            var obj = Instantiate(stone, new Vector3(putPos.x, spaceSize, putPos.z), Quaternion.Euler(-180, 0, 0));
                            obj.AddComponent<Stone>().SetState(State.Black);
                            stoneState[9 - putPos.z, putPos.x] = State.Black;
                            stones[9 - putPos.z, putPos.x] = obj;
                            bCount++;
                        }
                        else if (turn == State.White)
                        {
                            var obj = Instantiate(stone, new Vector3(putPos.x, spaceSize, putPos.z), Quaternion.Euler(0, 0, 0));
                            obj.AddComponent<Stone>().SetState(State.White);
                            stoneState[9 - putPos.z, putPos.x] = State.White;
                            stones[9 - putPos.z, putPos.x] = obj;
                            wCount++;
                        }
                    }

                    TurnStone(9 - putPos.z, putPos.x);
                }
            }
            turn = (turn == State.Black) ? State.White : State.Black;
        }
    }

    State[] GetTurnAbleStones(int z, int x)
    {
        State enemyState = (turn == State.Black) ? State.White : State.Black;

        var turnStoneList = new List<State>();

        var isTop = z == 0;
        var isButtom = z == raw - 1;
        var isLeft = x == 0;
        var isRight = x == col - 1;


        if (!isTop && !isLeft && stoneState[z-1, x-1] != State.Empty)
        {
            var m_x = x;
            var m_z = z;
            var list = new List<State>();
            while (stoneState[m_z-1, m_x-1] == enemyState)
            {
                if ((m_x < 0 || m_z < 0))
                {
                    list.Clear();
                    break;
                }

                m_x--;
                m_z--;

                if (stoneState[m_z, m_x] == State.Empty)
                {
                    list.Clear();
                    break;

                }
                stoneState[m_z, m_x] = turn;
                list.Add(stoneState[m_z, m_x]);
            }
            if (list != null)
            {
                foreach (var l in list)
                {
                    turnStoneList.Add(l);
                }
            }
        }

        if (!isTop && !isRight && stoneState[z - 1, x + 1] != State.Empty)
        {
            var m_x = x;
            var m_z = z;
            var list = new List<State>();
            while (stoneState[m_z - 1, m_x + 1] == enemyState)
            {
                if ((m_x > col || m_z < 0))
                {
                    list.Clear();
                    break;
                }

                m_x++;
                m_z--;

                if (stoneState[m_z, m_x] == State.Empty)
                {
                    list.Clear();
                    break;

                }
                stoneState[m_z, m_x] = turn;
                list.Add(stoneState[m_z, m_x]);
            }
            if (list != null)
            {
                foreach (var l in list)
                {
                    turnStoneList.Add(l);
                }
            }
        }

        if (!isButtom && !isLeft && stoneState[z + 1, x - 1] != State.Empty)
        {
            var m_x = x;
            var m_z = z;
            var list = new List<State>();
            while (stoneState[m_z + 1, m_x - 1] == enemyState)
            {
                if ((m_x < 0 || m_z > raw))
                {
                    list.Clear();
                    break;
                }

                m_x--;
                m_z++;

                if (stoneState[m_z, m_x] == State.Empty)
                {
                    list.Clear();
                    break;

                }
                stoneState[m_z, m_x] = turn;
                list.Add(stoneState[m_z, m_x]);
            }
            if (list != null)
            {
                foreach (var l in list)
                {
                    turnStoneList.Add(l);
                }
            }
        }

        if (!isButtom && !isRight && stoneState[z + 1, x + 1] != State.Empty)
        {
            var m_x = x;
            var m_z = z;
            var list = new List<State>();
            while (stoneState[m_z + 1, m_x + 1] == enemyState)
            {
                if ((m_x > col - 1 || m_z > raw - 1))
                {
                    list.Clear();
                    break;
                }

                m_x++;
                m_z++;

                if (stoneState[m_z, m_x] == State.Empty)
                {
                    list.Clear();
                    break;

                }
                stoneState[m_z, m_x] = turn;
                list.Add(stoneState[m_z, m_x]);
            }
            if (list != null)
            {
                foreach (var l in list)
                {
                    turnStoneList.Add(l);
                }
            }
        }

        if (!isTop && stoneState[z - 1, x] != State.Empty)
        {
            var m_x = x;
            var m_z = z;
            var list = new List<State>();
            while (stoneState[m_z - 1, m_x] == enemyState)
            {
                if (m_z < 0)
                {
                    list.Clear();
                    break;
                }

                m_z--;

                if (stoneState[m_z, m_x] == State.Empty)
                {
                    list.Clear();
                    break;

                }
                stoneState[m_z, m_x] = turn;
                list.Add(stoneState[m_z, m_x]);
            }
            if (list != null)
            {
                foreach (var l in list)
                {
                    turnStoneList.Add(l);
                }
            }
        }

        if (!isButtom && stoneState[z + 1, x] != State.Empty)
        {
            var m_x = x;
            var m_z = z;
            var list = new List<State>();
            while (stoneState[m_z + 1, m_x] == enemyState)
            {
                if (m_z > raw - 1)
                {
                    list.Clear();
                    break;
                }

                m_z++;

                if (stoneState[m_z, m_x] == State.Empty)
                {
                    list.Clear();
                    break;

                }
                stoneState[m_z, m_x] = turn;
                list.Add(stoneState[m_z, m_x]);
            }
            if (list != null)
            {
                foreach (var l in list)
                {
                    turnStoneList.Add(l);
                }
            }
        }

        if (!isLeft && stoneState[z, x - 1] != State.Empty)
        {
            var m_x = x;
            var m_z = z;
            var list = new List<State>();
            while (stoneState[m_z, m_x-1] == enemyState)
            {
                if (m_x < 0)
                {
                    list.Clear();
                    break;
                }

                m_x--;

                if (stoneState[m_z, m_x] == State.Empty)
                {
                    list.Clear();
                    break;

                }
                stoneState[m_z, m_x] = turn;
                stones[m_z, m_x].GetComponent<Stone>().ChageState(turn);
                list.Add(stoneState[m_z, m_x]);
            }
            if (list != null)
            {
                foreach (var l in list)
                {
                    turnStoneList.Add(l);
                }
            }
        }

        if (!isTop && stoneState[z, x + 1] != State.Empty)
        {
            var m_x = x;
            var m_z = z;
            var list = new List<State>();
            while (stoneState[m_z, m_x + 1] == enemyState)
            {
                if (m_x > col)
                {
                    list.Clear();
                    break;
                }

                m_x++;

                if (stoneState[m_z, m_x] == State.Empty)
                {
                    list.Clear();
                    break;

                }
                stoneState[m_z, m_x] = turn;
                list.Add(stoneState[m_z, m_x]);
            }
            if (list != null)
            {
                foreach (var l in list)
                {
                    turnStoneList.Add(l);
                }
            }
        }

        return turnStoneList.ToArray();
    }

    void TurnStone(int z, int x)
    {
        Debug.Log($"{z}, {x}");
        foreach(var s in GetTurnAbleStones(z, x))
        {
            Debug.Log(s);
        }
    }
}



public class Point
{
    public int x;
    public int z;
    public  Point(int _x, int _z)
    {
        x = _x;
        z = _z;
    }
}



