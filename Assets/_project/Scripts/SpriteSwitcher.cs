using UnityEngine;

namespace _project.Scripts
{
    public class SpriteSwitcher : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Sprite[] _sprites;
    
        private int _currentSprite = 0;

        private void Awake()
        {
            if (_spriteRenderer == null) _spriteRenderer = GetComponent<SpriteRenderer>();
            if (_spriteRenderer != null && _sprites.Length > 0)
            {
                _spriteRenderer.sprite = _sprites[_currentSprite];
            }
        }
        public void SwitchSprite()
        {
            if (_spriteRenderer == null || _sprites.Length <= 1) return;
            _currentSprite++;
            if (_currentSprite >= _sprites.Length) _currentSprite = 0;
            _spriteRenderer.sprite = _sprites[_currentSprite];
        }
    }
}
