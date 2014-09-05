using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Drako3
{
    
   public abstract class Figure
    {
        public Point position;
        public bool isSelected;
       public  virtual bool MoveTo(Point p,List<Hex> hexs)
        {
           Hex.GetHexByAxialCoordinates(hexs,p).figure=Hex.GetHexByAxialCoordinates(hexs,position).figure;
           Hex.GetHexByAxialCoordinates(hexs,position).figure = null;
           
            position = p;
            return true;
        }
       public virtual bool Attack(Point p)
       {
           return true;
       }
       public Fraction ReturnFraction()
       {

           if (this is Dragon)
           {
               return Fraction.Dragon;
           }
           else 
               return Fraction.Dwarf;
           
       }

    }
}
