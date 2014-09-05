using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

enum DwarfType
{
    Crossbowman,
    Webber,
    Leader

}
namespace Drako3
{

    class Dwarf: Figure
    {
        public DwarfType type;
        public int hp;
        //public int b=1;
        public Dwarf(DwarfType t,Point p)
        {
            type = t;
            position = p;
            isSelected = false;
            switch(type)
            {
                case(DwarfType.Crossbowman):
                    hp = 5;
                    break;
                case(DwarfType.Leader):
                    hp = 6;
                    break;
                case(DwarfType.Webber):
                    hp = 4;
                    break;
                default: break;

            }


        }
    }
}
