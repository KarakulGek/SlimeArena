using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
//Основа для SO Смерти противника
public class EnemyDeathSOBase : ScriptableObject
{
    protected Enemy enemy;
    protected Transform transform;
    protected GameObject gameObject;

    protected Transform playerTransform;

    protected bool decaying = false;
    protected bool animationFinished = false;
    protected float delayTime = 0;

    [SerializeField] float DecayDelay;
    [SerializeField] float DecaySpeed = 0.05f;

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
        enemy.animator.SetTrigger("Die");
        enemy.fadeTime = 0;
    }
    public virtual void DoExitLogic() 
    {
        enemy.Invulnurable = false;
        ResetValues(); 
    }

    public virtual void DoFrameUpdateLogic() 
    {
        if (enemy.fadeTime < 1)
        {
            enemy.HealthBar.gameObject.GetComponentInParent<CanvasGroup>().alpha = Mathf.Lerp(1, 0, enemy.fadeTime / 1);
            enemy.fadeTime += Time.deltaTime;
        }
        else
        {
            enemy.HealthBar.gameObject.GetComponentInParent<CanvasGroup>().alpha = 0;
        }

        if (!decaying)
        {
            delayTime += Time.deltaTime;
            if (delayTime >= DecayDelay)
            {
                decaying = true;
            }
        }
        else
        {
            transform.position -= new Vector3(0, Time.deltaTime * DecaySpeed, 0);
            if (transform.position.y < -1)
            {
                Destroy(gameObject);
            }
        }
    }
    public virtual void DoPhysicsUpdateLogic() { }
    public virtual void DoAnimationTriggerEventLogic(Enemy.AnimationTriggerType triggerType) 
    {
        if (triggerType == Enemy.AnimationTriggerType.EndState)
        {
            animationFinished = true;
        }
    }
    public virtual void ResetValues() 
    {
        decaying = false;
        animationFinished = false;
        delayTime = 0;
    }
}
