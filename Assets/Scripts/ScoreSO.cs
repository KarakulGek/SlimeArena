using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//SO ��� ���������� �����
[CreateAssetMenu(fileName = "ScoreSave", menuName = "ScoreSave/ScoreSave")]
public class ScoreSO : ScriptableObject
{
    public float HighScore;
    public float LastScore;
}
