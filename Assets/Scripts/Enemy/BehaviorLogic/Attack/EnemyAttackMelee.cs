using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//SO для ближнебойной атаки противника
[CreateAssetMenu(fileName = "AttackMelee", menuName = "Enemy Logic/Attack Logic/Attack Melee")]
public class EnemyAttackMelee : EnemyAttackSOBase
{
    public override void DoAnimationTriggerEventLogic(Enemy.AnimationTriggerType triggerType)
    {
        base.DoAnimationTriggerEventLogic(triggerType);
        if (triggerType == Enemy.AnimationTriggerType.Attack)
        {
            enemy.CurrentHitBox = Instantiate(enemy.HitBox.gameObject, enemy.transform);
            enemy.CurrentHitBox.GetComponent<EnemyHitbox>().Sender = enemy;
        }
        if (triggerType == Enemy.AnimationTriggerType.EndAttack)
        {
            Destroy(enemy.CurrentHitBox);
        }
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
