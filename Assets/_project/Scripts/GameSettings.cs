using System;
using _project.Scripts.Localisation;
using UnityEngine;

namespace _project.Scripts
{
    public class GameSettings : MonoBehaviour
    {
        public static GameSettings Instance;
        public static event Action<Loc> OnLocChanged; 

        public Loc loc { get; private set; }

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            loc = Loc.FR_fr;
            
            DontDestroyOnLoad(gameObject);
        }

        public static void SetLoc(Loc loc)
        {
            Instance.loc = loc;
            OnLocChanged?.Invoke(loc);
        }

    }
}