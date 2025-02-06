using GraphicsLabor.Scripts.Attributes.LaborerAttributes.DrawerAttributes;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField,Scene] private string _sceneName;
    private void Start()
    {
        SceneManager.LoadScene(_sceneName);
    }
}
