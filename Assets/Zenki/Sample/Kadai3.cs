using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Kadai3 : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] int row;
    [SerializeField] int col;
    Image[,] images;
    int rNum = 0;
    int cNum = 0;

    float time = 0;
    float clickCount = 0;
    Color[] color = { Color.black, Color.white }; 

    private void Start()
    {
        images = new Image[row, col];

        int wCount = 0;
        int bCount = 0;

        for (var r = 0; r < row; r++)
        {
            for (var c = 0; c < col; c++)
            {
                var obj = new GameObject($"Cell({r}, {c})");
                obj.transform.parent = transform;

                int index = Random.Range(0, 2);
                images[r, c] = obj.AddComponent<Image>();
                images[r, c].color = color[index];
                if (images[r, c].color == color[0]) bCount++;
                else if (images[r, c].color == color[1]) wCount++;
            }
        }
        if (bCount == row * col || wCount == row * col)
        {
            while (true)
            {
                for (var r = 0; r < row; r++)
                {
                    for (var c = 0; c < col; c++)
                    {
                        if (r == 0 && c == 0)
                        {
                            bCount = 0;
                            wCount = 0;
                        }
                        int index = Random.Range(0, 2);
                        images[r, c].color = color[index];
                        if (images[r, c].color == color[0]) bCount++;
                        else if (images[r, c].color == color[1]) wCount++;
                    }
                }
                if (bCount != row * col || wCount != row * col) break;
            }
        }
        Debug.Log("start");
    }

    private void Update()
    {
        time += Time.deltaTime;
        for (var r = 0; r < row; r++)
        {
            for (var c = 0; c < col; c++)
            {
                if (images[r, c].color == Color.white) return;
            }
        }
        //Debug.Log("ok");
        //Debug.Log($"Time:{time}  Žè”:{clickCount}");
        return;
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        clickCount++;
        var cell = eventData.pointerCurrentRaycast.gameObject;
        var name = cell.name.Replace("Cell(", "");
        name = name.Replace(")", "");
        var str = name.Split(",");
        rNum = int.Parse(str[0]);
        cNum = int.Parse(str[1]);

        bool isTop = rNum == 0;
        bool isBottom = rNum == row - 1;
        bool isLeft = cNum == 0;
        bool isRight = cNum == col - 1;


        if (images[rNum, cNum].color == Color.white)
        {
            images[rNum, cNum].color = Color.black;
        }
        else
        {
            images[rNum, cNum].color = Color.white;
        }
        if (!isTop)
        {
            if (images[rNum - 1, cNum].color == Color.white)
            {
                images[rNum - 1, cNum].color = Color.black;
            }
            else
            {
                images[rNum - 1, cNum].color = Color.white;
            }
        }
        if (!isBottom)
        {
            if (images[rNum + 1, cNum].color == Color.white)
            {
                images[rNum + 1, cNum].color = Color.black;
            }
            else
            {
                images[rNum + 1, cNum].color = Color.white;
            }
        }
        if (!isLeft)
        {
            if (images[rNum, cNum - 1].color == Color.white)
            {
                images[rNum, cNum - 1].color = Color.black;
            }
            else
            {
                images[rNum, cNum - 1].color = Color.white;
            }
        }
        if (!isRight)
        {
            if (images[rNum, cNum + 1].color == Color.white)
            {
                images[rNum, cNum + 1].color = Color.black;
            }
            else
            {
                images[rNum, cNum + 1].color = Color.white;
            }
        }
    }

    bool JudgeStart(int r, int c)
    {
        bool isTop = r == 0;
        bool isBottom = r == row - 1;
        bool isLeft = c == 0;
        bool isRight = c == col - 1;

        int wCount = 0;
        for (int x = 0; x < row; x++)
        {
            for(int y = 0; y < col; y++)
            {
                if (x == r && y == c && images[x, y].color == color[1])
                {
                    wCount++;
                    continue;
                }
                if (!isTop && x == r - 1 && y == c && images[x, y].color == color[1])
                {
                    wCount++;
                    continue;
                }
                if(!isBottom && x == r + 1 && y == c && images[x, y].color == color[1])
                {
                    wCount++;
                    continue;
                }
                if (!isLeft && x == r && y == c - 1 && images[x, y].color == color[1])
                {
                    wCount++;
                    continue;
                }
                if (!isRight && x == r && y == c + 1 && images[x, y].color == color[1])
                {
                    wCount++;
                    continue;
                }
                if (images[x, y].color == color[1])
                {
                    wCount++;
                }
            }
        }
        if (!isTop && !isBottom && !isLeft && !isRight && wCount == 4) return false;
        else if((!isTop || !isBottom || !isLeft || !isRight) && wCount == 3) return false;
        
        return true;
    }
}
