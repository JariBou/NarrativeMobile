using System;
using UnityEngine;
using UnityEngine.Events;

public class KnifeZone : MonoBehaviour
{
    [SerializeField] private DragTargetZone _knifeTargetZone;
    [SerializeField] private int _nbKnives;
    [SerializeField] private UnityEvent _onAllKnivesPlaced;
    
    private void Awake()
    {
        _knifeTargetZone.onItemDragged += KnifeDragged;
    }

    private void KnifeDragged(int nbItemDragged)
    {
        if (nbItemDragged == _nbKnives)
        {
            _onAllKnivesPlaced?.Invoke(); 
            _knifeTargetZone.onItemDragged -= KnifeDragged;
        }
    }
}
