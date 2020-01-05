using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TetrisGame.WFP.Entities;

namespace TetrisGame.WFP
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        TetrominoEntity t = new TetrominoEntity('J', 0, 0);
        TetrominoEntity t1 = new TetrominoEntity('I', 200, 0);
        TetrominoEntity t2 = new TetrominoEntity('L', 400, 0);
        TetrominoEntity t3 = new TetrominoEntity('O', 600, 0);
        TetrominoEntity t4 = new TetrominoEntity('S', 0, 200);
        TetrominoEntity t5 = new TetrominoEntity('T', 200, 200);
        TetrominoEntity t6 = new TetrominoEntity('Z', 400, 200);

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            GameSpace.Children.Clear();
            if(e.Key == Key.A)
            {
                t.Rotate90Left();
                t1.Rotate90Left();
                t2.Rotate90Left();
                t3.Rotate90Left();
                t4.Rotate90Left();
                t5.Rotate90Left();
                t6.Rotate90Left();
            }

            if (e.Key == Key.D)
            {
                t.Rotate90Right();
                t1.Rotate90Right();
                t2.Rotate90Right();
                t3.Rotate90Right();
                t4.Rotate90Right();
                t5.Rotate90Right();
                t6.Rotate90Right();
            }

            Draw(t);
            Draw(t1);
            Draw(t2);
            Draw(t3);
            Draw(t4);
            Draw(t5);
            Draw(t6);
        }

        void Draw(TetrominoEntity t)
        {
            for (int i = 0; i < t.MatrixSize; i++)
            {
                for (int j = 0; j < t.MatrixSize; j++)
                {
                    if (t.Blocks[i, j] == null)
                        continue;
                    var el = t.Blocks[i, j].UI;
                    GameSpace.Children.Add(el);
                    Canvas.SetLeft(el, t.OriginX + i * 40);
                    Canvas.SetTop(el, t.OriginY + j * 40);
                }
            }
        }
    }
}
