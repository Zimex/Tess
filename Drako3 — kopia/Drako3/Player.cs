using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
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
   [DataContract]
   [KnownType(typeof(Card))]
   
   public  class Player
    {
        private string name;
        private List<Card> hand = new List<Card>();
        private List<Card> graveyard = new List<Card>();
        private List<Card> library = new List<Card>();
      //  private Grid grid;
        private Fraction fraction;
        private bool isActiv;
        //public bool fraction; //0-Dragon, 1-Dwarf
        [DataMember]
        public string Name
        {
            get {return name ;}
            set {name=value ;}
        }
        [DataMember]
        public List<Card> Hand
       {
           get { return hand; }
           set { hand = value; }
       }
        [DataMember]
        public List<Card> Graveyard
       {
           get { return graveyard; }
           set { graveyard = value; }
       }
        [DataMember]
        public List<Card> Library
       {
           get { return library; }
           set { library = value; }
       }
        //[DataMember]
        //public Grid Grid
        //{
        //    get { return grid; }
        //    set { grid = value; }
        //}
        [DataMember]
        public Fraction Fraction
       {
           get { return fraction; }
           set { fraction = value; }
       }
        [DataMember]
        public bool IsActiv
       {
           get { return isActiv; }
           set { isActiv = value; }
       }
       public Player()
        {

        }
       public Player(string n, Fraction f)//+Grid g
       {
           //   grid = g;
           name = n;
           fraction = f;
           if (fraction == Fraction.Dragon)
           {
               isActiv = true;
               for (int i = 1; i <= Card.dragonCards; i++)
               {
                   library.Add(new Card(i, Fraction.Dragon));
               }
           }
           else
           {
               isActiv = false;
               for (int i = 1; i <= Card.dwarfCards; i++)
               {
                   library.Add(new Card(i, fraction));
               }

           }
       }
            //if(fraction==Fraction.Dragon)
            //{
            //    for(int i=1;i<=Card.dragonCards;i++)
            //    {
            //        library.Add(new Card(i, Fraction.Dragon));
            //    }
            //}
            //else
            //{
            //    for (int i = 1; i <= Card.dwarfCards; i++)
            //    {
            //        library.Add(new Card(i, fraction));
            //    }
            //}

       // }
        //public void DrawInitialHand()
        //{
        //    hand = new List<Card>();
       
        //    Card.DrawCard(this, 4);
        //}
       //public Fraction ReturnActiveFraction()
       //{
       //    if (this.isActiv == true)
       //        return this.fraction;
       //    else
       //    {
       //        if (this.fraction == Fraction.Dragon)
       //            return Fraction.Dwarf;
       //        else
       //            return Fraction.Dragon;

       //    }
       //}

       //public int GetHandCardIndexFromId(int i)
       //{
       //    int index=-1;
       //    foreach(Card c in hand)
       //    {
       //        if (c.Id == i)
       //        {
       //            index = hand.IndexOf(c);
       //            return index;
       //        }
       //    }
       //    return index;

       //}

       //public void RenderHand()
       // {
       //     foreach (Image img in grid.Children)
       //     {
       //         BitmapImage src = new BitmapImage();
       //         int index = grid.Children.IndexOf(img);

       //         if (index >= this.hand.Count)
       //         {
       //             if (this.fraction == Fraction.Dwarf)
       //             {

       //                 src.UriSource = new Uri("Images/Dwarf.jpg", UriKind.Relative);
       //                 // src.UriSource = new Uri(P1.hand[0].src, UriKind.Relative);

       //                 img.Source = src;
       //             }
       //             else
       //             {

       //                 src.UriSource = new Uri("Images/Dragon.jpg", UriKind.Relative);

       //                 img.Source = src;
       //             }
       //         }
       //         else
       //         {
       //              src.UriSource = new Uri(this.hand[index].Src, UriKind.Relative);
                  
       //             img.Source = src;


       //         }
             
       //     }
       // }


    }
}
