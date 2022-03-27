using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private int[,] matrix;
    private GameObject[,] matrixBlocks;

    // Start is called before the first frame update
    void Start()
    {
        matrix = new int[NUMBER_OF_ROWS,NUMBER_OF_COLUMNS];
        matrixBlocks = new GameObject[NUMBER_OF_ROWS,NUMBER_OF_COLUMNS];
        createBlocks();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void createBlocks(){

        for (int i=0; i<NUMBER_OF_ROWS; i++){
            for (int j=0; j<NUMBER_OF_COLUMNS; j++){
                GameObject newBlock = Instantiate(blockPrefab, transform.position, transform.rotation);
                SpriteRenderer renderer = newBlock.GetComponent<SpriteRenderer>();

                renderer.sprite = noBlock;
                newBlock.transform.Translate(new Vector2(i * distanceBetweenBlocks, j * distanceBetweenBlocks));

                newBlock.GetComponent<Block>().currentState = 1;
            }
        }

    }

    void paintBlocks(){

        for (int i=0; i<NUMBER_OF_ROWS; i++){
            for (int j=0; j<NUMBER_OF_COLUMNS; j++){
                
            }
        }

    }
}
