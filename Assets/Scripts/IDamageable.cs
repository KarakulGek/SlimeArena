using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Интерфейс для получения урона, смерти и лечения для противников и игрока
public interface IDamagable
{
    void Damage(float damageAmount);
    void Heal(float healAmount);
    void Die();
    float MaxHealth { get; set; }
    float CurrentHealth { get; set; }
}
