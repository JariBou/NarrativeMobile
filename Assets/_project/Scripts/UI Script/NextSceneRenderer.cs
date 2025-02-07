using System;
using UnityEngine;
using UnityEngine.UI;

public class NextSceneRenderer : MonoBehaviour
{
    [SerializeField] private RawImage _rawImage;
    [SerializeField] private Camera _rendererCamera;
    private RenderTexture _renderTexture;

    private void Start()
    {
        _renderTexture = new RenderTexture(Camera.main.pixelWidth, Camera.main.pixelHeight, 16, RenderTextureFormat.ARGB32);
        _renderTexture.Create();
        _rendererCamera.targetTexture = _renderTexture;
        _rendererCamera.rect = new Rect(_rendererCamera.rect.x, _rendererCamera.rect.y, Camera.main.rect.width, Camera.main.rect.height);
        _rawImage.texture = _renderTexture;
        _rawImage.rectTransform.sizeDelta = new Vector2(Camera.main.pixelWidth, Camera.main.pixelHeight);
        //_rawImage.rectTransform.localScale = Vector3.one * (2.11f/(Camera.main.pixelWidth * 1f/Camera.main.pixelHeight)) / _rawImage.transform.parent.localScale.x;
        _rawImage.rectTransform.localScale = Vector3.one / _rawImage.transform.parent.localScale.x;
    }

    public void Render(Vector3 camPos)
    {
        
        _rendererCamera.transform.position = camPos;
        /*_rendererCamera.Render();
        Texture2D tex = new Texture2D(_renderTexture.width, _renderTexture.height, TextureFormat.RGBA32, false);
        RenderTexture.active = _renderTexture;
        //tex.ReadPixels(new Rect(0, 0, _renderTexture.width, _renderTexture.height), 0, 0);
        Graphics.CopyTexture(_renderTexture, tex);
        tex.Apply();*/
        
    }
}
