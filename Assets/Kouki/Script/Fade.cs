using System;
using System.Collections;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class Fade : MonoBehaviour
{
    [SerializeField]
    private Image[] _characters = default;
    private Image[] _backGraounds = default;

    public IEnumerator FadeIn(float time, CancellationToken ct)
    {
        Debug.Log($"Actor FadeIn: time={time}", this);
        var color = _characters[0].color;

        // color のアルファ値を徐々に 1 に近づける処理
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

        // color のアルファ値を徐々に 0 に近づける処理
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
