
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEngine.SceneManagement.SceneManager;

namespace _project.Scripts.Menus
{
    public class LoadingScript : MonoBehaviour
    {
        public int _sceneBuildIndexToLoad;

        private void Awake()
        {
            foreach (var key in new List<string>(){"volume_master", "volume_sfx", "volume_voices"}.Where(key => !PlayerPrefs.HasKey(key)))
            {
                PlayerPrefs.SetFloat(key, 1);
            }
        }

        private void Start()
        {
            LoadScene(_sceneBuildIndexToLoad);
        }
    }
}