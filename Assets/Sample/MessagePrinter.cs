using TMPro;
using UnityEngine;

public class MessagePrinter : MonoBehaviour
{
    [SerializeField] 
    private TMP_Text _textUi = default;

    [SerializeField]
    private string _message = "";

    [SerializeField]
    private float _speed = 1.0f;

    private int _currentIndex = -1;
    private float _elapsed = 0;
    private float _interval;

    public bool IsPrinting
    {
        get
        {
            if(_currentIndex + 1 < _message.Length)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
    void Start()
    {
        ShowMessage(_message);
    }

    void Update()
    {
        if(_textUi is null || _message is null || _currentIndex + 1 >= _message.Length) { return; }

        _elapsed += Time.deltaTime;
        if (_elapsed > _interval)
        {
            _elapsed = 0;
            _currentIndex++;
            _textUi.text += _message[_currentIndex];
        }
    }

    public void ShowMessage(string message)
    {
        if (_textUi is null) { return; }
        _message = message;
        _textUi.text = "";
        _elapsed = 0;
        _currentIndex = -1;
        _interval = _speed / _message.Length;
    }

    public void Skip()
    {
        _textUi.text = _message;
        _currentIndex = _message.Length - 1;
    }
}
