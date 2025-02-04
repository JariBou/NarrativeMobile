using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneUI : MonoBehaviour
{
    [SerializeField] private GameObject _sceneCircle;
    [SerializeField] private Sprite[] _lockAmountSprites;

    [SerializeField] private GameObject _goBackButton;
    
    [Header("Scene Circle Sizes")]
    [SerializeField] private Vector2 _activeSceneSize = new Vector2(20,20);
    [SerializeField] private Vector2 _normalSceneSize = new Vector2(10,10);

    private GameObject[] _sceneCircles;

    private void Start()
    {
        SceneManager.Instance.SetupSceneUI(this);
        _goBackButton.SetActive(false);
    }

    public void SetupUI(List<RoomScene> roomScenes)
    {
        _sceneCircles = new GameObject[roomScenes.Count];
        for (int i = 0; i < roomScenes.Count; i++)
        {
            _sceneCircles[i] = Instantiate(_sceneCircle, transform);
            _sceneCircles[i].GetComponent<Image>().sprite = _lockAmountSprites[roomScenes[i].LockAmount];
        }
    }

    public void UpdateCurrentRoomUI(int currentRoomIndex)
    {
        for (int i = 0; i < _sceneCircles.Length; i++)
        {
            _sceneCircles[i].GetComponent<RectTransform>().sizeDelta = i==currentRoomIndex? _activeSceneSize : _normalSceneSize;
            Debug.Log(i==currentRoomIndex? _activeSceneSize : _normalSceneSize);
        }
    }

    public void ChangeLockAmountOfRoom(int lockAmount, int roomIndex)
    {
        _sceneCircles[roomIndex].GetComponent<Image>().sprite = _lockAmountSprites[lockAmount];
    }

    public void UpdateChangedSceneElement(SceneElement sceneElement)
    {
        _goBackButton.SetActive(!sceneElement.IsMainRoom());
    }
}
