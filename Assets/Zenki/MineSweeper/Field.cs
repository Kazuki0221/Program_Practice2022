using System.Collections.Generic;
using UnityEngine;

public class Field : MonoBehaviour
{
    [SerializeField] GameObject cellObject; 　　　　　　　 //ブロックのプレファブ
    public GameObject[,] blocks;　                         //全ブロック数

    float spaceSize = 1.2f;                                //ブロック同士の間隔

    //ステージ作成
    public void CreateBlocks(int hCount, int vCount, int bombCount)
    {
        blocks = new GameObject[hCount, vCount];
        InitiaLizedBlocks(hCount, vCount, bombCount);
    }

    //ステージの初期化
    void InitiaLizedBlocks(int raw, int col, int bc)
    {

        for (int r = 0; r < raw; r++)
        {
            for(int c = 0; c < col; c++)
            {
                blocks[r,c] = Instantiate(cellObject, new Vector3((c-5) * spaceSize , spaceSize, (5-r) *spaceSize), cellObject.transform.rotation);
                blocks[r, c].name = $"CloseBlock({r},{c})";
                blocks[r, c].tag = "Safty";
            }
        }
        var blocksLength = raw * col;
        if(bc > blocksLength)
        {
            bc = blocksLength;
        }
        
        for(int i = 0; i < bc;)
        {
            int rx = Random.Range(0, raw);
            int ry = Random.Range(0, col);

            if (blocks[rx, ry].tag != "Bomb")
            {
                i++;
                blocks[rx, ry].tag = "Bomb";
            }
        }
        

    }

    //ステージの再初期化
    public void ResetBlocks(int raw, int col, int bc, string name)
    {        
        for (int r = 0; r < raw; r++)
        {
            for (int c = 0; c < col; c++)
            {
                blocks[r, c].tag = "Safty";
            }
        }

        var blocksLength = raw * col;

        if (bc > blocksLength)
        {
            bc = blocksLength;
        }

        for (int i = 0; i < bc;)
        {
            int rx = Random.Range(0, raw);
            int ry = Random.Range(0, col);

            if (blocks[rx, ry].tag != "Bomb")
            {
                i++;
                blocks[rx, ry].tag = "Bomb";
            }
        }

        foreach (var b in blocks)
        {
            b.GetComponent<Block>().SetMaterial();
        }

        var _name = name.Replace("CloseBlock(", "");
        _name = _name.Replace(")", "");
        var str = _name.Split(",");
        int _raw = int.Parse(str[0]);
        int _col = int.Parse(str[1]);

        if (blocks[_raw, _col].tag == "Bomb")
        {
            ResetBlocks(raw, col, bc, $"CloseBlock({_raw},{_col})");
        }
        else if (blocks[_raw, _col].tag == "Safty")
        {
            OpenAdjacentBlock(_raw, _col);
        }
    }

    //周囲のブロックを配列として返す関数
    public GameObject[] GetAdjacentBlocks(int h, int v)
    {
        var b = new List<GameObject>();

        var isTop = h == 0;
        var isButtom = h == blocks.GetLength(0) - 1;
        var isLeft = v == 0;
        var isRight = v == blocks.GetLength(1) - 1;

        b.Add(blocks[h, v]);
        if (!isTop && !isLeft) b.Add(blocks[h - 1, v - 1]);
        if(!isTop && !isRight) b.Add(blocks[h - 1, v + 1]);
        if (!isButtom && !isLeft) b.Add(blocks[h + 1, v - 1]);
        if (!isButtom && !isRight) b.Add(blocks[h + 1, v + 1]);
        if (!isTop) b.Add(blocks[h - 1, v]);
        if (!isButtom) b.Add(blocks[h + 1, v]);
        if (!isLeft) b.Add(blocks[h, v - 1]);
        if (!isRight) b.Add(blocks[h, v + 1]);

        return b.ToArray();
    }

    //ブロックを開ける関数
    public void OpenAdjacentBlock(int h, int v)
    {
        var block = blocks[h, v].GetComponent<Block>();
        if (block.GetBombCount() == 0)
        {
            foreach (var b in GetAdjacentBlocks(h, v))
            {
                if (b.tag != "Open") b.GetComponent<Block>().Open();
            }
        }
        else if(block.GetBombCount() > 0)
        {
            block.Open();
        }
    }

    //クリア判定
    public bool Judge()
    {
        int openCount = 0;

        foreach(var b in blocks)
        {
            if (b.tag == "Open") openCount++;
        }
        
        if(openCount + FindObjectOfType<GameManager>().GetBombCount() == blocks.GetLength(0) * blocks.GetLength(1))
        {
            return true;
        }
        return false;
    }
}
