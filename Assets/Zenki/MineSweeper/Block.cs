using System;
using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField] Material close = null;                   //閉じている状態のマテリアル

    [SerializeField] Material[] material = new Material[11];  //開いた時のマテリアル

    Material tempMaterial;　　　　　　　　　　　　　　　　　　//補間用マテリアル変数

    int bombCount = 0;　　　　　　　　　　　　　　　　　　　　//周囲の爆弾の数
    public Action OpenAdjacentBlock { get; private set;}

    public bool select = false;                               //このブロックをクリックしたかどうか

    bool onFlag = false;                                      //旗が立っているかどうか
    void Start()
    {
        SetMaterial();
    }

   //ブロックが開いた時の挙動
    public void Open()
    {
        if (this.tag == "Open")
        {
            return;
        }

        if(tag == "Bomb" && select == true)
        {

            Set(0);
            GetComponent<Renderer>().material = tempMaterial;
        }
        else
        {
            if (tag == "Safty")
            {
                tag = "Open";
                var name = this.gameObject.name.Replace("CloseBlock(", "");
                name = name.Replace(")", "");
                var str = name.Split(",");
                int raw = int.Parse(str[0]);
                int col = int.Parse(str[1]);

                var field = FindObjectOfType<Field>();

                if (bombCount == 0)
                {
                    field.OpenAdjacentBlock(raw, col);
                }
                GetComponent<Renderer>().material = tempMaterial;

            }
        }
    }

    //旗設置
    public void SetFlag()
    {
        GetComponent<Renderer>().material = material[9];
        onFlag = true;
    }

    //旗除去
    public void ResetFlag()
    {
        GetComponent<Renderer>().material = close;
        onFlag = false;
    }

    public int GetBombCount()
    {
        return bombCount;
    }

    public bool GetFlagTrigger()
    {
        return onFlag;
    }

    //マテリアルを補間用変数にセットする関数
    void Set(int index)
    {
        tempMaterial = material[index];
    }

    //マテリアルをブロックにセット
    public void SetMaterial()
    {
        bombCount = 0;
        if (tag == "Safty")
        {
            var name = this.gameObject.name.Replace("CloseBlock(", "");
            name = name.Replace(")", "");
            var str = name.Split(",");
            int raw = int.Parse(str[0]);
            int col = int.Parse(str[1]);

            var field = FindObjectOfType<Field>();

            foreach (var b in field.GetAdjacentBlocks(raw, col))
            {
                if (b.tag == "Bomb") bombCount++;
            }

            if (bombCount == 0)
            {
                Set(10);

            }
            else if (bombCount > 0)
            {
                Set(bombCount);
            }
        }
    }

}
