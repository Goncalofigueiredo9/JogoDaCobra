using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JogoDaCobra
{
    public partial class Form1 : Form
    {
        private List<Circle> Snake = new List<Circle>(); // lista para a cobra
        private Circle food = new Circle(); // class circle chamada comida
        public Form1()
        {
            InitializeComponent();

            new Settings(); // associar a classe settings aqui

            gameTimer.Interval = 1000 / Settings.Speed; // mudar o game time para a classe Settings speed
            gameTimer.Tick += updateScreen;
            gameTimer.Start();

            StartGame();
        }

        private void updateScreen(object sender, EventArgs e)
        {
            if (Settings.GameOver)
            {
                if (input.KeyPress(Keys.Enter))
                {
                    StartGame();
                }
            }
            else
            {
                if (input.KeyPress(Keys.Right) && Settings.direction != Directions.Left)
                {
                    Settings.direction = Directions.Right;
                }
                else if (input.KeyPress(Keys.Left) && Settings.direction != Directions.Right)
                {
                    Settings.direction = Directions.Left;
                }
                else if (input.KeyPress(Keys.Up) && Settings.direction != Directions.Down)
                {
                    Settings.direction = Directions.Up;
                }
                else if (input.KeyPress(Keys.Down) && Settings.direction != Directions.Up)
                {
                    Settings.direction = Directions.Down;
                }

                movePlayer();
            }
            pbCanvas.Invalidate();
        }

        private void movePlayer()
        {
            for (int i = Snake.Count - 1; i >= 0; i--)
            {
                if (i == 0)
                {
                    switch (Settings.direction)
                    {
                        case Directions.Right:
                            Snake[i].X++;
                            break;
                        case Directions.Left:
                            Snake[i].X--;
                            break;
                        case Directions.Up:
                            Snake[i].Y--;
                            break;
                        case Directions.Down:
                            Snake[i].Y++;
                            break;
                    }
                    int maxXpos = pbCanvas.Size.Width / Settings.Widht;
                    int maxYpos = pbCanvas.Size.Height / Settings.Height;

                    if (Snake[i].X < 0 || Snake[i].Y < 0
                        || Snake[i].X > maxXpos || Snake[i].Y > maxYpos)
                    {
                        die();
                    }
                    for (int j = 1; j < Snake.Count; j++)
                    {
                        if (Snake[i].X == Snake[j].X && Snake[i].Y == Snake[j].Y)
                        {
                            die();
                        }
                    }
                    if (Snake[0].X == food.X && Snake[0].Y == food.Y)
                    {
                        eat();
                    }
                }
                else
                {
                    Snake[i].X = Snake[i - 1].X;
                    Snake[i].Y = Snake[i - 1].Y;
                }
            }
        }

        private void updateGraphics(object sender, PaintEventArgs e)
        {
            Graphics canvas = e.Graphics;
            if (!Settings.GameOver)
            {
                for (int i = 0; i < Snake.Count; i++)
                {
                    Brush snakeColour;
                    if (i == 0)
                    {
                        snakeColour = Brushes.Black;
                    }
                    else
                    {
                        snakeColour = Brushes.Silver;
                    }
                    canvas.FillEllipse(snakeColour,
                        new Rectangle(Snake[i].X * Settings.Widht,
                                      Snake[i].Y * Settings.Height,
                                      Settings.Widht, Settings.Height));
                    canvas.FillEllipse(Brushes.Red,
                        new Rectangle(food.X * Settings.Widht,
                            food.Y * Settings.Height, Settings.Widht, Settings.Height));
                }
            }
            else
            {
                string gameOver = "Game Over \n" + "Resultado final é " + Settings.Score + "\nPressiona Enter para Reiniciar \n";
                label3.Text = gameOver;
                label3.Visible = true;
            }
        }

        private void StartGame()
        {
            label3.Visible = false;
            new Settings();
            Snake.Clear();
            Circle head = new Circle();
            head.X = 10;
            head.Y = 5;
            Snake.Add(head);
            label1.Text = Settings.Score.ToString();
            GenerateFood();
        }
        private void GenerateFood()
        {
            int maxXPos = pbCanvas.Size.Width / Settings.Widht;
            int maxYPos = pbCanvas.Size.Height / Settings.Height;

            Random random = new Random();
            food = new Circle { X = random.Next(0, maxXPos), Y = random.Next(0, maxYPos) };
        }

        private void eat()
        {
            Circle circle = new Circle
            {
                X = Snake[Snake.Count - 1].X,
                Y = Snake[Snake.Count - 1].Y
            };
            Snake.Add(circle);

            Settings.Score += Settings.Points;
            label1.Text = Settings.Score.ToString();

            GenerateFood();
        }

        private void die()
        {
            Settings.GameOver = true;
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {

            input.changeState(e.KeyCode, true);
            if (gameTimer.Enabled == true) { if (e.KeyCode == Keys.Space) gameTimer.Enabled = false;}
            else gameTimer.Enabled = true;
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            input.changeState(e.KeyCode, false);
        }

        private void ajuda(object sender, EventArgs e)
        {
            Help.ShowHelp(this, "C:\\Users\\GoncaloF\\source\\repos\\Goncalofigueiredo9\\JogoDaCobra\\JogoDaCobra\\Resources\\ajuda.chm");
        }
    }
}
