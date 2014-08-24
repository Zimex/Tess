using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Drako3
{
    
    class Dragon:Figure
    {
        public int shieldHp = 4;
        public int wingsHp = 2;
        public int legsHp = 3;
        public int fireHp = 2;
        public Point position;
        public Dragon(Point p)
        {
            position = p;
        }
    }
}
