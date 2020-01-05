using System;
using System.Collections.Generic;
using System.Text;

namespace TetrisGame.WFP.Entities
{
    class TetrominoEntity
    {

        public TetrominoEntity(char type, int originX, int originY)
        {
            OriginX = originX; 
            OriginY = originY;
            if (type == 'L')
            {
                MatrixSize = 3;
                Blocks = new TetrominoBlockEntity[MatrixSize, MatrixSize];
                Blocks[0, 1] = new TetrominoBlockEntity();
                Blocks[1, 1] = new TetrominoBlockEntity();
                Blocks[2, 1] = new TetrominoBlockEntity();
                Blocks[2, 2] = new TetrominoBlockEntity();
            }
            if (type == 'I')
            {
                MatrixSize = 4;
                Blocks = new TetrominoBlockEntity[MatrixSize,MatrixSize];
                Blocks[1, 0] = new TetrominoBlockEntity();
                Blocks[1, 1] = new TetrominoBlockEntity();
                Blocks[1, 2] = new TetrominoBlockEntity();
                Blocks[1, 3] = new TetrominoBlockEntity();
            }
            if (type == 'O')
            {
                MatrixSize = 4;
                Blocks = new TetrominoBlockEntity[MatrixSize,MatrixSize];
                Blocks[1, 1] = new TetrominoBlockEntity();
                Blocks[2, 2] = new TetrominoBlockEntity();
                Blocks[2, 1] = new TetrominoBlockEntity();
                Blocks[1, 2] = new TetrominoBlockEntity();
            }
            if (type == 'T')
            {
                MatrixSize = 3;
                Blocks = new TetrominoBlockEntity[MatrixSize,MatrixSize];
                Blocks[1, 0] = new TetrominoBlockEntity();
                Blocks[1, 1] = new TetrominoBlockEntity();
                Blocks[1, 2] = new TetrominoBlockEntity();
                Blocks[2, 1] = new TetrominoBlockEntity();
            }
            if (type == 'J')
            {
                MatrixSize = 3;
                Blocks = new TetrominoBlockEntity[MatrixSize,MatrixSize];
                Blocks[0, 1] = new TetrominoBlockEntity();
                Blocks[1, 1] = new TetrominoBlockEntity();
                Blocks[2, 1] = new TetrominoBlockEntity();
                Blocks[2, 0] = new TetrominoBlockEntity();
            }
            if (type == 'S')
            {
                MatrixSize = 3;
                Blocks = new TetrominoBlockEntity[MatrixSize,MatrixSize];
                Blocks[0, 0] = new TetrominoBlockEntity();
                Blocks[1, 0] = new TetrominoBlockEntity();
                Blocks[1, 1] = new TetrominoBlockEntity();
                Blocks[2, 1] = new TetrominoBlockEntity();
            }
            if (type == 'Z')
            {
                MatrixSize = 3;
                Blocks = new TetrominoBlockEntity[MatrixSize,MatrixSize];
                Blocks[0, 1] = new TetrominoBlockEntity();
                Blocks[0, 2] = new TetrominoBlockEntity();
                Blocks[1, 0] = new TetrominoBlockEntity();
                Blocks[1, 1] = new TetrominoBlockEntity();
            }
        }

        public TetrominoBlockEntity[,] Blocks { get; set; }

        public int MatrixSize { get; set; }
        public int OriginX { get; set; }
        public int OriginY { get; set; }
        public void Rotate90Right()
        {
            var temp = new TetrominoBlockEntity[MatrixSize, MatrixSize];
            for (int i = 0; i < MatrixSize; i++)
                for (int j = 0; j < MatrixSize; j++)
                    temp[i, j] = Blocks[MatrixSize - j - 1, i];
            Blocks = temp;
        }

        public void Rotate90Left()
        {
            var temp = new TetrominoBlockEntity[MatrixSize, MatrixSize];
            for (int i = 0; i < MatrixSize; i++)
                for (int j = 0; j < MatrixSize; j++)
                    temp[i, j] = Blocks[j, MatrixSize - i - 1];
            Blocks = temp;
        }

    }
}
