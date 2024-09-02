using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Хитбокс для атак противника
public class EnemyHitbox : MonoBehaviour
{
    public Enemy Sender;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<Player>().Damage(Sender.DamageValue);
            Destroy(gameObject);
        }
    }
}
