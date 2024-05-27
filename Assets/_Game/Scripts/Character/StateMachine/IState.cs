public interface IState
{
    void OnEnter(Enemy enemy);

    void OnExecute(Enemy enemy);

    void OnExit(Enemy enemy);

}
public enum CharacterState
{
    IdleState = 0,
    MoveState = 1,
    AttackState = 2,
}
