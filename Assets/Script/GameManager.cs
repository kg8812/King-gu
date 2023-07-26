using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public enum GameState
{
    BeforeStart,
    InGame,
    Paused,
    GameOver
}
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public UnityEvent OnGameOver = new();
    public UnityEvent OnRestart = new();
    public UnityEvent OnGameStart = new();
    public Transform originPos;
   public bool IsOnGame { get { return State == GameState.InGame || State == GameState.Paused; } }

    public GameState State { get; private set; } = GameState.BeforeStart;

    public UnityEvent OnGameFinished = new();
    public GameObject credit;
    public void Awake()
    {
        Instance = this;
        
        State = GameState.BeforeStart;
        Time.timeScale = 1;
        credit.SetActive(false);
    }
   
    public void ExitGame()
    {
        Application.Quit();
    }

    public void GameOver()
    {       
        State = GameState.GameOver;
        Time.timeScale = 0;
        OnGameOver.Invoke();
    }

    public void UnPause()
    {
        if (State == GameState.Paused)
        {
            State = GameState.InGame;
            Time.timeScale = 1;
            BGM.instance.UnPause();
        }
    }

    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        OnRestart.Invoke();
    }

    public void Pause()
    {        
        if (State == GameState.InGame)
        {
            State = GameState.Paused;
            Time.timeScale = 0;
            BGM.instance.Pause();
        }
    }

    public void StartGame()
    {
        Time.timeScale = 1;
        State = GameState.InGame;
        OnGameStart.Invoke();
    }      

    public void WinGame()
    {
        State = GameState.GameOver;
        OnGameFinished.Invoke();
    }

    public Transform skyPos;
    public SkyScroll sky;

    public IEnumerator MoveCam()
    {
        
        while(Vector2.Distance(Camera.main.transform.position, skyPos.position) > 0.04f)
        {
            Camera.main.transform.Translate(Vector3.up * Time.deltaTime * 5);
            yield return new WaitForEndOfFrame();
        }

        sky.scrollSpeed = 0.25f;
        credit.SetActive(true);
    }
}
