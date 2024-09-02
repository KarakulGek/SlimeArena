using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Проверка входа игрока в радиус атаки
public class EnemyStrikingDistanceCheck : MonoBehaviour
{
    public Enemy _enemy;

    private void Awake()
    {
        _enemy = GetComponentInParent<Enemy>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
            _enemy.SetWithinStrikingDistance(true);
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
            _enemy.SetWithinStrikingDistance(false);
    }
}
