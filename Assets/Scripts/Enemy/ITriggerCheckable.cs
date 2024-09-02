using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Интерфейс для проверки входа в радиус
public interface ITriggerCheckable
{
    bool IsWithinStrikingDistance { get; set; }
    void SetWithinStrikingDistance(bool isWithinStrikingDistance);
}
