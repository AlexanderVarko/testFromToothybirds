using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : State
{
    public Move(Player player, StateMachine stateMachine) : base(player, stateMachine)
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
        if(player.anim.GetFloat("walk") < 1&&player.IsGrounded())
        {
            player.anim.SetFloat("walk",1f, .1f, Time.deltaTime);
        }
        if (player._joyButton.pressed)
        {
            stateMachine.ChangeState(player.jump);
            player.anim.SetBool("jump", true);
        }
        if (!player._joyButton.pressed)
        {
            player.anim.SetTrigger("grounded");
        }
            
        Vector3 movementDirection = new Vector3(player._joy.Horizontal, 0, player._joy.Vertical);
        if (movementDirection == Vector3.zero)
        {
            stateMachine.ChangeState(player.idle);
        }
    }
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        if (player.IsGrounded() && player._joyButton.pressed)
        {
            stateMachine.ChangeState(player.jump);
        }
        player.JoyMove();
    }
}
