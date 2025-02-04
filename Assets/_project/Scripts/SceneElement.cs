using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SceneElement : MonoBehaviour
{
    [SerializeField] private bool _isMainRoom;
    public Vector3 GetPosition()
    {
        return transform.position;
    }

    public Sprite GetSceneSprite()
    {
        return GetComponent<SpriteRenderer>().sprite;
    }

    public bool IsMainRoom() => _isMainRoom;
 
}
