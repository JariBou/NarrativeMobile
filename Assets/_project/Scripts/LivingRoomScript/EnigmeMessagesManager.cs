using UnityEngine;
using UnityEngine.Events;

public class EnigmeMessagesManager : MonoBehaviour
{
    private bool _firstEnigmaFinished = false;
    
    [SerializeField] private UnityEvent _onFirstEnigmaFinished;
    [SerializeField] private UnityEvent _onAllEnigmaFinished;

    public void EnigmaFinished()
    {
        if (_firstEnigmaFinished)
        {
            _onAllEnigmaFinished?.Invoke();
            return;
        }
        _firstEnigmaFinished = true;
        _onFirstEnigmaFinished?.Invoke();
    }


}
