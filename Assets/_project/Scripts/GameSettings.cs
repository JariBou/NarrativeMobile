using _project.Scripts.Localisation;
using UnityEngine;

namespace _project.Scripts
{
    public class GameSettings : MonoBehaviour
    {
        public static GameSettings Instance;

        public Loc loc { get; private set; }

        private void Awake()
        {
            Instance = this;
            loc = Loc.FR_fr;
            
            DontDestroyOnLoad(gameObject);
        }

        public static void SetLoc(Loc loc)
        {
            Instance.loc = loc;
        }
    }
}