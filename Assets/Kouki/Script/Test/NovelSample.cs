using System.Collections;
using System.Threading;
using UnityEngine;
using SkipScript;

//public class SkipRequestSource
//{
//    /// <summary>
//    /// スキップ判定用のトークンを返す。
//    /// </summary>
//    public SkipRequestToken Token
//        => new SkipRequestToken(this);

//    /// <summary>
//    /// スキップを要求されている場合は true。
//    /// </summary>
//    public bool IsSkipRequested { get; private set; }

//    /// <summary>
//    /// スキップを要求する。
//    /// </summary>
//    public void Skip() { IsSkipRequested = true; }
//}

//public struct SkipRequestToken
//{
//    private SkipRequestSource _source;

//    public SkipRequestToken(SkipRequestSource source)
//        => _source = source;

//    /// <summary>
//    /// スキップを要求されている場合は true。
//    /// </summary>
//    public bool IsSkipRequested => _source.IsSkipRequested;
//}

public class NovelSample : MonoBehaviour
{
    [SerializeField]
    Fade _actor;
    void Start()
    {
        StartCoroutine(RunAsync());
    }

    IEnumerator RunAsync()
    {
        while (true)
        {
            yield return WaitClick(); // クリックを待つ
            yield return null; // 直前の GetMouseButtonDown が連続しないように1フレーム待つ

            var cts = new CancellationTokenSource();
            StartCoroutine(CancelIfClicked(cts));
            yield return _actor.FadeOut(2, cts.Token); // 2秒かけてフェードアウト

            yield return WaitClick(); // クリックを待つ
            yield return null; // 直前の GetMouseButtonDown が連続しないように1フレーム待つ

            cts = new CancellationTokenSource();
            StartCoroutine(CancelIfClicked(cts));
            yield return _actor.FadeIn(2, cts.Token); // ２秒かけてフェードイン

            yield return WaitClick(); // クリックを待つ
            yield return null;
        }
    }

    private IEnumerator CancelIfClicked(CancellationTokenSource cts)
    {
        while (!IsSkipRequested()) { yield return null; }
        cts.Cancel();
    }

    IEnumerator WaitClick()
    {
        while (!Input.GetMouseButtonDown(0))
        {
            while (!IsSkipRequested())
            {
                yield return null;
            }
        }
    }

    static bool IsSkipRequested()
    {
        return Input.GetMouseButtonDown(0);
    }
}
