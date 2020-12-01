using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "SaveData", menuName = "SaveData")]
public class SaveData: ScriptableObject
{
    public int recordPoints;
    public int currentPoints;

    public SaveData()
    {
        recordPoints = 0;
        currentPoints = 0;
    }
}