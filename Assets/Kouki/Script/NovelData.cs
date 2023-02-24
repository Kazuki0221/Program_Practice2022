using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class NovelData 
{
    static TextAsset csvFile;
    static List<string[]> novelData = new List<string[]>();     //�V�i���I�f�[�^
    public List<int> id = new List<int>();�@�@�@�@�@�@�@�@�@�@�@//�L�������Ƃ�ID(�C���f�b�N�X)
    public List<string> name = new List<string>();�@�@�@�@�@�@�@//�L�������̃f�[�^
    public List<string> text = new List<string>();�@�@�@�@�@�@�@//�e�L�X�g�f�[�^
    public List<string> charaAdress = new List<string>();�@�@�@�@//�L�����摜�f�[�^
    public List<string> backGroundAdress = new List<string>();�@//�w�i�f�[�^
    public List<bool> onBackFade = new List<bool>();�@�@�@�@�@�@//�w�i���ύX�̎w��
    public List<bool> onCharaChange = new List<bool>();�@�@�@�@//�L�����̏o���A�ύX�̎w��

    /// <summary>
    /// CSV�t�@�C���̓ǂݍ���
    /// </summary>
    static void CSVReader()
    {
        csvFile = Resources.Load("NovelText") as TextAsset;
        StringReader reader = new StringReader(csvFile.text);
        while(reader.Peek() != -1)
        {
            string line = reader.ReadLine();
            novelData.Add(line.Split(','));
        }
    }

    public void Init()
    {
        CSVReader();

        if (novelData != null)
        {
            for (int i = 0; i < novelData.Count - 1; i++)
            {
                id.Add(int.Parse(novelData[i + 1][0]));
                name.Add(novelData[i + 1][1]);
                text.Add(novelData[i + 1][2]);

                charaAdress.Add(novelData[i + 1][3]);
                if (charaAdress[i] != "-")
                {
                    onCharaChange.Add(true);
                }
                else
                {
                    onCharaChange.Add(false);
                }

                backGroundAdress.Add(novelData[i + 1][4]);
                if (backGroundAdress[i] != "-")
                {
                    onBackFade.Add(true);
                }
                else
                {
                    onBackFade.Add(false);
                }
            }
            //NovelManager�ɑ���A�h���X�͉摜�f�[�^�݂̂ɂ������̂ŕύX���Ȃ��w��"-"�͍폜
            charaAdress.RemoveAll(s => s.Contains("-"));
            backGroundAdress.RemoveAll(s => s.Contains("-"));
        }
        else
        {
            Debug.Log("Error");
        }
    }
}
