using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyScriptableObject", menuName = "EnemyStats")]
public class EnemyScriptableObject : ScriptableObject
{
    public GameObject enemyTarget;

    public float basicBoneSpeed;
    public float basicBoneDamage;
    public float basicBonehealth;

    public float bigBoneSpeed;
    public float bigBoneDamage;
    public float bigBoneHealth;

    /*
    private void OnEnable()
    {
        if (GameObject.FindGameObjectWithTag("Target") != null)
            enemyTarget = GameObject.FindGameObjectWithTag("Target");
    }*/
}
