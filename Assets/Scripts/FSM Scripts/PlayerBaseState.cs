using UnityEngine;

public abstract class PlayerBaseState
{
    public abstract void EnterState(PlayerControler_FSM player);

    public abstract void ExitState(PlayerControler_FSM player);

    public abstract void Update(PlayerControler_FSM player);
}
