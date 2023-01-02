using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreHandeler : MonoBehaviour
{
    private int score = 0;

    public int Score
    { 
        get 
        { 
            return score;
        } 
        set
        {
            score = value;
        }
    }
}
