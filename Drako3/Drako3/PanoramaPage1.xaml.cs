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
        Dwarf leader = new Dwarf(DwarfType.Leader, new Point(1, -2));
        Dwarf crossbowman = new Dwarf(DwarfType.Crossbowman, new Point(-2, 1));
        Dwarf webber = new Dwarf(DwarfType.Webber, new Point(1, 1));
        Dragon dragon = new Dragon(new Point(0, 0));
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
            //ImagePath = "Images/Dragon.jpg";
            //libraryImage.DataContext = ImagePath;
            //ImageSource s = libraryImage.Source;
            //string ss = libraryImage.Source.ToString();

            leader = new Dwarf(DwarfType.Leader, new Point(1, -2));
            crossbowman = new Dwarf(DwarfType.Crossbowman, new Point(-2, 1));
            webber = new Dwarf(DwarfType.Webber, new Point(1, 1));
            dragon = new Dragon(new Point(0, 0));
            figures = new List<Figure>();
            selectedFigure = null;
            figures.Add(leader);
            figures.Add(crossbowman);
            figures.Add(webber);
            figures.Add(dragon);

            board = new Board(figures);
            game = new Game(GameType.HOT_SEATS, P1, P2, board, libraryImage);
            //board.DrawBoard(BoardGrid);
            game.board.DrawBoard(BoardGrid);



            // hexs = board.hexs;
            hexs = game.board.hexs;
        }
        public void SelectFigure(Hex h)
        {
            Figure figure;
            if (h.figure != null)
            {
                figure = h.figure;
                h.figure.isSelected = true;

                if (figure.GetType() == typeof(Dragon))
                {
                    h.polygon.StrokeThickness = 5;
                    h.polygon.Fill = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromArgb(150, 255, 0, 0));
                    h.polygon.Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromArgb(255, 255, 0, 0));
                }
                else if (figure.GetType() == typeof(Dwarf))
                {
                    Dwarf d = figure as Dwarf;

                    switch (d.type)
                    {
                        case (DwarfType.Crossbowman):
                            h.polygon.StrokeThickness = 5;
                            h.polygon.Fill = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromArgb(150, 255, 255, 0));
                            h.polygon.Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromArgb(255, 255, 255, 0));

                            break;
                        case (DwarfType.Leader):
                            h.polygon.StrokeThickness = 5;
                            h.polygon.Fill = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromArgb(150, 255, 128, 0));
                            h.polygon.Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromArgb(255, 255, 128, 0));

                            break;
                        case (DwarfType.Webber):
                            h.polygon.StrokeThickness = 5;
                            h.polygon.Fill = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromArgb(150, 128, 255, 0));
                            h.polygon.Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromArgb(255, 128, 255, 0));


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
                if (p.X >= game.board.screenSize.Width - 30 && p.X <= game.board.screenSize.Width - 5 && p.Y >= 5 && p.Y <= 25)
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
                if (p.X >= 5 && p.X <= 25 && p.Y >= game.board.screenSize.Height - 30 && p.Y <= game.board.screenSize.Height - 5)
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
                    if (h.figure != null)
                        if (game.board.activeFraction == h.figure.ReturnFraction())
                        {
                            selectedFigure = h.figure;
                            SelectFigure(h);

                            if (game.playedCard.FlyActionValue() > 0)
                            {
                                foreach (List<Hex> list in Hex.GetMoveRange(Hex.GetHexByFigure(hexs, selectedFigure), game.playedCard.FlyActionValue(), hexs))
                                {

                                    foreach (Hex eachHex in list)
                                    {
                                        eachHex.inMoveRange = true;
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
                                            eachHex.inMoveRange = true;
                                            Hex.rangeFigure = selectedFigure;
                                        }
                                    }
                                    Board.ResetPolygons(hexs);
                                    Board.ResetPolygons(hexs);
                                }

                        }

                }

                else
                    if (selectedFigure == h.figure && selectedFigure!=null)//odznaczenie
                    {
                        h.figure.isSelected = false;
                        foreach (Hex each in hexs)
                        {
                            each.inMoveRange = false;
                            Hex.rangeFigure = null;
                        }
                        Board.ResetPolygons(hexs);
                        Board.ResetPolygons(hexs);

                        selectedFigure = null;
                    }
                    else
                        if (h.figure == null && selectedFigure != null && game.playedCard != null) //ruch
                        {

                            if (Hex.CanBeMovedTo(Hex.GetHexByFigure(hexs, selectedFigure), h, game.playedCard.SingleMoveActionValue(), hexs))
                            {
                               
                                foreach (Hex hex in hexs)
                                {
                                    hex.inMoveRange = false;
                                    Hex.rangeFigure = null;

                                }
                                selectedFigure.MoveTo(h.axialCoordinates, hexs);
                                selectedFigure.isSelected = false;
                                selectedFigure = null;
                                //try
                                game.activPlayer.hand.RemoveAt(game.activPlayer.GetHandCardIndexFromId(game.playedCard.id));
                                game.activPlayer.RenderHand();
                                game.playedCard = null;
                                playedCardImage.Source = new System.Windows.Media.Imaging.BitmapImage();

                                game.ActionsLeft--;
                                Board.ResetPolygons(hexs);
                                Board.ResetPolygons(hexs);
                            }
                            else if (h != null)
                                if (Hex.CanBeMovedTo(Hex.GetHexByFigure(hexs, selectedFigure), h, game.playedCard.FlyActionValue(), hexs))
                                {
                                    foreach (Hex hex in hexs)
                                    {
                                        hex.inMoveRange = false;
                                        Hex.rangeFigure = null;

                                    }
                                    selectedFigure.MoveTo(h.axialCoordinates, hexs);
                                    selectedFigure.isSelected = false;
                                    selectedFigure = null;
                                    //try
                                    game.activPlayer.hand.RemoveAt(game.activPlayer.GetHandCardIndexFromId(game.playedCard.id));
                                    game.activPlayer.RenderHand();
                                    game.playedCard = null;
                                    playedCardImage.Source = new System.Windows.Media.Imaging.BitmapImage();
                                    game.ActionsLeft--;

                                    Board.ResetPolygons(hexs);
                                    Board.ResetPolygons(hexs);
                                }
                        }
                        else if(selectedFigure!=null && h.figure!=null && game.playedCard!=null)//atak
                        {
                            if (selectedFigure.ReturnFraction() != h.figure.ReturnFraction())
                            {
                                if (game.playedCard.SingleAttackActionValue() > 0)
                                {
                                    Hex selectedHex = Hex.GetHexByFigure(hexs, selectedFigure);
                                    List<Hex> neighbors = Hex.Neighbors(selectedHex, hexs);
                                    foreach (Hex each in neighbors)
                                    {
                                        if (each == h)
                                        {
                                            if (h.figure is Dwarf)
                                            {
                                                Dwarf k = h.figure as Dwarf;
                                                k.hp = k.hp - game.playedCard.SingleAttackActionValue();
                                                selectedFigure.isSelected = false;
                                                selectedFigure = null;
                                                //try
                                                game.activPlayer.hand.RemoveAt(game.activPlayer.GetHandCardIndexFromId(game.playedCard.id));
                                                game.activPlayer.RenderHand();
                                                game.playedCard = null;
                                                game.ActionsLeft--;
                                                playedCardImage.Source = new System.Windows.Media.Imaging.BitmapImage();
                                                foreach(Hex each2 in hexs)
                                                {
                                                    each2.inMoveRange = false;
                                                    Hex.rangeFigure = null;
                                                }

                                                
                                                if (k.hp < 0)
                                                {
                                                    figures.Remove(k);
                                                    Hex.GetHexByFigure(hexs,k).figure = null;

                                                }
                                                Board.ResetPolygons(hexs);
                                                Board.ResetPolygons(hexs); 
                                            }
                                            else
                                                if (h.figure is Dragon)
                                                {
                                                    Dragon d = h.figure as Dragon;
                                                    d.shieldHp = d.shieldHp - game.playedCard.SingleAttackActionValue();

                                                    selectedFigure.isSelected = false;
                                                    selectedFigure = null;
                                                    //try
                                                    game.activPlayer.hand.RemoveAt(game.activPlayer.GetHandCardIndexFromId(game.playedCard.id));
                                                    game.activPlayer.RenderHand();
                                                    game.playedCard = null;
                                                    game.ActionsLeft--;
                                                    playedCardImage.Source = new System.Windows.Media.Imaging.BitmapImage();
                                                    foreach (Hex each2 in hexs)
                                                    {
                                                        each2.inMoveRange = false;
                                                        Hex.rangeFigure = null;
                                                    }


                                                    if (d.shieldHp < 0)
                                                    {
                                                        figures.Remove(d);
                                                        Hex.GetHexByFigure(hexs, d).figure = null;

                                                    }
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
            if(Card.DrawCard(game.activPlayer, 2))
            game.ActionsLeft--;
            //  game.activPlayer.
        }



        private void Image_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            
            Image img = sender as Image;
            int r = Grid.GetRow(img);
            int c = Grid.GetColumn(img);
            int n = r * 4 + c;
            if (game.CardsToDiscard == 0)
            { 
            if (n < game.activPlayer.hand.Count)
            {
                Card card = game.activPlayer.hand[n];
                //game.activPlayer.hand.RemoveAt(n);
                // game.activPlayer.RenderHand();
                game.playedCard = card;
                if (game.playedCard != null)
                {
                    playedCardImage.Source = new System.Windows.Media.Imaging.BitmapImage(
                    new Uri(game.playedCard.src, UriKind.RelativeOrAbsolute));
                }



                // panorama.
                //   panorama.SetValue(Panorama.SelectedIndexProperty, panorama.Items[0]);
                (panorama.Items[1] as PanoramaItem).Visibility = Visibility.Collapsed;
                panorama.SetValue(Panorama.SelectedItemProperty, panorama.Items[(1 + 1) % panorama.Items.Count]);
                panorama.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
                (panorama.Items[1] as PanoramaItem).Visibility = Visibility.Visible;


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
                if (n < game.activPlayer.hand.Count)
                {
                    Card card = game.activPlayer.hand[n];
                    game.activPlayer.hand.RemoveAt(game.activPlayer.GetHandCardIndexFromId(card.id));
                    game.CardsToDiscard--;
                    game.activPlayer.RenderHand();
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




    }
}