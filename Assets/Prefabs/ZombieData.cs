using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "newZombie", menuName = "MyScriptableObjects/Zombie")]

public class ZombieData : ScriptableObject
{
    public string type;
    public int life;
    public float size;
    public float forceMultiplier;
    public float peso;


}
