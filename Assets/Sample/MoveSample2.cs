using System.Collections;
using System.Globalization;
using UnityEngine;
public interface IAwaiter<T> // ���ʂ��󂯎�鑤�̂��߂̃C���^�[�t�F�C�X
{
    /// <summary>
    /// �������I���������ǂ����B
    /// </summary>
    bool IsCompleted { get; }

    /// <summary>
    /// �����̌��ʁB
    /// </summary>
    T Result { get; }
}

class Awaiter<T> : IAwaiter<T> // ���ʂ�ݒ肷�鑤�̎���
{
    public bool IsCompleted { get; private set; }

    public T Result { get; private set; }

    /// <summary>
    /// �������I�����Č��ʂ�ݒ肷��B
    /// </summary>
    public void SetResult(T result)
    {
        Result = result;
        IsCompleted = true;
    }
}

public class MoveSample2 : MonoBehaviour
{
    //private void Start()
    //{
    //    StartCoroutine(RunAsync());
    //}

    //private IEnumerator RunAsync()
    //{
    //    while (true)
    //    {
    //        Debug.Log("�}�E�X�̃{�^�����͂�҂��܂�");
    //        yield return WaitForMouseButtonDown(out var awaiter);

    //        // Awaiter �̏I����́A�K�����ʂ��ۏ؂���Ă���
    //        Debug.Log($"�}�E�X��{awaiter.Result}�{�^����������܂���");
    //        yield return null;
    //    }
    //}

    //private IEnumerator WaitForMouseButtonDown(out IAwaiter<int> awaiter)
    //{
    //    var awaiterImpl = new Awaiter<int>();
    //    var e = WaitForMouseButtonDown(awaiterImpl);
    //    awaiter = awaiterImpl;
    //    return e;
    //}

    //private IEnumerator WaitForMouseButtonDown(Awaiter<int> awaiter)
    //{
    //    // �ǂ̃}�E�X�{�^���������ꂽ�̂��A���ʂ�Ԃ������B
    //    while (true)
    //    {
    //        for (var i = 0; i < 3; i++)
    //        {
    //            if (Input.GetMouseButtonDown(i))
    //            {
    //                awaiter.SetResult(i); // �������I���E���ʂ�ݒ�
    //                yield break;
    //            }
    //        }

    //        yield return null;
    //    }
    //}

    private void Start()
    {
        StartCoroutine(Run());
    }

    private IEnumerator Run()
    {
        var selection = new string[]
            {
                "�I����1",
                "�I����2",
                "�I����3",
            };
        Debug.Log("�I������\�����ē��͂�҂��܂��B");
        yield return WaitForSelection(selection, out var awaiter);
        Debug.Log($"�I�������ʂ� {selection[awaiter.Result]} �ł����B"); 

    }
    public IEnumerator WaitForSelection(string[] messages, out IAwaiter<int> awaiter)
    {
        var result = new Awaiter<int>();
        var e = WaitForSelection(messages, result);
        awaiter = result;
        return e;
    }

    private IEnumerator WaitForSelection(string[] messages, Awaiter<int> awaiter)
    {
        yield return null;
    }
}
