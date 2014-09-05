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
        public GameType gameType;
        public Player P1, P2;
        private int actionsLeft;
        private int cardsToDiscard=0;
        public Board board;
        public Player activPlayer;
        public Image libraryImage;
        
       // int actionsLeft = 2;
        public Card playedCard;
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
            }
        }
        //public Dwarf leader;
        //public Dwarf crossbowman; 
        //public Dwarf webber;
        //public Dragon dragon;
        //public List<Figure> figures;
        public Game(GameType type, Player p1, Player p2, Board b, Image lib)
        {
            gameType = type;
            P1 = p1;
            P2 = p2;
            libraryImage = lib;
            board = b;
            if (p1.isActiv == true)
                activPlayer = P1;
            else activPlayer = P2;
            // leader = new Dwarf(DwarfType.Leader, new Point(1, -2));
            // crossbowman = new Dwarf(DwarfType.Crossbowman, new Point(-2, 1));
            // webber = new Dwarf(DwarfType.Webber, new Point(1, 1));
            // dragon = new Dragon(new Point(0, 0));
            //List<Figure> figures = new List<Figure>();
            //figures.Add(leader);
            //figures.Add(crossbowman);
            //figures.Add(webber);
            //figures.Add(dragon);
         //   Card.InitiateActionsDictionary();
        }

       

        public void ChangeTurn()
        {
            if(activPlayer.hand.Count>6)
            {
                cardsToDiscard = activPlayer.hand.Count - 6;
                return;
            }
            playedCard = null;
            if (P1.isActiv == true)
            {
                P1.isActiv = false;
                P2.isActiv = true;
                board.activeFraction = P2.fraction;
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
                P2.isActiv = false;
                P1.isActiv = true;
                activPlayer = P1;
                P1.RenderHand();
                actionsLeft = 2;
                board.activeFraction = P1.fraction;
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
