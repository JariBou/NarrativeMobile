using System.Collections;
using GraphicsLabor.Scripts.Attributes.LaborerAttributes.DrawerAttributes;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsManager : MonoBehaviour
{
    [SerializeField] private GameObject _creditsPanel;
    [SerializeField] private Vector3 _creditsFinishPosition;
    [SerializeField] private float _creditsTime;
    [SerializeField, Scene] private string _mainMenuScene;

    private void Start()
    {
        StartCoroutine(CreditsCoroutine());
    }

    private IEnumerator CreditsCoroutine()
    {
        Vector3 basePosition = _creditsPanel.transform.localPosition;
        for (int i = 0; i < _creditsTime * 100; i++)
        {
            float alpha = i / (_creditsTime * 100f - 1);
            _creditsPanel.transform.localPosition = Vector3.Lerp(basePosition, _creditsFinishPosition, alpha);
            Debug.Log(alpha);
            yield return new WaitForSeconds(0.01f);
        }
        GoBackMainMenu();
    }
    
    public void GoBackMainMenu(){
        SceneManager.LoadScene(_mainMenuScene);
    }
}
