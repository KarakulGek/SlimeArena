using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
//Скрипт управления игрой
public class GameController : MonoBehaviour
{
    static public float Score = 0;
    static public TextMeshProUGUI ScoreText;
    [SerializeField] public List<Enemy> Enemies;
    [SerializeField] public List<Consumable> Consumables;
    [SerializeField][Tooltip("SpawnRate = ModA / (ModB + Score)")] float EnemySpawnRateModifierA;
    [SerializeField][Tooltip("SpawnRate = ModA / (ModB + Score)")] float EnemySpawnRateModifierB;
    public float EnemySpawnRate;
    public float EnemySpawnCooldown = 3f;
    [SerializeField]public float ConsumableSpawnRate = 10f;
    public float ConsumableSpawnCooldown = 0;
    public ScoreSO ScoreSave;
    private void Start()
    {
        EnemySpawnRate = EnemySpawnRateModifierA / (EnemySpawnRateModifierB + Score);
        Score = 0;
        ScoreText = GameObject.Find("UI").transform.Find("Score").GetComponent<TextMeshProUGUI>();
        ScoreUpdate();
    }
    private void Update()
    {
        //Создаёт противников
        if (EnemySpawnCooldown >= EnemySpawnRate)
        {
            EnemySpawnRate = EnemySpawnRateModifierA / (EnemySpawnRateModifierB + Score);
            EnemySpawnCooldown = 0;
            Instantiate(Enemies[Random.Range(0, Enemies.Count)].gameObject, new Vector3(Random.Range(-12f, 12f), 0,Random.Range(-10f, 6f)), Quaternion.identity);
        }
        EnemySpawnCooldown += Time.deltaTime;
        //Создаёт съедобные предметы
        if (ConsumableSpawnCooldown >= ConsumableSpawnRate)
        {
            ConsumableSpawnCooldown = 0;
            Instantiate(Consumables[Random.Range(0, Consumables.Count)].gameObject, new Vector3(Random.Range(-12f, 12f), 0, Random.Range(-10f, 6f)), Quaternion.identity);
        }
        ConsumableSpawnCooldown += Time.deltaTime;
    }
    static public void ScoreUpdate()
    {
        ScoreText.text = "Очки: " + Score;
    }
    //Сохраняет очки при поражении
    public void GameOver()
    {
        ScoreSave.LastScore = Score;
        ScoreSave.HighScore = Mathf.Max(ScoreSave.HighScore, Score);
        SceneManager.LoadScene("Menu");
    }
}
