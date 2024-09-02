using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Скрипт атаки игрока
public class PlayerAttack : MonoBehaviour
{
    public PlayerHitbox HitBox;
    public Player Sender;
    public GameObject CurrentHitBox;
    public GameObject VFX;
    public GameObject CurrentVFX;
    public float Damage;
    public void AnimationTriggerEvent(AnimationTriggerType triggerType)
    {
        if(triggerType == PlayerAttack.AnimationTriggerType.AttackStart)
        {
            CurrentHitBox = Instantiate(HitBox.gameObject, transform);
            CurrentHitBox.GetComponent<PlayerHitbox>().Damage = Damage;
            CurrentVFX = Instantiate(VFX, gameObject.transform);
            //CurrentVFX.transform.localPosition += new Vector3(0, 0, 1);
        }
        if (triggerType == PlayerAttack.AnimationTriggerType.AttackEnd)
        {
            Destroy(CurrentHitBox);
        }
    }
    public enum AnimationTriggerType
    {
        AttackStart,
        AttackEnd
    }
}
