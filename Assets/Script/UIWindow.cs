using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIWindow : MonoBehaviour
{
    public GameObject bg;

    private void OnEnable()
    {
        bg.SetActive(true);
    }
    private void OnDisable()
    {
        bg.SetActive(false);
    }
}
