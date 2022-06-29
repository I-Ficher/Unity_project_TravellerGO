using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float spawnrate = 0.10f;
    [SerializeField] private float catchRate = 0.10f;
    [SerializeField] private int attack = 0;
    [SerializeField] private int defense = 0;
    [SerializeField] private int hp = 10;
    [SerializeField] private AudioClip deathsound;
    [SerializeField] private AudioClip Alert;

    private AudioSource audioSource;


    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        Assert.IsNotNull(audioSource);
        Assert.IsNotNull(deathsound);
        Assert.IsNotNull(Alert);
    }
    

    public float spawnRate
    {
        get
        {
            return spawnRate;
        }
    }

    public float CatchRate
    {
        get
        {
            return catchRate;
        }
    }

    public int Attack
    {
        get
        {
            return attack;
        }
    }

    public int Defense
    {
        get
        {
            return defense;
        }
    }

    public int Hp
    {
        get
        {
            return hp;
        }
    }

    public AudioClip Deathsound
    {
        get { return deathsound; }
    }

    private void OnMouseDown()
    {
        EnemySceneManager[] managers = FindObjectsOfType<EnemySceneManager>();
        
        if (CaptureSceneManager.maxThrowAttepts <= 1) {
            audioSource.PlayOneShot(Alert);
            } 
        else {
            audioSource.PlayOneShot(deathsound);
            foreach (EnemySceneManager enemySceneManager in managers)
            {
                if (enemySceneManager.gameObject.activeSelf)
                {
                    enemySceneManager.EnemyTrapped(this.gameObject);
                }
            }
        }
        
    }
    private void OnCollisionEnter(Collision other)
    {
        EnemySceneManager[] managers = FindObjectsOfType<EnemySceneManager>();
        
        foreach (EnemySceneManager enemySceneManager in managers)
        {
            if (enemySceneManager.gameObject.activeSelf)
            {
                enemySceneManager.enemyCollision(this.gameObject,other);
            }
        }
    }
}
