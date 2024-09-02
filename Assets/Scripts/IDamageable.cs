using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//��������� ��� ��������� �����, ������ � ������� ��� ����������� � ������
public interface IDamagable
{
    void Damage(float damageAmount);
    void Heal(float healAmount);
    void Die();
    float MaxHealth { get; set; }
    float CurrentHealth { get; set; }
}
