using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//SO для дальнобойной атаки противника
[CreateAssetMenu(fileName = "AttackRanged", menuName = "Enemy Logic/Attack Logic/Attack Ranged")]
public class EnemyAttackRanged : EnemyAttackSOBase
{
    protected Vector3 PredictedPlayerPosition;
    protected Vector3 PreviousPlayerPosition;
    protected bool tracking = true;
    protected Transform FirePoint;
    protected GameObject ShotProjectile;
    
    [SerializeField] Projectile Projectile;
    [SerializeField] float TrackingSpeed = 0.2f;
    [SerializeField] float TrackedDistance = 1f;

    public override void DoAnimationTriggerEventLogic(Enemy.AnimationTriggerType triggerType)
    {
        base.DoAnimationTriggerEventLogic(triggerType);
        if (triggerType == Enemy.AnimationTriggerType.Attack)
        {
            tracking = false;
            ShotProjectile = Instantiate(Projectile.gameObject, FirePoint.position, FirePoint.rotation);
            ShotProjectile.GetComponent<Projectile>().Sender = enemy;
        }
    }

    public override void DoEnterLogic()
    {
        base.DoEnterLogic();
        tracking = true;
        PreviousPlayerPosition = playerTransform.position;
        FirePoint = enemy.transform.Find("FirePoint").transform;
    }

    public override void DoExitLogic()
    {
        base.DoExitLogic();
    }

    public override void DoFrameUpdateLogic()
    {
        base.DoFrameUpdateLogic();
        PredictedPlayerPosition = playerTransform.position +(playerTransform.position - PreviousPlayerPosition).normalized * TrackedDistance;
        PreviousPlayerPosition = playerTransform.position;
        transform.rotation = Quaternion.LookRotation(
            Vector3.RotateTowards(
                transform.forward,
                new Vector3(PredictedPlayerPosition.x - transform.position.x, 0, PredictedPlayerPosition.z - transform.position.z).normalized,
                TrackingSpeed * Time.deltaTime, 0f));
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
