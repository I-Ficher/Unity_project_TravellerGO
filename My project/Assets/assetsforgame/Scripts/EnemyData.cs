using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class EnemyData
{
     private float spawnrate;
     private float catchRate;
     private int attack;
     private int defense;
     private int hp;
     private string deathsound;
    public float spawnRate
    {
        get{ return spawnRate; }
    }

    public float CatchRate { get { return CatchRate; } }
    public int Attack { get { return Attack; } }
    public int Defense { get { return Defense; } }
    public int Hp { get { return Hp; } }
    public string Deathsound { get { return deathsound; } }
    public EnemyData(Enemy enemy)
    {
        spawnrate = enemy.spawnRate;
        catchRate = enemy.CatchRate;
        attack = enemy.Attack;
        defense = enemy.Defense;
        hp = enemy.Hp;
        AudioClip clip;
        deathsound = enemy.Deathsound.name;
    }
}
