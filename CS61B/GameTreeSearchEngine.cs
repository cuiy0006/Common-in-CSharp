using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS61B
{
    public enum Side
    { 
        None,
        Computer,
        Player
    }


    public class GameTreeSearchEngine
    {
        public delegate void GridStatusChangeHandler(int Row, int Colume);

        public event GridStatusChangeHandler OnGridStatusChanged;

        public delegate void GameOverHandler(Side WinnerSide);

        public event GameOverHandler OnGameOver;

        

        private Side _CurrentSide;

        public Side CurrentSide
        {
            get { return _CurrentSide; }
        }


        private Side _BeginningSide;

        public Side BeginningSide
        {
            set { _BeginningSide = value; }
            get { return _BeginningSide; }
        }


        private Side[,] _Grid;

        public Side[,] Grid
        {
            get { return _Grid; }
        }

        public GameTreeSearchEngine()
        {
            ClearGrid();
            _CurrentSide = Side.Player;
            _BeginningSide = Side.Player;
        }


        public Side IsEnd(out bool isEnd)
        {
            if (_Grid[0, 0] == Side.Computer && _Grid[0, 1] == Side.Computer && _Grid[0, 2] == Side.Computer)
            {
                isEnd = true;
                return Side.Computer;
            }
            else if (_Grid[0, 0] == Side.Player && _Grid[0, 1] == Side.Player && _Grid[0, 2] == Side.Player)
            {
                isEnd = true;
                return Side.Player;
            }
            else if (_Grid[1, 0] == Side.Computer && _Grid[1, 1] == Side.Computer && _Grid[1, 2] == Side.Computer)
            {
                isEnd = true;
                return Side.Computer;
            }
            else if (_Grid[1, 0] == Side.Player && _Grid[1, 1] == Side.Player && _Grid[1, 2] == Side.Player)
            {
                isEnd = true;
                return Side.Player;
            }

            else if (_Grid[2, 0] == Side.Computer && _Grid[2, 1] == Side.Computer && _Grid[2, 2] == Side.Computer)
            {
                isEnd = true;
                return Side.Computer;
            }
            else if (_Grid[2, 0] == Side.Player && _Grid[2, 1] == Side.Player && _Grid[2, 2] == Side.Player)
            {
                isEnd = true;
                return Side.Player;
            }

            else if (_Grid[0, 0] == Side.Computer && _Grid[1, 0] == Side.Computer && _Grid[2, 0] == Side.Computer)
            {
                isEnd = true;
                return Side.Computer;
            }
            else if (_Grid[0, 0] == Side.Player && _Grid[1, 0] == Side.Player && _Grid[2, 0] == Side.Player)
            {
                isEnd = true;
                return Side.Player;
            }

            else if (_Grid[0, 1] == Side.Computer && _Grid[1, 1] == Side.Computer && _Grid[2, 1] == Side.Computer)
            {
                isEnd = true;
                return Side.Computer;
            }
            else if (_Grid[0, 1] == Side.Player && _Grid[1, 1] == Side.Player && _Grid[2, 1] == Side.Player)
            {
                isEnd = true;
                return Side.Player;
            }

            else if (_Grid[0, 2] == Side.Computer && _Grid[1, 2] == Side.Computer && _Grid[2, 2] == Side.Computer)
            {
                isEnd = true;
                return Side.Computer;
            }
            else if (_Grid[0, 2] == Side.Player && _Grid[1, 2] == Side.Player && _Grid[2, 2] == Side.Player)
            {
                isEnd = true;
                return Side.Player;
            }

            else if (_Grid[0, 0] == Side.Computer && _Grid[1, 1] == Side.Computer && _Grid[2, 2] == Side.Computer)
            {
                isEnd = true;
                return Side.Computer;
            }
            else if (_Grid[0, 0] == Side.Player && _Grid[1, 1] == Side.Player && _Grid[2, 2] == Side.Player)
            {
                isEnd = true;
                return Side.Player;
            }
            else if (_Grid[0, 2] == Side.Computer && _Grid[1, 1] == Side.Computer && _Grid[2, 0] == Side.Computer)
            {
                isEnd = true;
                return Side.Computer;
            }
            else if (_Grid[0, 2] == Side.Player && _Grid[1, 1] == Side.Player && _Grid[2, 0] == Side.Player)
            {
                isEnd = true;
                return Side.Player;
            }
            else
            {
                isEnd = true;
                foreach (Side Element in _Grid)
                {
                    if (Element == Side.None)
                        isEnd = false;
                }

                return Side.None;
            }
        }

        public void ClearGrid()
        {
            _Grid = new Side[3, 3]
                {
                    {Side.None,Side.None,Side.None},
                    {Side.None,Side.None,Side.None},
                    {Side.None,Side.None,Side.None}
                };
        }

        public void Start()
        {
            if (_BeginningSide == Side.Computer)
            {
                Best best = this.ChooseMove(Side.Computer, -1, 1);
                _Grid[best.move.Row, best.move.Column] = Side.Computer;
                if (OnGridStatusChanged != null)
                    OnGridStatusChanged(best.move.Row, best.move.Column);

                bool isEnd;
                Side WinnerSide = IsEnd(out isEnd);
                if (isEnd && OnGameOver != null)
                    OnGameOver(WinnerSide);
            }
            else
            {
                _BeginningSide = Side.Computer;
            }
        }



        private Best ChooseMove(Side side, int alpha, int beta)
        {
            if (side == Side.None)
                throw new Exception();

            Best myBest = new Best();
            if (side == Side.Computer)
                myBest.Score = alpha;
            else
                myBest.Score = beta;
            Best reply;

            bool isEnd;
            Side WinnerSide = IsEnd(out isEnd);

            if (isEnd)
            {
                if (WinnerSide == Side.None)
                    myBest.Score = 0;
                else if (WinnerSide == Side.Computer)
                    myBest.Score = 1;
                else
                    myBest.Score = -1;
                return myBest;
            }

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (_Grid[i, j] == Side.None)
                    {
                        _Grid[i, j] = side;
                        Side TheOtherSide;
                        if (side == Side.Computer)
                            TheOtherSide = Side.Player;
                        else
                            TheOtherSide = Side.Computer;

                        reply = ChooseMove(TheOtherSide, alpha, beta);

                        _Grid[i, j] = Side.None;

                        if (side == Side.Computer && reply.Score > myBest.Score)
                        {
                            myBest.move.Row = i;
                            myBest.move.Column = j;
                            myBest.Score = reply.Score;
                            alpha = reply.Score;
                        }
                        else if (side == Side.Player && reply.Score < myBest.Score)
                        {

                            myBest.move.Row = i;
                            myBest.move.Column = j;
                            myBest.Score = reply.Score;
                            beta = reply.Score;
                        }

                        if (alpha > beta)
                            return myBest;
                    }
                }
            }
            return myBest;
        }


        private class Best
        {
            private int _Score;
            public int Score
            {
                set { _Score = value; }
                get { return _Score; }
            }

            public Move move;
        }

        private struct Move
        {
            public int Row;
            public int Column;
        }
    }
}
