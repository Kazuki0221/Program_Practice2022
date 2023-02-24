using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class NovelManager : MonoBehaviour
{
    private NovelData novelData;

    enum Mode
    { 
        Normal,
        Auto,
    }
    Mode mode = Mode.Normal;

    enum Speed
    { 
        Normal,
        HighSpeed
    }
    Speed sMode = Speed.Normal;

    //Text
    [Header("�e�L�X�g")]
    [SerializeField] TextMeshProUGUI m_context;
    [SerializeField] TextMeshProUGUI m_name;
    int nowCount = 0;
    /// <summary>
    /// �����Ŏ��̃e�L�X�g�֐i�ފԊu
    /// </summary>
    [SerializeField] float interval = 5.0f;         
    float m_time = 0;
    float elapsedSpeedOfTime = 0.01f;
    float tempelapsed;
    /// <summary>
    /// �������ꕶ�����\������ۂ̊Ԋu
    /// </summary>
    [SerializeField] float delayTime = 1.0f;        
    float delaySpeed = 0.01f;
    float tempdelaySpeed;
    float m_delay = 0;
    int textNumber = 0;                             //������̕����ԍ�
    bool beginText = true;                          //�e�L�X�g�̊J�n�t���O
    bool isSkip = false;                            //�����X�L�b�v�̃t���O

    [Header("UI"),SerializeField] float fadeSpeed = 1f;
    [SerializeField]Button[] modeButton;
    bool onClickButton = false;

    //BackGroundFade
    [Header("�w�i")]
    [SerializeField] Image[] backGrounds;
    int imageNumber = 0;
    bool isBackGroundFade = false;
    [SerializeField] int[] fadeTiming;

    //Character
    [Header("�L�����N�^�[")]
    [SerializeField,Tooltip("�\���p�C���[�W")] Image[] charaImg = default;
    int imgCount = 0;
    /// <summary>
    /// �摜�f�[�^
    /// </summary>
    [SerializeField] List<Sprite> charaImgs = default;
    bool isChangeCharacter = false;
    Color white = Color.white;
    Color gray = Color.gray;

    void Start()
    {
        novelData = new NovelData();
        novelData.Init();
        m_name.text = "";
        m_context.text = "";
        m_time = interval;
        m_delay = delayTime;

        for(int i = 0; i < backGrounds.Length; i++)
        {
            backGrounds[i].sprite = Resources.Load<Sprite>(novelData.backGroundAdress[i]);
            if(i > 0)
            {
                var backColor = backGrounds[i].color;
                backColor.a = 0;
                backGrounds[i].color = backColor;
            }
        }

        foreach(var charaAdress in novelData.charaAdress)
        {
            var img = Resources.Load<Sprite>(charaAdress);
            if (!charaImgs.Contains(img))
            {
                charaImgs.Add(img);
            }
        }

        for (int i = 0; i < charaImg.Length; i++)
        {
            charaImg[i].sprite = charaImgs[i];
            if (i > 0)
            {
                var charaColor = charaImg[i].color;
                charaColor.a = 0;
                charaImg[i].color = charaColor;
            }
        }
    }
    void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject() && onClickButton)
        {
            onClickButton = false;
            return;
        }

        if (nowCount < novelData.text.Count)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (beginText) //������\�����Ă���Œ��̂ݎ��s
                {
                    isSkip = true;
                }
                else if (nowCount < novelData.text.Count - 1)�@�@//���̃e�L�X�g��
                {
                    m_time = interval;
                    nowCount++;
                    m_context.text = "";
                    beginText = true;

                    if (novelData.onBackFade[nowCount])
                    {
                        isBackGroundFade = true;
                    }

                    if (nowCount > 0 && novelData.onCharaChange[nowCount])
                    {
                        isChangeCharacter = true;
                    }
                }
            }

            if (!beginText)�@�@�@�@//�������Ō�܂ŕ\������܂ł͎��Ԃ̓J�E���g���Ȃ�
            {
                m_time -= elapsedSpeedOfTime;
            }

            if (mode == Mode.Auto && m_time <= 0 && nowCount < novelData.text.Count - 1)�@�@�@//�����Ŏ��̃e�L�X�g��
            {
                nowCount++;
                m_time = interval;
                beginText = true;
            }
        }
        if (beginText)
        {
            UpdateTalk();
        }
    }

    /// <summary>
    /// ��b�̗�����Ǘ�����֐�
    /// </summary>
    void UpdateTalk()
    {
        if (isBackGroundFade)
        {
            UpdateGround();
        }

        if (isChangeCharacter)
        {
            if (charaImg[0].sprite != charaImgs[novelData.id[nowCount]] && charaImg[1].sprite != charaImgs[novelData.id[nowCount]])
            {
                charaImg[imgCount % 2].sprite = charaImgs[novelData.id[nowCount]];
                charaImg[imgCount % 2].color = gray;
                charaImg[(imgCount + 1) % 2].color = white;
                imgCount++;
            }
            var chara1 = charaImg[0];
            var chara2 = charaImg[1];
            UpdateCharacter(chara1, chara2);
        }
        UpdateText();
    }
    /// <summary>
    /// �e�L�X�g��\������ۂ̏������܂Ƃ߂��֐�
    /// </summary>
    void UpdateText()
    {
        m_name.text = novelData.name[nowCount];
        if (isSkip)//�X�L�b�v����(����)
        {
            m_context.text = novelData.text[nowCount];
            isSkip = false;
            beginText = false;
            m_delay = delayTime;
            textNumber = 0;
        }
        if (beginText)//�e�L�X�g���ꕶ�����\��
        {
            if (textNumber < novelData.text[nowCount].Length)
            {
                m_delay -= delaySpeed;
                if (m_delay <= 0)
                {
                    textNumber++;
                    m_context.text = novelData.text[nowCount].Substring(0, textNumber);
                    m_delay = delayTime;
                }
            }
            else
            {
                beginText = false;
                m_delay = delayTime;
                textNumber = 0;
            }
        }
    }
    /// <summary>
    /// �w�i�̃t�F�[�h����
    /// </summary>
    void UpdateGround()
    {
        if (imageNumber + 1 >= backGrounds.Length) return;

        var c1 = backGrounds[imageNumber].color;
        var c2 = backGrounds[imageNumber + 1].color;

        if (isSkip)
        {
            c1.a = 0;
            c2.a = 1;
            backGrounds[imageNumber].color = c1;
            backGrounds[imageNumber + 1].color = c2;
            isBackGroundFade = false;
            imageNumber++;
        }

        if (isBackGroundFade)
        {
            c1.a -= Time.deltaTime * fadeSpeed;
            c2.a += Time.deltaTime * fadeSpeed;

            backGrounds[imageNumber].color = c1;
            backGrounds[imageNumber + 1].color = c2;

            if (c1.a <= 0 && c2.a >= 1)
            {
                isBackGroundFade = false;
                imageNumber++;
            }
        }
    }
    /// <summary>
    /// �L�����N�^�[�̃t�F�[�h����
    /// </summary>
    /// <param name="chara1"></param>
    /// <param name="chara2"></param>
    void UpdateCharacter(Image chara1, Image chara2)
    {
        var c1 = chara1.color;
        var c2 = chara2.color;

        if (isSkip)
        {
            if (c2.a <= 1)
            {
                c1 = gray;
                c2.a = 1;
                chara1.color = c1;
                chara2.color = c2;
                isChangeCharacter = false;
            }
            else if (c1 == gray && c2 == white)
            {
                c1 = white;
                c2 = gray;
                chara1.color = c1;
                chara2.color = c2;
                isChangeCharacter = false;
            }
            else
            {
                c1 = gray;
                c2 = white;
                chara1.color = c1;
                chara2.color = c2;
                isChangeCharacter= false;
            }
        }
        if (isChangeCharacter)
        {
            if (c2.a < 1)
            {
                c1 = gray;                
                c2.a += Time.deltaTime * fadeSpeed;
                chara1.color = c1;
                chara2.color = c2;

                if (c2.a >= 1)
                {
                    isChangeCharacter = false;
                }
            }
            else if (c1 == gray && c2 == white)
            {
                c1 = white;
                c2 = gray;
                chara1.color = c1;
                chara2.color = c2;
                isChangeCharacter = false;
            }
            else
            {
                c1 = gray;
                c2 = white;
                chara1.color = c1;
                chara2.color = c2;
                isChangeCharacter= false;
            }
        }
    }

    public void ClickAutoBtn()
    {        
        if(mode == Mode.Normal)
        {
            modeButton[0].image.color = gray;
            mode = Mode.Auto;
        }
        else if(mode == Mode.Auto)
        {
            modeButton[0].image.color = white;
            mode= Mode.Normal;
        }
        onClickButton = true;
    }

    public void ClickSpeedBtn()
    {
        if (sMode == Speed.Normal)
        {
            modeButton[1].image.color = gray;
            sMode = Speed.HighSpeed;
            tempelapsed = elapsedSpeedOfTime;
            elapsedSpeedOfTime *= 2;
            tempdelaySpeed = delaySpeed;
            delaySpeed *= 2;
        }
        else if(sMode == Speed.HighSpeed)
        {
            modeButton[1].image.color = white;
            sMode = Speed.Normal;
            elapsedSpeedOfTime = tempelapsed;
            delaySpeed = tempdelaySpeed;
        }
        onClickButton = true;
    }
}
