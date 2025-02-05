using UnityEngine;
using UnityEngine.Events;

namespace _project.Scripts.Bedroom_Scripts
{
    public class ClothesBasket : MonoBehaviour
    {
        [SerializeField] private int _clothesCapacity;
        private DragTargetZone _dragTargetZone;

        [SerializeField] private Sprite _filledBasketSprite;

        [SerializeField] private UnityEvent _onClothesBasketFilled;
        private void Awake()
        {
            _dragTargetZone = GetComponent<DragTargetZone>();
            _dragTargetZone.onItemDragged += OnItemDragged;
        }

        private void OnItemDragged(int nbDraggedItem)
        {
            if (nbDraggedItem == _clothesCapacity)
            {
                GetComponent<SpriteRenderer>().sprite = _filledBasketSprite;
                _onClothesBasketFilled?.Invoke();
            }
        }
    }
}
