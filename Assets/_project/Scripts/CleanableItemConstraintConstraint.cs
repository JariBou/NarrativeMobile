using GraphicsLabor.Scripts.Attributes.LaborerAttributes.InspectedAttributes;
using UnityEngine;

public class CleanableItemConstraintConstraint : MonoBehaviour, IDraggableItemConstraint
{
    private bool _isClean = false;
    
    public bool IsConstraintCompleted()
    {
        return _isClean;
    }

    public void Awake()
    {
        _isClean = false;
    }

    [Button]
    public void Clean()
    {
        _isClean = true;
    }
}
