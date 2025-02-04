using System;
using System.Collections.Generic;
using _project.Scripts.UI_Script;
using GraphicsLabor.Scripts.Attributes.LaborerAttributes.InspectedAttributes;
using JetBrains.Annotations;
using UnityEngine;

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
        
            _cameraDefaultZ = Camera.main.transform.position.z;
            _currentSceneElement = _roomScenes[_currentRoomSceneIndex].SceneElement;
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
            int nextIndex = _currentRoomSceneIndex + (moveLeft ? -1 : 1);
            if (nextIndex >= _roomScenes.Count || nextIndex < 0) return false;
        
            RoomScene roomScene = GetSceneByIndex(nextIndex);
            if (roomScene.CanAccess())
            {
                //Change Scene Animation
            
                Vector3 targetPos = roomScene.SceneElement.GetPosition();
                Camera.main.transform.position = new Vector3(targetPos.x, targetPos.y, Camera.main.transform.position.z);
            
                Debug.Log(roomScene.SceneName);
            
                _currentRoomSceneIndex = nextIndex;
                _currentSceneElement = roomScene.SceneElement;
                if (_sceneUI != null) _sceneUI.UpdateCurrentRoomUI(_currentRoomSceneIndex);
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

        public void MoveTo(SceneElement sceneElement)
        {
            Vector3 targetPos = sceneElement.GetPosition();
            Camera.main.transform.position = new Vector3(targetPos.x, targetPos.y, _cameraDefaultZ);
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
        }

        public void GoBack()
        {
            MoveTo(_roomScenes[_currentRoomSceneIndex].SceneElement);
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