using System;
using UnityEngine;
using UnityEngine.Events;

public class String : MonoBehaviour
{
    [SerializeField] private Sprite _almostBrokenSprite;
    [SerializeField] private Sprite _brokeSprite;
    [SerializeField] private int _nbTapToBreak;
    private int _nbTap;
    private bool _isBroken;
    private SpriteRenderer _spriteRenderer;

    [SerializeField] private UnityEvent _onBreak;
    
    public bool IsBroken() => _isBroken;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _isBroken = false;
    }

    public void Break()
    {
        _nbTap++;
        if (_nbTap == 1) _spriteRenderer.sprite = _almostBrokenSprite;
        if (_nbTap >= _nbTapToBreak && !_isBroken)
        {
            _spriteRenderer.sprite = _brokeSprite;
            _isBroken = true;
            _onBreak?.Invoke();
        }
    }
}
