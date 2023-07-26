using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyScroll : MonoBehaviour
{
    public Material bgMat;
    public float scrollSpeed = 0f;

    private void Start()
    {
        bgMat.mainTextureOffset = Vector3.zero;
    }
    // Update is called once per frame
    void Update()
    {
        bgMat.mainTextureOffset += scrollSpeed * Time.deltaTime * Vector2.up;
    }
}
