using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusXp : MonoBehaviour
{
    [SerializeField] private int bonus = 10;

    private void OnMouseDown()
    {
        GameManager.Instance.CurrentPlayer.AddXp(bonus);
        Destroy(gameObject);
    }

}
