using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class NovelData 
{
    static TextAsset csvFile;
    static List<string[]> novelData = new List<string[]>();     //シナリオデータ
    public List<int> id = new List<int>();　　　　　　　　　　　//キャラごとのID(インデックス)
    public List<string> name = new List<string>();　　　　　　　//キャラ名のデータ
    public List<string> text = new List<string>();　　　　　　　//テキストデータ
    public List<string> charaAdress = new List<string>();　　　　//キャラ画像データ
    public List<string> backGroundAdress = new List<string>();　//背景データ
    public List<bool> onBackFade = new List<bool>();　　　　　　//背景が変更の指示
    public List<bool> onCharaChange = new List<bool>();　　　　//キャラの出現、変更の指示

    /// <summary>
    /// CSVファイルの読み込み
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
            //NovelManagerに送るアドレスは画像データのみにしたいので変更しない指示"-"は削除
            charaAdress.RemoveAll(s => s.Contains("-"));
            backGroundAdress.RemoveAll(s => s.Contains("-"));
        }
        else
        {
            Debug.Log("Error");
        }
    }
}
