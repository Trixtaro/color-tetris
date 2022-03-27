using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BlockColors {
    NoColor = 0,
    Yellow = 1,
    Blue = 2,
    Red = 3
}

public class TetrisManager : MonoBehaviour
{

    public const int NUMBER_OF_ROWS = 10;
    public const int NUMBER_OF_COLUMNS = 10;

    public float distanceBetweenBlocks = 0.1f;

    public Sprite blueBlock;
    public Sprite redBlock;
    public Sprite yellowBlock;
    public Sprite noBlock;

    public GameObject blockPrefab;

    private BlockColors[,] matrix;
    private GameObject[,] matrixBlocks;

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        matrix = new BlockColors[NUMBER_OF_ROWS,NUMBER_OF_COLUMNS];
        matrixBlocks = new GameObject[NUMBER_OF_ROWS,NUMBER_OF_COLUMNS];
        createBlocks();
    }

    // Update is called once per frame
    void Update()
    {
        if (Random.Range(0,2) > 0.9)
            changeSomeBlocks();
    }

    void createBlocks(){

        for (int i=0; i<NUMBER_OF_ROWS; i++){
            for (int j=0; j<NUMBER_OF_COLUMNS; j++){
                GameObject newBlock = Instantiate(blockPrefab, transform.position, transform.rotation);
                SpriteRenderer renderer = newBlock.GetComponent<SpriteRenderer>();

                renderer.sprite = noBlock;
                newBlock.transform.Translate(new Vector2(i * distanceBetweenBlocks, j * distanceBetweenBlocks));

                newBlock.GetComponent<Block>().currentState = BlockColors.NoColor;

                matrixBlocks[i,j] = newBlock;
            }
        }

    }

    void paintBlocks(){
        for (int i=0; i<NUMBER_OF_ROWS; i++){
            for (int j=0; j<NUMBER_OF_COLUMNS; j++){
                if(matrixBlocks[i,j].GetComponent<Block>().currentState != matrix[i,j]){
                    matrixBlocks[i,j].GetComponent<Block>().currentState = matrix[i,j];
                    matrixBlocks[i,j].GetComponent<SpriteRenderer>().sprite = getColorOfBlock(matrix[i,j]);
                }
            }
        }
    }

    void changeSomeBlocks(){
        int randomValueX = Random.Range(0, NUMBER_OF_COLUMNS);
        int randomValueY = Random.Range(0, NUMBER_OF_ROWS);

        matrix[randomValueX,randomValueY] = (BlockColors) Random.Range(1,4);

        paintBlocks();
    }

    Sprite getColorOfBlock(BlockColors color){
        switch(color){
            case BlockColors.NoColor: return noBlock;
            case BlockColors.Yellow: return yellowBlock;
            case BlockColors.Blue: return blueBlock;
            case BlockColors.Red: return redBlock;
            default: return noBlock;
        }
    }
}
