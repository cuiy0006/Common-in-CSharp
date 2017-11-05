using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS61B.PROJ2
{
    public abstract class Player
    {
        public abstract Move ChooseMove();
        public abstract bool OpponentMove(Move move);
        public abstract bool ForceMove(Move move);

        
        public bool IsMoveLegal(Move move)
        {
            return true;
        }
    }

    
}
