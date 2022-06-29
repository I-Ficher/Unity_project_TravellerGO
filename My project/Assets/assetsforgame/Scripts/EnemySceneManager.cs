using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemySceneManager : MonoBehaviour
{
    public abstract void playerTrapped(GameObject player);
    public abstract void EnemyTrapped(GameObject enemy);
    public virtual void enemyCollision(GameObject enemy,Collision other)
    {
    }
}
