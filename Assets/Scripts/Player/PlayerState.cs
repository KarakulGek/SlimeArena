using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
//�����, ������������ ��� ������ ��� ��������� ������
public class PlayerState
{
    protected Player player;
    protected PlayerStateMachine playerStateMachine;

    public PlayerState(Player player, PlayerStateMachine playerStateMachine)
    {
        this.player = player;
        this.playerStateMachine = playerStateMachine;
    }
    public virtual void EnterState()
    {

    }
    public virtual void ExitState()
    {

    }
    public virtual void FrameUpdate()
    {

    }
    public virtual void PhysicsUpdate()
    {

    }
    public virtual void AnimationTriggerEvent(Player.AnimationTriggerType triggerType)
    {

    }
}
