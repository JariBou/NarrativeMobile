using UnityEngine;

namespace _project.Scripts.MessagingSystem
{
    public class GameClock : MonoBehaviour
    {
        public static GameClock Instance { get; private set; }

        [SerializeField] private FDateTime startTime;
        [SerializeField] private int realTimeSeconds;
        [SerializeField] private int forGameSeconds;

        private float _internalClock;

        private void Awake()
        {
            Instance = this;
        }

        private void FixedUpdate()
        {
            _internalClock += Time.fixedDeltaTime;
            // Debug.Log(GetDateTime());
        }

        public static FDateTime GetDateTime()
        {
            int addedSeconds = (int)Instance._internalClock;
            return new FDateTime(Instance.startTime).AddSeconds(addedSeconds);
        }
    }
}