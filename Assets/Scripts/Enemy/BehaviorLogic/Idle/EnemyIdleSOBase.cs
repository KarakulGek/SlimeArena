using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Основа для SO стояние противника
public class EnemyIdleSOBase : ScriptableObject
{
    protected Enemy enemy;
    protected Transform transform;
    protected GameObject gameObject;

    protected Transform playerTransform;

    protected float currentIdleTime = 0;

    [SerializeField] float IdleTime = 1.25f;

    public virtual void Initialize(GameObject gameObject, Enemy enemy)
    {
        this.gameObject = gameObject;
        transform = gameObject.transform;
        this.enemy = enemy;

        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public virtual void DoEnterLogic()
    {
        enemy.animator.SetTrigger("Idle");
    }
    public virtual void DoExitLogic() 
    {
        ResetValues();
    }

    public virtual void DoFrameUpdateLogic() 
    {
        currentIdleTime += Time.deltaTime;
        if (currentIdleTime >= IdleTime)
        {
            if (!enemy.IsWithinStrikingDistance) enemy.StateMachine.ChangeState(enemy.WalkState);
            else enemy.StateMachine.ChangeState(enemy.AttackState);
        }
    }
    public virtual void DoPhysicsUpdateLogic() { }
    public virtual void DoAnimationTriggerEventLogic(Enemy.AnimationTriggerType triggerType) { }
    public virtual void ResetValues() 
    {
        currentIdleTime = 0;
    }
}
