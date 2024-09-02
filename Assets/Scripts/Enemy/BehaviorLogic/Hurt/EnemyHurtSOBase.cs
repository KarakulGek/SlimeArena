using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Основа для SO получения урона противником
public class EnemyHurtSOBase : ScriptableObject
{
    protected Enemy enemy;
    protected Transform transform;
    protected GameObject gameObject;

    protected Transform playerTransform;

    public virtual void Initialize(GameObject gameObject, Enemy enemy)
    {
        this.gameObject = gameObject;
        transform = gameObject.transform;
        this.enemy = enemy;

        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public virtual void DoEnterLogic()
    {
        enemy.Invulnurable = true;
        enemy.animator.SetTrigger("Hurt");
    }
    public virtual void DoExitLogic() 
    {
        enemy.Invulnurable = false;
        ResetValues();
    }

    public virtual void DoFrameUpdateLogic() { }
    public virtual void DoPhysicsUpdateLogic() { }
    public virtual void DoAnimationTriggerEventLogic(Enemy.AnimationTriggerType triggerType) { }
    public virtual void ResetValues() { }
}
