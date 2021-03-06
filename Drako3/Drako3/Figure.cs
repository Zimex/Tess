﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Drako3
{
    [DataContract]
    [KnownType(typeof(Hex))]
   public abstract class Figure
    {
        private Point position;
        private bool isSelected;
        public Figure()
        { }
        [DataMember]
       public Point Position
        {
            get { return position; }
            set { position = value; }

        }
        [DataMember]
        public bool IsSelected
       {
           get { return isSelected; }
           set { isSelected = value; }

       }
       public  virtual bool MoveTo(Point p,List<Hex> hexs)
        {
           Hex.GetHexByAxialCoordinates(hexs,p).Figure=Hex.GetHexByAxialCoordinates(hexs,position).Figure;
           Hex.GetHexByAxialCoordinates(hexs,position).Figure = null;
           
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
