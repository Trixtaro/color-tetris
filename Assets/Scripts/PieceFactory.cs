using System;
public enum PieceFactoryMode{
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
        
        return new Piece(
            typeOfPiece, 
            this.initialPositionX, 
            this.initialPositionY
        );
    }

}
