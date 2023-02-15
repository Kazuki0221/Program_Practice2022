using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    GameObject target;                          //�N���b�N��̃u���b�N�𓾂邽�߂̕ϐ�
    [SerializeField] int hCount;                //���̃u���b�N��
    [SerializeField] int vCount;                //�c�̃u���b�N��
    [SerializeField] int bombCount;             //�S���e��

    Field field;                                //field�R���|�[�l���g�̕ϐ�

    bool isStart = false;                       //�X�^�[�g���邩�ǂ����̔���
    bool isGame = true;                         //�Q�[�����v���C���Ă��邩�ǂ����̔���
    bool gameOver = false;                      //�Q�[���I�[�o�[�̔���

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
        //�Q�[���̃v���C���̋���
        if(isGame == true)
        {
            time += Time.deltaTime;
            //���N���b�N�Ńu���b�N���J��
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
            //�E�N���b�N�Ŋ���ݒu�E����
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
