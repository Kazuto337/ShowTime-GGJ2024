using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    private float score;

    public PlayerData(float score)
    {
        Score = score;
    }

    public float Score { get => score; set => score = value; }
}
