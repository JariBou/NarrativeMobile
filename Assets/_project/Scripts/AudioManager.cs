using UnityEngine;

namespace _project.Scripts
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance;
        [SerializeField] private AudioSource _voicelinesSource;
        [SerializeField] private AudioSource _sfxSource;
        [SerializeField] private AudioClip[] _audioClip;

        private void Awake()
        {
            Instance = this;
            foreach (var clip in _audioClip)
            {
                _voicelinesSource.Stop();
                _voicelinesSource.PlayOneShot(clip);
            }
            // _audioSource.PlayOneShot();
        }


        public static void PlayDialog(AudioClip clip)
        {
            if (clip == null) return;
            Instance._voicelinesSource.Stop();
            Instance._voicelinesSource.PlayOneShot(clip);
        }

        public static void PlaySfx(AudioClip clip)
        {
            Instance._sfxSource.PlayOneShot(clip);
        }
    }
}