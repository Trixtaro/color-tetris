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

    private BlockColors[,] matrix;
    private GameObject[,] matrixBlocks;
    private Piece currentPiece = new Piece(Pieces.PieceSquare, 4, 4);
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
        if (Input.GetKeyDown(KeyCode.LeftControl)){
            cleanPreviousPiecePosition();
            currentPiece.rotateCounterClockWise();
            paintBlocks();
        }

        if (Input.GetKeyDown(KeyCode.Space)){
            cleanPreviousPiecePosition();
            currentPiece.rotateClockWise();
            paintBlocks();
        }

        if (Input.GetKeyDown(KeyCode.DownArrow)){
            cleanPreviousPiecePosition();
            currentPiece.moveDown();
            paintBlocks();
        }

        if (Input.GetKeyDown(KeyCode.UpArrow)){
            cleanPreviousPiecePosition();
            currentPiece.moveUp();
            paintBlocks();
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow)){
            cleanPreviousPiecePosition();
            currentPiece.moveLeft();
            paintBlocks();
        }

        if (Input.GetKeyDown(KeyCode.RightArrow)){
            cleanPreviousPiecePosition();
            currentPiece.moveRight();
            paintBlocks();
        }
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

        if (currentPiece != null){
            paintCurrentPiece();
        }

        for (int i=0; i<NUMBER_OF_ROWS; i++){
            for (int j=0; j<NUMBER_OF_COLUMNS; j++){
                if(matrixBlocks[i,j].GetComponent<Block>().currentState != matrix[i,j]){
                    matrixBlocks[i,j].GetComponent<Block>().currentState = matrix[i,j];
                    matrixBlocks[i,j].GetComponent<SpriteRenderer>().sprite = getColorOfBlock(matrix[i,j]);
                }
            }
        }
    }

    void paintCurrentPiece(){

        BlockColors[,] piece = currentPiece.piece;

        for (int i=0; i < piece.GetLength(0); i++){
            for (int j=0; j < piece.GetLength(1); j++){
                int newPositionX = i + currentPiece.positionX;
                int newPositionY = j + currentPiece.positionY;

                if (isInsideMatrixBounds(newPositionX, newPositionY))
                    matrix[newPositionX, newPositionY] = piece[i,j];
            }
        }
    }

    void cleanPreviousPiecePosition(){
        BlockColors[,] piece = currentPiece.piece;

        for (int i=0; i < piece.GetLength(0); i++){
            for (int j=0; j < piece.GetLength(1); j++){
                int positionX = i + currentPiece.positionX;
                int positionY = j + currentPiece.positionY;

                if (isInsideMatrixBounds(positionX, positionY))
                    matrix[positionX, positionY] = BlockColors.NoColor;
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

    bool isInsideMatrixBounds(int x, int y){
        if (x < 0)
            return false;

        if (y < 0)
            return false;

        if (x >= NUMBER_OF_COLUMNS)
            return false;

        if (y >= NUMBER_OF_ROWS)
            return false;

        return true;
    }
}
