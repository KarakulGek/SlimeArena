using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//��������� ��� �������� ����� � ������
public interface ITriggerCheckable
{
    bool IsWithinStrikingDistance { get; set; }
    void SetWithinStrikingDistance(bool isWithinStrikingDistance);
}
