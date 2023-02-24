using System.Collections;
using System.Threading;
using UnityEngine;
using SkipScript;

//public class SkipRequestSource
//{
//    /// <summary>
//    /// �X�L�b�v����p�̃g�[�N����Ԃ��B
//    /// </summary>
//    public SkipRequestToken Token
//        => new SkipRequestToken(this);

//    /// <summary>
//    /// �X�L�b�v��v������Ă���ꍇ�� true�B
//    /// </summary>
//    public bool IsSkipRequested { get; private set; }

//    /// <summary>
//    /// �X�L�b�v��v������B
//    /// </summary>
//    public void Skip() { IsSkipRequested = true; }
//}

//public struct SkipRequestToken
//{
//    private SkipRequestSource _source;

//    public SkipRequestToken(SkipRequestSource source)
//        => _source = source;

//    /// <summary>
//    /// �X�L�b�v��v������Ă���ꍇ�� true�B
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
            yield return WaitClick(); // �N���b�N��҂�
            yield return null; // ���O�� GetMouseButtonDown ���A�����Ȃ��悤��1�t���[���҂�

            var cts = new CancellationTokenSource();
            StartCoroutine(CancelIfClicked(cts));
            yield return _actor.FadeOut(2, cts.Token); // 2�b�����ăt�F�[�h�A�E�g

            yield return WaitClick(); // �N���b�N��҂�
            yield return null; // ���O�� GetMouseButtonDown ���A�����Ȃ��悤��1�t���[���҂�

            cts = new CancellationTokenSource();
            StartCoroutine(CancelIfClicked(cts));
            yield return _actor.FadeIn(2, cts.Token); // �Q�b�����ăt�F�[�h�C��

            yield return WaitClick(); // �N���b�N��҂�
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
