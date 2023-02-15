using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    GameObject target;                          //クリック先のブロックを得るための変数
    [SerializeField] int hCount;                //横のブロック数
    [SerializeField] int vCount;                //縦のブロック数
    [SerializeField] int bombCount;             //全爆弾数

    Field field;                                //fieldコンポーネントの変数

    bool isStart = false;                       //スタートするかどうかの判定
    bool isGame = true;                         //ゲームをプレイしているかどうかの判定
    bool gameOver = false;                      //ゲームオーバーの判定

    //UI
    float time = 0;                             
    [SerializeField] Text text;                 
    void Start()
    {
        field = FindObjectOfType<Field>();
        field.CreateBlocks(hCount, vCount, bombCount);
    }

    void Update()
    {
        //ゲームのプレイ中の挙動
        if(isGame == true)
        {
            time += Time.deltaTime;
            //左クリックでブロックを開く
            if (Input.GetMouseButtonDown(0))
            {
                target = null;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit = new RaycastHit();

                if (Physics.Raycast(ray, out hit))
                {
                    target = hit.collider.gameObject;
                    target.GetComponent<Block>().select = true;
                    
                    if (target.tag == "Safty")
                    {
                        isStart = true;
                        var name = target.name.Replace("CloseBlock(", "");
                        name = name.Replace(")", "");
                        var str = name.Split(",");
                        int raw = int.Parse(str[0]);
                        int col = int.Parse(str[1]);
                        field.OpenAdjacentBlock(raw, col);
                        if (field.Judge() == true)
                        {
                            isGame = false;
                        }
                    }
                    else if (target.tag == "Bomb" && isStart == false && bombCount < hCount * vCount )
                    {
                        field.ResetBlocks(hCount, vCount, bombCount, target.name);
                    }
                    else if (target.tag == "Bomb")
                    {
                        target.GetComponent<Block>().Open();
                        isGame = false;
                        gameOver = true;
                    }
                }
            }
            //右クリックで旗を設置・除去
            if (Input.GetMouseButtonDown(1))
            {
                target = null;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit = new RaycastHit();

                if (Physics.Raycast(ray, out hit))
                {
                    target = hit.collider.gameObject;
                    if(target.tag != "Open")
                    {
                        if (target.GetComponent<Block>().GetFlagTrigger() == false)
                        {
                            target.GetComponent<Block>().SetFlag();
                        }
                        else if (target.GetComponent<Block>().GetFlagTrigger() == true)
                        {
                            target.GetComponent<Block>().ResetFlag();
                        }

                    }
                }
            }
        }
        else
        {
            if(gameOver == true)
            {
                Debug.Log("GameOver");
            }
            else
            {
                Debug.Log("Clear");
            }
        }
        text.text = time.ToString("0");

    }

    public int GetBombCount()
    {
        return bombCount;
    }

}
