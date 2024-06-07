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


    private GameState currentState;
    public GameState CurrentState
    {
        get { return currentState; }
    }
    // Start is called before the first frame update
    void Start()
    {
        UIManager.Instance.OpenUI<CanvasMenu>();
    }

    // Update is called once per frame
    void Update()
    {

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
                SettingState();
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

    }

    private void VictoryState()
    {

    }

    private void SettingState()
    {

    }

    private void GamePlayState()
    {

    }

    private void MainMenuState()
    {

    }
    public GameState GetCurrentState()
    {
        return CurrentState;
    }
}
