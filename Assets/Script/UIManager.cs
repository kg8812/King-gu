using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject Exit;
    public GameObject Restart;
    public TextMeshProUGUI meter;
    public static float dist = 0;
    readonly bool[] meters = new bool[5];

    int count = 1;
    public Characters characters;

    public UnityEvent Every50M = new();

    public float moveSpeed = 5f;
    private void Start()
    {
        for (int i = 0; i < meters.Length; i++)
        {
            meters[i] = false;
        }
        count = 1;        
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameManager.Instance.IsOnGame)
            {
                if (Exit.activeSelf)
                {
                    Exit.SetActive(false);
                    GameManager.Instance.UnPause();
                }
                else if (GameManager.Instance.State == GameState.InGame)
                {
                    Exit.SetActive(true);
                    GameManager.Instance.Pause();

                }
            }
        }
        if (GameManager.Instance.State == GameState.InGame)
        {
            dist += Time.deltaTime * moveSpeed;
        }

        if (dist > 50 * count)
        {
            if (count <= meters.Length && !meters[count - 1])
            {
                meters[count - 1] = true;
                characters.Spawn(count - 1);
            }
            count++;
            Every50M.Invoke();
        }

        if (dist > 300 && GameManager.Instance.State != GameState.GameOver)
        {
            GameManager.Instance.WinGame();
        }
        meter.text = Mathf.Floor(dist).ToString() + " M";
    }

    public static UIManager Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
        ResetMeter();
    }
    public void Quit()
    {
        GameManager.Instance.ExitGame();
    }

    public void ResetMeter()
    {
        dist = 0;
    }
}
