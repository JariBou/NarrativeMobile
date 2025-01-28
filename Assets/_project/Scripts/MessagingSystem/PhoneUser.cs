using UnityEngine;

namespace _project.Scripts.MessagingSystem
{
    [CreateAssetMenu(fileName = "PhoneUser0", menuName = "MessagingSystem/Phone User")]
    public class PhoneUser : ScriptableObject
    {
        [SerializeField] private string _userId;
        [SerializeField] private Texture2D _icon;
        [SerializeField] private string _name;

        public Texture2D Icon => _icon;
        public string Name => _name;
        public string UserId => _userId;
    }
}