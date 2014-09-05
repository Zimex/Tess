using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Drako3
{
   public  enum Fraction
    {
        Dragon,Dwarf
    }
   public  class Player
    {
        public string name;
        public List<Card> hand=new List<Card>();
        public List<Card> graveyard=new List<Card>();
        public List<Card> library=new List<Card>();

        public Grid grid;
       public Fraction fraction ;
       public bool isActiv;
        //public bool fraction; //0-Dragon, 1-Dwarf
        public Player(string n, Fraction f, Grid g)
        {
            grid = g;
            name = n;
            fraction = f;
            if (fraction == Fraction.Dragon)
                isActiv = true;
            else
                isActiv = false;
            if(fraction==Fraction.Dragon)
            {
                for(int i=1;i<=Card.dragonCards;i++)
                {
                    library.Add(new Card(i, Fraction.Dragon));
                }
            }
            else
            {
                for (int i = 1; i <= Card.dwarfCards; i++)
                {
                    library.Add(new Card(i, fraction));
                }
            }

        }
        public void DrawInitialHand()
        {
            hand = new List<Card>();
            /*
            hand.Add(new Card(1,fraction));
            hand.Add(new Card(2, fraction));
            hand.Add(new Card(3, fraction));
            hand.Add(new Card(4, fraction));
             * */
            Card.DrawCard(this, 4);
        }
       public Fraction ReturnActiveFraction()
       {
           if (this.isActiv == true)
               return this.fraction;
           else
           {
               if (this.fraction == Fraction.Dragon)
                   return Fraction.Dwarf;
               else
                   return Fraction.Dragon;

           }
       }

       public int GetHandCardIndexFromId(int i)
       {
           int index=-1;
           foreach(Card c in hand)
           {
               if (c.id == i)
               {
                   index = hand.IndexOf(c);
                   return index;
               }
           }
           return index;

       }

       public void RenderHand()
        {
            foreach (Image img in grid.Children)
            {
                BitmapImage src = new BitmapImage();
                int index = grid.Children.IndexOf(img);

                if (index >= this.hand.Count)
                {
                    if (this.fraction == Fraction.Dwarf)
                    {

                        src.UriSource = new Uri("Images/Dwarf.jpg", UriKind.Relative);
                        // src.UriSource = new Uri(P1.hand[0].src, UriKind.Relative);

                        img.Source = src;
                    }
                    else
                    {

                        src.UriSource = new Uri("Images/Dragon.jpg", UriKind.Relative);
                        // src.UriSource = new Uri(P1.hand[0].src, UriKind.Relative);

                        img.Source = src;
                    }
                }
                else
                {
                     src.UriSource = new Uri(this.hand[index].src, UriKind.Relative);
                    // src.UriSource = new Uri(P1.hand[index].src, UriKind.Relative);
                  //  src.UriSource = new Uri("Images/Dragon.jpg", UriKind.Relative);
                    img.Source = src;


                }
             
            }
        }


    }
}
