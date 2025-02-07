using System;
using GraphicsLabor.Scripts.Attributes.LaborerAttributes.InspectedAttributes;
using UnityEngine;
using UnityEngine.Events;

namespace _project.Scripts
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class SceneElement : MonoBehaviour
    {
        [SerializeField] private bool _isMainRoom;
        [SerializeField] private UnityEvent _onFirstEnterRoom;
        private bool _hasEnteredRoom;
        [SerializeField] private bool _hasOwnBackground;
        [SerializeField, HideIf("_hasOwnBackground")] private Sprite _overlaySprite;
        private SpriteRenderer _spriteRenderer;
        public Vector3 GetPosition()
        {
            return transform.position;
        }

        public Vector3 GetCameraPosition()
        {
            return new Vector3(transform.position.x, transform.position.y, -10);
        }

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public bool HasOwnBackground() => _hasOwnBackground;

        public Sprite GetSceneSprite()
        {
            if (_hasOwnBackground) return _spriteRenderer.sprite;
            return _overlaySprite?? _spriteRenderer.sprite;
        }

        public bool IsMainRoom() => _isMainRoom;

        public void EnterRoom()
        {
            if (_hasEnteredRoom) return;
            _hasEnteredRoom = true;
            _onFirstEnterRoom?.Invoke();
        }
 
    }
}
