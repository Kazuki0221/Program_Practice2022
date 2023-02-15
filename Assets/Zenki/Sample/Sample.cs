using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Sample : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI[] text = new TextMeshProUGUI[2];
    [SerializeField]Button btn;
    private void Start()
    {
        btn.onClick.AddListener(PushButton);
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast((Vector2)ray.origin, (Vector2)ray.direction);
            if(!hit)
            {
                text[0].text = "Start";
                text[1].text = "Null";
            }
        }
    }

    public void PushButton()
    {
        text[0].text = "Null";
        text[1].text = "PushButton";
    }
}
