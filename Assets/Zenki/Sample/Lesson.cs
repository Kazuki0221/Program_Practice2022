using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lesson : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //int x = 100;
       
        ////�{���Z�q�ɂ�镶���񌋍�
        //Debug.Log("i = " + x);

        ////$����n�܂镶�����Ԃ��g�����ꍇ
        //Debug.Log($"i = {x}");

        ////��̕������ԁA���Ԃ�string.Format()�Ɠ��`
        //Debug.Log(string.Format("i = {0}", x));

        ////�ϐ��^���ÖٓI�ɐ��_���Ă����var���֗�
        //var y = 1;

        //�v�f����^���_�\�Ȃ�v�f�^�͏ȗ��\
        //[]���̗v�f����{}�v�f�����琄�_�\
        int[] array = new[] {10, 20, 30 };
        for(var i = 0; i < 3; i++)
        {
            Debug.Log($"array[{i}] = {array[i]}");
        }
        
    }
}
