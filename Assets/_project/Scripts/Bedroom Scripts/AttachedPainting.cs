using UnityEngine;
using UnityEngine.Events;

namespace _project.Scripts.Bedroom_Scripts
{
    public class AttachedPainting : MonoBehaviour
    {
        [SerializeField] private String[] _attachedStrings;

        [SerializeField] private UnityEvent _onPaintingFall;

        public void StringBroke()
        {
            if (AreAllStringsBroken())
            {
                _onPaintingFall?.Invoke();
            }
        }

        private bool AreAllStringsBroken()
        {
        
            foreach (String attachedString in _attachedStrings)
            {
                if (!attachedString.IsBroken()) return false;
            }
            return true;
        }
    }
}
