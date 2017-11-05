using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS61B.PROJ2
{
    public struct Move
    {
        private int _Row;
        public int Row
        {
            set { _Row = value; }
            get { return _Row; }
        }
        private int _Column;
        public int Column
        {
            set { _Column = value; }
            get { return _Column; }
        }

        private int _BestScore;
        public int BestScore
        {
            get { return _BestScore; }
            set { _BestScore = value; }
        }
    }


    public class Cell
    {
        private GraphSearchColor _CellInGraphColor = GraphSearchColor.White;
        public GraphSearchColor CellInGraphColor
        {
            set { _CellInGraphColor = value; }
            get { return _CellInGraphColor; }
        }

        //private GameBoardColor 


    }

    public enum GameBoardColor
    { 
        Null,
        Empty,
        White,
        Black
    }

    public enum GraphSearchColor
    { 
        White,
        Gray,
        Black
    }
}
