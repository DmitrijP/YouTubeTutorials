using System.Windows.Media;
using System.Windows.Shapes;

namespace SnakeGame.UI.Entities
{
    class SnakeElement : GameEntity
    {
        public SnakeElement(int size)
        {
            UIElement = new Rectangle
            {
                Width = size - 4,
                Height = size - 4,
                Fill = Brushes.Green
            };
        }
        public bool IsHead { get; set; }
    }
}
