using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class EnemyFactory : Singleton<EnemyFactory>
{

    [SerializeField] private Enemy[] availaibleEnemy;
    [SerializeField] private float waitTime = 180.0f;
    [SerializeField] private int StartingEnemys = 5;
    [SerializeField] private float minRange = 5.0f;
    [SerializeField] private float maxRange = 50.0f;

    private List<Enemy> liveEnemy = new List<Enemy>();
    private Enemy selectedEnemy;
    private Player player;
    public List<Enemy> LiveEnemies { 
        get {
            return liveEnemy;
        } 
    }

    public int LvlToEnemys()
    {
       int x= GameManager.Instance.CurrentPlayer.lvl;
        return x + StartingEnemys;
    }
    public Enemy SelectedEnemy { get { return selectedEnemy; } }

    private void Awake()
    {
        Assert.IsNotNull(availaibleEnemy);
        
    }

    private void Start()
    {
        player = GameManager.Instance.CurrentPlayer;
        Assert.IsNotNull(player);
        for(int i = 0; i < LvlToEnemys(); i++)
        {
            InstanstiateEnemy();
        }
        StartCoroutine(GeneratEnemy());
        InvokeRepeating("InstanstiateEnemy", 20f, 20f);
    }

    public void EnemyWasSelected(Enemy enemy)
    {
        selectedEnemy = enemy;
    }

    private IEnumerator GeneratEnemy()
    {
        while (true)
        {
            InstanstiateEnemy();
            yield return new WaitForSeconds(waitTime);
        }
    }

    private void InstanstiateEnemy()
    {
        int index = Random.Range(0, availaibleEnemy.Length);
        float x = player.transform.position.x+GenerateRange();
        float y = player.transform.position.y;
        float z = player.transform.position.z + GenerateRange();
        liveEnemy.Add(Instantiate(availaibleEnemy[index],new Vector3(x,y,z),Quaternion.identity));
    }
    private float GenerateRange()
    {
        float randomNum = Random.Range(minRange, maxRange);
        bool isPosition = Random.Range(0, 10) < 5;
        return randomNum * (isPosition ? 1 : -1);
    }
}
