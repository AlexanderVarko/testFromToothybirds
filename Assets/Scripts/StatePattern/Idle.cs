using UnityEngine;
public class Idle : State
{

    public Idle(Player player, StateMachine stateMachine) : base(player, stateMachine)
    {
    }
    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (player.anim.GetFloat("walk") > 0)
        {
            player.anim.SetFloat("walk", 0.001f ,0.1f,Time.deltaTime);
        }
        if (player._joyButton.pressed)
        {
            player.anim.SetBool("jump", true);
            stateMachine.ChangeState(player.jump);
        }
        Vector3 movementDirection = new Vector3(player._joy.Horizontal, 0, player._joy.Vertical);
        if (movementDirection != Vector3.zero)
        {
            stateMachine.ChangeState(player.move);
        }
    }
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        
    }
}
