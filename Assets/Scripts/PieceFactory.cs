using System;
public enum PieceFactoryMode{
    OnlyColorRed,
    OnlyPieceT,
    OnlyPieceL,
    OnlySquare,
    Random
}

public class PieceFactory : Factory<Piece>
{
    public PieceFactoryMode mode;
    public int initialPositionX = 0;
    public int initialPositionY = 0;

    public PieceFactory(int initialPositionX, int initialPositionY){
        this.mode = PieceFactoryMode.Random;
        this.initialPositionX = initialPositionX;
        this.initialPositionY = initialPositionY;
    }

    public Piece generate(){
        switch(this.mode){
            case PieceFactoryMode.OnlyColorRed:
                return this.generateOneColorPiece(BlockColors.Red);
            case PieceFactoryMode.OnlySquare:
                return this.generateOnePiece(PieceTypes.PieceSquare);
            case PieceFactoryMode.OnlyPieceL:
                return this.generateOnePiece(PieceTypes.PieceL);
            case PieceFactoryMode.OnlyPieceT:
                return this.generateOnePiece(PieceTypes.PieceT);
            case PieceFactoryMode.Random:
                return this.generateRandomPiece();
            default:
                return this.generateRandomPiece();
        }
    }

    public void setMode(PieceFactoryMode mode){
        this.mode = mode;
    }

    private Piece generateRandomPiece(){
        int numberOfPieceTypes = Enum.GetValues(typeof(PieceTypes)).Length;
        PieceTypes typeOfPiece = (PieceTypes) UnityEngine.Random.Range(0, numberOfPieceTypes + 1);
        
        Piece newPiece = new Piece(
            typeOfPiece, 
            this.initialPositionX, 
            this.initialPositionY
        );
        newPiece.randomizeColor();
        return newPiece;
    }

    private Piece generateOnePiece(PieceTypes typeOfPiece){
        Piece newPiece = new Piece(
            typeOfPiece, 
            this.initialPositionX, 
            this.initialPositionY
        );
        newPiece.randomizeColor();

        return newPiece;
    }

    private Piece generateOneColorPiece(BlockColors color){
        int numberOfPieceTypes = Enum.GetValues(typeof(PieceTypes)).Length;
        PieceTypes typeOfPiece = (PieceTypes) UnityEngine.Random.Range(0, numberOfPieceTypes + 1);

        Piece newPiece = new Piece(
            typeOfPiece, 
            this.initialPositionX, 
            this.initialPositionY
        );
        newPiece.setColor(color);

        return newPiece;
    }

}
