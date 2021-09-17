using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/EnemyData", order = 1)]
public class EnemyData : ScriptableObject
{
    public int health;
    public int damage;
    public int level;
    public int movementSpeed;
    public Type type;
}


public enum Type
{
    melee,
    range
};




