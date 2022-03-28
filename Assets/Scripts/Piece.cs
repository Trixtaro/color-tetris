using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Pieces{
    PieceSquare
}

public enum BlockColors {
    NoColor = 0,
    Yellow = 1,
    Blue = 2,
    Red = 3
}

public class Piece
{
    public BlockColors[,] piece;
    public int positionX;
    public int positionY;

    public Piece(Pieces type, int positionX, int positionY){
        this.piece = createMatrix(type);
        this.positionX = positionX;
        this.positionY = positionY;
    }

    static BlockColors[,] createMatrix(Pieces type){
        BlockColors[,] matrix;
        
        switch(type){
            default: matrix = new BlockColors[,]{
                { BlockColors.Blue, BlockColors.Red },
                { BlockColors.Yellow, BlockColors.Blue },
            };
                
            break;
        }
        
        return matrix;
    }

    public void moveDown(){
        this.positionY--;
    }
    public void moveUp(){
        this.positionY++;
    }

    public void moveLeft(){
        this.positionX--;
    }

    public void moveRight(){
        this.positionX++;
    }

    public bool rotateClockWise(){
        int length = piece.GetLength(0);

        BlockColors[,] auxMatrix = new BlockColors[length, length];

        for (int i = 0; i < length; ++i) {
            for (int j = 0; j < length; ++j) {
                auxMatrix[i, j] = piece[length - j - 1, i];
            }
        }

        piece = auxMatrix;

        return true;
    }

    public bool rotateCounterClockWise(){
        int length = piece.GetLength(0);

        BlockColors[,] auxMatrix = new BlockColors[length, length];

        for (int i = 0; i < length; ++i) {
            for (int j = 0; j < length; ++j) {
                auxMatrix[i, j] = piece[j,length - i - 1];
            }
        }

        piece = auxMatrix;

        return true;
    }

}
