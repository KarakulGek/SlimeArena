using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Основа для SO хотьбы противника
public class EnemyWalkSOBase : ScriptableObject
{
    protected Enemy enemy;
    protected Transform transform;
    protected GameObject gameObject;

    protected Transform playerTransform;

    [SerializeField] public float RotationSpeed = 5;

    public virtual void Initialize(GameObject gameObject, Enemy enemy)
    {
        this.gameObject = gameObject;
        transform = gameObject.transform;
        this.enemy = enemy;

        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public virtual void DoEnterLogic()
    {
        enemy.animator.SetTrigger("Walk");
    }
    public virtual void DoExitLogic()
    {
        ResetValues();
    }

    public virtual void DoFrameUpdateLogic() 
    {
        
    }
    public virtual void DoPhysicsUpdateLogic() { }
    public virtual void DoAnimationTriggerEventLogic(Enemy.AnimationTriggerType triggerType) { }
    public virtual void ResetValues() { }
}
