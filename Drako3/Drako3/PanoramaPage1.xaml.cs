using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Drako3
{

    public partial class PanoramaPage1 : PhoneApplicationPage
    {
        public Game game;
        public List<Hex> hexs;
        public Board board;
        List<Polygon> p;
        Dwarf leader;// = new Dwarf(DwarfType.Leader, new Point(1, -2));
        Dwarf crossbowman;// = new Dwarf(DwarfType.Crossbowman, new Point(-2, 1));
        Dwarf webber;// = new Dwarf(DwarfType.Webber, new Point(1, 1));
        Dragon dragon;// = new Dragon(new Point(0, 0));
        List<Figure> figures = new List<Figure>();
        Figure selectedFigure = null;
        Player P1, P2;
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            /*string strItemIndex;
            if (NavigationContext.QueryString.TryGetValue("goto", out strItemIndex))
                panorama.DefaultItem = panorama.Items[Convert.ToInt32(strItemIndex)];
            
            if(game.playedCard!=null)
            {
                playedCardImage.Source = new System.Windows.Media.Imaging.BitmapImage(
                new Uri(game.playedCard.src, UriKind.RelativeOrAbsolute));
            }
             * */
         //   this = new PanoramaPage1();
            base.OnNavigatedTo(e);
        }

        public string ImagePath
        {
            get;
            set;
        }
        public PanoramaPage1()
        {

            InitializeComponent();
            Card.InitiateActionsDictionary();
            panorama.DefaultItem = panoramaItemBoard;
            P1 = new Player("Zimex", Fraction.Dragon, HandGrid);
            P1.DrawInitialHand();
            P2 = new Player("Zaremox", Fraction.Dwarf, HandGrid);
            P2.DrawInitialHand();
            P1.RenderHand();
            libraryImage.Source = new System.Windows.Media.Imaging.BitmapImage(
                new Uri(@"/Images/Dragon.jpg", UriKind.RelativeOrAbsolute));
           // DragonImage.Width = Application.Current.Host.Content.ActualWidth;
            //DragonImage.Height = Application.Current.Host.Content.ActualHeight;

            //ImagePath = "Images/Dragon.jpg";
            //libraryImage.DataContext = ImagePath;
            //ImageSource s = libraryImage.Source;
            //string ss = libraryImage.Source.ToString();

            leader = new Dwarf(DwarfType.Leader, new Point(1, -2),this);
            crossbowman = new Dwarf(DwarfType.Crossbowman, new Point(-2, 1),this);
            webber = new Dwarf(DwarfType.Webber, new Point(1, 1),this);
            dragon = new Dragon(new Point(0, 0), this);
            figures = new List<Figure>();
            selectedFigure = null;
            figures.Add(leader);
            figures.Add(crossbowman);
            figures.Add(webber);
            figures.Add(dragon);

            board = new Board(figures);
            game = new Game(GameType.HOT_SEATS, P1, P2, board, libraryImage,this);
            //board.DrawBoard(BoardGrid);
            game.board.DrawBoard(BoardGrid);



            // hexs = board.hexs;
            hexs = game.board.Hexs;
          List<Hex> lines=  Hex.GetStraightLines(hexs, Hex.GetHexByPosition(new Point(0, 0),hexs));
        }
        public void SelectFigure(Hex h)
        {
            Figure figure;
            if (h.Figure != null)
            {
                figure = h.Figure;
                h.Figure.IsSelected = true;

                if (figure.GetType() == typeof(Dragon))
                {
                    h.Polygon.StrokeThickness = 5;
                    h.Polygon.Fill = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromArgb(150, 255, 0, 0));
                    h.Polygon.Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromArgb(255, 255, 0, 0));
                }
                else if (figure.GetType() == typeof(Dwarf))
                {
                    Dwarf d = figure as Dwarf;

                    switch (d.Type)
                    {
                        case (DwarfType.Crossbowman):
                            h.Polygon.StrokeThickness = 5;
                            h.Polygon.Fill = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromArgb(150, 255, 255, 0));
                            h.Polygon.Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromArgb(255, 255, 255, 0));

                            break;
                        case (DwarfType.Leader):
                            h.Polygon.StrokeThickness = 5;
                            h.Polygon.Fill = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromArgb(150, 255, 128, 0));
                            h.Polygon.Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromArgb(255, 255, 128, 0));

                            break;
                        case (DwarfType.Webber):
                            h.Polygon.StrokeThickness = 5;
                            h.Polygon.Fill = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromArgb(150, 128, 255, 0));
                            h.Polygon.Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromArgb(255, 128, 255, 0));


                            break;
                    }
                }
            }
        }

        private void PanoramaItem_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {

            Point p = e.GetPosition(null);
            if (this.Orientation == PageOrientation.LandscapeLeft)
            {
                if (p.X >= game.board.ScreenSize.Width - 30 && p.X <= game.board.ScreenSize.Width - 5 && p.Y >= 5 && p.Y <= 25)
                {

                    game.board.ChangeActivePlayer(game.P1.ReturnActiveFraction());
                    game.ChangeTurn();
                    if (game.P1.ReturnActiveFraction() == Fraction.Dragon)
                    {
                        libraryImage.Source = new System.Windows.Media.Imaging.BitmapImage(
                new Uri(@"/Images/Dragon.jpg", UriKind.RelativeOrAbsolute));
                    }
                    else
                    {
                        libraryImage.Source = new System.Windows.Media.Imaging.BitmapImage(
                new Uri(@"/Images/Dwarf.jpg", UriKind.RelativeOrAbsolute));
                    }


                    return;
                }
            }
            else if (this.Orientation == PageOrientation.LandscapeRight)
            {
                if (p.X >= 5 && p.X <= 25 && p.Y >= game.board.ScreenSize.Height - 30 && p.Y <= game.board.ScreenSize.Height - 5)
                {

                    game.board.ChangeActivePlayer(game.P1.ReturnActiveFraction());
                    game.ChangeTurn();
                    if (game.P1.ReturnActiveFraction() == Fraction.Dragon)
                    {
                        libraryImage.Source = new System.Windows.Media.Imaging.BitmapImage(
                new Uri(@"/Images/Dragon.jpg", UriKind.RelativeOrAbsolute));
                    }
                    else
                    {
                        libraryImage.Source = new System.Windows.Media.Imaging.BitmapImage(
                new Uri(@"/Images/Dwarf.jpg", UriKind.RelativeOrAbsolute));
                    }

                    return;
                }
            }

            p = game.board.ConvertPhonePointToBoardPoint(p, this.Orientation);
            Hex h = Hex.PixelToHex(p, hexs);
            if (h != null)
            {
                if (selectedFigure == null && game.playedCard != null) //zaznaczenie
                {

                    if (h.Figure != null)
                    {
                        if (game.DoubleMove && game.WasMovedInDouble == h.Figure)
                            return;
                        if (game.board.ActiveFraction == h.Figure.ReturnFraction())
                        {
                            selectedFigure = h.Figure;
                            SelectFigure(h);

                            if (game.playedCard.FlyActionValue() > 0)
                            {
                                foreach (List<Hex> list in Hex.GetMoveRange(Hex.GetHexByFigure(hexs, selectedFigure), game.playedCard.FlyActionValue(), hexs))
                                {

                                    foreach (Hex eachHex in list)
                                    {
                                        eachHex.InMoveRange = true;
                                        Hex.rangeFigure = selectedFigure;
                                    }
                                }
                                Board.ResetPolygons(hexs);
                                Board.ResetPolygons(hexs);
                            }
                            else
                                if (game.playedCard.SingleMoveActionValue() > 0)
                                {
                                    List<List<Hex>> range = Hex.GetMoveRange(Hex.GetHexByFigure(hexs, selectedFigure), game.playedCard.SingleMoveActionValue(), hexs);
                                    foreach (List<Hex> list in range)
                                    {

                                        foreach (Hex eachHex in list)
                                        {
                                            eachHex.InMoveRange = true;
                                            Hex.rangeFigure = selectedFigure;
                                        }
                                    }
                                    Board.ResetPolygons(hexs);
                                    Board.ResetPolygons(hexs);
                                }

                        }
                    }
                }

                else
                    if (selectedFigure == h.Figure && selectedFigure!=null)//odznaczenie
                    {
                        h.Figure.IsSelected = false;
                        foreach (Hex each in hexs)
                        {
                            each.InMoveRange = false;
                            Hex.rangeFigure = null;
                        }
                        Board.ResetPolygons(hexs);
                        Board.ResetPolygons(hexs);
                        
                        selectedFigure = null;
                    }
                    else if(game.DoubleAttack) //drugi atak
                    {
                        {
                            if (game.WasAttackingInDouble == selectedFigure)
                                return;
                            game.DoubleAttack = false;
                            game.WasAttackingInDouble = null;
                            Dragon d = h.Figure as Dragon;
                            if (d.ShieldHp >= game.playedCard.SingleAttackActionValue())
                            {
                                d.ShieldHp = d.ShieldHp - game.playedCard.SingleAttackActionValue();

                                selectedFigure.IsSelected = false;
                                selectedFigure = null;
                                //try
                                game.ActivPlayer.Hand.RemoveAt(game.ActivPlayer.GetHandCardIndexFromId(game.playedCard.Id));
                                game.ActivPlayer.RenderHand();
                                game.playedCard = null;
                                game.ActionsLeft--;
                                playedCardImage.Source = new System.Windows.Media.Imaging.BitmapImage();
                                foreach (Hex each2 in hexs)
                                {
                                    each2.InMoveRange = false;
                                    Hex.rangeFigure = null;
                                }
                            }
                            else if (d.ShieldHp > 0 && game.playedCard.SingleAttackActionValue() > d.ShieldHp)
                            {
                                game.DamageToDragon = game.playedCard.SingleAttackActionValue() - d.ShieldHp;
                                d.ShieldHp = 0;
                                selectedFigure.IsSelected = false;
                                selectedFigure = null;
                                //try
                                game.ActivPlayer.Hand.RemoveAt(game.ActivPlayer.GetHandCardIndexFromId(game.playedCard.Id));
                                game.ActivPlayer.RenderHand();
                                game.playedCard = null;
                                game.ActionsLeft--;
                                playedCardImage.Source = new System.Windows.Media.Imaging.BitmapImage();
                                foreach (Hex each2 in hexs)
                                {
                                    each2.InMoveRange = false;
                                    Hex.rangeFigure = null;
                                }
                                (panorama.Items[2] as PanoramaItem).Visibility = Visibility.Collapsed;
                                panorama.SetValue(Panorama.SelectedItemProperty, panorama.Items[(1) % panorama.Items.Count]);
                                panorama.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
                                (panorama.Items[2] as PanoramaItem).Visibility = Visibility.Visible;
                                //przejscie do karty smoka i zadnaie obrazen
                            }
                            else if (d.ShieldHp == 0)//przejscie do karty smoka i nadanie obrazen
                            {
                                selectedFigure.IsSelected = false;
                                selectedFigure = null;
                                //try
                                game.ActivPlayer.Hand.RemoveAt(game.ActivPlayer.GetHandCardIndexFromId(game.playedCard.Id));
                                game.ActivPlayer.RenderHand();
                                game.playedCard = null;
                                game.ActionsLeft--;
                                playedCardImage.Source = new System.Windows.Media.Imaging.BitmapImage();
                                foreach (Hex each2 in hexs)
                                {
                                    each2.InMoveRange = false;
                                    Hex.rangeFigure = null;
                                }
                                game.DamageToDragon = game.playedCard.SingleAttackActionValue();
                                (panorama.Items[2] as PanoramaItem).Visibility = Visibility.Collapsed;
                                panorama.SetValue(Panorama.SelectedItemProperty, panorama.Items[(1) % panorama.Items.Count]);
                                panorama.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
                                (panorama.Items[2] as PanoramaItem).Visibility = Visibility.Visible;

                            }


                            //  if (d.ShieldHp < 0)
                            //{
                            //  figures.Remove(d);
                            //Hex.GetHexByFigure(hexs, d).figure = null;

                            // }
                            Board.ResetPolygons(hexs);
                            Board.ResetPolygons(hexs);

                        }
                    }
                    else if(game.DoubleMove)//drugi ruch
                    {
                        foreach (Hex hex in hexs)
                        {
                            hex.InMoveRange = false;
                            Hex.rangeFigure = null;

                        }
                        selectedFigure.MoveTo(h.AxialCoordinates, hexs);
                        selectedFigure.IsSelected = false;
                        selectedFigure = null;
                        game.WasMovedInDouble = null;
                        //try
                         game.ActivPlayer.Hand.RemoveAt(game.ActivPlayer.GetHandCardIndexFromId(game.playedCard.Id));
                          game.ActivPlayer.RenderHand();
                         game.playedCard = null;
                         playedCardImage.Source = new System.Windows.Media.Imaging.BitmapImage();

                           game.ActionsLeft--;
                        game.DoubleMove = false;
                        Board.ResetPolygons(hexs);
                        Board.ResetPolygons(hexs);
                    }else
                        if (h.Figure == null && selectedFigure != null && game.playedCard != null) //ruch
                        {

                            if (Hex.CanBeMovedTo(Hex.GetHexByFigure(hexs, selectedFigure), h, game.playedCard.SingleMoveActionValue(), hexs))
                            {
                               
                                foreach (Hex hex in hexs)
                                {
                                    hex.InMoveRange = false;
                                    Hex.rangeFigure = null;

                                }
                                selectedFigure.MoveTo(h.AxialCoordinates, hexs);
                                selectedFigure.IsSelected = false;
                                selectedFigure = null;
                                //try
                                game.ActivPlayer.Hand.RemoveAt(game.ActivPlayer.GetHandCardIndexFromId(game.playedCard.Id));
                                game.ActivPlayer.RenderHand();
                                game.playedCard = null;
                                playedCardImage.Source = new System.Windows.Media.Imaging.BitmapImage();

                                game.ActionsLeft--;
                                Board.ResetPolygons(hexs);
                                Board.ResetPolygons(hexs);
                            }
                            else
                                if (Hex.CanBeMovedTo(Hex.GetHexByFigure(hexs, selectedFigure), h, game.playedCard.DoubleMoveActionValue(), hexs)) //podwojny ruch
                                {

                                    foreach (Hex hex in hexs)
                                    {
                                        hex.InMoveRange = false;
                                        Hex.rangeFigure = null;

                                    }
                                    game.WasMovedInDouble = selectedFigure;
                                    selectedFigure.MoveTo(h.AxialCoordinates, hexs);
                                    selectedFigure.IsSelected = false;
                                    selectedFigure = null;
                                    //try
                                   // game.activPlayer.hand.RemoveAt(game.activPlayer.GetHandCardIndexFromId(game.playedCard.id));
                                  //  game.activPlayer.RenderHand();
                                   // game.playedCard = null;
                                   // playedCardImage.Source = new System.Windows.Media.Imaging.BitmapImage();

                                 //   game.ActionsLeft--;
                                    game.DoubleMove = true;
                                    Board.ResetPolygons(hexs);
                                    Board.ResetPolygons(hexs);
                                }
                                else if (h != null)
                                    if (Hex.CanBeMovedTo(Hex.GetHexByFigure(hexs, selectedFigure), h, game.playedCard.FlyActionValue(), hexs))
                                    {
                                        foreach (Hex hex in hexs)
                                        {
                                            hex.InMoveRange = false;
                                            Hex.rangeFigure = null;

                                        }
                                        selectedFigure.MoveTo(h.AxialCoordinates, hexs);
                                        selectedFigure.IsSelected = false;
                                        selectedFigure = null;
                                        //try
                                        game.ActivPlayer.Hand.RemoveAt(game.ActivPlayer.GetHandCardIndexFromId(game.playedCard.Id));
                                        game.ActivPlayer.RenderHand();
                                        game.playedCard = null;
                                        playedCardImage.Source = new System.Windows.Media.Imaging.BitmapImage();
                                        game.ActionsLeft--;

                                        Board.ResetPolygons(hexs);
                                        Board.ResetPolygons(hexs);
                                    }
                        }
                        else if(selectedFigure!=null && h.Figure!=null && game.playedCard!=null)//atak
                        {
                            if (selectedFigure.ReturnFraction() != h.Figure.ReturnFraction())
                            {
                                if(game.playedCard.DoubleAttackActionValue() > 0)//double attack
                                {
                                    {
                                        Dragon d = h.Figure as Dragon;
                                        game.DoubleAttack = true;
                                        game.WasAttackingInDouble = selectedFigure;
                                        if (d.ShieldHp >= game.playedCard.SingleAttackActionValue())
                                        {
                                            d.ShieldHp = d.ShieldHp - game.playedCard.SingleAttackActionValue();

                                            selectedFigure.IsSelected = false;
                                            selectedFigure = null;
                                            //try
                                           // game.activPlayer.hand.RemoveAt(game.activPlayer.GetHandCardIndexFromId(game.playedCard.id));
                                           // game.activPlayer.RenderHand();
                                          //  game.playedCard = null;
                                          //  game.ActionsLeft--;
                                         //   playedCardImage.Source = new System.Windows.Media.Imaging.BitmapImage();
                                            foreach (Hex each2 in hexs)
                                            {
                                                each2.InMoveRange = false;
                                                Hex.rangeFigure = null;
                                            }
                                        }
                                        else if (d.ShieldHp > 0 && game.playedCard.SingleAttackActionValue() > d.ShieldHp)
                                        {
                                            game.DamageToDragon = game.playedCard.SingleAttackActionValue() - d.ShieldHp;
                                            d.ShieldHp = 0;
                                            selectedFigure.IsSelected = false;
                                            selectedFigure = null;
                                            //try
                                           // game.activPlayer.hand.RemoveAt(game.activPlayer.GetHandCardIndexFromId(game.playedCard.id));
                                            //game.activPlayer.RenderHand();
                                            //game.playedCard = null;
                                            //game.ActionsLeft--;
                                            //playedCardImage.Source = new System.Windows.Media.Imaging.BitmapImage();
                                            foreach (Hex each2 in hexs)
                                            {
                                                each2.InMoveRange = false;
                                                Hex.rangeFigure = null;
                                            }
                                            (panorama.Items[2] as PanoramaItem).Visibility = Visibility.Collapsed;
                                            panorama.SetValue(Panorama.SelectedItemProperty, panorama.Items[(1) % panorama.Items.Count]);
                                            panorama.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
                                            (panorama.Items[2] as PanoramaItem).Visibility = Visibility.Visible;
                                            //przejscie do karty smoka i zadnaie obrazen
                                        }
                                        else if (d.ShieldHp == 0)//przejscie do karty smoka i nadanie obrazen
                                        {
                                            selectedFigure.IsSelected = false;
                                            selectedFigure = null;
                                            //try
                                            //game.activPlayer.hand.RemoveAt(game.activPlayer.GetHandCardIndexFromId(game.playedCard.id));
                                            //game.activPlayer.RenderHand();
                                            //game.playedCard = null;
                                            //game.ActionsLeft--;
                                            //playedCardImage.Source = new System.Windows.Media.Imaging.BitmapImage();
                                            foreach (Hex each2 in hexs)
                                            {
                                                each2.InMoveRange = false;
                                                Hex.rangeFigure = null;
                                            }
                                            game.DamageToDragon = game.playedCard.SingleAttackActionValue();
                                            (panorama.Items[2] as PanoramaItem).Visibility = Visibility.Collapsed;
                                            panorama.SetValue(Panorama.SelectedItemProperty, panorama.Items[(1) % panorama.Items.Count]);
                                            panorama.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
                                            (panorama.Items[2] as PanoramaItem).Visibility = Visibility.Visible;

                                        }


                                        //  if (d.ShieldHp < 0)
                                        //{
                                        //  figures.Remove(d);
                                        //Hex.GetHexByFigure(hexs, d).figure = null;

                                        // }
                                        Board.ResetPolygons(hexs);
                                        Board.ResetPolygons(hexs);

                                    }
                                }
                            else
                                if (game.playedCard.SingleAttackActionValue() > 0)//single attack
                                {
                                    Hex selectedHex = Hex.GetHexByFigure(hexs, selectedFigure);
                                    List<Hex> neighbors = Hex.Neighbors(selectedHex, hexs);
                                    foreach (Hex each in neighbors)
                                    {
                                        if (each == h)
                                        {
                                            if (h.Figure is Dwarf) //atak w krasnoluda
                                            {
                                                Dwarf k = h.Figure as Dwarf;
                                                k.Hp = k.Hp - game.playedCard.SingleAttackActionValue();
                                                selectedFigure.IsSelected = false;
                                                selectedFigure = null;
                                                //try
                                                game.ActivPlayer.Hand.RemoveAt(game.ActivPlayer.GetHandCardIndexFromId(game.playedCard.Id));
                                                game.ActivPlayer.RenderHand();
                                                game.playedCard = null;
                                                game.ActionsLeft--;
                                                playedCardImage.Source = new System.Windows.Media.Imaging.BitmapImage();
                                                foreach(Hex each2 in hexs)
                                                {
                                                    each2.InMoveRange = false;
                                                    Hex.rangeFigure = null;
                                                }

                                                
                                                if (k.Hp < 0)
                                                {
                                                    figures.Remove(k);
                                                    Hex.GetHexByFigure(hexs,k).Figure = null;

                                                }
                                                Board.ResetPolygons(hexs);
                                                Board.ResetPolygons(hexs); 
                                            }
                                            else
                                                if (h.Figure is Dragon) //atak w smoka
                                                {
                                                    Dragon d = h.Figure as Dragon;
                                                    if (d.ShieldHp >= game.playedCard.SingleAttackActionValue())
                                                    {
                                                        d.ShieldHp = d.ShieldHp - game.playedCard.SingleAttackActionValue();

                                                        selectedFigure.IsSelected = false;
                                                        selectedFigure = null;
                                                        //try
                                                        game.ActivPlayer.Hand.RemoveAt(game.ActivPlayer.GetHandCardIndexFromId(game.playedCard.Id));
                                                        game.ActivPlayer.RenderHand();
                                                        game.playedCard = null;
                                                        game.ActionsLeft--;
                                                        playedCardImage.Source = new System.Windows.Media.Imaging.BitmapImage();
                                                        foreach (Hex each2 in hexs)
                                                        {
                                                            each2.InMoveRange = false;
                                                            Hex.rangeFigure = null;
                                                        }
                                                    }
                                                    else if(d.ShieldHp > 0 && game.playedCard.SingleAttackActionValue() >d.ShieldHp)
                                                    {
                                                        game.DamageToDragon = game.playedCard.SingleAttackActionValue() - d.ShieldHp;
                                                        d.ShieldHp = 0;
                                                        selectedFigure.IsSelected = false;
                                                        selectedFigure = null;
                                                        //try
                                                        game.ActivPlayer.Hand.RemoveAt(game.ActivPlayer.GetHandCardIndexFromId(game.playedCard.Id));
                                                        game.ActivPlayer.RenderHand();
                                                        game.playedCard = null;
                                                        game.ActionsLeft--;
                                                        playedCardImage.Source = new System.Windows.Media.Imaging.BitmapImage();
                                                        foreach (Hex each2 in hexs)
                                                        {
                                                            each2.InMoveRange = false;
                                                            Hex.rangeFigure = null;
                                                        }
                                                        (panorama.Items[2] as PanoramaItem).Visibility = Visibility.Collapsed;
                                                        panorama.SetValue(Panorama.SelectedItemProperty, panorama.Items[(1) % panorama.Items.Count]);
                                                        panorama.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
                                                        (panorama.Items[2] as PanoramaItem).Visibility = Visibility.Visible;
                                                        //przejscie do karty smoka i zadnaie obrazen
                                                    }
                                                    else if(d.ShieldHp==0)//przejscie do karty smoka i nadanie obrazen
                                                    {
                                                        game.DamageToDragon = game.playedCard.SingleAttackActionValue();

                                                        selectedFigure.IsSelected = false;
                                                        selectedFigure = null;
                                                        //try
                                                        game.ActivPlayer.Hand.RemoveAt(game.ActivPlayer.GetHandCardIndexFromId(game.playedCard.Id));
                                                        game.ActivPlayer.RenderHand();
                                                        game.playedCard = null;
                                                        game.ActionsLeft--;
                                                        playedCardImage.Source = new System.Windows.Media.Imaging.BitmapImage();
                                                        foreach (Hex each2 in hexs)
                                                        {
                                                            each2.InMoveRange = false;
                                                            Hex.rangeFigure = null;
                                                        }
                                                        (panorama.Items[2] as PanoramaItem).Visibility = Visibility.Collapsed;
                                                        panorama.SetValue(Panorama.SelectedItemProperty, panorama.Items[(1) % panorama.Items.Count]);
                                                        panorama.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
                                                        (panorama.Items[2] as PanoramaItem).Visibility = Visibility.Visible;

                                                    }


                                                  //  if (d.ShieldHp < 0)
                                                    //{
                                                      //  figures.Remove(d);
                                                        //Hex.GetHexByFigure(hexs, d).figure = null;

                                                   // }
                                                    Board.ResetPolygons(hexs);
                                                    Board.ResetPolygons(hexs); 
                                                    
                                                }

                                                break;
                                        }
                                    }
                                }
                            }
                        }

            }

        }

        private void libraryImage_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            if (game.DoubleMove || game.DoubleAttack)
                return;
            if(Card.DrawCard(game.ActivPlayer, 2))
            game.ActionsLeft--;
            if (game.ActionsLeft < 2)
            {
                (panorama.Items[2] as PanoramaItem).Visibility = Visibility.Collapsed;
                panorama.SetValue(Panorama.SelectedItemProperty, panorama.Items[(3) % panorama.Items.Count]);
                panorama.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
                (panorama.Items[2] as PanoramaItem).Visibility = Visibility.Visible;
            }
                //  game.activPlayer.
        }



        private void Image_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            if (game.DoubleMove)
                return;
            Image img = sender as Image;
            int r = Grid.GetRow(img);
            int c = Grid.GetColumn(img);
            int n = r * 4 + c;
            if (game.CardsToDiscard == 0)
            { 
            if (n < game.ActivPlayer.Hand.Count)
            {
                Card card = game.ActivPlayer.Hand[n];
                //game.activPlayer.hand.RemoveAt(n);
                // game.activPlayer.RenderHand();
                game.playedCard = card;
                if (game.playedCard != null)
                {
                    playedCardImage.Source = new System.Windows.Media.Imaging.BitmapImage(
                    new Uri(game.playedCard.Src, UriKind.RelativeOrAbsolute));
                }



                // panorama.
                //   panorama.SetValue(Panorama.SelectedIndexProperty, panorama.Items[0]);
                (panorama.Items[3] as PanoramaItem).Visibility = Visibility.Collapsed;
                panorama.SetValue(Panorama.SelectedItemProperty, panorama.Items[(2) % panorama.Items.Count]);
                panorama.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
                (panorama.Items[3] as PanoramaItem).Visibility = Visibility.Visible;


                /******************************************
                 * http://xme.im/slide-or-change-panorama-selected-item-programatically
                 Do zrobienia na kiedy indziej
                 private void slidePanorama(Panorama pan) {     FrameworkElement panWrapper = VisualTreeHelper.GetChild(pan, 0) as FrameworkElement;     FrameworkElement panTitle = VisualTreeHelper.GetChild(panWrapper, 1) as FrameworkElement;     //Get the panorama layer to calculate all panorama items size     FrameworkElement panLayer = VisualTreeHelper.GetChild(panWrapper, 2) as FrameworkElement;     //Get the title presenter to calculate the title size     FrameworkElement panTitlePresenter = VisualTreeHelper.GetChild(VisualTreeHelper.GetChild(panTitle, 0) as FrameworkElement, 1) as FrameworkElement;       //Current panorama item index     int curIndex = pan.SelectedIndex;       //Get the next of next panorama item     FrameworkElement third = VisualTreeHelper.GetChild(pan.Items[(curIndex + 2) % pan.Items.Count] as PanoramaItem, 0) as FrameworkElement;       //Be sure the RenderTransform is TranslateTransform     if (!(pan.RenderTransform is TranslateTransform)         || !(panTitle.RenderTransform is TranslateTransform))     {         pan.RenderTransform = new TranslateTransform();         panTitle.RenderTransform = new TranslateTransform();     }       //Increase width of panorama to let it render the next slide (if not, default panorama is 480px and the null area appear if we transform it)     pan.Width = 960;       //Animate panorama control to the right     Storyboard sb = new Storyboard();     DoubleAnimation a = new DoubleAnimation();     a.From = 0;     a.To = -(pan.Items[curIndex] as PanoramaItem).ActualWidth; //Animate the x transform to a width of one item     a.Duration = new Duration(TimeSpan.FromMilliseconds(700));     a.EasingFunction = new CircleEase(); //This is default panorama easing effect     sb.Children.Add(a);     Storyboard.SetTarget(a, pan.RenderTransform);     Storyboard.SetTargetProperty(a, new PropertyPath(TranslateTransform.XProperty));       //Animate panorama title separately     DoubleAnimation aTitle = new DoubleAnimation();     aTitle.From = 0;     aTitle.To = (panLayer.ActualWidth - panTitlePresenter.ActualWidth) / (pan.Items.Count - 1) * 1.5; //Calculate where should the title animate to     aTitle.Duration = a.Duration;     aTitle.EasingFunction = a.EasingFunction; //This is default panorama easing effect     sb.Children.Add(aTitle);     Storyboard.SetTarget(aTitle, panTitle.RenderTransform);     Storyboard.SetTargetProperty(aTitle, new PropertyPath(TranslateTransform.XProperty));       //Start the effect     sb.Begin();       //After effect completed, we change the selected item     a.Completed += (obj, args) =>     {         //Reset panorama width         pan.Width = 480;         //Change the selected item         (pan.Items[curIndex] as PanoramaItem).Visibility = Visibility.Collapsed;         pan.SetValue(Panorama.SelectedItemProperty, pan.Items[(curIndex + 1) % pan.Items.Count]);         pan.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));         (pan.Items[curIndex] as PanoramaItem).Visibility = Visibility.Visible;         //Reset panorama render transform         (pan.RenderTransform as TranslateTransform).X = 0;         //Reset title render transform         (panTitle.RenderTransform as TranslateTransform).X = 0;           //Because of the next of next item will be load after we change the selected index to next item         //I do not want it appear immediately without any effect, so I create a custom effect for it         if (!(third.RenderTransform is TranslateTransform))         {             third.RenderTransform = new TranslateTransform();         }         Storyboard sb2 = new Storyboard();         DoubleAnimation aThird = new DoubleAnimation() { From = 100, To = 0, Duration = new Duration(TimeSpan.FromMilliseconds(300)) };           sb2.Children.Add(aThird);         Storyboard.SetTarget(aThird, third.RenderTransform);         Storyboard.SetTargetProperty(aThird, new PropertyPath(TranslateTransform.XProperty));         sb2.Begin();     }; } 
                 * ****************************************/


                // NavigationService.Navigate(new Uri("/PanoramaPage1.xaml?goto=0", UriKind.Relative));
            }
            }
            else
            {
                if (n < game.ActivPlayer.Hand.Count)
                {
                    Card card = game.ActivPlayer.Hand[n];
                    game.ActivPlayer.Hand.RemoveAt(game.ActivPlayer.GetHandCardIndexFromId(card.Id));
                    game.CardsToDiscard--;
                    game.ActivPlayer.RenderHand();
                    if (game.CardsToDiscard == 0)
                    {
                        (panorama.Items[1] as PanoramaItem).Visibility = Visibility.Collapsed;
                        panorama.SetValue(Panorama.SelectedItemProperty, panorama.Items[(1 + 1) % panorama.Items.Count]);
                        panorama.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
                        (panorama.Items[1] as PanoramaItem).Visibility = Visibility.Visible;
                    }
                   
                }
            }
            
        }

        private void PhoneApplicationPage_BackKeyPress(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            NavigationService.Navigate(new Uri("/GameMenu.xaml", UriKind.Relative));

        }

        private void DragonImage_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            int w = (int)DragonImage.Width;
            int h = (int)DragonImage.Height;
            Point p = e.GetPosition(sender as System.Windows.Controls.Image);
            if (game.DamageToDragon > 0)
            {
                if (p.X > 0 && p.X < 0.18 * w && p.Y > 0.38 * h && p.Y < 0.88 * h && dragon.WingsHp>0 && dragon.ShieldHp==0)  //skrzydla
                {
                    dragon.WingsHp--;
                   // if(dragon.WingsHp==0 && dragon.ShieldHp==0 && dragon.LegsdHp==0 && dragon.FireHp==0)
                    if(dragon.IsDead())
                    {
                        //usun smoka
                        figures.Remove(dragon);
                        Hex.GetHexByFigure(hexs, dragon).Figure = null;

                        
                    }
                    game.DamageToDragon--;
                    if (game.DamageToDragon == 0 && game.ActionsLeft == 0)
                        game.ChangeTurn();

                    //MessageBox.Show("Wings");
                    
                }
                else
                    if (p.X > 0.38 * w && p.X < 0.88 * w && p.Y > 0.77 * h && p.Y < h && dragon.LegsHp>0 && dragon.ShieldHp==0)  //Nogi
                    {
                        dragon.LegsHp--;
                        if (dragon.IsDead())
                        {
                            //usun smoka
                            figures.Remove(dragon);
                            Hex.GetHexByFigure(hexs, dragon).Figure = null;


                        }
                        game.DamageToDragon--;
                        if (game.DamageToDragon == 0 && game.ActionsLeft == 0)
                            game.ChangeTurn();
                      //  MessageBox.Show("Legs");
                    }
                    else
                        if (p.X > 0.82 * w && p.X < w && p.Y > 0.38 * h && p.Y < 0.88 * h && dragon.ShieldHp==0 && dragon.FireHp>0)  //Ogien
                        {
                            dragon.FireHp--;
                            if (dragon.IsDead())
                            {
                                //usun smoka
                                figures.Remove(dragon);
                                Hex.GetHexByFigure(hexs, dragon).Figure = null;


                            }
                            game.DamageToDragon--;
                            if (game.DamageToDragon == 0 && game.ActionsLeft == 0)
                                game.ChangeTurn();
                         //   MessageBox.Show("Fire");
                        }
            }

        }




    }
}