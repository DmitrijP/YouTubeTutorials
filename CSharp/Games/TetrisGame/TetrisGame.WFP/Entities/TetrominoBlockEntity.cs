using System.Windows.Media;
using System.Windows.Shapes;

namespace TetrisGame.WFP.Entities
{
    class TetrominoBlockEntity  : GameEntity
    {
        public TetrominoBlockEntity()
        {
            UI = new Rectangle()
            {
                Width = 35,
                Height = 35,
                Fill = Brushes.Green
            };
        }
    }
}
