using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drako3
{
    public enum GameType
    {
        HOT_SEATS
    }
    public class Game
    {
        GameType gameType;
        Player P1, P2;
        Board board;
    }
}
