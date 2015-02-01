
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Drako3
{
    public class Worker
    {
        public string dragonLibrary = "Images/Dragon.jpg";
        public string dwarfLibrary = "Images/Dwarf.jpg";
        private static Dictionary<int, List<Action>> dragonActionsDictionary = new Dictionary<int, List<Action>>();
        private static Dictionary<int, List<Action>> dwarfsActionsDictionary = new Dictionary<int, List<Action>>();
        private static bool areActionsInitialized = false;
        private List<Image> crossbowmanImageList = new List<Image>();
        private List<Image> webberImageList = new List<Image>();
        private List<Image> leaderImageList = new List<Image>();
        public Image playedCardImage;
        public Image libraryImage;
        public List<Figure> figures;
        public Game game;
        public PanoramaPage1 page;
        public Grid dragonGrid;
        public Grid dwarfsGrid;
        public Grid boardGrid;
        public Grid handGrid;
        public Figure selectedFigure;
        
        public Worker(PanoramaPage1 p, Grid dragonGrid, Grid dwarfsGrid, Grid boardGrid, Grid handGrid,Game g,Image library, Image playedCard)
        {
            page=p;
            this.dragonGrid = dragonGrid;
            this.dwarfsGrid = dwarfsGrid;
            this.boardGrid = boardGrid;
            this.handGrid = handGrid;
            this.libraryImage = library;
            this.playedCardImage = playedCard;
            Dwarf leader = new Dwarf(DwarfType.Leader, new Point(1, -2));
            Dwarf crossbowman = new Dwarf(DwarfType.Crossbowman, new Point(-2, 1));
            Dwarf webber = new Dwarf(DwarfType.Webber, new Point(1, 1));
            Dragon dragon = new Dragon(new Point(0, 0));
            figures = new List<Figure>();
            selectedFigure = null;
            figures.Add(leader);
            figures.Add(crossbowman);
            figures.Add(webber);
            figures.Add(dragon);
            game = g;
            game.Leader = leader;
            game.Webber = webber;
            game.Crossbowman = crossbowman;
            game.Dragon = dragon;
            game.ActionsLeft = 1;

            foreach (Figure f in figures)
            {
                Hex.GetHexByAxialCoordinates(game.Board.Hexs, f.Position).Figure = f;
            }
        }
          
        public bool Draw(Player p,int amount)
        {
            if (p.Hand.Count + amount > 8) return false;
            Card c;
            int i;
            Random r = new Random((int)DateTime.Now.Ticks);
            for (int j = 0; j < amount; j++)
            {
                if (p.Library.Count == 0)
                {

                }
                else
                {
                    i = r.Next(p.Library.Count - 1);
                    c = p.Library[i];
                    p.Library.RemoveAt(i);
                    if (p.Fraction == Fraction.Dragon)
                        Card.dragonCardsLeft--;
                    else
                        Card.dwarfCardsLeft--;
                    p.Hand.Add(new Card(c.Id, p.Fraction));
                }
            }
            RenderHand();
            return true;
        }

        public void RenderHand()
        {


            foreach (Image img in handGrid.Children)
            {
                BitmapImage src = new BitmapImage();
                int index = handGrid.Children.IndexOf(img);

                if (index >= game.ActivePlayer.Hand.Count)
                {
                    if (game.ActivePlayer.Fraction == Fraction.Dwarf)
                    {

                        src.UriSource = new Uri("Images/Dwarf.jpg", UriKind.Relative);
                        // src.UriSource = new Uri(P1.hand[0].src, UriKind.Relative);

                        img.Source = src;
                    }
                    else
                    {

                        src.UriSource = new Uri("Images/Dragon.jpg", UriKind.Relative);

                        img.Source = src;
                    }
                }
                else
                {
                    src.UriSource = new Uri(game.ActivePlayer.Hand[index].Src, UriKind.Relative);

                    img.Source = src;


                }

            }
        }

        public void InitializeDamage()
        {

            foreach (Image img in page.DwarfsGrid.Children)
            {
                if (img.Name == "crossbowmanCounter1")
                {
                    crossbowmanImageList.Add(img);
                }
                else
                    if (img.Name == "crossbowmanCounter2")
                    {
                        crossbowmanImageList.Add(img);
                    }
                    else
                        if (img.Name == "crossbowmanCounter3")
                        {
                            crossbowmanImageList.Add(img);
                        }
                        else
                            if (img.Name == "crossbowmanCounter4")
                            {
                                crossbowmanImageList.Add(img);
                            }
                            else
                                if (img.Name == "crossbowmanCounter5")
                                {
                                    crossbowmanImageList.Add(img);
                                }
                                else
                                    if (img.Name == "webberCounter1")
                                    {
                                        webberImageList.Add(img);
                                    }
                                    else
                                        if (img.Name == "webberCounter2")
                                        {
                                            webberImageList.Add(img);
                                        }
                                        else
                                            if (img.Name == "webberCounter3")
                                            {
                                                webberImageList.Add(img);
                                            }
                                            else
                                                if (img.Name == "webberCounter4")
                                                {
                                                    webberImageList.Add(img);
                                                }
                                                else
                                                    if (img.Name == "leaderCounter1")
                                                    {
                                                        leaderImageList.Add(img);
                                                    }
                                                    else
                                                        if (img.Name == "leaderCounter2")
                                                        {
                                                            leaderImageList.Add(img);
                                                        }
                                                        else
                                                            if (img.Name == "leaderCounter3")
                                                            {
                                                                leaderImageList.Add(img);
                                                            }
                                                            else
                                                                if (img.Name == "leaderCounter4")
                                                                {
                                                                    leaderImageList.Add(img);
                                                                }
                                                                else
                                                                    if (img.Name == "leaderCounter5")
                                                                    {
                                                                        leaderImageList.Add(img);
                                                                    }
                                                                    else
                                                                        if (img.Name == "leaderCounter6")
                                                                        {
                                                                            leaderImageList.Add(img);

                                                                        }


            }
        }

        public static KeyValuePair<int, List<Action>> AddAction(int id, Action a1)
        {
            KeyValuePair<int, List<Action>> result = new KeyValuePair<int, List<Action>>();
            List<Action> list = new List<Action>();
            list.Add(a1);
            result = new KeyValuePair<int, List<Action>>(id, list);
            return result;

        }
        public static KeyValuePair<int, List<Action>> AddAction(int id, Action a1, Action a2)
        {
            KeyValuePair<int, List<Action>> result = new KeyValuePair<int, List<Action>>();
            List<Action> list = new List<Action>();
            list.Add(a1);
            list.Add(a2);
            result = new KeyValuePair<int, List<Action>>(id, list);
            return result;

        }

        public void InitializeActionsDictionary()
        {

            KeyValuePair<int, List<Action>> list = new KeyValuePair<int, List<Action>>();
            if (!areActionsInitialized)
            {
                list = AddAction(1, new Action(ActionType.FIRE, 1));
                dragonActionsDictionary.Add(list.Key, list.Value);

                list = AddAction(2, new Action(ActionType.SINGLE_ATTACK, 3));
                dragonActionsDictionary.Add(list.Key, list.Value);

                list = AddAction(3, new Action(ActionType.FLY, 1));
                dragonActionsDictionary.Add(list.Key, list.Value);

                list = AddAction(4, new Action(ActionType.BLOCK, 1));
                dragonActionsDictionary.Add(list.Key, list.Value);

                list = AddAction(5, new Action(ActionType.SINGLE_ATTACK, 2), new Action(ActionType.SINGLE_MOVE, 1));
                dragonActionsDictionary.Add(list.Key, list.Value);

                list = AddAction(6, new Action(ActionType.SINGLE_MOVE, 2));
                dragonActionsDictionary.Add(list.Key, list.Value);

                list = AddAction(7, new Action(ActionType.SINGLE_MOVE, 1));
                dragonActionsDictionary.Add(list.Key, list.Value);

                list = AddAction(8, new Action(ActionType.FIRE, 2));
                dragonActionsDictionary.Add(list.Key, list.Value);

                list = AddAction(9, new Action(ActionType.SINGLE_ATTACK, 2), new Action(ActionType.SINGLE_MOVE, 2));
                dragonActionsDictionary.Add(list.Key, list.Value);

                list = AddAction(10, new Action(ActionType.SINGLE_MOVE, 1), new Action(ActionType.BLOCK, 1));
                dragonActionsDictionary.Add(list.Key, list.Value);

                list = AddAction(11, new Action(ActionType.SINGLE_ATTACK, 1), new Action(ActionType.SINGLE_MOVE, 2));
                dragonActionsDictionary.Add(list.Key, list.Value);

                list = AddAction(1, new Action(ActionType.SINGLE_MOVE, 1), new Action(ActionType.BLOCK, 1));
                dwarfsActionsDictionary.Add(list.Key, list.Value);

                list = AddAction(2, new Action(ActionType.BLOCK, 1));
                dwarfsActionsDictionary.Add(list.Key, list.Value);

                list = AddAction(3, new Action(ActionType.DOUBLE_MOVE, 1), new Action(ActionType.BOLT, 1));
                dwarfsActionsDictionary.Add(list.Key, list.Value);

                list = AddAction(4, new Action(ActionType.SINGLE_MOVE, 2), new Action(ActionType.BOLT, 1));
                dwarfsActionsDictionary.Add(list.Key, list.Value);

                list = AddAction(5, new Action(ActionType.BOLT, 1));
                dwarfsActionsDictionary.Add(list.Key, list.Value);

                list = AddAction(6, new Action(ActionType.DOUBLE_MOVE, 1), new Action(ActionType.WEB, 1));
                dwarfsActionsDictionary.Add(list.Key, list.Value);

                list = AddAction(7, new Action(ActionType.DOUBLE_MOVE, 1), new Action(ActionType.BLOCK, 1));
                dwarfsActionsDictionary.Add(list.Key, list.Value);

                list = AddAction(8, new Action(ActionType.SINGLE_MOVE, 2), new Action(ActionType.BLOCK, 1));
                dwarfsActionsDictionary.Add(list.Key, list.Value);

                list = AddAction(9, new Action(ActionType.WEB, 1), new Action(ActionType.BLOCK, 1));
                dwarfsActionsDictionary.Add(list.Key, list.Value);

                list = AddAction(10, new Action(ActionType.SINGLE_MOVE, 1), new Action(ActionType.SINGLE_ATTACK, 1));
                dwarfsActionsDictionary.Add(list.Key, list.Value);

                list = AddAction(11, new Action(ActionType.DOUBLE_MOVE, 1));
                dwarfsActionsDictionary.Add(list.Key, list.Value);

                list = AddAction(12, new Action(ActionType.SINGLE_ATTACK, 2));
                dwarfsActionsDictionary.Add(list.Key, list.Value);

                list = AddAction(13, new Action(ActionType.SINGLE_ATTACK, 1), new Action(ActionType.BOLT, 1));
                dwarfsActionsDictionary.Add(list.Key, list.Value);

                list = AddAction(14, new Action(ActionType.WEB, 1));
                dwarfsActionsDictionary.Add(list.Key, list.Value);

                list = AddAction(15, new Action(ActionType.SINGLE_ATTACK, 1));
                dwarfsActionsDictionary.Add(list.Key, list.Value);

                list = AddAction(16, new Action(ActionType.DOUBLE_ATTACK, 1));
                dwarfsActionsDictionary.Add(list.Key, list.Value);
            }
            areActionsInitialized = true;
        }

        public void InitializeBoard()
        {
            double w = game.Board.Center.X;
            for (int j = 0; j < 3; j++)
            {
                game.Board.Hexs.Add(new Hex(new Point(((game.Board.Center.X - game.Board.HexWidth) + j * game.Board.HexWidth), (game.Board.Center.Y - 1.5 * game.Board.HexHeight)), new Point(j, -2)));
                for (int i = 0; i < 6; i++)
                {
                    double angle = 2 * Math.PI / 6 * (i + 0.5);
                    double x = (((game.Board.Center.X - game.Board.HexWidth) + j * game.Board.HexWidth) + game.Board.HexHeight / 2 * Math.Cos(angle));
                    double y = ((game.Board.Center.Y - 1.5 * game.Board.HexHeight) + game.Board.HexHeight / 2 * Math.Sin(angle));
                    game.Board.Hexs[game.Board.Hexs.Count - 1].Corners.Add(new Point(x, y));
                }
            }

            for (int j = 0; j < 4; j++)
            {
                game.Board.Hexs.Add(new Hex(new Point(((game.Board.Center.X - game.Board.HexWidth * 1.5) + j * game.Board.HexWidth), (game.Board.Center.Y - 0.75 * game.Board.HexHeight))));
                game.Board.Hexs[game.Board.Hexs.Count - 1].AxialCoordinates = new Point(j - 1, -1);
                for (int i = 0; i < 6; i++)
                {
                    double angle = 2 * Math.PI / 6 * (i + 0.5);
                    double x = (((game.Board.Center.X - game.Board.HexWidth * 1.5) + j * game.Board.HexWidth) + game.Board.HexHeight / 2 * Math.Cos(angle));
                    double y = ((game.Board.Center.Y - 0.75 * game.Board.HexHeight) + game.Board.HexHeight / 2 * Math.Sin(angle));
                    game.Board.Hexs[game.Board.Hexs.Count - 1].Corners.Add(new Point(x, y));
                }
            }

            for (int j = 0; j < 5; j++)
            {
                game.Board.Hexs.Add(new Hex(new Point(((game.Board.Center.X - game.Board.HexWidth * 2) + j * game.Board.HexWidth), (game.Board.Center.Y))));
                game.Board.Hexs[game.Board.Hexs.Count - 1].AxialCoordinates = new Point(j - 2, -0);

                for (int i = 0; i < 6; i++)
                {
                    double angle = 2 * Math.PI / 6 * (i + 0.5);
                    double x = (((game.Board.Center.X - game.Board.HexWidth * 2) + j * game.Board.HexWidth) + game.Board.HexHeight / 2 * Math.Cos(angle));
                    double y = ((game.Board.Center.Y) + game.Board.HexHeight / 2 * Math.Sin(angle));
                    game.Board.Hexs[game.Board.Hexs.Count - 1].Corners.Add(new Point(x, y));

                }
            }

            for (int j = 0; j < 4; j++)
            {
                game.Board.Hexs.Add(new Hex(new Point(((game.Board.Center.X - game.Board.HexWidth * 1.5) + j * game.Board.HexWidth), (game.Board.Center.Y + 0.75 * game.Board.HexHeight))));
                game.Board.Hexs[game.Board.Hexs.Count - 1].AxialCoordinates = new Point(j - 2, 1);

                for (int i = 0; i < 6; i++)
                {
                    double angle = 2 * Math.PI / 6 * (i + 0.5);
                    double x = (((game.Board.Center.X - game.Board.HexWidth * 1.5) + j * game.Board.HexWidth) + game.Board.HexHeight / 2 * Math.Cos(angle));
                    double y = ((game.Board.Center.Y + 0.75 * game.Board.HexHeight) + game.Board.HexHeight / 2 * Math.Sin(angle));
                    game.Board.Hexs[game.Board.Hexs.Count - 1].Corners.Add(new Point(x, y));

                }
            }

            for (int j = 0; j < 3; j++)
            {
                game.Board.Hexs.Add(new Hex(new Point(((game.Board.Center.X - game.Board.HexWidth) + j * game.Board.HexWidth), (game.Board.Center.Y + 1.5 * game.Board.HexHeight))));
                game.Board.Hexs[game.Board.Hexs.Count - 1].AxialCoordinates = new Point(j - 2, 2);

                for (int i = 0; i < 6; i++)
                {
                    double angle = 2 * Math.PI / 6 * (i + 0.5);
                    double x = (((game.Board.Center.X - game.Board.HexWidth) + j * game.Board.HexWidth) + game.Board.HexHeight / 2 * Math.Cos(angle));
                    double y = ((game.Board.Center.Y + 1.5 * game.Board.HexHeight) + game.Board.HexHeight / 2 * Math.Sin(angle));
                    game.Board.Hexs[game.Board.Hexs.Count - 1].Corners.Add(new Point(x, y));

                }
            }
            foreach (Figure f in figures)
            {
                Hex.GetHexByAxialCoordinates(game.Board.Hexs, f.Position).Figure = f;
            }
            ResetPolygons();
    
        }
        public  void ResetPolygons()
        {

            foreach (Hex h in game.Board.Hexs)
                h.ResetPolygon();
        }

       public Fraction ReturnActiveFraction()
        {

            return game.ActivePlayer.Fraction;
        }

        public void DrawBoard() //List<Polygon> drawBoard()
       {



           for (int i = 0; i < game.Board.Hexs.Count; i++)
           {
               if (game.Board.Hexs[i].Figure == null) boardGrid.Children.Add(game.Board.Hexs[i].Polygon);

           }
           for (int i = 0; i < game.Board.Hexs.Count; i++)
           {
               if (game.Board.Hexs[i].Figure != null) boardGrid.Children.Add(game.Board.Hexs[i].Polygon);

           }
         //  boardGrid.Children.Add(game.ActivePlayer);


           return;
       }

        public void ChangeTurn()
        {
            if (game.DamageToDragon == 0)
            {
                if (game.ActivePlayer.Hand.Count > 6)
                {
                    game.CardsToDiscard = game.ActivePlayer.Hand.Count - 6;
                    return;
                }
                game.playedCard = null;
                if (game.P1.IsActiv == true)
                {
                    game.P1.IsActiv = false;
                    game.P2.IsActiv = true;
                    game.Board.ActiveFraction = game.P2.Fraction;
                    game.ActivePlayer = game.P2;
                    RenderHand();
                    game.ActionsLeft = 2;

                    if (ReturnActiveFraction() == Fraction.Dragon)
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
                    game.P2.IsActiv = false;
                    game.P1.IsActiv = true;
                    game.ActivePlayer = game.P1;
                    RenderHand();
                    game.ActionsLeft = 2;
                    game.Board.ActiveFraction = game.P1.Fraction;
                    if (ReturnActiveFraction() == Fraction.Dragon)
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
