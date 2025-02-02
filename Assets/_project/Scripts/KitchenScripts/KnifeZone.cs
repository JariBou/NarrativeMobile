using System;
using UnityEngine;
using UnityEngine.Events;

public class KnifeZone : MonoBehaviour
{
    [SerializeField] private DragTargetZone[] _knifeTargetZones;
    [SerializeField] private UnityEvent _onAllKnivesPlaced;

    private int _knivesPlaced = 0;
    private void Awake()
    {
        foreach (DragTargetZone knifeTargetZone in _knifeTargetZones)
        {
            knifeTargetZone.onItemDragged += KnifeDragged;
        }
    }

    private void KnifeDragged(int nbItemDragged)
    {
        if (nbItemDragged == 1)
        {
            _knivesPlaced++;
            if (_knivesPlaced == _knifeTargetZones.Length) _onAllKnivesPlaced?.Invoke();
        }
    }
}
