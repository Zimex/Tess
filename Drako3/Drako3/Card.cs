using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Drako3
{
    public class Card
    {
       public int id;
      // public bool type; //0-Dragon, 1-Dwarf
       Fraction fraction;
        public static int dragonCards=11;
        public static int dwarfCards = 16;
        public static int dragonCardsLeft = 11;
        public static int dwarfCardsLeft = 16;
      public  string src;
        public Card(int i,Fraction f)
        {
            int n;
            if(f==Fraction.Dragon)
                n=Card.dragonCards;
                    else 
                        n=Card.dwarfCards;
            if (i > 0 && i <= n)
            {
                id = i;
                fraction = f;
               // src = new BitmapImage();
                string c;
                
                if (fraction == Fraction.Dwarf)
                
                    c = "K";
                
                    else
                    c = "D";
                //if (i < 10) c += 0;
                //src.UriSource = new Uri("Images/Cards/"+c+id.ToString(), UriKind.Relative);
                src = "Images/Cards/" + c + id.ToString()+".png";
            }

        }
        public static bool DrawCard(Player p, int amount)
        {

            if (p.hand.Count + amount > 8) return false;
            Card c;
            int i;
            Random r = new Random((int)DateTime.Now.Ticks);
            for (int j = 0; j < amount; j++)
            {
                i = r.Next(p.library.Count - 1) ;
                c = p.library[i];
                p.library.RemoveAt(i);
                if (p.fraction == Fraction.Dragon)
                    Card.dragonCardsLeft--;
                else
                    Card.dwarfCardsLeft--;
                p.hand.Add(new Card(c.id, p.fraction));
            }
            p.RenderHand();

            return true;
        }
    }
}
