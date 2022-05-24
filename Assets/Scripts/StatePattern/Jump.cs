using UnityEngine;
public class Jump : State
{

    public Jump(Player player, StateMachine stateMachine) : base(player, stateMachine)
    {
    }
    public override void Enter()
    {
        base.Enter();
        player.rb.velocity = Vector3.up * player.jumpPower;
        AudioManager.singleton.JumpingSound();
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
        if (player.IsGrounded())
        {
            stateMachine.ChangeState(player.idle);
        }
        player.JoyMove();
    }
}
