using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TextSample : MonoBehaviour
{
    //Text
    [Header("�e�L�X�g")]
    [SerializeField] TextMeshProUGUI m_context;
    [SerializeField] TextMeshProUGUI m_name;

    [SerializeField] List<string> m_strList = new List<string>();   //������List
    int nowCount = 0;
    [SerializeField] float interval = 5.0f;         //�����Ŏ��̃e�L�X�g�֐i�ފԊu
    float m_time = 0;
    [SerializeField] float feedTime = 1.0f;         //�������ꕶ�����\������ۂ̊Ԋu
    float m_feed = 0;

    int textNumber = 0;                             //������̕����ԍ�
    bool beginText = true;                          //�e�L�X�g�̊J�n�t���O
    bool isSkip = false;                            //�����X�L�b�v�̃t���O

    //BackGroundFade
    [Header("�w�i")]
    [SerializeField] FadeSample fadeSample = default;
    bool isBackGroundFade = false;
    [SerializeField] int[] fadeTiming;
    int ftCount = 0;

    //Character
    [Header("�L�����N�^�[")]
    [SerializeField] CharaFadeSample charaFadeSample = default;
    bool isChangeCharacter = false;
    [SerializeField] int chageTiming = 0;


    void Start()
    {
        m_name.text = "�q�c�W����";
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
                if (m_feed > 0) //������\�����Ă���Œ��̂ݎ��s
                {
                    isSkip = true;
                }
                else�@�@�@�@�@�@//���̃e�L�X�g��
                {
                    m_time = interval;
                    nowCount++;
                }
                beginText = true;

            }

            if (!beginText)�@�@�@�@//�������Ō�܂ŕ\������܂ł͎��Ԃ̓J�E���g���Ȃ�
            {
                m_time -= 0.01f;
            }

            if (m_time <= 0)�@�@�@//�����Ŏ��̃e�L�X�g��
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
                m_name.text = "�I�I�J�~����";
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
    //�e�L�X�g��\������ۂ̏������܂Ƃ߂��֐�
    void UpdateText()
    {
        if (isSkip)//�X�L�b�v����(����)
        {
            m_context.text = m_strList[nowCount];
            isSkip = false;
            beginText = false;
            m_feed = feedTime;
            textNumber = 0;
        }
        if (beginText)//�e�L�X�g���ꕶ�����\��
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
