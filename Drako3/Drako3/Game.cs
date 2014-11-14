using Microsoft.Phone.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
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
    public class Game
    {
        private GameType gameType;
        private Player p1, p2;
        private int actionsLeft;
        private int cardsToDiscard=0;
        private int damageToDragon = 0;
        public Board board;
        private Player activPlayer;
        private Image libraryImage;
        private bool doubleAttack=false;
        private bool doubleMove = false;
        private Figure wasMovedInDouble = null;
        private Figure wasAttackingInDouble = null;
        public PanoramaPage1 page;
        
        public Card playedCard;

        public GameType GameType
        {
            get { return gameType; }
            set { gameType = value; }
        }
        public Player P1
        {
            get { return p1; }
            set { p1 = value; }
        }
        public Player P2
        {
            get { return p2; }
            set { p2 = value; }
        }
        public Board Board
        {
            get { return board; }
            set { board = value; }
        }
        public Player ActivPlayer
        {
            get { return activPlayer; }
            set { activPlayer = value; }
        }

        public Image LibraryImage
        {
            get { return libraryImage; }
            set { libraryImage = value; }
        }
       

        public Figure WasMovedInDouble
        {
            get { return wasMovedInDouble; }
            set
            {
                wasMovedInDouble = value;

            }
        }
        public Figure WasAttackingInDouble
        {
            get { return wasAttackingInDouble; }
            set
            {
                wasAttackingInDouble = value;

            }
        }

        public bool DoubleAttack
        {
            get { return doubleAttack; }
            set
            {
                doubleAttack = value;
              
            }
        }
        public bool DoubleMove
        {
            get { return doubleMove; }
            set
            {
                doubleMove = value;

            }
        }

        public int ActionsLeft
        {
            get { return actionsLeft; }
            set { actionsLeft=value;
            if (actionsLeft < 1)
                ChangeTurn();
            }
        }

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
                    (page.panorama.Items[2] as PanoramaItem  ).Visibility = Visibility.Collapsed;
                    page.panorama.SetValue(Panorama.SelectedItemProperty, page.panorama.Items[(3) % page.panorama.Items.Count]);
                   page.panorama.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
                    (page.panorama.Items[2] as PanoramaItem).Visibility = Visibility.Visible;
 
                }
            }
        }
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
