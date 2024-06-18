using System.Collections;
using UnityEngine;

public enum GameState
{
    MainMenu = 0,
    Gameplay = 1,
    Victory = 2,
    Fail = 3,
    Pause = 4,
}
public class GameManager : Singleton<GameManager>
{


    [SerializeField] private GameState currentState;
    public GameState CurrentState
    {
        get { return currentState; }
    }
    // Start is called before the first frame update
    void Start()
    {
        ChangeState(GameState.MainMenu);
    }
    

    public void ChangeState(GameState newState)
    {
        currentState = newState;
        switch (newState)
        {
            case GameState.MainMenu:
                MainMenuState();
                break;
            case GameState.Gameplay:
                GamePlayState();
                break;
            case GameState.Pause:
                PausegState();
                break;
            case GameState.Victory:
                VictoryState();
                break;
            case GameState.Fail:
                FailState();
                break;
        }

    }

    private void FailState()
    {
        Time.timeScale = 0;
        UIManager.Instance.CloseAll();
        UIManager.Instance.OpenUI<CanvasFail>();
    }

    private void VictoryState()
    {
        Time.timeScale = 0;
        UIManager.Instance.CloseAll();
        UIManager.Instance.OpenUI<CanvasVictory>();
    }

    private void PausegState()
    {
        Time.timeScale = 0;
        UIManager.Instance.CloseAll();
        UIManager.Instance.OpenUI<CanvasPause>();
    }

    private void GamePlayState()
    {
        Time.timeScale = 1;
        UIManager.Instance.CloseAll();
        UIManager.Instance.OpenUI<CanvasGamePlay>();
    }

    private void MainMenuState()
    {
        Time.timeScale = 1;

        UIManager.Instance.CloseAll();
        UIManager.Instance.OpenUI<CanvasMenu>();
        UIManager.Instance.OpenUI<CanvasGold>();

        LevelManager.Instance.ReLoadLevel();
    }

    public GameState GetCurrentState()
    {
        return CurrentState;
    }


    public void Victory()
    {

        ChangeState(GameState.Victory);
        Debug.Log("THANG------------------");

    }

    public void Fail()
    {
        ChangeState(GameState.Fail);
        Debug.Log("THUA------------------");
    }
}
