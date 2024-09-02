using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//SO для хотьбы дальнобойного врага
[CreateAssetMenu(fileName = "WalkRanged", menuName = "Enemy Logic/Walk Logic/Walk Ranged")]
public class EnemyWalkRanged : EnemyWalkSOBase
{
    protected Vector3 TargetPoint;

    public override void DoAnimationTriggerEventLogic(Enemy.AnimationTriggerType triggerType)
    {
        base.DoAnimationTriggerEventLogic(triggerType);
    }

    public override void DoEnterLogic()
    {
        base.DoEnterLogic();
        TargetPoint = (playerTransform.position - transform.position) / 2
            + Vector3.Scale(
                playerTransform.position - transform.position,
                new Vector3(Random.Range(-1, 1), 0, Random.Range(-1, 1))
                /2)
            + transform.position;
    }

    public override void DoExitLogic()
    {
        base.DoExitLogic();
    }

    public override void DoFrameUpdateLogic()
    {
        base.DoFrameUpdateLogic();
        if (enemy.IsWithinStrikingDistance)
        {
            enemy.StateMachine.ChangeState(enemy.AttackState);
        }
        enemy.Move(TargetPoint - transform.position, RotationSpeed);
        if ((TargetPoint - transform.position).magnitude < 0.5)
        {
            enemy.StateMachine.ChangeState(enemy.IdleState);
        }
    }

    public override void DoPhysicsUpdateLogic()
    {
        base.DoPhysicsUpdateLogic();
    }

    public override void Initialize(GameObject gameObject, Enemy enemy)
    {
        base.Initialize(gameObject, enemy);
    }

    public override void ResetValues()
    {
        base.ResetValues();
    }
}
