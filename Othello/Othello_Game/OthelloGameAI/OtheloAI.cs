using System;
using System.Collections.Generic;

namespace Othello.Logic
{
    internal static class OthelloAI
    {
        internal static DiskPosition RandomizeNextPlay(List<OthelloDisk> io_PossibleMovesForAI)
        {
            Random rnd = new Random();
            int randomPostion = rnd.Next(0, io_PossibleMovesForAI.Count - 1);
            return io_PossibleMovesForAI[randomPostion].Position;
        }
    }
}
