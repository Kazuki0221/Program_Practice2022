using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharaFadeSample : MonoBehaviour
{
    public Image[] charaImg = default;
    public bool isFade = false;
    float fadeSpeed = 1f;
    Color white = Color.white;
    Color gray = Color.gray;

    public void CharaFade(Image chara1, Image chara2)
    {
        var c1 = chara1.color;
        var c2 = chara2.color;
        if (isFade)
        {
            if (chara2.color.a <= 1)
            {
                c2.a += Time.deltaTime * fadeSpeed;
                chara2.color = c2;

                if (c2.a >= 1)
                {
                    isFade = false;
                }
            }
            else if (c1 == gray && c2 == white)
            {
                c1 = white;
                c2 = gray;
                isFade = false;
            }
            else
            {
                c1 = gray;
                c2 = white;
                isFade = false;
            }
        }
    }
}
