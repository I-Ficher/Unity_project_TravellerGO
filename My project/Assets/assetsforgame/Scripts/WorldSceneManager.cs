using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WorldSceneManager : EnemySceneManager
{
    public override void EnemyTrapped(GameObject enemy)
    {
        List<GameObject> list = new List<GameObject>();
        list.Add(enemy);
        SceneTransitionManager.Instance.GoToScene(EnemyConstant.Scene_Capture, list);
    }

    public override void playerTrapped(GameObject player)
    {
    }
    void Start()
    {
        
    }
    void Update()
    {
        
    }
}
