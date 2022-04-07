using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    Animator enemyAnimator;

    private void Awake()
    {
        enemyAnimator = GetComponentInChildren<Animator>();
    }

    public void SetBool(string action, bool set)
    {
        enemyAnimator.SetBool(action, set);
    }

    public void SetTrigger(string set)
    {
        enemyAnimator.SetTrigger(set);
    }
}
