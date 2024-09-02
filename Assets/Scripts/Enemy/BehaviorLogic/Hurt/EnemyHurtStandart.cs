using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//—тандартное SO получени€ урона противником
[CreateAssetMenu(fileName = "HurtStandart", menuName = "Enemy Logic/Hurt Logic/Hurt Standart")]
public class EnemyHurtStandart : EnemyHurtSOBase
{
    public override void DoAnimationTriggerEventLogic(Enemy.AnimationTriggerType triggerType)
    {
        if (triggerType == Enemy.AnimationTriggerType.EndState)
        {
            if(!enemy.IsWithinStrikingDistance) enemy.StateMachine.ChangeState(enemy.WalkState);
            else enemy.StateMachine.ChangeState(enemy.AttackState);
        }
        base.DoAnimationTriggerEventLogic(triggerType);
    }

    public override void DoEnterLogic()
    {
        base.DoEnterLogic();
    }

    public override void DoExitLogic()
    {
        base.DoExitLogic();
    }

    public override void DoFrameUpdateLogic()
    {
        base.DoFrameUpdateLogic();
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
