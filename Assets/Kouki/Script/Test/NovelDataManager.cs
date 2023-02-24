using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NovelDataManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] Image charaImg;
    [SerializeField] Image backImg;
    private NovelData novelData;
    int tCount = 0;
    int bCount = 0;
    int cCount = 0;
    private void Start()
    {
        novelData = new NovelData();
        novelData.Init();
    }

    public void ClickButton()
    {
        if (tCount >= novelData.text.Count) return;
        if(cCount >= novelData.charaAdress.Count)
        {
            cCount = novelData.charaAdress.Count - 1;
        }
        if(bCount >= novelData.backGroundAdress.Count)
        {
            bCount = novelData.backGroundAdress.Count - 1;
        }

        nameText.text = novelData.name[tCount];
        text.text = novelData.text[tCount];

        charaImg.sprite = Resources.Load<Sprite>(novelData.charaAdress[cCount]);

        backImg.sprite = Resources.Load<Sprite>(novelData.backGroundAdress[bCount]);

        
        tCount++;
        
        if (cCount < novelData.charaAdress.Count && tCount < novelData.text.Count && nameText.text != novelData.name[tCount])
        {
            cCount++;
        }
        if (bCount < novelData.backGroundAdress.Count)
        {
            bCount++;
        }

    }
}
