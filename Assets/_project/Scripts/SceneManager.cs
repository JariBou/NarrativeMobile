using System;
using System.Collections;
using System.Collections.Generic;
using _project.Scripts.Nodes;
using _project.Scripts.UI_Script;
using GraphicsLabor.Scripts.Attributes.LaborerAttributes.InspectedAttributes;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace _project.Scripts
{
    public class SceneManager : MonoBehaviour
    {
        public static SceneManager Instance;
    
        [SerializeField] private List<RoomScene> _roomScenes;
        [SerializeField] private int _currentRoomSceneIndex;
        private RoomScene _errorRoomScene = new RoomScene("Error", -1);
    
        private SceneUI _sceneUI;

        private float _cameraDefaultZ;
        private SceneElement _currentSceneElement;

        [Header("Ending Animation")]
        [SerializeField] private UnityEvent _startedEnding;
        [SerializeField] private Image _fadeBlackSprite;
        [SerializeField] private float _fadeToBlackDuration;
        [SerializeField] private float _timeBeforeFadeBlack;
        [SerializeField] private AudioClip _phoneRingSound;
        [SerializeField] private AudioClip _phoneAnswerSound;
        private bool _hasSeenProof;
        
        [Header("Scene Transition")]
        [SerializeField] private SpriteRenderer _sceneTransitionSprite;
        [SerializeField] private RawImage _sceneTransitionImage;
        [SerializeField] private RawImage _uiBlocker;
        [SerializeField] private float _sceneTransitionTime;
        [SerializeField] private AudioClip _sceneTransitionSound;
        [SerializeField] private NextSceneRenderer _nextSceneRenderer;
        private Camera _camera;

        [SerializeField] private TouchInputManager _touchInputManager;
        


        public void ClosePhone()
        {
            if (_currentSceneElement == null)
            {
                _currentSceneElement = _roomScenes[_currentRoomSceneIndex].SceneElement;
                MoveTo(_currentSceneElement);
            }
        }

        public void HasSeenProof()
        {
            _hasSeenProof = true;
            StartCoroutine(Ending());
        }

        public void SetupSceneUI(SceneUI sceneUI)
        {
            _sceneUI = sceneUI;
            sceneUI.SetupUI(_roomScenes);
            sceneUI.UpdateCurrentRoomUI(_currentRoomSceneIndex);
        }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
        
            _camera = Camera.main;
            _cameraDefaultZ = _camera.transform.position.z;
        }

        //UnlockScene, if selected scene can now be accessible, returns true
        public void UnlockScene(string sceneName)
        {
            RoomScene roomScene = GetSceneByName(sceneName);

            if (roomScene.SceneName != sceneName) return ;
        
            int roomIndex = _roomScenes.IndexOf(roomScene);

            roomScene.LockAmount -= 1;
        
            _roomScenes[roomIndex] = roomScene;

            if (_sceneUI != null) _sceneUI.ChangeLockAmountOfRoom(roomScene.LockAmount, roomIndex);
        
            if (roomScene.CanAccess()) return ;
            return ;
        }

        [Button]
        public void MoveLeft() => MoveScene(true);
        [Button]
        public void MoveRight() => MoveScene(false);
    
        public bool MoveScene(bool moveLeft)
        {
            if (_currentSceneElement!=null && !_currentSceneElement.IsMainRoom()) return false;
            // AudioManager.PlaySfx(_sceneTransitionSound);
            int nextIndex = _currentRoomSceneIndex + (moveLeft ? -1 : 1);
            if (nextIndex >= _roomScenes.Count || nextIndex < 0) return false;
        
            RoomScene roomScene = GetSceneByIndex(nextIndex);
            if (roomScene.CanAccess())
            {
                //Change Scene Animation
                StartCoroutine(MoveToScene(roomScene.SceneElement));
                /*
                Vector3 targetPos = roomScene.SceneElement.GetPosition();
                _camera.transform.position = new Vector3(targetPos.x, targetPos.y, _camera.transform.position.z);
                Debug.Log(roomScene.SceneName);

                _currentRoomSceneIndex = nextIndex;
                _currentSceneElement = roomScene.SceneElement;
                _currentSceneElement.EnterRoom();
                if (_sceneUI != null) _sceneUI.UpdateCurrentRoomUI(_currentRoomSceneIndex);*/
                return true;
            }
            else
            {
                //Animation Scene Inaccessible
                return false;
            }
        }

        private RoomScene GetSceneByName(string sceneName)
        {
            if (_roomScenes.Exists(x => x.SceneName == sceneName))
            {
                return _roomScenes.Find(x => x.SceneName == sceneName);
            }
            return _errorRoomScene;
        }
    
        private RoomScene GetSceneBySceneElement(SceneElement sceneElement)
        {
            if (_roomScenes.Exists(x => x.SceneElement == sceneElement))
            {
                return _roomScenes.Find(x => x.SceneElement == sceneElement);
            }
            return _errorRoomScene;
        }

        private int GetSceneIndexFromSceneElement(SceneElement sceneElement)
        {
            for (int i = 0; i < _roomScenes.Count; i++)
            {
                if (_roomScenes[i].SceneElement == sceneElement)
                {
                    return i;
                }
            }

            return -1;
        }

        private RoomScene GetSceneByIndex(int sceneIndex)
        {
            if (sceneIndex < 0 || sceneIndex > _roomScenes.Count) return _errorRoomScene;
            return _roomScenes[sceneIndex];
        }

        public void MoveTo(SceneElement sceneElement, Vector3 elementPosition)
        {
            if (sceneElement.IsMainRoom())
            {
                //Main room transition
            }
            else
            {
                //MoveToScene Transition
                StartCoroutine(MoveToSceneWithTargetPos(sceneElement, elementPosition));
            }
        }
        public void MoveTo(SceneElement sceneElement)
        {
            Vector3 targetPos = sceneElement.GetPosition();
            _camera.transform.position = new Vector3(targetPos.x, targetPos.y, _cameraDefaultZ);
            if (sceneElement.IsMainRoom())
            {
                int sceneIndex = GetSceneIndexFromSceneElement(sceneElement);
                if (sceneIndex != -1)
                {
                    _currentRoomSceneIndex = sceneIndex;
                    _sceneUI.UpdateCurrentRoomUI(_currentRoomSceneIndex);
                }
            }
            _currentSceneElement = sceneElement;
            _sceneUI.UpdateChangedSceneElement(sceneElement);
            _currentSceneElement.EnterRoom();
        }

        #region MoveScene Animations

        //Move to Scene Animation
        private IEnumerator MoveToSceneWithTargetPos(SceneElement sceneElement, Vector3 targetPos)
        {
            _nextSceneRenderer.Render(sceneElement.GetCameraPosition());
            // AudioManager.PlaySfx(_sceneTransitionSound);
            /*_sceneTransitionSprite.sprite = sceneElement.GetSceneSprite();
            _sceneTransitionSprite.color = new Color(1, 1, 1, 0);
            _sceneTransitionSprite.gameObject.SetActive(true);*/
            _sceneTransitionImage.gameObject.SetActive(true);
            _uiBlocker.gameObject.SetActive(true);
            _touchInputManager.SetCheckingTouchDetection(false);
            _sceneTransitionImage.color = new Color(1, 1, 1, 0);
            float defaultOrthSize = _camera.orthographicSize;
            Vector3 defaultCamPos = _camera.transform.position;
            for (int i = 0; i < _sceneTransitionTime * 100f; i++)
            {
                float alpha = i / (_sceneTransitionTime * 100f - 1);
                _camera.orthographicSize = Mathf.Lerp(defaultOrthSize, defaultOrthSize * 0.5f, alpha);
                _camera.transform.position = Vector3.Lerp(defaultCamPos, targetPos, alpha);
                /*_sceneTransitionSprite.transform.localScale = Vector3.Lerp(Vector3.one, Vector3.one * 0.5f, alpha); ;
                _sceneTransitionSprite.color = Color.Lerp(new Color(1,1,1,0), Color.white, alpha);*/
                _sceneTransitionImage.color = Color.Lerp(new Color(1,1,1,0), Color.white, alpha);
                yield return new WaitForSeconds(0.01f);
            }
            yield return new WaitForSeconds(_sceneTransitionTime/10f);
            MoveTo(sceneElement);
            _camera.orthographicSize = defaultOrthSize;
            _sceneTransitionImage.gameObject.SetActive(false);
            _uiBlocker.gameObject.SetActive(false);
            _touchInputManager.SetCheckingTouchDetection(true);
            /*_sceneTransitionSprite.transform.localScale = Vector3.one;
            _sceneTransitionSprite.gameObject.SetActive(false);*/
        }
        
        private IEnumerator MoveToScene(SceneElement sceneElement)
        {
            // AudioManager.PlaySfx(_sceneTransitionSound);
            _nextSceneRenderer.Render(sceneElement.GetCameraPosition());
            /*_sceneTransitionSprite.sprite = sceneElement.GetSceneSprite();
            _sceneTransitionSprite.color = new Color(1, 1, 1, 0);
            _sceneTransitionSprite.gameObject.SetActive(true);*/
            _sceneTransitionImage.gameObject.SetActive(true);
            _uiBlocker.gameObject.SetActive(true);
            _touchInputManager.SetCheckingTouchDetection(false);
            for (int i = 0; i < _sceneTransitionTime * 100f; i++)
            {
                float alpha = i / (_sceneTransitionTime * 100f - 1);
                //_sceneTransitionSprite.color = Color.Lerp(new Color(1,1,1,0), Color.white, alpha);
                _sceneTransitionImage.color = Color.Lerp(new Color(1,1,1,0), Color.white, alpha);
                yield return new WaitForSeconds(0.01f);
            }
            yield return new WaitForSeconds(_sceneTransitionTime/10f);
            MoveTo(sceneElement);
            //_sceneTransitionSprite.gameObject.SetActive(false);
            _sceneTransitionImage.gameObject.SetActive(false);
            _uiBlocker.gameObject.SetActive(false);
            _touchInputManager.SetCheckingTouchDetection(true);
        }
        
        #endregion
        public void GoBack()
        {
            StartCoroutine(MoveToScene(_roomScenes[_currentRoomSceneIndex].SceneElement));
        }

        private IEnumerator Ending()
        {
            float opacity = 0f;
            _startedEnding?.Invoke();
            _fadeBlackSprite.gameObject.SetActive(true);
            AudioManager.PlaySfx(_phoneRingSound);
            yield return new WaitForSeconds(_timeBeforeFadeBlack);
            AudioManager.PlaySfx(_phoneAnswerSound);
            for (int i = 0; i < _fadeToBlackDuration/0.1f; i++)
            {
                opacity += 0.1f/_fadeToBlackDuration;
                _fadeBlackSprite.color = new Color(_fadeBlackSprite.color.r, _fadeBlackSprite.color.g, _fadeBlackSprite.color.b, opacity);
                yield return new WaitForSeconds(0.1f);
            }
            UnityEngine.SceneManagement.SceneManager.LoadScene("EndingScreen");
        }
    }

    [Serializable]
    public struct RoomScene : IEquatable<RoomScene>
    {
        public RoomScene(string sceneName, int lockAmount)
        {
            SceneName = sceneName;
            LockAmount = lockAmount;
            SceneElement = null;
        }
    
        public string SceneName;
    
        public int LockAmount;
    
        [SerializeField] public SceneElement SceneElement;
    
        public bool CanAccess()
        {
            return LockAmount <= 0;
        }

        public bool Equals([NotNull] RoomScene other)
        {
            return SceneName == other.SceneName;
        }

        public override bool Equals(object obj)
        {
            return obj is RoomScene other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(SceneName, LockAmount);
        }
    }
}