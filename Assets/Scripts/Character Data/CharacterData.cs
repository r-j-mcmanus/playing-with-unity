using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterData : MonoBehaviour
{
    public int[] maxHealth;
    public int[] health;

    //public int maxBodyHealth = 100;
    //public int maxLArmHealth = 100;
    //public int maxRArmHealth = 100;
    //public int maxLegHealth = 100;

    //public int bodyHealth = 100;
    //public int lArmHealth = 100;
    //public int rArmHealth = 100;
    //public int legHealth = 100;

    //public int bodyRemainingDamage = 0;
    //public int lArmRemainingDamage = 0;
    //public int rArmRemainingDamage = 0;
    //public int legRemainingDamage = 0;

    
    public float speed = 3.0f;
    public float maxMoveRange = 30.0f;
    [HideInInspector]
    public Vector3 turnStartPosition;
    [HideInInspector]
    public Quaternion turnStartEURotation;


    public string weaponType = "rifle";
    public int attack = 50;
    public float attackRange = 20.0f;

    public Vector3 targetPosition;

    [HideInInspector]
    public BodyController bodyController;

    public CharacterData()
    {
        maxHealth = new int[4] { 100, 100, 100, 100 };
        health = (int[])maxHealth.Clone();
    }

    public void Start()
    {
        bodyController = transform.Find("TempBody").GetComponent<BodyController>();
    }

    public void RecordTurnStartPosition()
    {
        turnStartPosition = transform.position;
        turnStartEURotation = transform.rotation;
    }

    public void RevertToTurnStartPosition()
    {
        transform.position = turnStartPosition;
        transform.rotation = turnStartEURotation;
    }


    public int GetRandLiveBodyPart()
    {
        int[] parts = { 0, 1, 2, 3 };
        Utility.Shuffle(parts);
        //Debug.Log(parts);
        for(int i=0; i<4; i++ )
        {
            if (health[parts[i]]>0)
                return parts[i];
        }
        return 0;
    }

    public void DamageBodyPart(BodyPart bodyPart, int damage)
    {
        health[(int) bodyPart] = Mathf.Max(health[(int)bodyPart] - damage, 0);
    }

    public int DamageRandBodyPart(int damage)
    {
        int bodyPart = GetRandLiveBodyPart();
        health[bodyPart] = Mathf.Max(health[bodyPart] - damage, 0);
        return bodyPart;
    }

    public int HealBodyPart(BodyPart bodyPart, int damage)
    {
        return health[(int) bodyPart] = Mathf.Min(health[(int) bodyPart] + damage, maxHealth[(int) bodyPart]);
    }

    public float HealthFraction(BodyPart bodyPart)
    {
        return (float) health[(int) bodyPart] / maxHealth[(int) bodyPart];
    }

    public int DamageBodyPart(int bodyPart, int damage)
    {
        return health[bodyPart] = Mathf.Max(health[bodyPart] - damage, 0);
    }

    public int HealBodyPart(int bodyPart, int damage)
    {
        return health[bodyPart] = Mathf.Min(health[bodyPart] + damage, maxHealth[bodyPart]);
    }

    public float HealthFraction(int bodyPart)
    {
        return (float)health[bodyPart] / maxHealth[bodyPart];
    }
}

