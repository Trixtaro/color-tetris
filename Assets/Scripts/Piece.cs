﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PieceTypes{
    PieceSquare,
    PieceL,
    PieceS,
    PieceZ,
    PieceLine
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

    public Piece(PieceTypes type, int positionX, int positionY){
        this.piece = createMatrix(type);
        this.positionX = positionX;
        this.positionY = positionY;
    }

    static BlockColors[,] createMatrix(PieceTypes type){
        BlockColors[,] matrix;
        
        switch(type){
            case PieceTypes.PieceSquare:
                matrix = new BlockColors[,]{
                    { BlockColors.Blue, BlockColors.Red },
                    { BlockColors.Yellow, BlockColors.Blue }
                };
                break;
            case PieceTypes.PieceL:
                matrix = new BlockColors[,]{
                    { BlockColors.NoColor, BlockColors.NoColor, BlockColors.NoColor },
                    { BlockColors.Blue, BlockColors.NoColor, BlockColors.NoColor },
                    { BlockColors.Yellow, BlockColors.Blue, BlockColors.Red },
                };
                break;
            case PieceTypes.PieceS:
                matrix = new BlockColors[,]{
                    { BlockColors.NoColor, BlockColors.NoColor, BlockColors.NoColor },
                    { BlockColors.NoColor, BlockColors.Blue, BlockColors.Red },
                    { BlockColors.Yellow, BlockColors.Red, BlockColors.NoColor },
                };
                break;
            case PieceTypes.PieceZ:
                matrix = new BlockColors[,]{
                    { BlockColors.NoColor, BlockColors.NoColor, BlockColors.NoColor },
                    { BlockColors.Blue, BlockColors.Red, BlockColors.NoColor },
                    { BlockColors.NoColor, BlockColors.Yellow, BlockColors.Red },
                };
                break;
            case PieceTypes.PieceLine:
                matrix = new BlockColors[,]{
                    { BlockColors.NoColor, BlockColors.Yellow, BlockColors.NoColor, BlockColors.NoColor },
                    { BlockColors.NoColor, BlockColors.Blue, BlockColors.NoColor, BlockColors.NoColor },
                    { BlockColors.NoColor, BlockColors.Yellow, BlockColors.NoColor, BlockColors.NoColor },
                    { BlockColors.NoColor, BlockColors.Blue, BlockColors.NoColor, BlockColors.NoColor },
                };
                break;
            default: 
                matrix = new BlockColors[,]{
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

    public BlockColors[,] rotatedPiece(bool clockwise){
        int length = piece.GetLength(0);

        BlockColors[,] auxMatrix = new BlockColors[length, length];

        for (int i = 0; i < length; ++i) {
            for (int j = 0; j < length; ++j) {
                auxMatrix[i, j] = piece[
                    clockwise ? (length - j - 1) : j, 
                    !clockwise ? (length - i - 1) : i
                ];
            }
        }

        return auxMatrix;
    }

    public bool rotate(BlockColors[,] board, bool clockwise){
        BlockColors[,] newPiece = rotatedPiece(clockwise);

        bool isTouching = Piece.isTouchingTheBoard(board, newPiece, this.positionX, this.positionY);

        if (isTouching)
            return false;

        piece = newPiece;

        return true;
    }

    public static bool isTouchingTheBoard(BlockColors[,] board, BlockColors[,] piece, int positionX, int positionY){
        
        for (int i = 0; i < piece.GetLength(0); ++i) {
            for (int j = 0; j < piece.GetLength(1); ++j) {
                if (piece[i,j] != BlockColors.NoColor 
                    && board[positionX + i, positionY + j] != BlockColors.NoColor){
                    return true;
                }
            }
        }

        return false;

    }
    
}
