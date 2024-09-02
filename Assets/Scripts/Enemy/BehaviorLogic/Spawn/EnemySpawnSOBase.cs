using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Основа для SO появление противника
public class EnemySpawnSOBase : ScriptableObject
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
        enemy.fadeTime = 0;
    }
    public virtual void DoExitLogic() 
    {
        enemy.Invulnurable = false;
        enemy.HealthBar.gameObject.GetComponentInParent<CanvasGroup>().alpha = 1;
        ResetValues(); 
    }

    public virtual void DoFrameUpdateLogic() 
    {
        if (enemy.fadeTime < 1)
        {
            enemy.HealthBar.gameObject.GetComponentInParent<CanvasGroup>().alpha = Mathf.Lerp(0, 1, enemy.fadeTime / 1);
            enemy.fadeTime += Time.deltaTime;
        }
        else
        {
            enemy.HealthBar.gameObject.GetComponentInParent<CanvasGroup>().alpha = 1;
        }
    }
    public virtual void DoPhysicsUpdateLogic() { }
    public virtual void DoAnimationTriggerEventLogic(Enemy.AnimationTriggerType triggerType) 
    {
        if (triggerType == Enemy.AnimationTriggerType.EndState)
        {
            if (!enemy.IsWithinStrikingDistance) enemy.StateMachine.ChangeState(enemy.WalkState);
            else enemy.StateMachine.ChangeState(enemy.AttackState);
        }   
    }
    public virtual void ResetValues() { }
}
