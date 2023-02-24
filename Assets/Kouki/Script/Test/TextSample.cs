using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TextSample : MonoBehaviour
{
    //Text
    [Header("テキスト")]
    [SerializeField] TextMeshProUGUI m_context;
    [SerializeField] TextMeshProUGUI m_name;

    [SerializeField] List<string> m_strList = new List<string>();   //文字列List
    int nowCount = 0;
    [SerializeField] float interval = 5.0f;         //自動で次のテキストへ進む間隔
    float m_time = 0;
    [SerializeField] float feedTime = 1.0f;         //文字を一文字ずつ表示する際の間隔
    float m_feed = 0;

    int textNumber = 0;                             //文字列の文字番号
    bool beginText = true;                          //テキストの開始フラグ
    bool isSkip = false;                            //文字スキップのフラグ

    //BackGroundFade
    [Header("背景")]
    [SerializeField] FadeSample fadeSample = default;
    bool isBackGroundFade = false;
    [SerializeField] int[] fadeTiming;
    int ftCount = 0;

    //Character
    [Header("キャラクター")]
    [SerializeField] CharaFadeSample charaFadeSample = default;
    bool isChangeCharacter = false;
    [SerializeField] int chageTiming = 0;


    void Start()
    {
        m_name.text = "ヒツジくん";
        m_context.text = "";
        m_time = interval;
        m_feed = feedTime;

        var color = charaFadeSample.charaImg[1].color;
        color.a = 0;
        charaFadeSample.charaImg[1].color = color;
    }
    void Update()
    {
        if (nowCount < m_strList.Count - 1)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (m_feed > 0) //文字を表示している最中のみ実行
                {
                    isSkip = true;
                }
                else　　　　　　//次のテキストへ
                {
                    m_time = interval;
                    nowCount++;
                }
                beginText = true;

            }

            if (!beginText)　　　　//文字を最後まで表示するまでは時間はカウントしない
            {
                m_time -= 0.01f;
            }

            if (m_time <= 0)　　　//自動で次のテキストへ
            {
                nowCount++;
                m_time = interval;
                beginText = true;
            }

            if (nowCount == fadeTiming[ftCount])
            {
                isBackGroundFade = true;
            }

            if (nowCount == chageTiming)
            {
                isChangeCharacter = true;
                m_name.text = "オオカミくん";
            }

        }
        UpdateText();
        if (isBackGroundFade)
        {
            if (nowCount == fadeTiming[ftCount])
            {
                fadeSample.isFade = true;
                fadeSample.FadeBackGround();
            }
            
            if (!fadeSample.isFade)
            {
                isBackGroundFade = false;
                if(ftCount < fadeTiming.Length - 1)
                {
                    ftCount++;
                }
            }
        }

        if(isChangeCharacter)
        {
            charaFadeSample.isFade = true;
            var chara1 = charaFadeSample.charaImg[0];
            var chara2 = charaFadeSample.charaImg[1];
            charaFadeSample.CharaFade(chara1, chara2);

            if(!charaFadeSample.isFade)
            {
                isChangeCharacter = false;
            }
        }

    }
    //テキストを表示する際の処理をまとめた関数
    void UpdateText()
    {
        if (isSkip)//スキップ処理(文字)
        {
            m_context.text = m_strList[nowCount];
            isSkip = false;
            beginText = false;
            m_feed = feedTime;
            textNumber = 0;
        }
        if (beginText)//テキストを一文字ずつ表示
        {
            if (textNumber < m_strList[nowCount].Length)
            {
                m_feed -= 0.01f;
                if (m_feed <= 0)
                {
                    textNumber++;
                    m_context.text = m_strList[nowCount].Substring(0, textNumber);
                    m_feed = feedTime;
                }
            }
            else
            {
                beginText = false;
                m_feed = feedTime;

                textNumber = 0;
            }
        }
    }
}
