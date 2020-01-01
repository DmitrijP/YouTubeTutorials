using System;
using SnakeGame.UI.Entities;
using System.Windows.Threading;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows.Controls;

namespace SnakeGame.UI
{
    class GameWorld
    {
        private MainWindow mainWindow;
        public int ElementSize { get; private set; }
        public int ColumnCount { get; private set; }
        public int RowCount { get; private set; }
        public double GameAreaWidth { get; private set; }
        public double GameAreaHeight { get; private set; }
        Random _randoTron;

        public Apple Apple { get; set; }
        public Snake Snake { get; set; }
        DispatcherTimer _gameLoopTimer;
        public bool IsRunning { get; set; }
        public GameWorld(MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;
            _randoTron = new Random(DateTime.Now.Millisecond / DateTime.Now.Second);
        }

        public void InitializeGame(int difficulty, int elementSize)
        {
            ElementSize = elementSize;
            GameAreaWidth = mainWindow.GameWorld.ActualWidth;
            GameAreaHeight = mainWindow.GameWorld.ActualHeight;
            ColumnCount = (int)GameAreaWidth / ElementSize;
            RowCount = (int)GameAreaHeight / ElementSize;

            DrawGameWorld();
            InitializeSnake();
            InitializeTimer(difficulty);
            IsRunning = true;
        }

        private void InitializeTimer(int difficulty)
        {
             var interval = TimeSpan.FromSeconds(0.1 + .9 / difficulty).Ticks;
            _gameLoopTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromTicks(interval)
            };
            _gameLoopTimer.Tick += MainGameLoop;
            _gameLoopTimer.Start();
        }
        private void InitializeSnake()
        {
            Snake = new Snake(ElementSize);
            Snake.PositionFirstElement(ColumnCount, RowCount, MovementDirection.Right);
        }

        private void MainGameLoop(object sender, EventArgs e)
        {
            Snake.MoveSnake();
            CheckCollision();
            CreateApple();
            Draw();
        }

        private void Draw()
        {
            DrawSnake();
            DrawApple();
        }

        private void DrawGameWorld()
        {
            int i = 0;
            for (; i < RowCount; i++)
                mainWindow.GameWorld.Children.Add(GenerateHorizontalWorldLine(i));
            int j = 0;
            for (; j < ColumnCount; j++)
                mainWindow.GameWorld.Children.Add(GenerateVerticalWorldLine(j));
            mainWindow.GameWorld.Children.Add(GenerateVerticalWorldLine(j));
            mainWindow.GameWorld.Children.Add(GenerateHorizontalWorldLine(i));
        }
        private void DrawSnake()
        {
            foreach (var snakeElement in Snake.Elements)
            {
                if (!mainWindow.GameWorld.Children.Contains(snakeElement.UIElement))
                    mainWindow.GameWorld.Children.Add(snakeElement.UIElement);
                Canvas.SetLeft(snakeElement.UIElement, snakeElement.X + 2);
                Canvas.SetTop(snakeElement.UIElement, snakeElement.Y + 2);
            }
        }

        private void DrawApple()
        {
            if (!mainWindow.GameWorld.Children.Contains(Apple.UIElement))
                mainWindow.GameWorld.Children.Add(Apple.UIElement);
            Canvas.SetLeft(Apple.UIElement, Apple.X + 2);
            Canvas.SetTop(Apple.UIElement, Apple.Y + 2);
        }
        private Line GenerateVerticalWorldLine(int j)
        {
            return new Line
            {
                Stroke = Brushes.Black,
                X1 = j * ElementSize,
                Y1 = 0,
                X2 = j * ElementSize,
                Y2 = ElementSize * RowCount
            };
        }

        private Line GenerateHorizontalWorldLine(int i)
        {
            return new Line
            {
                Stroke = Brushes.Black,
                X1 = 0,
                Y1 = i * ElementSize,
                X2 = ElementSize * ColumnCount,
                Y2 = i * ElementSize
            };
        }

        private void CheckCollision()
        {
            if(CollisionWithApple())
                ProcessCollisionWithApple();
            if(Snake.CollisionWithSelf() || CollisionWithWorldBounds())
            {
                mainWindow.GameOver();
                StopGame();
            }
        }

        private bool CollisionWithApple()
        {
            if (Apple == null || Snake == null || Snake.Head == null)
                return false;
            SnakeElement head = Snake.Head;
            return (head.X == Apple.X && head.Y == Apple.Y);
        }

        private void ProcessCollisionWithApple()
        {
            mainWindow.IncrementScore();
            mainWindow.GameWorld.Children.Remove(Apple.UIElement);
            Apple = null;
            Snake.Grow();
            IncreaseGameSpeed();
        }

        private void CreateApple()
        {
            if (Apple != null)
                return;
            Apple = new Apple(ElementSize)
            {
                X = _randoTron.Next(0, ColumnCount) * ElementSize,
                Y = _randoTron.Next(0, RowCount) * ElementSize
            };
        }
        private bool CollisionWithWorldBounds()
        {
            if (Snake == null || Snake.Head == null)
                return false;
            var snakeHead = Snake.Head;
            return (snakeHead.X > GameAreaWidth - ElementSize ||
                snakeHead.Y > GameAreaHeight - ElementSize ||
                snakeHead.X < 0 || snakeHead.Y < 0);
        }

        public void StopGame()
        {
            _gameLoopTimer.Stop();
            _gameLoopTimer.Tick -= MainGameLoop;
            IsRunning = false;
        }

        private void IncreaseGameSpeed()
        {
            var part = _gameLoopTimer.Interval.Ticks / 10;
            _gameLoopTimer.Interval = TimeSpan.FromTicks(_gameLoopTimer.Interval.Ticks - part);
        }

        public void PauseGame()
        {
            _gameLoopTimer.Stop();
            IsRunning = false;
        }
        public void ContinueGame()
        {
            _gameLoopTimer.Start();
            IsRunning = true;
        }

        internal void UpdateMovementDirection(MovementDirection movementDirection)
        {
            if (Snake != null)
                Snake.UpdateMovementDirection(movementDirection);
        }
    }
}
