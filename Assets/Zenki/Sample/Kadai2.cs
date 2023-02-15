using UnityEngine;
using UnityEngine.UI;

public class Kadai2 : MonoBehaviour
{
    [SerializeField] int row;
    [SerializeField] int col;
    Image[,] images;
    int rNum = 0;
    int cNum = 0;

    private void Start()
    {
        images = new Image[row, col];
        for (var r = 0; r < row; r++)
        {
            for (var c = 0; c < col; c++)
            {
                var obj = new GameObject($"Cell({r}, {c})");
                obj.transform.parent = transform;

                images[r, c] = obj.AddComponent<Image>();
            }
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            Debug.Log($"cel[{rNum},{cNum}])");
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow)) // 左キーを押した
        {
            if (cNum <= 0)
            {
                cNum = col - 1;
            }
            else cNum--;

            for (int i = 1; images[rNum, cNum] == null; i++)//画像が空だったら飛ばす
            {
                if (i <= 0) cNum = col - 1;
                else cNum--;
            }
        }
        if (Input.GetKeyDown(KeyCode.RightArrow)) // 右キーを押した
        {
            
            if (cNum >= col - 1) cNum = 0;
            else cNum++;

            for (int i = 1; images[rNum, cNum] == null; i++)//画像が空だったら飛ばす
            {
                if (cNum >= col - 1) cNum = 0;
                else cNum++;
            }


        }
        if (Input.GetKeyDown(KeyCode.UpArrow)) // 上キーを押した
        {

            if (rNum <= 0) rNum = row - 1;
            else rNum--;

            for (int i = 1; images[rNum, cNum] == null; i++)//画像が空だったら飛ばす
            {
                if (i <= 0) rNum = row - 1;
                else rNum--;
            }

        }
        if (Input.GetKeyDown(KeyCode.DownArrow)) // 下キーを押した
        {
            if (rNum >= row - 1) rNum = 0;
            else rNum++;

            for (int i = 1; images[rNum, cNum] == null; i++)//画像が空だったら飛ばす
            {
                if (rNum >= row - 1) rNum = 0;
                else rNum++;
            }

        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Destroy(images[rNum, cNum]);
            if (rNum >= row - 1 && cNum >= col - 1)
            {
                rNum = 0;
                cNum = 0;
            }
            else if(cNum >= col - 1)
            {
                rNum++;
                cNum = 0;
            }
            else
            {
                cNum++;
            }
            
        }
        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < col; j++)
            {
                if (images[i, j])
                {
                    if (i == rNum && j == cNum)
                    {
                        images[i, j].color = Color.red;
                    }
                    else
                    {
                        images[i, j].color = Color.white;
                    }
                }

            }
        }
    }
}