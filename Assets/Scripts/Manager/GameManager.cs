using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using TMPro;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public GameObject[] Player;
    public GameObject[] Enemy;
    public GameObject StartPanel;
    public GameObject GameOverPanel;
    public GameObject ClearPanel;
    public TMP_Dropdown Dropdown;
    public int EnemyCount = 3;
    public int Stage = 1;
    public int playerHp = 1;
    public bool IsPause;


    const float xSize = 10f;
    const float yMaxSize = 19f;
    const float yMinSize = 0f;

    float xSpawnValue;
    float ySpawnValue;
    Vector2 spawnValue;

    private StateMachine stateMachine;
    private int enemyIndex = 0;

    private void Awake()
    {
        stateMachine = GetComponent<StateMachine>();

        // 초기 상태 설정 
        stateMachine.ChangeState(new StartState());
    }

    public void Init()
    {
        EnemyCount = 3;
        Stage = 1;
        playerHp = 100;
        enemyIndex = 0;
        ObjectPoolManager.Instance.DespawnAll();
        
    }

    public void UpdateGameState()
    {
        if (EnemyCount == 0)
        {
            stateMachine.ChangeState(new PlayingState());
        }

        if (playerHp <= 0)
        {
            stateMachine.ChangeState(new GameOverState());
        }

        if (Stage == 40)
        {
            stateMachine.ChangeState(new ClearState());
        }
    }

    public void StartButton()
    {
        StartPanel.SetActive(false);
        stateMachine.ChangeState(new PlayingState());
    }

    public void PopupStarts()
    {
        StartPanel.SetActive(true);
    }

    public void GameOverButton()
    {
        GameOverPanel.SetActive(false);
        stateMachine.ChangeState(new StartState());
    }

    public void PopupGameOver()
    {
        GameOverPanel.SetActive(true);
    }

    public void ClearButton()
    {
        ClearPanel.SetActive(false);
        stateMachine.ChangeState(new StartState());
    }

    public void PopupClear()
    {
        ClearPanel.SetActive(true);
    }

    public void SelectPlayer()
    {
        GameObject beforePlayer = FindObjectOfType<PlayerController>().gameObject;
        if (beforePlayer != null)
        {
            beforePlayer.gameObject.SetActive(false);
        }
        Player[Dropdown.value].SetActive(true);
    }

    public void SpawnEnemy()
    {
        EnemyCount = 3;
        for (int i = 0; i < EnemyCount; i++)
        {
            ObjectPoolManager.Instance.SpawnFromPool(Enemy[enemyIndex].name, RandomSpawn());
            enemyIndex++;

            if(enemyIndex > Enemy.Length - 1)
            {
                enemyIndex = 0;
            }
        }
    }

    public Vector2 RandomSpawn()
    {
        xSpawnValue = Random.Range(-xSize, xSize);
        ySpawnValue = Random.Range(-yMinSize, yMaxSize);

        spawnValue = new Vector2(xSpawnValue, ySpawnValue);

        return spawnValue;
    }
}
