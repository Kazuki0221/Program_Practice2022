using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeGameManager : MonoBehaviour
{
    [SerializeField] GameObject cell = default;
    [SerializeField] int raw;
    [SerializeField] int col;
    [SerializeField] float spaceSize = 1.2f;
    [SerializeField] float changeTime = 2f;
    GameObject[,] cells = null;
    LifeState [,]cellsState = null;

    [SerializeField] int progrees = 0;

    [SerializeField] Mode mode = Mode.Manual;

    [SerializeField] InputField inputField = null;

    void Start()
    {
        cells = new GameObject[raw, col];
        cellsState = new LifeState[raw, col];
        for (int r = 0; r < raw; r++)
        {
            for(int c = 0; c < col; c++)
            {
                cells[r, c] = Instantiate(cell, new Vector3((c - 5) * spaceSize,(5 - r) * spaceSize, 0), cell.transform.rotation);
                cells[r, c].name = $"{r}, {c}";
                var rand = Random.Range(0, 100);
                if(rand >= 0 && rand < 10)
                {
                    cellsState[r, c] = LifeState.Survive;
                }
                else
                {
                    cellsState[r, c] = LifeState.Death;
                }
            }
        }
        for (int r = 0; r < raw; r++)
        {
            for (int c = 0; c < col; c++)
            {
                cells[r, c].GetComponent<Cell>().ChangeState(cellsState[r, c]);
            }
        }

        inputField = inputField.GetComponent<InputField>();
    }

    void Update()
    {
       if(mode == Mode.Auto)
        {
            changeTime -= Time.deltaTime;
            if(changeTime <= 0)
            {
                Next();
                changeTime = 0.2f;
            }
       }
    }
    public void NextState()
    {
        var state = new LifeState[raw,col];
        for (int r = 0; r < raw; r++)
        {
            for(int c = 0; c < col; c++)
            {
                bool isTop = r == 0;
                bool isBottom = r == raw - 1;
                bool isLeft = c == 0;
                bool isRight = c == col - 1;
                var surviveCount = 0;

                if (!isTop && CellState(r - 1, c))
                {
                    surviveCount++;
                }
                if (!isBottom && CellState(r + 1, c))
                {
                    surviveCount++;
                }
                if (!isLeft && CellState(r, c - 1))
                {
                    surviveCount++;
                }
                if (!isRight && CellState(r, c + 1))
                {
                    surviveCount++;
                }
                if (!isTop && !isLeft && CellState(r - 1, c - 1))
                {
                    surviveCount++;
                }
                if (!isTop && !isRight && CellState(r - 1, c + 1))
                {
                    surviveCount++;
                }
                if (!isBottom && !isLeft && CellState(r + 1, c - 1))
                {
                    surviveCount++;
                }
                if (!isBottom && !isRight && CellState(r + 1, c + 1))
                {
                    surviveCount++;
                }

                var isLife = false;
                if (CellState(r, c) && surviveCount == 2 || surviveCount == 3)
                {
                    isLife = true;
                }
                else if (!CellState(r, c) && surviveCount == 3)
                {
                    isLife = true;
                }

                if (isLife)
                {
                    state[r, c] = LifeState.Survive;
                }
                else
                {
                    state[r, c] = LifeState.Death;
                }
            }
        }
        cellsState = state;
        for(int r = 0; r < raw; r++)
        {
            for (int c = 0; c < col; c++)
            {
                cells[r, c].GetComponent<Cell>().ChangeState(cellsState[r, c]);
            }
        }

    }
    public void Next()
    {
        NextState();
        
        progrees++;
    }

   public void SetProgrees()
   {
        var p = int.Parse(inputField.text);
        progrees = p;
        for (int i = 0; i < p; i++)
        {
            NextState();
        }
   }
    public bool CellState(int r, int c)
    {
        return cellsState[r, c] == LifeState.Survive;
    }
}

public enum Mode
{ 
    Manual,
    Auto
}

