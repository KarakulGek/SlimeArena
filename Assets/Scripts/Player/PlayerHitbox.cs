using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Скрипт хитбокса для атак игрока
public class PlayerHitbox : MonoBehaviour
{
    public float Damage;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            other.gameObject.GetComponent<Enemy>().Damage(Damage);
        }
    }
}
