using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Основа для SO атаки противников
public class EnemyAttackSOBase : ScriptableObject
{
    protected Enemy enemy;
    protected Transform transform;
    protected GameObject gameObject;

    protected Transform playerTransform;

    [SerializeField] public int NumberOfAnimations = 1;

    public virtual void Initialize(GameObject gameObject, Enemy enemy)
    {
        this.gameObject = gameObject;
        transform = gameObject.transform;
        this.enemy = enemy;

        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public virtual void DoEnterLogic() 
    {
        enemy.animator.SetTrigger("Attack" + Random.Range(1, NumberOfAnimations + 1));
    }
    public virtual void DoExitLogic() { ResetValues(); }

    public virtual void DoFrameUpdateLogic() { }
    public virtual void DoPhysicsUpdateLogic() { }
    public virtual void DoAnimationTriggerEventLogic(Enemy.AnimationTriggerType triggerType) 
    {
        if (triggerType == Enemy.AnimationTriggerType.EndState)
        {
            enemy.StateMachine.ChangeState(enemy.IdleState);
        }
    }
    public virtual void ResetValues() { }
}
