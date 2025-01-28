using System;
using System.Collections.Generic;
using System.Linq;
using GraphicsLabor.Scripts.Attributes.LaborerAttributes.InspectedAttributes;
using JetBrains.Annotations;
using UnityEngine;

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
    }

    //UnlockScene, if selected scene can now be accessible, returns true
    public bool UnlockScene(string sceneName)
    {
        RoomScene roomScene = GetSceneByName(sceneName);

        if (roomScene.SceneName != sceneName) return false;
        
        roomScene.LockAmount -= 1;

        int roomIndex = _roomScenes.IndexOf(roomScene);

        if (_sceneUI != null) _sceneUI.ChangeLockAmountOfRoom(roomScene.LockAmount, roomIndex);
        
        if (roomScene.CanAccess()) return true;
        return false;
    }

    [Button]
    public void MoveLeft() => MoveScene(true);
    [Button]
    public void MoveRight() => MoveScene(false);
    
    public bool MoveScene(bool moveLeft)
    {
        int nextIndex = _currentRoomSceneIndex + (moveLeft ? -1 : 1);
        if (nextIndex > _roomScenes.Count || nextIndex < 0) return false;
        
        RoomScene roomScene = GetSceneByIndex(nextIndex);
        if (roomScene.CanAccess())
        {
            //Change Scene Animation
            
            Vector3 targetPos = roomScene.SceneElement.GetPosition();
            Camera.main.transform.position = new Vector3(targetPos.x, targetPos.y, Camera.main.transform.position.z);
            
            Debug.Log(roomScene.SceneName);
            
            _currentRoomSceneIndex = nextIndex;
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

    private RoomScene GetSceneByIndex(int sceneIndex)
    {
        if (sceneIndex < 0 || sceneIndex > _roomScenes.Count) return _errorRoomScene;
        return _roomScenes[sceneIndex];
    }

    public void MoveTo(SceneElement sceneElement)
    {
        Vector3 targetPos = sceneElement.GetPosition();
        Camera.main.transform.position = new Vector3(targetPos.x, targetPos.y, _cameraDefaultZ);
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