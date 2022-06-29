using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaptureSceneManager : EnemySceneManager
{
    
    [SerializeField] private GameObject orb;
    [SerializeField] private Vector3 spawnPoint;
    [SerializeField] private GameObject EnemyGameObject;
    public static int maxThrowAttepts = 2;
    public static int minThrwo = 1;
    public static int orbs;
    private int currentThrowAttempts;
    private CaptureSceneStatus status=CaptureSceneStatus.InProgress;
    
    public int MaxThrowAttepts { get { return maxThrowAttepts; } }

    public int CurrentThrowAttempts { get { return currentThrowAttempts; } }

    public CaptureSceneStatus Status { get { return status; } }

    private void Start()
    {
        RandomPosition();
        CalculateMaxThrows();
        currentThrowAttempts = maxThrowAttepts;
    }
    public void RandomPosition()
    {
        int i = Random.Range(0, 3);
        switch (i) 
        {
            case 0:
                Instantiate(EnemyGameObject, new Vector3(-0.1066097f, 0.35f, 0.06572533f), new Quaternion(0f, 180f, 0f, 0f));
                break;
            case 1:
                Instantiate(EnemyGameObject, new Vector3(-1.6f, 4.3f, 6.24f), new Quaternion(0f, 180f, 0f, 0f));
                break;
            case 2:
                Instantiate(EnemyGameObject, new Vector3(3.12f, 5.8f, 14.25f), new Quaternion(0f, 200f, 0f, 0f));
                break;
            default:
                Instantiate(EnemyGameObject, new Vector3(-0.1066097f, 0.35f, 0.06572533f), new Quaternion(0f, 180f, 0f, 0f));
                break;
        }
    }
    private void CalculateMaxThrows()
    {
         maxThrowAttepts -= minThrwo;
    }
    
    public void OrbDestroy()
    {
        currentThrowAttempts--;
        if (currentThrowAttempts <= 0)
        {
            if (status!=CaptureSceneStatus.Successfull) {
                status = CaptureSceneStatus.Fail;
                Invoke("MoveToWorldScene", 2.0f);
            }
        }
        else
        {
            Instantiate(orb,spawnPoint,Quaternion.identity);
        }
    }

    public override void EnemyTrapped(GameObject enemy)
    {
        print("CaptureSceneManger.EnemyTrapped activated");
    }

    public override void playerTrapped(GameObject player)
    {
        print("CaptureSceneManger.playerTrapped activated");
    }

    public override void enemyCollision(GameObject enemy, Collision other)
    {

        status = CaptureSceneStatus.Successfull;
        GameManager.Instance.CurrentPlayer.AddScore(10);
        GameManager.Instance.CurrentPlayer.AddXp(10);
        Destroy(EnemyGameObject);
        Invoke("MoveToWorldScene",2.0f);
    }

    public void MoveToWorldScene()
    {
        
        SceneTransitionManager.Instance.GoToScene(EnemyConstant.Scene_world, new List<GameObject>());
    }
}
