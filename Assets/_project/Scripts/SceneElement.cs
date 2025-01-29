using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SceneElement : MonoBehaviour
{
    public Vector3 GetPosition()
    {
        return transform.position;
    }

    public Sprite GetSceneSprite()
    {
        return GetComponent<SpriteRenderer>().sprite;
    }
}
