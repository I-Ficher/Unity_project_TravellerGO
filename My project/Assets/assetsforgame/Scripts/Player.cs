using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField] private int xp=0;
    [SerializeField] private int requireXp=100;
    [SerializeField] private int levelBase=100;
    [SerializeField] private int Score = 0;
    [SerializeField] private List<GameObject> enemy=new List<GameObject>();
    public int lvl = 1;
    private string path;
   
   
    public int Xp { 
        get {
            return xp; 
        } 
    }

    public int RequiredXp { get { return requireXp; } }
    public int LevelBase { get { return levelBase; } }
    

    public List<GameObject> Enemy { 
        get { return enemy; 
        } 
    }

    public int Lvl
    {
        get { return lvl; }
    }

    public int Score1 { get { return Score; } }
    //загрузка при старте и место сохранения файла
    public void Start()
    {
 
        path = Application.persistentDataPath + "/player.data";
        Load();
        
        
    }
    public void AddScore(int score)
    {
        this.Score += score;
        Save();
    }

    public void AddXp(int xp)
    {
        this.xp += Mathf.Max(0, xp);
        InitLevelData();
        Save();
    }

    public void AddEnemy(GameObject enemy)
    {
        if(enemy)
        Enemy.Add(enemy);
        Save();
    }

    private void InitLevelData()
    {
        lvl = (xp / levelBase) + 1;
        requireXp = levelBase * lvl;
    }

    private void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream fileStream = File.Create(path);
        PlayerData playerData = new PlayerData(this);
        bf.Serialize(fileStream, playerData);
        fileStream.Close();

    }

    private void Load()
    {
        if (File.Exists(path))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fileStream = File.Open(path,FileMode.Open);
            PlayerData playerData = (PlayerData)bf.Deserialize(fileStream);
            fileStream.Close();

            xp = playerData.Xp;
            requireXp = playerData.RequireXP;
            levelBase = playerData.LevelBase;
            lvl = playerData.LVL;
            Score = playerData.Score1;

        }
        else
        {
            InitLevelData();
        }
    }
}
