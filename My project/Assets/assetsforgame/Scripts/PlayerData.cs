using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class PlayerData 
{
    private int xp;
    private int requireXP;
    private int levelBase;
    private int lvl;
    private int Score;
    private List<EnemyData> enemys;
    public int Xp { get { return xp; } }
    public int RequireXP { get { return requireXP; } }
    public int LevelBase { get { return levelBase; } }
    public int LVL { get { return lvl; } }

    public List<EnemyData> Enemies { get { return enemys; } }
    public int Score1 { get { return Score; } }
    public PlayerData(Player player)
    {
        xp = player.Xp;
        requireXP = player.RequiredXp;
        levelBase = player.LevelBase;
        lvl = player.Lvl;
        Score = player.Score1;
        foreach(GameObject Enemyobj in player.Enemy)
        {
            Enemy enemy = Enemyobj.GetComponent<Enemy>();
            if (enemy != null)
            {
                EnemyData enemyData = new EnemyData(enemy);
                Enemies.Add(enemyData);
            }
        }
    }
}
