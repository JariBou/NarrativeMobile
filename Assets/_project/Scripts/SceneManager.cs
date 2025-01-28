using System;
using System.Collections.Generic;
using System.Linq;
using GraphicsLabor.Scripts.Attributes.LaborerAttributes.InspectedAttributes;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    [SerializeField] private List<RoomScene> _roomScenes;
    [SerializeField] private int _currentRoomSceneIndex;
    private RoomScene _errorScene = new RoomScene("Error", -1);
    

    //UnlockScene, if selected scene can now be accessible, returns true
    public bool UnlockScene(string sceneName)
    {
        RoomScene roomScene = GetSceneByName(sceneName);

        if (roomScene.SceneName != sceneName) return false;
        
        roomScene.LockAmount -= 1;
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
            //Change Scene
            
            Debug.Log(roomScene.SceneName);
            
            _currentRoomSceneIndex = nextIndex;
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
        return _errorScene;
    }

    private RoomScene GetSceneByIndex(int sceneIndex)
    {
        if (sceneIndex < 0 || sceneIndex > _roomScenes.Count) return _errorScene;
        return _roomScenes[sceneIndex];
    }
}

[Serializable]
public struct RoomScene
{
    public RoomScene(string sceneName, int lockAmount)
    {
        SceneName = sceneName;
        LockAmount = lockAmount;
    }
    
    public string SceneName;
    
    public int LockAmount;

    public bool CanAccess()
    {
        return LockAmount <= 0;
    }

}