using System;
using UnityEngine;

namespace _project.Scripts.LivingRoomScript
{
    public class BoxButton : MonoBehaviour
    {
        [SerializeField] private Sprite[] _sprites;
        [SerializeField] private int _startSprite;
        [SerializeField] private int _spriteTarget;
        private SpriteRenderer _spriteRenderer;

        public event Action _onValueChanged;

        private int _currentSprite;

        private void Awake()
        {
            _currentSprite = _startSprite;
            if (_spriteRenderer == null) _spriteRenderer = GetComponent<SpriteRenderer>();
            if (_spriteRenderer != null && _sprites.Length > 0)
            {
                _spriteRenderer.sprite = _sprites[_currentSprite];
            }
        }
    
        public void ButtonPressed()
        {
            if (_spriteRenderer == null || _sprites.Length <= 1) return;
            _currentSprite++;
            if (_currentSprite >= _sprites.Length) _currentSprite = 0;
            _spriteRenderer.sprite = _sprites[_currentSprite];
            _onValueChanged?.Invoke();
        }
    
        public bool IsOnTargetSprite() => _currentSprite == _spriteTarget;
    }
}
