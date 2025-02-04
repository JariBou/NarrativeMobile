using System;
using GraphicsLabor.Scripts.Attributes.LaborerAttributes.InspectedAttributes;
using UnityEngine;

public class DragTargetZone : MonoBehaviour
{
    [SerializeField] private bool _destroyDraggedObject = false;

    [SerializeField, HideIf("_destroyDraggedObject")]
    private Vector3[] _draggedObjectsPositions;

    private int nbDraggedObjects = 0;
    
    public event Action<int> onItemDragged;
    
    private Collider2D _collider;
    public Collider2D GetCollider() => _collider;

    private void Awake()
    {
        _collider = GetComponent<Collider2D>();
        nbDraggedObjects = 0;
    }

    public void DraggedItem(DraggableElement draggableElement)
    {
        if (_destroyDraggedObject)
        {
            Destroy(draggableElement.gameObject);
        }
        else
        {
            if (_draggedObjectsPositions.Length <= nbDraggedObjects) return;
            draggableElement.transform.position = transform.position + _draggedObjectsPositions[nbDraggedObjects];
            draggableElement.LockDragging();
        }
        nbDraggedObjects++;
        onItemDragged?.Invoke(nbDraggedObjects);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        if (_destroyDraggedObject) return;
        foreach (Vector3 draggedObjectsPosition in _draggedObjectsPositions)
        {
            Gizmos.DrawWireSphere(transform.position + draggedObjectsPosition, 0.3f);
        }
    }
}
