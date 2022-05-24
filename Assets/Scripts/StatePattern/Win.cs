using UnityEngine;
public class Win:State
{

    public Win(Player player, StateMachine stateMachine) : base(player, stateMachine)
    {
    }
    public override void Enter()
    {
        base.Enter();
        player._joy.gameObject.SetActive(false);
        player._joyButton.gameObject.SetActive(false);
        player.anim.SetTrigger("Finish");
    }

    public override void Exit()
    {
        base.Exit();
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
    
    }
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
