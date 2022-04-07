using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Directions{
    Left,
    Right,
    Up,
    Down
}

public class TetrisManager : MonoBehaviour
{

    public const int NUMBER_OF_COLUMNS = 10;
    public const int NUMBER_OF_ROWS = 15;

    public float distanceBetweenBlocks = 0.1f;

    public Sprite blueBlock;
    public Sprite redBlock;
    public Sprite yellowBlock;
    public Sprite noBlock;

    public GameObject blockPrefab;

    private BlockColors[,] matrix;
    private GameObject[,] matrixBlocks;
    private Piece currentPiece;
    private PieceFactory factory;
    public int pieceInitialPositionX = 2;
    public int pieceInitialPositionY = 2;
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;

        this.matrix = new BlockColors[NUMBER_OF_COLUMNS,NUMBER_OF_ROWS];
        this.matrixBlocks = new GameObject[NUMBER_OF_COLUMNS,NUMBER_OF_ROWS];

        this.factory = new PieceFactory(this.pieceInitialPositionX, this.pieceInitialPositionY);
        this.factory.setMode(PieceFactoryMode.Random);
        this.currentPiece = this.factory.generate();

        createBlocks();
        paintBlocks();

        InvokeRepeating("GameStep", 1f, 1f);
    }

    void GameStep(){
        if (!isPieceTouchingSide(Directions.Down)){
            cleanPreviousPiecePosition();
            currentPiece.moveDown();
            paintBlocks();
        } else {
            Piece newPiece = this.factory.generate();

            if (Piece.isTouchingTheBoard(matrix, newPiece.piece, this.pieceInitialPositionX, this.pieceInitialPositionY)){
                Debug.Log("Game Over");
                cleanBoard();
            } else {
                this.currentPiece = newPiece;
            }

            paintBlocks();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl)){
            cleanPreviousPiecePosition();
            currentPiece.rotate(matrix, false);
            paintBlocks();
        }

        if (Input.GetKeyDown(KeyCode.Space)){
            cleanPreviousPiecePosition();
            currentPiece.rotate(matrix, true);
            paintBlocks();
        }

        if (Input.GetKeyDown(KeyCode.DownArrow) && !isPieceTouchingSide(Directions.Down)){
            cleanPreviousPiecePosition();
            currentPiece.moveDown();
            paintBlocks();
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) && !isPieceTouchingSide(Directions.Up)){
            cleanPreviousPiecePosition();
            currentPiece.moveUp();
            paintBlocks();
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow) && !isPieceTouchingSide(Directions.Left)){
            cleanPreviousPiecePosition();
            currentPiece.moveLeft();
            paintBlocks();
        }

        if (Input.GetKeyDown(KeyCode.RightArrow) && !isPieceTouchingSide(Directions.Right)){
            cleanPreviousPiecePosition();
            currentPiece.moveRight();
            paintBlocks();
        }
    }

    void createBlocks(){

        for (int i=0; i<NUMBER_OF_COLUMNS; i++){
            for (int j=0; j<NUMBER_OF_ROWS; j++){
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

        for (int i=0; i<NUMBER_OF_COLUMNS; i++){
            for (int j=0; j<NUMBER_OF_ROWS; j++){
                if(matrixBlocks[i,j].GetComponent<Block>().currentState != matrix[i,j]){
                    matrixBlocks[i,j].GetComponent<Block>().currentState = matrix[i,j];
                    matrixBlocks[i,j].GetComponent<SpriteRenderer>().sprite = getColorOfBlock(matrix[i,j]);
                }
            }
        }
    }

    void cleanBoard(){
        this.matrix = new BlockColors[NUMBER_OF_COLUMNS,NUMBER_OF_ROWS];
    }

    void paintCurrentPiece(){

        BlockColors[,] piece = currentPiece.piece;

        for (int i=0; i < piece.GetLength(0); i++){
            for (int j=0; j < piece.GetLength(1); j++){
                int newPositionX = i + currentPiece.positionX;
                int newPositionY = j + currentPiece.positionY;

                if (isInsideMatrixBounds(newPositionX, newPositionY) && piece[i,j] != BlockColors.NoColor)
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

                if (isInsideMatrixBounds(positionX, positionY)  && piece[i,j] != BlockColors.NoColor)
                    matrix[positionX, positionY] = BlockColors.NoColor;
            }
        }
    }

    void changeSomeBlocks(){
        int randomValueX = Random.Range(0, NUMBER_OF_ROWS);
        int randomValueY = Random.Range(0, NUMBER_OF_COLUMNS);

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

    bool isPieceTouchingSide(Directions direction){

        for(int i = 0; i < currentPiece.piece.GetLength(1); i++){
            for (int j = 0; j < currentPiece.piece.GetLength(1); j++){

                int newPositionX; 
                int newPositionY; 
                bool isNoBlockInPiece;
                
                switch(direction){
                    case Directions.Up:
                        newPositionX = i + currentPiece.positionX;
                        newPositionY = currentPiece.piece.GetLength(1) + currentPiece.positionY - j;
                        isNoBlockInPiece = currentPiece.piece[i,currentPiece.piece.GetLength(1)-1-j] != BlockColors.NoColor;
                        break;
                    case Directions.Down:
                        newPositionX = i + currentPiece.positionX;
                        newPositionY = currentPiece.positionY - 1 + j;
                        isNoBlockInPiece = currentPiece.piece[i,j] != BlockColors.NoColor;
                        break;
                    case Directions.Left:
                        newPositionX = currentPiece.positionX - 1 + j;
                        newPositionY = i + currentPiece.positionY;
                        isNoBlockInPiece = currentPiece.piece[j,i] != BlockColors.NoColor;
                        break;
                    case Directions.Right:
                        newPositionX =  currentPiece.piece.GetLength(1) + currentPiece.positionX - j;
                        newPositionY = i + currentPiece.positionY;
                        isNoBlockInPiece = currentPiece.piece[currentPiece.piece.GetLength(1)-1-j,i] != BlockColors.NoColor;
                        break;
                    default:
                        newPositionX = currentPiece.positionX;
                        newPositionY = currentPiece.positionY;
                        isNoBlockInPiece = false;
                        break;
                }

                if (isNoBlockInPiece){
                    if (!isInsideMatrixBounds(newPositionX, newPositionY)){
                        return true;
                    }

                    if (matrix[newPositionX, newPositionY] != BlockColors.NoColor){
                        return true;
                    }

                    break;
                }
            }
        }

        return false;
    }

    void removeHorizontalLines(int quantityOfLines, int positionOfBottomLine) {

        for (int i = positionOfBottomLine; i < quantityOfLines; i++){
            for (int j = 0; j < NUMBER_OF_COLUMNS; j++){
                matrix[j,positionOfBottomLine + i] = BlockColors.NoColor;
            }
        }

        for (int i = positionOfBottomLine; i < NUMBER_OF_ROWS - quantityOfLines - 1; i++){
            for (int j = 0; j < NUMBER_OF_COLUMNS; j++){
                matrix[j,i] = matrix[j,positionOfBottomLine + i + quantityOfLines];
            }
        }

    }
}
