using System;
using System.Collections;
using System.Numerics;
using UnityEngine;
using UnityEngine.UI;
using Vector2 = UnityEngine.Vector2;

namespace _project.Scripts.UI_Script
{
    public class SceneCircleScript : MonoBehaviour
    {
        
        [SerializeField] private Image _circleImage;
        [SerializeField] private RectTransform _rectTransform;
        
        [SerializeField] private AnimationCurve _animCurve;
        [SerializeField] private float _animTime;
        
        private Vector2 _activeSceneSize = new Vector2(20,20);
        private Vector2 _normalSceneSize = new Vector2(10,10);

        private bool _selected;
        private float _timer;

        public void Init(Vector2 activeSceneSize, Vector2 normalSceneSize)
        {
            _activeSceneSize = activeSceneSize;
            _normalSceneSize = normalSceneSize;
            
            _rectTransform.sizeDelta = _normalSceneSize;
            _circleImage.color = new Color(1f, 1f, 1f, 0.44f);
        }

        public void SetState(bool selectedState)
        {
            if (selectedState == _selected) return;
            _selected = selectedState;
            _timer = 0;
            // _rectTransform.sizeDelta = selectedState ? _activeSceneSize : _normalSceneSize;
            _circleImage.color = new Color(1f, 1f, 1f, selectedState ? 1f : 0.44f);
            // _animator.SetTrigger(selectedState ? "Selected" : "Deselected");
        }

        private void Update()
        {
            if (_timer >= 1) return;
            
            _timer += Time.deltaTime / _animTime;
            if (_selected)
            {
                _rectTransform.sizeDelta = Vector2.Lerp(_normalSceneSize, _activeSceneSize, _animCurve.Evaluate(_timer));
            }
            else
            {
                _rectTransform.sizeDelta = Vector2.Lerp(_activeSceneSize, _normalSceneSize, _animCurve.Evaluate(_timer));
            }
        }

        public void SetSprite(Sprite sprite)
        {
            _circleImage.sprite = sprite;
        }

    }
}