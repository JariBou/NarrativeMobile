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
            int selectedLoc = PlayerPrefs.GetInt("loc", 0);
            Loc castedLoc = (Loc)selectedLoc;
            loc = castedLoc;
            
            DontDestroyOnLoad(gameObject);
        }

        public static void SetLoc(Loc loc)
        {
            Instance.loc = loc;
            OnLocChanged?.Invoke(loc);
            PlayerPrefs.SetInt("loc", (int)loc);
        }

    }
}