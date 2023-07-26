using System.Collections;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using UnityEngine;
using UnityEngine.Events;

public class Tutorial : MonoBehaviour
{
    public GameObject[] texts;
    int count = 0;
    public GameObject kingGu;

    public Transform spawnPos;
    public Transform pos;

    bool isSpawned = false;

    void Awake()
    {
        texts[0].SetActive(true);
        StartCoroutine(SpawnKingGu());
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isSpawned)
        {
            if (count < texts.Length)
            {
                texts[count++].SetActive(false);
            }

            if (count < texts.Length)
            {
                texts[count].SetActive(true);
            }
            else
            {
                StartCoroutine(StartGame());
            }
        }
    }
    IEnumerator SpawnKingGu()
    {
        if (!isSpawned)
        {
            GameObject obj = Instantiate(kingGu, spawnPos.transform.position, Quaternion.identity);

            float time = 0;
            float lerpTime = 0.6f;

            while (time < lerpTime)
            {
                time += Time.deltaTime;
                yield return new WaitForEndOfFrame();

                obj.transform.position = Vector3.Lerp(spawnPos.position, pos.position, time / lerpTime);
            }
            isSpawned = true;
        }
    }
    IEnumerator StartGame()
    {
        if (GameManager.Instance.State != GameState.BeforeStart) yield break;

        GameManager.Instance.StartGame();
    }
}
