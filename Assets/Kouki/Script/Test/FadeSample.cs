using UnityEngine;
using UnityEngine.UI;

public class FadeSample : MonoBehaviour
{
    //BackGround
    [SerializeField] Image[] image;
    int imageNumber = 0;
    public bool isFade = false;
    [SerializeField] float fadeSpeed = 1f;

    public void FadeBackGround()
    {
        if (imageNumber + 1 >= image.Length) return;

        if (isFade)
        {
            Color c1 = image[imageNumber].color;
            Color c2 = image[imageNumber + 1].color;
            c1.a -= Time.deltaTime * fadeSpeed;
            c2.a += Time.deltaTime * fadeSpeed;
            image[imageNumber].color = c1;
            image[imageNumber + 1].color = c2;
            

            if (c1.a <= 0 && c2.a >= 1)
            {
                isFade = false;
                imageNumber++;
            }
        }
    }
}
