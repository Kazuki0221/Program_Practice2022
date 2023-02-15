using System;
using System.Collections;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class Actor : MonoBehaviour
{
    [SerializeField]
    private Image[] _characters = default;
    private Image[] _backGraounds = default;


    //-------------------�f���Q�[�g���g���ꍇ--------------------------
    //public IEnumerator FadeIn(float time, Func<bool> condition)
    //{
    //    //Debug.Log($"{nameof(Actor)}{nameof(FadeIn)}:{nameof(time)} = {time}", this);
    //    var color = _image.color;
    //    var elapsed = 0f;
    //    while(condition() && elapsed < time)
    //    {
    //        elapsed += Time.deltaTime;
    //        color.a = elapsed / time;
    //        _image.color = color;
    //        yield return null;
    //    }

    //    color.a = 1f;
    //    _image.color = color;
    //    yield return null;
    //}

    //public IEnumerator FadeOut(float time, Func<bool> condition)
    //{
    //    //Debug.Log($"{nameof(Actor)}{nameof(FadeOut)}:{nameof(time)} = {time}", this);

    //    var color = _image.color;
    //    var elapsed = 0f;
    //    while (condition() && elapsed < time)
    //    {
    //        elapsed += Time.deltaTime;
    //        color.a = 1- elapsed / time;
    //        _image.color = color;
    //        yield return null;
    //    }

    //    color.a = 0f;
    //    _image.color = color;
    //    yield return null;
    //}

    //-------------------�N���X���g���ꍇ-------------------------------
    //public IEnumerator FadeIn(float time, SkipRequestToken skip)
    //{
    //    Debug.Log($"Actor FadeIn: time={time}", this);
    //    var color = _image.color;

    //    // color �̃A���t�@�l�����X�� 1 �ɋ߂Â��鏈��
    //    var elapsed = 0F;
    //    while (!skip.IsSkipRequested && elapsed < time)
    //    {
    //        elapsed += Time.deltaTime;
    //        color.a = elapsed / time;
    //        _image.color = color;
    //        yield return null;
    //    }

    //    color.a = 1;
    //    _image.color = color;
    //    yield return null;
    //}

    //public IEnumerator FadeOut(float time, SkipRequestToken skip)
    //{
    //    Debug.Log($"Actor FadeOut: time={time}", this);
    //    var color = _image.color;

    //    // color �̃A���t�@�l�����X�� 0 �ɋ߂Â��鏈��
    //    var elapsed = 0F;
    //    while (!skip.IsSkipRequested && elapsed < time)
    //    {
    //        elapsed += Time.deltaTime;
    //        color.a = 1 - elapsed / time;
    //        _image.color = color;
    //        yield return null;
    //    }

    //    color.a = 0;
    //    _image.color = color;
    //    yield return null;
    //}


    //--------------CancellationTokenSource()���g���ꍇ-------------------
    public IEnumerator FadeIn(float time, CancellationToken ct)
    {
        Debug.Log($"Actor FadeIn: time={time}", this);
        var color = _characters[0].color;

        // color �̃A���t�@�l�����X�� 1 �ɋ߂Â��鏈��
        var elapsed = 0F;
        while (!ct.IsCancellationRequested && elapsed < time)
        {
            elapsed += Time.deltaTime;
            color.a = elapsed / time;
            _characters[0].color = color;
            yield return null;
        }

        color.a = 1;
        _characters[0].color = color;
        yield return null;
    }

    public IEnumerator FadeOut(float time, CancellationToken ct)
    {
        Debug.Log($"Actor FadeOut: time={time}", this);
        var color = _characters[0].color;

        // color �̃A���t�@�l�����X�� 0 �ɋ߂Â��鏈��
        var elapsed = 0F;
        while (!ct.IsCancellationRequested && elapsed < time)
        {
            elapsed += Time.deltaTime;
            color.a = 1 - elapsed / time;
            _characters[0].color = color;
            yield return null;
        }

        color.a = 0;
        _characters[0].color = color;
        yield return null;
    }
    
}
