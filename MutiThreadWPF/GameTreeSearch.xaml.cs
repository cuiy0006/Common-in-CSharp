using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using CS61B;

namespace MutiThreadWPF
{
    /// <summary>
    /// Interaction logic for GameTreeSearch.xaml
    /// </summary>
    public partial class GameTreeSearch : Window
    {
        private GameTreeSearchEngine GameEngine;

        public GameTreeSearch()
        {
            InitializeComponent();

            GameEngine = new GameTreeSearchEngine();

            GameEngine.OnGridStatusChanged += ChangeButtonContentWhenGridChanged;

            GameEngine.OnGameOver += GameOver;

            AllButtonEnable(false);
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        #region Click
        private void B00_Click(object sender, RoutedEventArgs e)
        {
            ButtonBehavior(sender, 0, 0);
        }

        private void B01_Click(object sender, RoutedEventArgs e)
        {
            ButtonBehavior(sender, 0, 1);
        }

        private void B02_Click(object sender, RoutedEventArgs e)
        {
            ButtonBehavior(sender, 0, 2);
        }

        private void B10_Click(object sender, RoutedEventArgs e)
        {
            ButtonBehavior(sender, 1, 0);
        }

        private void B11_Click(object sender, RoutedEventArgs e)
        {
            ButtonBehavior(sender, 1, 1);
        }

        private void B12_Click(object sender, RoutedEventArgs e)
        {
            ButtonBehavior(sender, 1, 2);
        }

        private void B20_Click(object sender, RoutedEventArgs e)
        {
            ButtonBehavior(sender, 2, 0);
        }

        private void B21_Click(object sender, RoutedEventArgs e)
        {
            ButtonBehavior(sender, 2, 1);
        }

        private void B22_Click(object sender, RoutedEventArgs e)
        {
            ButtonBehavior(sender, 2, 2);
        }

        #endregion

        private bool IsComputerTurn(Side side)
        {
            if (side == Side.Computer)
                return true;
            else
                return false;
        }


        private void CheckBox_Click(object sender, RoutedEventArgs e)
        {
            //CheckBox CB = sender as CheckBox;
            //if (CB != null)
            //{
            //    if ((bool)CB.IsChecked)
            //        GameEngine.BeginningSide = Side.Computer;
            //    else
            //        GameEngine.BeginningSide = Side.Player;
            //}
        }


        private void ButtonBehavior(object sender, int Row, int Column)
        {
            if (!IsComputerTurn(GameEngine.CurrentSide))
            {
                Button bt = sender as Button;
                if (bt != null)
                {
                    bt.Content = "O";
                    bt.IsEnabled = false;
                    GameEngine.Grid[Row, Column] = Side.Player;
                    bool isEnd;
                    GameEngine.IsEnd(out isEnd);
                    if (isEnd)
                        AllButtonEnable(false);
                    else
                        GameEngine.Start();
                }
            }
        }


        private void GameOver(Side WinnerSide)
        {
            AllButtonEnable(false);
        }
        private void AllButtonEnable(bool Enable)
        {
            B00.IsEnabled = Enable;
            B01.IsEnabled = Enable;
            B02.IsEnabled = Enable;
            B10.IsEnabled = Enable;
            B11.IsEnabled = Enable;
            B12.IsEnabled = Enable;
            B20.IsEnabled = Enable;
            B21.IsEnabled = Enable;
            B22.IsEnabled = Enable;

            if (Enable)
            {
                B00.Content = "";
                B01.Content = "";
                B02.Content = "";
                B10.Content = "";
                B11.Content = "";
                B12.Content = "";
                B20.Content = "";
                B21.Content = "";
                B22.Content = "";
            }
        }

        private void ChangeButtonContentWhenGridChanged(int Row, int Column)
        {
            if (Row == 0)
            {
                if (Column == 0)
                {
                    B00.Content = "X";
                    B00.IsEnabled = false;
                }
                else if (Column == 1)
                {
                    B01.Content = "X";
                    B01.IsEnabled = false;
                }
                else if (Column == 2)
                {
                    B02.Content = "X";
                    B02.IsEnabled = false;
                }
            }
            else if (Row == 1)
            {
                if (Column == 0)
                {
                    B10.Content = "X";
                    B10.IsEnabled = false;
                }
                else if (Column == 1)
                {
                    B11.Content = "X";
                    B11.IsEnabled = false;
                }
                else if (Column == 2)
                {
                    B12.Content = "X";
                    B12.IsEnabled = false;
                }
            }
            else if (Row == 2)
            {
                if (Column == 0)
                {
                    B20.Content = "X";
                    B20.IsEnabled = false;
                }
                else if (Column == 1)
                {
                    B21.Content = "X";
                    B21.IsEnabled = false;
                }
                else if (Column == 2)
                {
                    B22.Content = "X";
                    B22.IsEnabled = false;
                }
            }
        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            AllButtonEnable(true);
            if ((bool)WhoFirst.IsChecked)
                GameEngine.BeginningSide = Side.Computer;
            else
                GameEngine.BeginningSide = Side.Player;
            GameEngine.Start();
            Start.IsEnabled = false;
            Clear.IsEnabled = true;
        }
        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            GameEngine.ClearGrid();
            AllButtonEnable(true);
            if ((bool)WhoFirst.IsChecked)
                GameEngine.BeginningSide = Side.Computer;
            else
                GameEngine.BeginningSide = Side.Player;
            GameEngine.Start();
        }
    }
}
