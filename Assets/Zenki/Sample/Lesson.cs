using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lesson : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //int x = 100;
       
        ////＋演算子による文字列結合
        //Debug.Log("i = " + x);

        ////$から始まる文字列補間を使った場合
        //Debug.Log($"i = {x}");

        ////上の文字列補間、実態はstring.Format()と同義
        //Debug.Log(string.Format("i = {0}", x));

        ////変数型を暗黙的に推論してくれるvarが便利
        //var y = 1;

        //要素から型推論可能なら要素型は省略可能
        //[]内の要素数も{}要素数から推論可能
        int[] array = new[] {10, 20, 30 };
        for(var i = 0; i < 3; i++)
        {
            Debug.Log($"array[{i}] = {array[i]}");
        }
        
    }
}
