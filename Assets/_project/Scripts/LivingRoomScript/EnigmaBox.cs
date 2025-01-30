using System;
using UnityEngine;
using UnityEngine.Events;

public class EnigmaBox : MonoBehaviour
{
     [SerializeField] private BoxButton[] _buttons;
     [SerializeField] private UnityEvent _onBoxCompleted;
     private bool _hasBeenCompleted = false;
     private void Awake()
     {
          foreach (BoxButton button in _buttons)
          {
               button._onValueChanged += OnButtonValueChanged;
          }
     }

     private void OnButtonValueChanged()
     {
          if (!_hasBeenCompleted && AreAllButtonsGood())
          {
               _hasBeenCompleted = true;
               _onBoxCompleted?.Invoke();
          }
     }

     private bool AreAllButtonsGood()
     {
          foreach (BoxButton button in _buttons)
          {
               if (!button.IsOnTargetSprite()) return false;
          }

          return true;
     }

     private void OnDestroy()
     {
          foreach (BoxButton button in _buttons)
          {
               if (button!= null) button._onValueChanged -= OnButtonValueChanged;
          }
     }
}

