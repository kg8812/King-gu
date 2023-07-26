using System.Collections;
using UnityEngine;

public class BGScroll : MonoBehaviour
{
    public Material bgMat;
    Vector2 dir = Vector2.right;

    public static float scrollSpeed = 0.2f;

    public Texture[] textures;

    int count;
    private void Start()
    {
        ChangeImage();
        scrollSpeed = 0.2f;
        bgMat.mainTextureOffset = Vector2.zero;
    }
    private void Update()
    {
        if (GameManager.Instance.State == GameState.InGame)
        {
            bgMat.mainTextureOffset += scrollSpeed * Time.deltaTime * dir;
        }
    }

    public void ChangeImage()
    {
        if (count < textures.Length)
        {
           bgMat.mainTexture = textures[count++];
        }
    }   

    public void Stop()
    {
        scrollSpeed = 0;
    }
}
