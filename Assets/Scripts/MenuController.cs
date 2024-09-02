using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
//Скрипт для управления главным меню
public class MenuController : MonoBehaviour
{
    public ScoreSO ScoreSave;
    public TextMeshProUGUI HighScore;
    public TextMeshProUGUI LastScore;

    public string saveFilePath;
    //Сохраняет или загружает рекорд
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
        HighScore.text = "Рекорд: " + ScoreSave.HighScore;
        LastScore.text = "Последний счёт: " + ScoreSave.LastScore;
    }
    //Сохранение
    public void SaveScore()
    {
        string saveScore = JsonUtility.ToJson(ScoreSave);
        File.WriteAllText(saveFilePath, saveScore);
    }
    //Загрузка
    public void LoadScore()
    {
        if (File.Exists(saveFilePath))
        {
            string loadScore = File.ReadAllText(saveFilePath);
            JsonUtility.FromJsonOverwrite(loadScore, ScoreSave);
        }
    }
    //Удаление рекорда
    public void DeleteSave()
    {
        ScoreSave.HighScore = 0;
        ScoreSave.LastScore = 0;
        SaveScore();
        HighScore.text = "Рекорд: " + ScoreSave.HighScore;
        LastScore.text = "Последний счёт: " + ScoreSave.LastScore;
    }
    //Начало игры
    public void StartGame()
    {
        SceneManager.LoadScene("MainScene");
    }
    //Выход
    public void Exit()
    {
        Application.Quit();
    }
}
