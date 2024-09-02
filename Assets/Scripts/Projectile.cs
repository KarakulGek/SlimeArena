using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Скрипт снаряда
public class Projectile : MonoBehaviour
{
    [SerializeField] public float speed = 1;
    public Enemy Sender;
    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<Player>().Damage(Sender.DamageValue);
            Destroy(gameObject);
        }
    }
}
