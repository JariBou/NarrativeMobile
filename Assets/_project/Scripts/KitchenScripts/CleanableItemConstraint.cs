using GraphicsLabor.Scripts.Attributes.LaborerAttributes.InspectedAttributes;
using UnityEngine;
using UnityEngine.Events;

public class CleanableItemConstraint : MonoBehaviour, IDraggableItemConstraint
{
    private bool _isClean = false;
    [SerializeField] private Sprite _dirtySprite;
    [SerializeField] private Sprite _cleanSprite;
    
    private SpriteRenderer _spriteRenderer;
    
    [SerializeField] private UnityEvent _onClean;
    
    public bool IsConstraintCompleted()
    {
        return _isClean;
    }

    public void Awake()
    {
        _isClean = false;
        _spriteRenderer = GetComponent<SpriteRenderer>();
        if (_spriteRenderer != null)
        {
            if (_dirtySprite != null) _spriteRenderer.sprite = _dirtySprite;
            else { _spriteRenderer.color = new Color(0.89f, 0.51f, 0.29f, 1f); }
        }
    }

    [Button]
    public void Clean()
    {
        if (_isClean) return;
        if (_cleanSprite != null) _spriteRenderer.sprite = _cleanSprite;
        else { _spriteRenderer.color = Color.white; }
        _isClean = true;
        _onClean?.Invoke();
    }
}
