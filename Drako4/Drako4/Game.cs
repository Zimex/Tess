using Microsoft.Phone.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Drako3
{
    public enum GameType
    {
        HOT_SEATS
    }
    [DataContract]
    [KnownType(typeof(Player))]
    [KnownType(typeof(Board))]
    [KnownType(typeof(Figure))]
    [KnownType(typeof(Dwarf))]
    [KnownType(typeof(Dragon))]
    
    public class Game
    {
        private GameType gameType;
        private Player p1, p2;
        private int actionsLeft;
        private int cardsToDiscard=0;
        private int damageToDragon = 0;
        [DataMember]
        public Board board;
        private Player activPlayer;
        private Image libraryImage;
        private bool doubleAttack=false;
        private bool doubleMove = false;
        private Figure wasMovedInDouble = null;
        private Figure wasAttackingInDouble = null;
        //[NonSerialized]
        [DataMember(IsRequired=false)]
        public PanoramaPage1 page;
        private Dwarf leader;// = new Dwarf(DwarfType.Leader, new Point(1, -2));
        private Dwarf crossbowman;// = new Dwarf(DwarfType.Crossbowman, new Point(-2, 1));
        private Dwarf webber;// = new Dwarf(DwarfType.Webber, new Point(1, 1));
        private Dragon dragon;// = new Dragon(new Point(0, 0));
        private bool showHandBeforeTurnChange=false;
        private bool changeTurnAfterSlide = false;
        [DataMember]
        public Card playedCard;

        [DataMember]
        public GameType GameType
        {
            get { return gameType; }
            set { gameType = value; }
        }
        [DataMember]
        public Player P1
        {
            get { return p1; }
            set { p1 = value; }
        }
        [DataMember]
        public Player P2
        {
            get { return p2; }
            set { p2 = value; }
        }
        [DataMember]
        public Board Board
        {
            get { return board; }
            set { board = value; }
        }
        [DataMember]
        public Player ActivPlayer
        {
            get { return activPlayer; }
            set { activPlayer = value; }
        }

        [DataMember]
        public Image LibraryImage
        {
            get { return libraryImage; }
            set { libraryImage = value; }
        }


        [DataMember]
        public Figure WasMovedInDouble
        {
            get { return wasMovedInDouble; }
            set
            {
                wasMovedInDouble = value;

            }
        }
        [DataMember]
        public Figure WasAttackingInDouble
        {
            get { return wasAttackingInDouble; }
            set
            {
                wasAttackingInDouble = value;

            }
        }

        [DataMember]
        public bool DoubleAttack
        {
            get { return doubleAttack; }
            set
            {
                doubleAttack = value;
              
            }
        }
        [DataMember]
        public bool DoubleMove
        {
            get { return doubleMove; }
            set
            {
                doubleMove = value;

            }
        }

        [DataMember]
        public int ActionsLeft
        {
            get { return actionsLeft; }
            set { actionsLeft=value;
            if (actionsLeft < 1)
                ChangeTurn();
            }
        }
        [DataMember]
        public Dwarf Leader
        {
            get { return leader;}
            set { leader = value; }
        }
        [DataMember]
        public Dwarf Webber
        {
            get { return webber; }
            set { webber = value; }
        }
        [DataMember]
        public Dwarf Crossbowman
        {
            get { return crossbowman; }
            set { crossbowman = value; }
        }

        [DataMember]
        public Dragon Dragon
        {
            get { return dragon; }
            set { dragon = value; }
        }
        [DataMember]
        public int CardsToDiscard
        {
            get { return cardsToDiscard; }
            set
            {
                cardsToDiscard = value;
                if (cardsToDiscard < 1)
                    ChangeTurn();
                else
                {
                    if (page != null && page.panorama != null && page.panorama.Items.Count > 1)
                    {
                        (page.panorama.Items[2] as PanoramaItem).Visibility = Visibility.Collapsed;
                        page.panorama.SetValue(Panorama.SelectedItemProperty, page.panorama.Items[(3) % page.panorama.Items.Count]);
                        page.panorama.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
                        (page.panorama.Items[2] as PanoramaItem).Visibility = Visibility.Visible;
                    }
                }
            }
        }
        [DataMember]
        public int DamageToDragon
        {
            get { return damageToDragon; }
            set
            {
                damageToDragon = value;
                if (damageToDragon < 1)
                {
                    //powrot do board
                }
            }
        }
        [DataMember]
        public bool ShowHandBeforeTurnChange
        {
            get { return showHandBeforeTurnChange; }
            set { showHandBeforeTurnChange=value; }
        }
        [DataMember]
        public bool ChangeTurnAfterSlide
        {
            get { return changeTurnAfterSlide; }
            set { changeTurnAfterSlide = value; }
        }
       public Game()
        {
            activPlayer = new Player();
            //page = new PanoramaPage1();
        }
        public Game(GameType type, Player p1, Player p2, Board b, Image lib, PanoramaPage1 p)
        {
            gameType = type;
            P1 = p1;
            P2 = p2;

            libraryImage = lib;
            board = b;
            if (p1.IsActiv == true)
                activPlayer = P1;
            else activPlayer = P2;
            page = p;
     
        }

       public void DragonWon ()
    {

    }
       public void DwarfsWon()
       {

       }

       public void ChangeTurn()
       {
           if (damageToDragon == 0)
           {
               if (activPlayer.Hand.Count > 6)
               {
                   cardsToDiscard = activPlayer.Hand.Count - 6;
                   return;
               }
               playedCard = null;
               if (p1.IsActiv == true)
               {
                   p1.IsActiv = false;
                   p2.IsActiv = true;
                   board.ActiveFraction = P2.Fraction;
                   activPlayer = P2;
                   P2.RenderHand();
                   actionsLeft = 2;

                   if (P1.ReturnActiveFraction() == Fraction.Dragon)
                   {
                       libraryImage.Source = new System.Windows.Media.Imaging.BitmapImage(
               new Uri(@"/Images/Dragon.jpg", UriKind.RelativeOrAbsolute));
                   }
                   else
                   {
                       libraryImage.Source = new System.Windows.Media.Imaging.BitmapImage(
               new Uri(@"/Images/Dwarf.jpg", UriKind.RelativeOrAbsolute));
                   }

               }
               else
               {
                   p2.IsActiv = false;
                   p1.IsActiv = true;
                   activPlayer = P1;
                   P1.RenderHand();
                   actionsLeft = 2;
                   board.ActiveFraction = P1.Fraction;
                   if (P1.ReturnActiveFraction() == Fraction.Dragon)
                   {
                       libraryImage.Source = new System.Windows.Media.Imaging.BitmapImage(
               new Uri(@"/Images/Dragon.jpg", UriKind.RelativeOrAbsolute));
                   }
                   else
                   {
                       libraryImage.Source = new System.Windows.Media.Imaging.BitmapImage(
               new Uri(@"/Images/Dwarf.jpg", UriKind.RelativeOrAbsolute));
                   }
               }
           }

       }
    }
   
}
