using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
//������ ��� ���������� ������� ����
public class MenuController : MonoBehaviour
{
    public ScoreSO ScoreSave;
    public TextMeshProUGUI HighScore;
    public TextMeshProUGUI LastScore;

    public string saveFilePath;
    //��������� ��� ��������� ������
    private void Start()
    {
        saveFilePath = Application.persistentDataPath + "/ScoreSave.json";
        if (ScoreSave.HighScore != 0)
        {
            SaveScore();
        }
        if (ScoreSave.HighScore == 0)
        {
            LoadScore();
        }
        HighScore.text = "������: " + ScoreSave.HighScore;
        LastScore.text = "��������� ����: " + ScoreSave.LastScore;
    }
    //����������
    public void SaveScore()
    {
        string saveScore = JsonUtility.ToJson(ScoreSave);
        File.WriteAllText(saveFilePath, saveScore);
    }
    //��������
    public void LoadScore()
    {
        if (File.Exists(saveFilePath))
        {
            string loadScore = File.ReadAllText(saveFilePath);
            JsonUtility.FromJsonOverwrite(loadScore, ScoreSave);
        }
    }
    //�������� �������
    public void DeleteSave()
    {
        ScoreSave.HighScore = 0;
        ScoreSave.LastScore = 0;
        SaveScore();
        HighScore.text = "������: " + ScoreSave.HighScore;
        LastScore.text = "��������� ����: " + ScoreSave.LastScore;
    }
    //������ ����
    public void StartGame()
    {
        SceneManager.LoadScene("MainScene");
    }
    //�����
    public void Exit()
    {
        Application.Quit();
    }
}
