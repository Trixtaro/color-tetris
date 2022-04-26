using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    const int POINTS_FOR_LINE = 200;
    const int POINTS_FOR_VERTICAL_BLOCK = 75;
    const int POINTS_FOR_HORIZONTAL_BLOCK = 50;
    public Text scoreLabel;
    public int currentScore;

    void Start()
    {   
        this.currentScore = 0;
    }

    public void restartScore(){
        this.currentScore = 0;
    }

    public int deleteLines(int quantityOfLines){
        int points = quantityOfLines * POINTS_FOR_LINE;
        addScore(points);
        return points; 
    }

    public int deleteVerticalBlocks(int quantityOfBlocks){
        int points = quantityOfBlocks * POINTS_FOR_VERTICAL_BLOCK;
        addScore(points);
        return points; 
    }

    public int deleteHorizontalBlocks(int quantityOfBlocks){
        int points = quantityOfBlocks * POINTS_FOR_HORIZONTAL_BLOCK;
        addScore(points);
        return points; 
    }

    private void addScore(int points){
        this.currentScore += points;
    }

    private void OnGUI() {
        scoreLabel.text = currentScore.ToString();
    }
}
