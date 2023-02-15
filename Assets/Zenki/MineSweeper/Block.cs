using System;
using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField] Material close = null;                   //���Ă����Ԃ̃}�e���A��

    [SerializeField] Material[] material = new Material[11];  //�J�������̃}�e���A��

    Material tempMaterial;�@�@�@�@�@�@�@�@�@�@�@�@�@�@�@�@�@�@//��ԗp�}�e���A���ϐ�

    int bombCount = 0;�@�@�@�@�@�@�@�@�@�@�@�@�@�@�@�@�@�@�@�@//���͂̔��e�̐�
    public Action OpenAdjacentBlock { get; private set;}

    public bool select = false;                               //���̃u���b�N���N���b�N�������ǂ���

    bool onFlag = false;                                      //���������Ă��邩�ǂ���
    void Start()
    {
        SetMaterial();
    }

   //�u���b�N���J�������̋���
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

    //���ݒu
    public void SetFlag()
    {
        GetComponent<Renderer>().material = material[9];
        onFlag = true;
    }

    //������
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

    //�}�e���A�����ԗp�ϐ��ɃZ�b�g����֐�
    void Set(int index)
    {
        tempMaterial = material[index];
    }

    //�}�e���A�����u���b�N�ɃZ�b�g
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
