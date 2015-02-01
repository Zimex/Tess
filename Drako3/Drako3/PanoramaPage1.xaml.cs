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
using Windows.Storage;

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

        public ClassToSerialize GameToClassToSerialize(Game g)
        {
            ClassToSerialize result=new ClassToSerialize();
            return result;
        }
        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            string gameToLoad=string.Empty;
            if(NavigationContext.QueryString.TryGetValue("gameToLoad",out gameToLoad))
            {
                ClassToSerialize c = new ClassToSerialize();
                c =await  MySerializer<ClassToSerialize>.LoadData(gameToLoad);
                game = new Game(c);
                
                Card.InitiateActionsDictionary();
                panorama.DefaultItem = panoramaItemBoard;
                game.P1.Grid = HandGrid;
                game.P2.Grid = HandGrid;
                game.LibraryImage = libraryImage;
           //     P1 = new Player("Zimex", Fraction.Dragon, HandGrid);
           //     P1.DrawInitialHand();
            //    P2 = new Player("Zaremox", Fraction.Dwarf, HandGrid);
             //   P2.DrawInitialHand();
                
                game.ActivPlayer.RenderHand();
          
              //  leader = new Dwarf(DwarfType.Leader, new Point(1, -2), this);
             //   crossbowman = new Dwarf(DwarfType.Crossbowman, new Point(-2, 1), this);
             //   webber = new Dwarf(DwarfType.Webber, new Point(1, 1), this);
             //   dragon = new Dragon(new Point(0, 0), this);
                figures = new List<Figure>();
                selectedFigure = null;
                figures.Add(game.Leader);
                figures.Add(game.Crossbowman);
                figures.Add(game.Webber);
                figures.Add(game.Dragon);
                leader = game.Leader;
                webber = game.Webber;
                crossbowman = game.Crossbowman;
                dragon = game.Dragon;
                board = new Board(figures);
               // game = new Game(GameType.HOT_SEATS, P1, P2, board, libraryImage, this);
                game.Board = board;
             //   game.Leader = leader;
              //  game.Webber = webber;
              //  game.Crossbowman = crossbowman;
              //  game.Dragon = dragon;
                BoardGrid.Children.Clear();
                game.Board.DrawBoard(BoardGrid);
                BoardGrid.Children.Add(playedCardImage);
                BoardGrid.Children.Add(libraryImage);
                //game.ActionsLeft = 1;

                // game.P1 = P1;
                // game.P2 = P2;
                hexs = game.Board.Hexs;
                if (game.ActivPlayer.Fraction == Fraction.Dragon)
                {
                    libraryImage.Source = new System.Windows.Media.Imaging.BitmapImage(
                        new Uri(@"/Images/Dragon.jpg", UriKind.RelativeOrAbsolute));
                }
                else
                {
                    libraryImage.Source = new System.Windows.Media.Imaging.BitmapImage(
                                           new Uri(@"/Images/Dwarf.jpg", UriKind.RelativeOrAbsolute));
                }
                Board.ResetPolygons(hexs);
                Board.ResetPolygons(hexs);
             //   List<Hex> lines = Hex.GetStraightLines(hexs, Hex.GetHexByPosition(new Point(0, 0), hexs));

           
            }

            
         
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
            P1 = new Player("Zimex", Fraction.Dragon,HandGrid);
            P1.DrawInitialHand();
            P2 = new Player("Zaremox", Fraction.Dwarf,HandGrid);
            P2.DrawInitialHand();
            P1.RenderHand();
            libraryImage.Source = new System.Windows.Media.Imaging.BitmapImage(
                new Uri(@"/Images/Dragon.jpg", UriKind.RelativeOrAbsolute));


            leader = new Dwarf(DwarfType.Leader, new Point(1, -2), this);
            crossbowman = new Dwarf(DwarfType.Crossbowman, new Point(-2, 1), this);
            webber = new Dwarf(DwarfType.Webber, new Point(1, 1), this);
            dragon = new Dragon(new Point(0, 0), this);
            figures = new List<Figure>();
            selectedFigure = null;
            figures.Add(leader);
            figures.Add(crossbowman);
            figures.Add(webber);
            figures.Add(dragon);

            board = new Board(figures);
            game = new Game(GameType.HOT_SEATS, P1, P2, board, libraryImage, this);
            game.Leader = leader;
            game.Webber = webber;
            game.Crossbowman = crossbowman;
            game.Dragon = dragon;
            game.Board.DrawBoard(BoardGrid);
            game.ActionsLeft = 1;

           // game.P1 = P1;
           // game.P2 = P2;
            hexs = game.Board.Hexs;
            List<Hex> lines = Hex.GetStraightLines(hexs, Hex.GetHexByPosition(new Point(0, 0), hexs));
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
                    h.Polygon.Fill = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromArgb(200, 255, 0, 0));
                    h.Polygon.Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromArgb(255, 255, 0, 0));
                }
                else if (figure.GetType() == typeof(Dwarf))
                {
                    Dwarf d = figure as Dwarf;

                    switch (d.Type)
                    {
                        case (DwarfType.Crossbowman):
                            h.Polygon.StrokeThickness = 5;
                            h.Polygon.Fill = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromArgb(200, 255, 255, 0));
                            h.Polygon.Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromArgb(255, 255, 255, 0));

                            break;
                        case (DwarfType.Leader):
                            h.Polygon.StrokeThickness = 5;
                            h.Polygon.Fill = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromArgb(200, 255, 128, 0));
                            h.Polygon.Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromArgb(255, 255, 128, 0));

                            break;
                        case (DwarfType.Webber):
                            h.Polygon.StrokeThickness = 5;
                            h.Polygon.Fill = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromArgb(200, 128, 255, 0));
                            h.Polygon.Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromArgb(255, 128, 255, 0));


                            break;
                    }
                }
            }
        }

        private void panorama_SelectionChanged(object sender, SelectionChangedEventArgs e) //zmiana panorama item
        {
            if (game.ShowHandBeforeTurnChange)
            {

                game.ShowHandBeforeTurnChange = false;
                game.ChangeTurnAfterSlide = true;
                return;
            }
            else
                if (game.ChangeTurnAfterSlide)
                {
                    game.ChangeTurnAfterSlide = false;
                    game.ActionsLeft--;
                }

            if(game.DamageToDragon>0)
            {
                //przejscie spowrotem do zycia smoka
                (panorama.Items[2] as PanoramaItem).Visibility = Visibility.Collapsed;
                    panorama.SetValue(Panorama.SelectedItemProperty, panorama.Items[(1) % panorama.Items.Count]);
                    panorama.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
                    (panorama.Items[2] as PanoramaItem).Visibility = Visibility.Visible;
                    //przejscie do karty smoka i zadnaie obrazen
                    tipTextBlock.Text = "Choose body part to attack to";
                    tipStoryBoard.Begin();

            }
            if(game.CardsToDiscard>0)
            {
                if(game.CardsToDiscard==1)
                tipTextBlock.Text = "Choose 1 card to discard";
                else
                    tipTextBlock.Text = "Choose "+game.CardsToDiscard+" cards to discard";

                tipStoryBoard.Begin();
            }
        }

        private void PanoramaItem_Tap(object sender, System.Windows.Input.GestureEventArgs e) //tap na polu
        {
            
            Point p = e.GetPosition(null);

            
            //if (this.Orientation == PageOrientation.LandscapeLeft)
            //{
            //    if (p.X >= game.Board.ScreenSize.Width - 30 && p.X <= game.Board.ScreenSize.Width - 5 && p.Y >= 5 && p.Y <= 25)
            //    {

            //        game.Board.ChangeActivePlayer(game.P1.ReturnActiveFraction());
            //        game.ChangeTurn();
            //        if (game.P1.ReturnActiveFraction() == Fraction.Dragon)
            //        {
            //            libraryImage.Source = new System.Windows.Media.Imaging.BitmapImage(
            //    new Uri(@"/Images/Dragon.jpg", UriKind.RelativeOrAbsolute));
            //        }
            //        else
            //        {
            //            libraryImage.Source = new System.Windows.Media.Imaging.BitmapImage(
            //    new Uri(@"/Images/Dwarf.jpg", UriKind.RelativeOrAbsolute));
            //        }


            //        return;
            //    }
            //}
            //else if (this.Orientation == PageOrientation.LandscapeRight)
            //{
            //    if (p.X >= 5 && p.X <= 25 && p.Y >= game.Board.ScreenSize.Height - 30 && p.Y <= game.Board.ScreenSize.Height - 5)
            //    {

            //        game.Board.ChangeActivePlayer(game.P1.ReturnActiveFraction());
            //        game.ChangeTurn();
            //        if (game.P1.ReturnActiveFraction() == Fraction.Dragon)
            //        {
            //            libraryImage.Source = new System.Windows.Media.Imaging.BitmapImage(
            //    new Uri(@"/Images/Dragon.jpg", UriKind.RelativeOrAbsolute));
            //        }
            //        else
            //        {
            //            libraryImage.Source = new System.Windows.Media.Imaging.BitmapImage(
            //    new Uri(@"/Images/Dwarf.jpg", UriKind.RelativeOrAbsolute));
            //        }

            //        return;
            //    }
            //}

            p = game.Board.ConvertPhonePointToBoardPoint(p, this.Orientation);
            Hex h = Hex.PixelToHex(p, hexs);
           
            if (h != null)
            {
                if (selectedFigure == null && game.playedCard != null) //zaznaczenie figury
                {

                    if (h.Figure != null) //jesli jest tam figura
                    {
                        if (game.DoubleMove && game.WasMovedInDouble == h.Figure)
                            return;
                        if (game.Board.ActiveFraction == h.Figure.ReturnFraction()) //jesli zaznaczamy swoją figure
                        {
                            selectedFigure = h.Figure;
                            SelectFigure(h);
                            MoveOrFlyRange();
                            BoltRange(h);

                        }
                    }
                }

                else
                    if (selectedFigure == h.Figure && selectedFigure != null)//odznaczenie
                    {
                        UnSelect(h);

                    }
                    else if (game.DoubleAttack) //drugi atak
                    {
                        SecondAttack(h);

                    }
                    else if (game.DoubleMove)//drugi ruch
                    {
                        SecondMove(h);

                    }
                    else
                        if (h.Figure == null && selectedFigure != null && game.playedCard != null) //ruch lub ogien
                        {

                            if (Hex.CanBeMovedTo(Hex.GetHexByFigure(hexs, selectedFigure), h, game.playedCard.SingleMoveActionValue(), hexs))
                            {
                                SingleMove(h);

                            }
                            else
                                if (Hex.CanBeMovedTo(Hex.GetHexByFigure(hexs, selectedFigure), h, game.playedCard.DoubleMoveActionValue(), hexs)) //podwojny ruch
                                {

                                    FirstMove(h);

                                }
                                else if (Hex.CanBeMovedTo(Hex.GetHexByFigure(hexs, selectedFigure), h, game.playedCard.FlyActionValue(), hexs))
                                {
                                    Fly(h);
                                }
                                else if (game.playedCard.FireActionValue() > 0)
                                {
                                    Fire(h);

                                }
                                //{
                                //    if (Hex.CanBeMovedTo(Hex.GetHexByFigure(hexs, selectedFigure), h, game.playedCard.FlyActionValue(), hexs))
                                //    {
                                //        Fly(h);

                                //    }
                                //    else if(game.playedCard.FireActionValue()>0)
                                //    {
                                //        Fire(h);
                                //    }
                                //}
                        }
                        else if (selectedFigure != null && h.Figure != null && game.playedCard != null)//atak
                        {
                            if (selectedFigure.ReturnFraction() != h.Figure.ReturnFraction()) //przeciwna figura wskazana
                            {
                                if (game.playedCard.DoubleAttackActionValue() > 0)//double attack pierwszy
                                {
                                    FirstAttack(h);
                                }
                                else if(game.playedCard.FireActionValue()>0 && game.ActivPlayer.Fraction==Fraction.Dragon)//ogien
                                {
                                    Fire(h);
                                }
                                else
                                    if (game.playedCard.BoltActionValue() > 0)//bolt
                                    {
                                        Bolt(h, BoltRange(Hex.GetHexByFigure(hexs,selectedFigure)));
                                    }
                                else
                                    if (game.playedCard.SingleAttackActionValue() > 0)//single attack
                                    {
                                        SingleAttack(h);
                                    }
                            }
                        }
                       

            }

        }

      

        private void libraryImage_Tap(object sender, System.Windows.Input.GestureEventArgs e) //Draw
        {
            if (game.DoubleMove || game.DoubleAttack)
                return;
            if (Card.DrawCard(game.ActivPlayer, 2))
            {
                // game.ActionsLeft--;
                //this.State[""]
                
                //game.ShowHandBeforeTurnChange = true;
                if (game.ActionsLeft == 1) //przejscie tury
                {
                    game.ShowHandBeforeTurnChange = true;
                    (panorama.Items[2] as PanoramaItem).Visibility = Visibility.Collapsed;
                    panorama.SetValue(Panorama.SelectedItemProperty, panorama.Items[(3) % panorama.Items.Count]);
                    panorama.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
                    (panorama.Items[2] as PanoramaItem).Visibility = Visibility.Visible;
                }
                else
                {
                  //  game.ActionsLeft--;

                    (panorama.Items[2] as PanoramaItem).Visibility = Visibility.Collapsed;
                    panorama.SetValue(Panorama.SelectedItemProperty, panorama.Items[(3) % panorama.Items.Count]);
                    panorama.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
                    (panorama.Items[2] as PanoramaItem).Visibility = Visibility.Visible;
                }
                game.playedCard = null;
                playedCardImage.Source = new System.Windows.Media.Imaging.BitmapImage();
            }
            //game.ActionsLeft--;
            //game.playedCard = null;
            //playedCardImage.Source = new System.Windows.Media.Imaging.BitmapImage();
            //if (selectedFigure != null)
            //    selectedFigure.IsSelected = false;
            //selectedFigure = null;
            //foreach (Hex each2 in hexs)
            //{
            //    each2.InMoveRange = false;
            //    Hex.rangeFigure = null;
            //}
            //Board.ResetPolygons(hexs);
            //Board.ResetPolygons(hexs);
            AfterCardPlay();

        }



        private void Image_Tap(object sender, System.Windows.Input.GestureEventArgs e) //wybranie karty z reki
        {
            if (game.DoubleMove)
                return;
            if (game.ChangeTurnAfterSlide && game.CardsToDiscard == 0)
            {
                //return;
                game.ChangeTurnAfterSlide = false;
                game.ActionsLeft--;

            }
            Image img = sender as Image;
            int r = Grid.GetRow(img);
            int c = Grid.GetColumn(img);
            int n = r * 4 + c;
            if (game.CardsToDiscard == 0)
            {
                if (n < game.ActivPlayer.Hand.Count)
                {
                    Card card = game.ActivPlayer.Hand[n];

                    game.playedCard = card;
                    if (game.playedCard != null)
                    {
                        playedCardImage.Source = new System.Windows.Media.Imaging.BitmapImage(
                        new Uri(game.playedCard.Src, UriKind.RelativeOrAbsolute));
                    }

                    (panorama.Items[3] as PanoramaItem).Visibility = Visibility.Collapsed;
                    panorama.SetValue(Panorama.SelectedItemProperty, panorama.Items[(2) % panorama.Items.Count]);
                    panorama.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
                    (panorama.Items[3] as PanoramaItem).Visibility = Visibility.Visible;
                    if (board.ActiveFraction == Fraction.Dragon)
                    {
                        foreach (Hex h in hexs)
                        {
                            if (h.Figure is Dragon)
                            {
                                selectedFigure = h.Figure;
                                SelectFigure(h);
                                MoveOrFlyRange();
                                FireRange(h);
                                

                            }
                        }

                    }

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
                    //if (game.CardsToDiscard > 0)
                    //{
                    //    (panorama.Items[2] as PanoramaItem).Visibility = Visibility.Collapsed;
                    //    panorama.SetValue(Panorama.SelectedItemProperty, panorama.Items[(3) % panorama.Items.Count]);
                    //    panorama.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
                    //    (panorama.Items[2] as PanoramaItem).Visibility = Visibility.Visible;
                    //}
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

        

        private async void PhoneApplicationPage_BackKeyPress(object sender, System.ComponentModel.CancelEventArgs e)
        {
            
           // e.Cancel = true;
           // ClassToSerialize c=new ClassToSerialize(game);
         //   await MySerializer<ClassToSerialize>.SaveData(c, "current");
            //c.I=6;
            //await MySerializer<ClassToSerialize>.SaveData(c, "current");
            ////await MySerializer<Game>.SaveData(game, "current.sav");
            //ClassToSerialize d =await MySerializer<ClassToSerialize>.LoadData("current");
         //   Game b = new Game();
            //await MySerializer<Hex>.SaveData(game.board.Hexs[0], "current");

           // Hex d = new Hex();
           // d = await MySerializer<Hex>.LoadData("current");
            NavigationService.Navigate(new Uri("/GameMenu.xaml", UriKind.Relative));

        }
       async protected  override void OnNavigatingFrom(NavigatingCancelEventArgs e)
{
    
 	 base.OnNavigatingFrom(e);
} 

        private void DragonImage_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            int w = (int)DragonImage.Width;
            int h = (int)DragonImage.Height;
            int r = 0;
            Point p = e.GetPosition(sender as System.Windows.Controls.Image);
            if (game.DamageToDragon > 0)
            {
                if (p.X > 0 && p.X < 0.18 * w && p.Y > 0.38 * h && p.Y < 0.88 * h && game.Dragon.WingsHp > 0 && game.Dragon.ShieldHp == 0)  //skrzydla
                {
                     r=game.Dragon.PutDamageOnWings(game.DamageToDragon);
                    if(game.Dragon.IsDead())
                    {
                        game.Dragon = null;
                    }
                    //game.Dragon.WingsHp--;
                    //// if(dragon.WingsHp==0 && dragon.ShieldHp==0 && dragon.LegsdHp==0 && dragon.FireHp==0)
                    //if (game.Dragon.IsDead())
                    //{
                    //    //usun smoka
                    //    figures.Remove(game.Dragon);
                    //    Hex.GetHexByFigure(hexs, game.Dragon).Figure = null;


                    //}
                    //game.DamageToDragon--;
                    //if (game.DamageToDragon == 0 && game.ActionsLeft == 0)
                    //    game.ChangeTurn();

                    //MessageBox.Show("Wings");

                }
                else
                    if (p.X > 0.38 * w && p.X < 0.88 * w && p.Y > 0.77 * h && p.Y < h && game.Dragon.LegsHp > 0 && game.Dragon.ShieldHp == 0)  //Nogi
                    {
                         r = game.Dragon.PutDamageOnLegs(game.DamageToDragon);
                         if (game.Dragon.IsDead())
                         {
                             game.Dragon = null;
                         }
                        //game.Dragon.LegsHp--;
                        //if (game.Dragon.IsDead())
                        //{
                        //    //usun smoka
                        //    figures.Remove(game.Dragon);
                        //    Hex.GetHexByFigure(hexs, game.Dragon).Figure = null;


                        //}
                        //game.DamageToDragon--;
                        //if (game.DamageToDragon == 0 && game.ActionsLeft == 0)
                        //    game.ChangeTurn();
                        ////  MessageBox.Show("Legs");
                    }
                    else
                        if (p.X > 0.82 * w && p.X < w && p.Y > 0.38 * h && p.Y < 0.88 * h && game.Dragon.ShieldHp == 0 && game.Dragon.FireHp > 0)  //Ogien
                        {
                             r = game.Dragon.PutDamageOnFire(game.DamageToDragon);
                             if (game.Dragon.IsDead())
                             {
                                 game.Dragon = null;
                             }
                            //game.Dragon.FireHp--;
                            //if (game.Dragon.IsDead())
                            //{
                            //    //usun smoka
                            //    figures.Remove(game.Dragon);
                            //    Hex.GetHexByFigure(hexs, game.Dragon).Figure = null;


                            //}
                            //game.DamageToDragon--;
                            //if (game.DamageToDragon == 0 && game.ActionsLeft == 0)
                            //    game.ChangeTurn();
                            ////   MessageBox.Show("Fire");
                        }
            }
            game.DamageToDragon = r;
            if(r>0)
            {


            }

        }
        public void SingleAttack(Hex h)
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
                        Hex kHex = Hex.GetHexByFigure(hexs, k);
                        k.PutDamageOnDwarf(game.playedCard.SingleAttackActionValue(),figures,hexs);
                       
                      // Hex kHex= Hex.GetHexByFigure(hexs,k);
                       damage.Margin = new Thickness(kHex.Center.X, kHex.Center.Y, 0, 0);
                       Canvas.SetZIndex(this.damage, 80);
                        
                        damageStoryBoard.Begin();
                       // k.Hp = k.Hp - game.playedCard.SingleAttackActionValue();

                        AfterCardPlay();
                        //selectedFigure.IsSelected = false;
                        //selectedFigure = null;
                        ////try
                        //game.ActivPlayer.Hand.RemoveAt(game.ActivPlayer.GetHandCardIndexFromId(game.playedCard.Id));
                        //game.ActivPlayer.RenderHand();
                        //game.playedCard = null;
                        //game.ActionsLeft--;
                        //playedCardImage.Source = new System.Windows.Media.Imaging.BitmapImage();
                        //foreach (Hex each2 in hexs)
                        //{
                        //    each2.InMoveRange = false;
                        //    Hex.rangeFigure = null;
                        //}


                        //if (k.Hp < 0)
                        //{
                        //    figures.Remove(k);
                        //    Hex.GetHexByFigure(hexs, k).Figure = null;

                        //}
                        
                    }
                    else
                        if (h.Figure is Dragon) //atak w smoka
                        {
                            Dragon d = h.Figure as Dragon;
                            d.PutDamageOnDragon(game.playedCard.SingleAttackActionValue(), this, game);
                            AfterCardPlay();
                            //if (d.ShieldHp > 0)
                            //{
                            //    int r=d.PutDamageOnShield(game.playedCard.SingleAttackActionValue());
                            //    game.DamageToDragon = r;

                            //    //d.ShieldHp = d.ShieldHp - game.playedCard.SingleAttackActionValue();

                            //    //selectedFigure.IsSelected = false;
                            //    //selectedFigure = null;
                            //    ////try
                            //    //game.ActivPlayer.Hand.RemoveAt(game.ActivPlayer.GetHandCardIndexFromId(game.playedCard.Id));
                            //    //game.ActivPlayer.RenderHand();
                            //    //game.playedCard = null;
                            //    //game.ActionsLeft--;
                            //    //playedCardImage.Source = new System.Windows.Media.Imaging.BitmapImage();
                            //    //foreach (Hex each2 in hexs)
                            //    //{
                            //    //    each2.InMoveRange = false;
                            //    //    Hex.rangeFigure = null;
                            //    //}
                            //    AfterCardPlay();
                            //    if(r>0)
                            //    {
                            //    (panorama.Items[2] as PanoramaItem).Visibility = Visibility.Collapsed;
                            //    panorama.SetValue(Panorama.SelectedItemProperty, panorama.Items[(1) % panorama.Items.Count]);
                            //    panorama.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
                            //    (panorama.Items[2] as PanoramaItem).Visibility = Visibility.Visible;
                            //    }
                            //}
                            //else //if (d.ShieldHp > 0 && game.playedCard.SingleAttackActionValue() > d.ShieldHp)
                            //{
                            //    game.DamageToDragon = game.playedCard.SingleAttackActionValue();
                            //   // d.ShieldHp = 0;
                            //    //selectedFigure.IsSelected = false;
                            //    //selectedFigure = null;
                            //    ////try
                            //    //game.ActivPlayer.Hand.RemoveAt(game.ActivPlayer.GetHandCardIndexFromId(game.playedCard.Id));
                            //    //game.ActivPlayer.RenderHand();
                            //    //game.playedCard = null;
                            //    //game.ActionsLeft--;
                            //    //playedCardImage.Source = new System.Windows.Media.Imaging.BitmapImage();
                            //    //foreach (Hex each2 in hexs)
                            //    //{
                            //    //    each2.InMoveRange = false;
                            //    //    Hex.rangeFigure = null;
                            //    //}
                            //    AfterCardPlay();
                            //    (panorama.Items[2] as PanoramaItem).Visibility = Visibility.Collapsed;
                            //    panorama.SetValue(Panorama.SelectedItemProperty, panorama.Items[(1) % panorama.Items.Count]);
                            //    panorama.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
                            //    (panorama.Items[2] as PanoramaItem).Visibility = Visibility.Visible;
                            //    //przejscie do karty smoka i zadnaie obrazen
                            //}
                            ////else if (d.ShieldHp == 0)//przejscie do karty smoka i nadanie obrazen
                            ////{
                            ////    game.DamageToDragon = game.playedCard.SingleAttackActionValue();

                            ////    //selectedFigure.IsSelected = false;
                            ////    //selectedFigure = null;
                            ////    ////try
                            ////    //game.ActivPlayer.Hand.RemoveAt(game.ActivPlayer.GetHandCardIndexFromId(game.playedCard.Id));
                            ////    //game.ActivPlayer.RenderHand();
                            ////    //game.playedCard = null;
                            ////    //game.ActionsLeft--;
                            ////    //playedCardImage.Source = new System.Windows.Media.Imaging.BitmapImage();
                            ////    //foreach (Hex each2 in hexs)
                            ////    //{
                            ////    //    each2.InMoveRange = false;
                            ////    //    Hex.rangeFigure = null;
                            ////    //}
                            ////    AfterCardPlay();
                            ////    (panorama.Items[2] as PanoramaItem).Visibility = Visibility.Collapsed;
                            ////    panorama.SetValue(Panorama.SelectedItemProperty, panorama.Items[(1) % panorama.Items.Count]);
                            ////    panorama.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
                            ////    (panorama.Items[2] as PanoramaItem).Visibility = Visibility.Visible;

                            ////}



                            ////Board.ResetPolygons(hexs);
                            ////Board.ResetPolygons(hexs);

                        }

                    break;
                }
            }
        }
        public void FirstMove(Hex h)
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
            game.DoubleMove = true;
            Board.ResetPolygons(hexs);
            Board.ResetPolygons(hexs);
        }
        public void SecondMove(Hex h)
        {
            selectedFigure.MoveTo(h.AxialCoordinates, hexs);
            AfterCardPlay();

            //foreach (Hex hex in hexs)
            //{
            //    hex.InMoveRange = false;
            //    Hex.rangeFigure = null;

            //}
            //selectedFigure.IsSelected = false;
            //selectedFigure = null;
            //game.WasMovedInDouble = null;
            ////try
            //game.ActivPlayer.Hand.RemoveAt(game.ActivPlayer.GetHandCardIndexFromId(game.playedCard.Id));
            //game.ActivPlayer.RenderHand();
            //game.playedCard = null;
            //playedCardImage.Source = new System.Windows.Media.Imaging.BitmapImage();

            //game.ActionsLeft--;
            //game.DoubleMove = false;
            //Board.ResetPolygons(hexs);
            //Board.ResetPolygons(hexs);
        }

        public void FirstAttack(Hex h)
        {
            Dragon d = h.Figure as Dragon;
            game.DoubleAttack = true;
            game.WasAttackingInDouble = selectedFigure;
            d.PutDamageOnDragon(game.playedCard.DoubleAttackActionValue(), this, game);
           // AfterCardPlay();
            //if (d.ShieldHp >= game.playedCard.SingleAttackActionValue())
            //{
            //    d.ShieldHp = d.ShieldHp - game.playedCard.SingleAttackActionValue();

            //    selectedFigure.IsSelected = false;
            //    selectedFigure = null;

            //    foreach (Hex each2 in hexs)
            //    {
            //        each2.InMoveRange = false;
            //        Hex.rangeFigure = null;
            //    }
            //}
            //else if (d.ShieldHp > 0 && game.playedCard.SingleAttackActionValue() > d.ShieldHp)
            //{
            //    game.DamageToDragon = game.playedCard.SingleAttackActionValue() - d.ShieldHp;
            //    d.ShieldHp = 0;
            //    selectedFigure.IsSelected = false;
            //    selectedFigure = null;

            //    foreach (Hex each2 in hexs)
            //    {
            //        each2.InMoveRange = false;
            //        Hex.rangeFigure = null;
            //    }
            //    (panorama.Items[2] as PanoramaItem).Visibility = Visibility.Collapsed;
            //    panorama.SetValue(Panorama.SelectedItemProperty, panorama.Items[(1) % panorama.Items.Count]);
            //    panorama.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
            //    (panorama.Items[2] as PanoramaItem).Visibility = Visibility.Visible;
            //    //przejscie do karty smoka i zadnaie obrazen
            //}
            //else if (d.ShieldHp == 0)//przejscie do karty smoka i nadanie obrazen
            //{
            //    selectedFigure.IsSelected = false;
            //    selectedFigure = null;

            //    foreach (Hex each2 in hexs)
            //    {
            //        each2.InMoveRange = false;
            //        Hex.rangeFigure = null;
            //    }
            //    game.DamageToDragon = game.playedCard.SingleAttackActionValue();
            //    (panorama.Items[2] as PanoramaItem).Visibility = Visibility.Collapsed;
            //    panorama.SetValue(Panorama.SelectedItemProperty, panorama.Items[(1) % panorama.Items.Count]);
            //    panorama.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
            //    (panorama.Items[2] as PanoramaItem).Visibility = Visibility.Visible;

            //}



            Board.ResetPolygons(hexs);
            Board.ResetPolygons(hexs);
        }
        private void Bolt(Hex h,List<Hex> boltlines)
        {
            if(h.Figure is Dragon && boltlines.Contains(h))
            {
                Dragon d = h.Figure as Dragon;
                //if(d.ShieldHp>0)
                //{
                //    int r = d.PutDamageOnShield(game.playedCard.BoltActionValue());

                //}
                d.PutDamageOnDragon(game.playedCard.BoltActionValue(),this,game);

                AfterCardPlay(); 
            }
            
            //Dwarf d = hex.Figure as Dwarf;
           // d.PutDamageOnDwarf(game.playedCard.FireActionValue(), figures, hexs);
        }
        public void Fire(Hex h)
        {
            Hex selectedHex = Hex.GetHexByFigure(hexs, selectedFigure);
            List<Hex> lines=Hex.GetStraightLines(hexs, selectedHex);
            List<Hex> linesInFire = new List<Hex>();


            if (lines.Contains(h))
            {
                Point selectedPoint = h.AxialCoordinates;
                Point currentPoint = selectedHex.AxialCoordinates;

                int xDiff = (int)(selectedPoint.X - currentPoint.X);
                int yDiff = (int)(selectedPoint.Y - currentPoint.Y);
                if (selectedPoint != currentPoint)
                {
                    if (xDiff == 0)
                    {
                        if (yDiff > 0)
                        {
                            for (int i = 1; i <= 4; i++)
                            {
                                if (lines.Contains(Hex.GetHexByAxialCoordinates(hexs, new Point(currentPoint.X, currentPoint.Y + (double)i))))
                                    linesInFire.Add(Hex.GetHexByAxialCoordinates(hexs, new Point(currentPoint.X, currentPoint.Y + (double)i)));
                            }
                        }
                        if (yDiff < 0)
                        {
                            for (int i = -1; i >= -4; i--)
                            {
                                if (lines.Contains(Hex.GetHexByAxialCoordinates(hexs, new Point(currentPoint.X, currentPoint.Y + (double)i))))
                                    linesInFire.Add(Hex.GetHexByAxialCoordinates(hexs, new Point(currentPoint.X, currentPoint.Y + (double)i)));
                            }

                        }
                    }
                    else

                        if (yDiff == 0)
                        {
                            if (xDiff > 0)
                            {
                                for (int i = 1; i <= 4; i++)
                                {
                                    if (lines.Contains(Hex.GetHexByAxialCoordinates(hexs, new Point(currentPoint.X + (double)i, currentPoint.Y))))
                                        linesInFire.Add(Hex.GetHexByAxialCoordinates(hexs, new Point(currentPoint.X + (double)i, currentPoint.Y)));
                                }
                            }
                            if (xDiff < 0)
                            {
                                for (int i = -1; i >= -4; i--)
                                {
                                    if (lines.Contains(Hex.GetHexByAxialCoordinates(hexs, new Point(currentPoint.X + (double)i, currentPoint.Y))))
                                        linesInFire.Add(Hex.GetHexByAxialCoordinates(hexs, new Point(currentPoint.X + (double)i, currentPoint.Y)));
                                }

                            }
                        }

                        else
                        {
                            if (xDiff == -yDiff)
                            {
                                if (xDiff > 0)
                                {
                                    for (int i = 1; i <= 4; i++)
                                    {
                                        if (lines.Contains(Hex.GetHexByAxialCoordinates(hexs, new Point(currentPoint.X + (double)i, currentPoint.Y - (double)i))))
                                            linesInFire.Add(Hex.GetHexByAxialCoordinates(hexs, new Point(currentPoint.X + (double)i, currentPoint.Y - (double)i)));
                                    }
                                }
                                else
                                    if (yDiff > 0)
                                    {
                                        for (int i = 1; i <= 4; i++)
                                        {
                                            if (lines.Contains(Hex.GetHexByAxialCoordinates(hexs, new Point(currentPoint.X - (double)i, currentPoint.Y + (double)i))))
                                                linesInFire.Add(Hex.GetHexByAxialCoordinates(hexs, new Point(currentPoint.X - (double)i, currentPoint.Y + (double)i)));
                                        }

                                    }
                            }
                        }
                }


            }
            else
                return;
            foreach(Hex hex in linesInFire)
            {
                if(hex.Figure is Dwarf)
                {
                    Dwarf d = hex.Figure as Dwarf;
                    d.PutDamageOnDwarf(game.playedCard.FireActionValue(), figures, hexs);
                }
            }
            AfterCardPlay();

        }
        public void FireRange(Hex h)
        {
            if (game.playedCard.FireActionValue() > 0)
            {
                List<Hex> lines = Hex.GetStraightLines(hexs, h);
                foreach (Hex hex in lines)
                {
                    hex.InMoveRange = true;


                }
                Hex.rangeFigure = h.Figure;
                Board.ResetPolygons(hexs);
                Board.ResetPolygons(hexs);
                
            }
        }
        private List<Hex> BoltRange(Hex h)
        {
            List<Hex> lines = Hex.GetStraightLines(hexs, h);
            List<Hex> boltLines = new List<Hex>(lines);
            if (game.playedCard.BoltActionValue() > 0)
            {
                //Point selectedPoint=Hex.GetHexByFigure(hexs,selectedFigure).AxialCoordinates;
                Point currentPoint=h.AxialCoordinates;
 
                
                
                foreach(Hex hex in lines)
                {
                    if(hex.AxialCoordinates!=currentPoint)
                    {
                        if (hex.Figure != null)
                        {
                            Point selectedPoint = hex.AxialCoordinates;
                            int xDiff = (int)(selectedPoint.X-currentPoint.X);
                            int yDiff = (int)(selectedPoint.Y-currentPoint.Y);
                            if (selectedPoint != currentPoint)
                            {
                                if (xDiff == 0)
                                {
                                    if (yDiff > 0)
                                    {
                                        for (int i = 1; i <= 4; i++)
                                        {
                                            if (Hex.GetHexByAxialCoordinates(hexs, new Point(selectedPoint.X, selectedPoint.Y)).Figure != null)
                                            {
                                                try
                                                {
                                                    boltLines.Remove(Hex.GetHexByAxialCoordinates(hexs, new Point(selectedPoint.X, selectedPoint.Y + (double)i)));
                                                    boltLines.Remove(Hex.GetHexByAxialCoordinates(hexs, new Point(selectedPoint.X, selectedPoint.Y + (double)i + 1)));
                                                    boltLines.Remove(Hex.GetHexByAxialCoordinates(hexs, new Point(selectedPoint.X, selectedPoint.Y + (double)i + 2)));

                                                }
                                                catch(Exception ex)
                                                {
                                                    break;
                                                }
                                            }
                                            else
                                                break;
                                            //if (lines.Contains(Hex.GetHexByAxialCoordinates(hexs, new Point(currentPoint.X, currentPoint.Y + (double)i))))
                                            //linesInFire.Add(Hex.GetHexByAxialCoordinates(hexs, new Point(currentPoint.X, currentPoint.Y + (double)i)));
                                        }
                                    }
                                    if (yDiff < 0)
                                    {


                                        for (int i = -1; i >= -4; i--)
                                        {
                                            if (Hex.GetHexByAxialCoordinates(hexs, new Point(selectedPoint.X, selectedPoint.Y)).Figure != null)
                                            {
                                                try
                                                {
                                                    boltLines.Remove(Hex.GetHexByAxialCoordinates(hexs, new Point(selectedPoint.X, selectedPoint.Y + (double)i)));
                                                    boltLines.Remove(Hex.GetHexByAxialCoordinates(hexs, new Point(selectedPoint.X, selectedPoint.Y + (double)i - 1)));
                                                    boltLines.Remove(Hex.GetHexByAxialCoordinates(hexs, new Point(selectedPoint.X, selectedPoint.Y + (double)i - 2)));

                                                }
                                                catch (Exception ex)
                                                {
                                                    break;
                                                }
                                            }
                                            else
                                                break;
                                        }

                                    }
                                }
                                else

                                    if (yDiff == 0)
                                    {
                                        if (xDiff > 0)
                                        {
                                            for (int i = 1; i <= 4; i++)
                                            {
                                                if (Hex.GetHexByAxialCoordinates(hexs, new Point(selectedPoint.X, selectedPoint.Y)).Figure != null)
                                                {
                                                    try
                                                    {
                                                        boltLines.Remove(Hex.GetHexByAxialCoordinates(hexs, new Point(selectedPoint.X + (double)i, selectedPoint.Y)));
                                                        boltLines.Remove(Hex.GetHexByAxialCoordinates(hexs, new Point(selectedPoint.X + (double)i + 1, selectedPoint.Y)));
                                                        boltLines.Remove(Hex.GetHexByAxialCoordinates(hexs, new Point(selectedPoint.X + (double)i + 2, selectedPoint.Y)));

                                                    }
                                                    catch (Exception ex)
                                                    {
                                                        break;
                                                    }
                                                }
                                                else
                                                    break;
                                            }
                                        }
                                        if (xDiff < 0)
                                        {
                                            for (int i = -1; i >= -4; i--)
                                            {
                                                if (Hex.GetHexByAxialCoordinates(hexs, new Point(selectedPoint.X, selectedPoint.Y)).Figure != null)
                                                {
                                                    try
                                                    {
                                                        boltLines.Remove(Hex.GetHexByAxialCoordinates(hexs, new Point(selectedPoint.X + (double)i, selectedPoint.Y)));
                                                        boltLines.Remove(Hex.GetHexByAxialCoordinates(hexs, new Point(selectedPoint.X + (double)i - 1, selectedPoint.Y)));
                                                        boltLines.Remove(Hex.GetHexByAxialCoordinates(hexs, new Point(selectedPoint.X + (double)i - 2, selectedPoint.Y)));

                                                    }
                                                    catch (Exception ex)
                                                    {
                                                        break;
                                                    }
                                                }
                                                else
                                                    break;
                                            }

                                        }
                                    }

                                    else
                                    {
                                        if (xDiff == -yDiff)
                                        {
                                            if (xDiff > 0)
                                            {
                                                for (int i = 1; i <= 4; i++)
                                                {
                                                    if (Hex.GetHexByAxialCoordinates(hexs, new Point(selectedPoint.X, selectedPoint.Y)).Figure != null)
                                                    {
                                                        try
                                                        {
                                                            boltLines.Remove(Hex.GetHexByAxialCoordinates(hexs, new Point(selectedPoint.X + (double)i, selectedPoint.Y - (double)i + 1)));
                                                            boltLines.Remove(Hex.GetHexByAxialCoordinates(hexs, new Point(selectedPoint.X + (double)i + 1, selectedPoint.Y - (double)i + 2)));
                                                            boltLines.Remove(Hex.GetHexByAxialCoordinates(hexs, new Point(selectedPoint.X + (double)i + 2, selectedPoint.Y - (double)i + 3)));

                                                        }
                                                        catch (Exception ex)
                                                        {
                                                            break;
                                                        }
                                                    }
                                                    else
                                                        break;
                                                }
                                            }
                                            else
                                                if (yDiff > 0)
                                                {
                                                    for (int i = 1; i <= 4; i++)
                                                    {
                                                        if (Hex.GetHexByAxialCoordinates(hexs, new Point(selectedPoint.X, selectedPoint.Y)).Figure != null)
                                                        {
                                                            try
                                                            {
                                                                boltLines.Remove(Hex.GetHexByAxialCoordinates(hexs, new Point(selectedPoint.X - (double)i, selectedPoint.Y + (double)i + 1)));
                                                                boltLines.Remove(Hex.GetHexByAxialCoordinates(hexs, new Point(selectedPoint.X - (double)i + 1, selectedPoint.Y + (double)i + 2)));
                                                                boltLines.Remove(Hex.GetHexByAxialCoordinates(hexs, new Point(selectedPoint.X - (double)i + 2, selectedPoint.Y + (double)i + 3)));

                                                            }
                                                            catch (Exception ex)
                                                            {
                                                                break;
                                                            }
                                                        }
                                                        else
                                                            break;


                                                    }


                                                }

                                        }
                                    }
                            }
                        }


                       
               
            }
        }
                foreach (Hex hx in boltLines)
                {
                    hx.InMoveRange = true;


                }
                Hex.rangeFigure = h.Figure;
                Board.ResetPolygons(hexs);
                Board.ResetPolygons(hexs);
            }
            return boltLines;

        }
        public void DamageTargetFigure(Figure f ,int dmg)
    {
            if( f is Dwarf)
            {
                Dwarf d = f as Dwarf;
                
            }

    }

         public void AfterCardPlay()
        {


            game.DoubleAttack = false;
            game.DoubleMove = false;
            game.WasAttackingInDouble = null;
            game.WasMovedInDouble = null;
             if(selectedFigure!=null)
            selectedFigure.IsSelected = false;
            selectedFigure = null;
            try
            {


                game.ActivPlayer.Hand.RemoveAt(game.ActivPlayer.GetHandCardIndexFromId(game.playedCard.Id));
            }
             catch(Exception ex)
            {

             }
            game.ActivPlayer.RenderHand();
            game.playedCard = null;
             if(!game.ChangeTurnAfterSlide)
            game.ActionsLeft--;
            playedCardImage.Source = new System.Windows.Media.Imaging.BitmapImage();
            foreach (Hex each2 in hexs)
            {
                each2.InMoveRange = false;
                Hex.rangeFigure = null;
            }
            Board.ResetPolygons(hexs);
            Board.ResetPolygons(hexs);

        }
        public void SecondAttack(Hex h)
        {
            {
                if (game.WasAttackingInDouble == selectedFigure)
                    return;
                game.DoubleAttack = false;
                game.WasAttackingInDouble = null;
                Dragon d = h.Figure as Dragon;
                d.PutDamageOnDragon(game.playedCard.DoubleAttackActionValue(), this, game);
                //AfterCardPlay();
                //if (d.ShieldHp >= game.playedCard.SingleAttackActionValue())
                //{
                //    d.ShieldHp = d.ShieldHp - game.playedCard.SingleAttackActionValue();

                //    //selectedFigure.IsSelected = false;
                //    //selectedFigure = null;
                //    ////try
                //    //game.ActivPlayer.Hand.RemoveAt(game.ActivPlayer.GetHandCardIndexFromId(game.playedCard.Id));
                //    //game.ActivPlayer.RenderHand();
                //    //game.playedCard = null;
                //    //game.ActionsLeft--;
                //    //playedCardImage.Source = new System.Windows.Media.Imaging.BitmapImage();
                //    //foreach (Hex each2 in hexs)
                //    //{
                //    //    each2.InMoveRange = false;
                //    //    Hex.rangeFigure = null;
                //    //}
                //}
                //else if (d.ShieldHp > 0 && game.playedCard.SingleAttackActionValue() > d.ShieldHp)
                //{
                //  //  game.DamageToDragon = game.playedCard.SingleAttackActionValue() - d.ShieldHp;
                //   // d.ShieldHp = 0;
                //    //selectedFigure.IsSelected = false;
                //    //selectedFigure = null;
                //    ////try
                //    //game.ActivPlayer.Hand.RemoveAt(game.ActivPlayer.GetHandCardIndexFromId(game.playedCard.Id));
                //    //game.ActivPlayer.RenderHand();
                //    //game.playedCard = null;
                //    //game.ActionsLeft--;
                //    //playedCardImage.Source = new System.Windows.Media.Imaging.BitmapImage();
                //    //foreach (Hex each2 in hexs)
                //    //{
                //    //    each2.InMoveRange = false;
                //    //    Hex.rangeFigure = null;
                //    //}
                //    (panorama.Items[2] as PanoramaItem).Visibility = Visibility.Collapsed;
                //    panorama.SetValue(Panorama.SelectedItemProperty, panorama.Items[(1) % panorama.Items.Count]);
                //    panorama.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
                //    (panorama.Items[2] as PanoramaItem).Visibility = Visibility.Visible;
                //    //przejscie do karty smoka i zadnaie obrazen
                //}
                //else if (d.ShieldHp == 0)//przejscie do karty smoka i nadanie obrazen
                //{
                //    //selectedFigure.IsSelected = false;
                //    //selectedFigure = null;
                //    ////try
                //    //game.ActivPlayer.Hand.RemoveAt(game.ActivPlayer.GetHandCardIndexFromId(game.playedCard.Id));
                //    //game.ActivPlayer.RenderHand();
                //    //game.playedCard = null;
                //    //game.ActionsLeft--;
                //    //playedCardImage.Source = new System.Windows.Media.Imaging.BitmapImage();
                //    //foreach (Hex each2 in hexs)
                //    //{
                //    //    each2.InMoveRange = false;
                //    //    Hex.rangeFigure = null;
                //    //}
                //    game.DamageToDragon = game.playedCard.SingleAttackActionValue();
                //    (panorama.Items[2] as PanoramaItem).Visibility = Visibility.Collapsed;
                //    panorama.SetValue(Panorama.SelectedItemProperty, panorama.Items[(1) % panorama.Items.Count]);
                //    panorama.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
                //    (panorama.Items[2] as PanoramaItem).Visibility = Visibility.Visible;

                //}


                AfterCardPlay();
               

            }
        }
        public void MoveOrFlyRange()
        {
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
                else if (game.playedCard.DoubleMoveActionValue() > 0)
                {
                    foreach (List<Hex> list in Hex.GetMoveRange(Hex.GetHexByFigure(hexs, selectedFigure), game.playedCard.DoubleMoveActionValue(), hexs))
                    {

                        foreach (Hex eachHex in list)
                        {
                            eachHex.InMoveRange = true;
                            Hex.rangeFigure = selectedFigure;
                        }
                    }
                }
            Board.ResetPolygons(hexs);
            Board.ResetPolygons(hexs);
        }
        public void SingleMove(Hex h)
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
        public void UnSelect(Hex h)
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
        public void Fly(Hex h)
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
}