using System.Windows.Media;
using System.Windows.Shapes;

namespace SnakeGame.UI.Entities
{
    class Apple : GameEntity
    {
        public Apple(int size)
        {
            Rectangle rect = new Rectangle
            {
                Width = size - 4,
                Height = size - 4,
                Fill = Brushes.Red ,
                RadiusX = 10,
                RadiusY = 10
            };
            UIElement = rect;
        }

        public override bool Equals(object obj)
        {
            if (obj is Apple apple)
                return X == apple.X && Y == apple.Y;
            else
                return false;
        }

        public override int GetHashCode() => base.GetHashCode();
        public override string ToString() => base.ToString();
    }
}
