using UnityEngine;

public class MessageSequencer : MonoBehaviour
{
    [SerializeField] MessagePrinter _printer = default;

    [SerializeField] string[] _messages = default;
    private int _currentIndex = -1;
    void Start()
    {
        MoveNext();
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if (_printer.IsPrinting)
            {
                _printer.Skip();
            }
            else if(_printer is { IsPrinting: false})// == if(_printer != null && !_printer.IsPrinting)
            {
                MoveNext();
            }
        }
    }

    private void MoveNext()
    {
        if(_messages is null or { Length: 0 }) { return; }

        if(_currentIndex + 1 < _messages.Length)
        {
            _currentIndex++;
            _printer?.ShowMessage(_messages[_currentIndex]);
        }
    }
}
