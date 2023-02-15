using UnityEngine;
using UnityEngine.UI;

public class Kadai1 : MonoBehaviour
{
    int num = 0;
    [SerializeField] int count;
    Image[] img;
    private void Start()
    {
        img = new Image[count];
        for (var i = 0; i < count; i++)
        {
            var obj = new GameObject($"Cell{i}");
            obj.transform.parent = transform;

            img[i] = obj.AddComponent<Image>();
            if (i == 0) { img[i].color = Color.red; }
            else { img[i].color = Color.white; }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow)) // 左キーを押した
        {
            if (num <= 0) num = img.Length - 1;
            else num--;

            for(int i = 1; img[num] == null; i++)//画像が空だったら飛ばす
            {
                if (i <= 0) num = img.Length - 1;
                else num--;
            }
        }
        if (Input.GetKeyDown(KeyCode.RightArrow)) // 右キーを押した
        {
            if(num >= img.Length - 1) num = 0;
            else num++;

            for (int i = 1; img[num] == null; i++)//画像が空だったら飛ばす
            {
                if (num >= img.Length - 1) num = 0;
                else num++;
            }

        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Destroy(img[num]);
            num++;
            if (num >= img.Length - 1) num = 0;
        }
        for (int i = 0; i < count; i++)
        {
            if (img[i])
            {
                if (i == num)
                {
                    img[i].color = Color.red;
                }
                else
                {
                    img[i].color = Color.white;
                }
            }
        }
    }
}