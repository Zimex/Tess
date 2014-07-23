using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Forms;
using Tesseract;
using System.IO;
using System.Drawing;
using Utilities;
using System.Text.RegularExpressions;
using System.Data.SqlClient;
using System.Runtime.InteropServices;

namespace Tess
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
       // string testImagePath = "./phototest.jpg";
        string testImagePath = @"C:\Users\urbanowicz\documents\visual studio 2013\Projects\Tess\Tess\Images\phototest.jpg";
        string testImagePath1 = @"C:\Users\urbanowicz\documents\visual studio 2013\Projects\Tess\Tess\Images\phototest1.jpg";
        bool isSection=false;
        System.Windows.Point begin, end,imgBegin,imgEnd;
        bool save = false;
       // bool
        private string text;
        public String t
        {
            get;
            set;
        }
        public MainWindow(String s):this()
        {
            t = s;
        }
        public MainWindow()
        {
            InitializeComponent();
            this.WindowState = WindowState.Maximized;
            Patterning p = new Patterning();
            BitmapImage imgdata;
            imgdata=new BitmapImage(new Uri(testImagePath,UriKind.RelativeOrAbsolute));
        
           img.Source=imgdata;
           processedImage.Source = imgdata;
           imageViewer.Content=img;
           processedImageViewer.Content = processedImage;
            MemoryStream ms = new MemoryStream();
            System.Windows.Media.Imaging.BmpBitmapEncoder enc = new BmpBitmapEncoder();
            enc.Frames.Add(BitmapFrame.Create(imgdata));
            enc.Save(ms);
            Bitmap m = new Bitmap(testImagePath);
            Bitmap bm = new Bitmap(ms);
            img.Width = bm.Width;
            img.Height = bm.Height;

            processedImage.Width = bm.Width;
            processedImage.Height = bm.Height;
            ms.Close();

            



        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog ofd=new Microsoft.Win32.OpenFileDialog();
            ofd.Filter = "Image files(*.tif;*.jpg;*.bmp; *.png)|*.tif;*.jpg;*.bmp;*.png";

            if(ofd.ShowDialog()==true)
            {
                string file = ofd.FileName;
                Load(file);

            }
        }
        private void Load(string path)
        {
            BitmapImage bmi = new BitmapImage();
            bmi.BeginInit();
            bmi.UriSource = new Uri(path, UriKind.RelativeOrAbsolute);
            bmi.EndInit();
            img.Source = bmi;
            processedImage.Source = bmi;
            testImagePath = path;
            System.Windows.MessageBox.Show(path);

        }
        private void Save()
        {
         //   BitmapImage bmi = new BitmapImage();
         ////   bmi.BeginInit();
         ////   bmi.UriSource = new Uri(testImagePath, UriKind.RelativeOrAbsolute);
         //  // bmi.CacheOption = BitmapCacheOption.OnDemand;
         ////   bmi.EndInit();
         //   FileStream filestream = new FileStream(@"C:\Users\urbanowicz\Documents\image.png", FileMode.Create);
         // //  PngBitmapEncoder encoder = new PngBitmapEncoder();
         //  // encoder.Frames.Add(BitmapFrame.Create(bmi));
         //  // encoder.Save(filestream);
         //   filestream.Close();
            
           // testImagePath = @"C:\Users\urbanowicz\Documents\image.png";

            
            Bitmap bm = new Bitmap(testImagePath);
            bm = Utilities.Media.Image.ExtensionMethods.BitmapExtensions.Sharpen(bm);
            bm.Save(@"C:\Users\urbanowicz\documents\visual studio 2013\Projects\Tess\Tess\Images\phototest.jpg", System.Drawing.Imaging.ImageFormat.Png);
            bm.Dispose();
            //BitmapImage bmi = new BitmapImage();
            //bmi.BeginInit();
            //bmi.UriSource = new Uri(testImagePath, UriKind.RelativeOrAbsolute);
            //bmi.EndInit();
            /*
            using(MemoryStream ms=new MemoryStream())
            {
            BmpBitmapEncoder enc = new BmpBitmapEncoder();
            enc.Frames.Add(BitmapFrame.Create(bmi));
            enc.Save(ms);
               // bm
                
            }
             * 
            
            BitmapImage result;
            using (MemoryStream stream = new MemoryStream())
            {
                bm.Save(stream, System.Drawing.Imaging.ImageFormat.Bmp);

                stream.Position = 0;
                result = new BitmapImage();
                result.BeginInit();
                // According to MSDN, "The default OnDemand cache option retains access to the stream until the image is needed."
                // Force the bitmap to load right now so we can dispose the stream.
                result.CacheOption = BitmapCacheOption.OnLoad;
                result.StreamSource = stream;
                result.EndInit();
                result.Freeze();
                
            }
            
          //  using (MemoryStream ms = new MemoryStream())
          //  {
          //      BmpBitmapDecoder dec = new BmpBitmapDecoder();
          //      dec.Frames.
          //      enc.Save(ms);
                // bm

            
            /*
            int xx = (int)(ds.x1);
            int yy = (int)(ds.y1);
            int w = (int)(Math.Abs(ds.x1 - ds.x2));
            int h = (int)(Math.Abs(ds.y1 - ds.y2));
            CroppedBitmap cb = new CroppedBitmap((BitmapSource)bi, new Int32Rect(xx, yy, w, h));
             * 
            FileStream filestream = new FileStream(@"C:\Users\urbanowicz\Documents\image.png", FileMode.Create);
            PngBitmapEncoder encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(result));
            encoder.Save(filestream);
            filestream.Close();
            testImagePath = @"C:\Users\urbanowicz\Documents\image.png";
            */
        }
        private void Section()
        {
    

            Bitmap m = new Bitmap(testImagePath);
            System.Windows.MessageBox.Show(m.Width.ToString() + " " + m.Height.ToString());
            CroppedBitmap cbm = new CroppedBitmap((BitmapImage)img.Source, new Int32Rect(0, 0, m.Width, m.Height));
            img.Source = cbm;
            isSection = true;
            
            
           // RenderTargetBitmap rtb = new RenderTargetBitmap((int)img.Source.Width, (int)img.Source.Height, 300d, 300d, PixelFormats.Default);
           // RenderTargetBitmap rtb = new RenderTargetBitmap((int)(1654 * (300 / 96)), (int)(2339 * (300 / 96)), 300d, 300d, PixelFormats.Default);
            RenderTargetBitmap rtb = new RenderTargetBitmap((int)(m.Width * (300 / 96)), (int)(m.Height * (300 / 96)), 300d, 300d, PixelFormats.Default);

            rtb.Render(img);
           // rtb.Render()

            BmpBitmapEncoder encoder = new BmpBitmapEncoder();
            //encoder.Frames.Add(BitmapFrame.Create(rtb));
            encoder.Frames.Add(BitmapFrame.Create(rtb));

          //  FileStream fs = File.Open(@"C:\Users\urbanowicz\Documents\section.png", FileMode.Create);
            //encoder.Save(fs);
            //fs.Close();
            using (var stream = File.Create(@"C:\Users\urbanowicz\Documents\section.png"))
            {
                encoder.Save(stream);
            }

            /*
               MemoryStream mStream= new MemoryStream(); 
	            
	            JpegBitmapEncoder jEncoder= new JpegBitmapEncoder(); 
	 
            jEncoder.Frames.Add(BitmapFrame.Create(cbm));  //the croppedBitmap is a CroppedBitmap object 
	            jEncoder.QualityLevel = 75; 
            jEncoder.Save(mStream); 
 
	            BitmapImage image = new BitmapImage(); 
	            image.BeginInit(); 
	            image.StreamSource = mStream; 
	            image.EndInit(); 
           // FileStream fs=new FileStream("")
            Bitmap bmp;
            using(MemoryStream outStream = new MemoryStream())
            { 
        BitmapEncoder enc = new BmpBitmapEncoder();
        enc.Frames.Add(BitmapFrame.Create(image));
        enc.Save(outStream);
         bmp = new System.Drawing.Bitmap(outStream);
        }
            System.Windows.MessageBox.Show(System.Windows.Application.Current.StartupUri.ToString());
            bmp.Save(System.Windows.Application.Current.StartupUri.ToString());
        */
    
         //   bmp
           // testImagePath
        }

        private void Section(int x, int y, int w, int h)
        {
            Bitmap m = new Bitmap(testImagePath);
            //System.Windows.MessageBox.Show(m.Width.ToString() + " " + m.Height.ToString());
            System.Windows.MessageBox.Show(x.ToString()+" "+y.ToString()+" "+w.ToString()+" "+h.ToString());
            CroppedBitmap cbm = new CroppedBitmap((BitmapImage)img.Source, new Int32Rect(x,y,w,h));
            img.Source = cbm;
            isSection = true;

            RenderTargetBitmap rtb;
            // RenderTargetBitmap rtb = new RenderTargetBitmap((int)img.Source.Width, (int)img.Source.Height, 300d, 300d, PixelFormats.Default);
           // if (incResolutionCheckBox.IsChecked == true)
            if (imageProcessingIncreaseResolutionRadioButton.IsChecked==true)
            {
                rtb = new RenderTargetBitmap((int)(w * (300 * (w / m.Width) / 96)), (int)(h * (300 * (h / m.Height) / 96)), 300d * (w / m.Width), 300d * (h / m.Height), PixelFormats.Default);
                rtb.Render(img);
                BmpBitmapEncoder encoder = new BmpBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(rtb));
                FileStream fs = File.Open(@"C:\Users\urbanowicz\Documents\section.png", FileMode.Create);
                 encoder.Save(fs);
                
                fs.Close();
                testImagePath = @"C:\Users\urbanowicz\Documents\section.png";
            }
            else
            {
               // rtb = new RenderTargetBitmap(w, h, 96d, 96d, PixelFormats.Default);

               // rtb.Render(img);
                // rtb.Render()
                JpegBitmapEncoder enc = new JpegBitmapEncoder();
                enc.Frames.Add(BitmapFrame.Create(cbm));
              //  BmpBitmapEncoder encoder = new BmpBitmapEncoder();
             //   encoder.Frames.Add(BitmapFrame.Create(rtb));

                FileStream fs = File.Open(@"C:\Users\urbanowicz\Documents\section.png", FileMode.Create);
                // encoder.Save(fs);
                enc.Save(fs);
                fs.Close();
            }
        }
        
        public void CreateARectangle(int x, int y , int w, int h)
        {
            // Create a Rectangle
            System.Windows.Shapes.Rectangle blueRectangle = new System.Windows.Shapes.Rectangle();
            blueRectangle.Height = h;
            blueRectangle.Width = w;

            // Create a blue and a black Brush
            SolidColorBrush blueBrush = new SolidColorBrush();
            blueBrush.Color = Colors.Blue;
            SolidColorBrush blackBrush = new SolidColorBrush();
            blackBrush.Color = Colors.Black;

            // Set Rectangle's width and color
            blueRectangle.StrokeThickness = 3;
            blueRectangle.Stroke = blackBrush;
            // Fill rectangle with blue color
            //blueRectangle.Fill = blueBrush;

            // Add Rectangle to the Grid.
           // grid.Children.Add(blueRectangle);
            
        }

        private void startButton_Click(object sender, RoutedEventArgs e)
        {
            if (imageProcessingIncreaseResolutionRadioButton.IsChecked == true)
            {
                Bitmap m = new Bitmap(testImagePath);
                RenderTargetBitmap rtb = new RenderTargetBitmap((int)(m.Width * (300   / 96)), (int)(m.Height * (300   / 96)), 300d , 300d , PixelFormats.Default);
                rtb.Render(img);
                BmpBitmapEncoder encoder = new BmpBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(rtb));
                FileStream fs = File.Open(@"C:\Users\urbanowicz\Documents\image.png", FileMode.Create);
                encoder.Save(fs);

                fs.Close();
                testImagePath = @"C:\Users\urbanowicz\Documents\image.png";
                BitmapImage imgdata;
                imgdata = new BitmapImage(new Uri(testImagePath, UriKind.RelativeOrAbsolute));
                img.Source = imgdata;
            }
            if(imageProcessingSharpening.IsChecked==true)
            {
                Bitmap m = new Bitmap(testImagePath);
               m= Utilities.Media.Image.ExtensionMethods.BitmapExtensions.Sharpen(m);
                m.Save(@"C:\Users\urbanowicz\Documents\image.png",System.Drawing.Imaging.ImageFormat.Png);
                testImagePath = @"C:\Users\urbanowicz\Documents\image.png";
                //m.Dispose();
                BitmapImage imgdata;
                imgdata = new BitmapImage(new Uri(testImagePath, UriKind.RelativeOrAbsolute));
                img.Source = imgdata;
            }
            if (imageProcessingChangeContrast.IsChecked == true)
            {
                Bitmap m = new Bitmap(testImagePath);
               // FileStream fs=new FileStream(@"C:\Users\urbanowicz\Documents\image.png",FileMode.Open);
                //Bitmap m = new Bitmap(fs);
               // fs.Flush();
              //  fs.Dispose();
                m = Utilities.Media.Image.ExtensionMethods.BitmapExtensions.AdjustContrast(m, (float)0.50);
                m.Save(@"C:\Users\urbanowicz\Documents\image.png", System.Drawing.Imaging.ImageFormat.Png);
                testImagePath = @"C:\Users\urbanowicz\Documents\image.png";
                //m.Dispose();
                BitmapImage imgdata;
                imgdata = new BitmapImage(new Uri(testImagePath, UriKind.RelativeOrAbsolute));
                img.Source = imgdata;
                /*
                try
                {
                    if (save == false)
                    {
                        m.Save(@"C:\Users\urbanowicz\Documents\image.png");
                        save = true;
                    }
                    else
                    {
                        m.Save(@"C:\Users\urbanowicz\Documents\image.jpg");
                        save = false;
                    }
                }
                catch
                {
                    System.Windows.MessageBox.Show("image being held open");
                    Bitmap bitmap = new Bitmap(m.Width, m.Height, m.PixelFormat);
                    Graphics g = Graphics.FromImage(bitmap);
                    g.DrawImage(m, new System.Drawing.Point(0, 0));
                    g.Dispose();
                    m.Dispose();
                    bitmap.Save(@"C:\Users\urbanowicz\Documents\image.png");
                    m = bitmap; // preserve clone        
                }
                //Bitmap n = new Bitmap(m);
               // m.Dispose();
              
               // File.Delete(@"C:\Users\urbanowicz\Documents\image.png");
         //       m.Save(@"C:\Users\urbanowicz\Documents\image.png");
               // m.Save(fs, System.Drawing.Imaging.ImageFormat.Png);
                
               // Bitmap n = new Bitmap(m.Width, m.Height, m.PixelFormat);
               // n.LockBits(new System.Drawing.Rectangle(0, 0, n.Width, n.Height), System.Drawing.Imaging.ImageLockMode.ReadWrite, n.PixelFormat);
               // m.LockBits(new System.Drawing.Rectangle(0, 0, m.Width, m.Height), System.Drawing.Imaging.ImageLockMode.ReadWrite, m.PixelFormat);
               //// Marshal.Copy(m.get)
                // m.Dispose(); 
                BitmapImage imgdata;

                if (save == true)
                    {
                        testImagePath = @"C:\Users\urbanowicz\Documents\image.png";
                        imgdata = new BitmapImage(new Uri(@"C:\Users\urbanowicz\Documents\image.png", UriKind.RelativeOrAbsolute));
                        img.Source = imgdata;
                        
                    }
                    else
                    {
                        testImagePath = @"C:\Users\urbanowicz\Documents\image.jpg";
                        imgdata = new BitmapImage(new Uri(@"C:\Users\urbanowicz\Documents\image.png", UriKind.RelativeOrAbsolute));
                        img.Source = imgdata;
                        
                    }
               // testImagePath = @"C:\Users\urbanowicz\Documents\image.png";
                
                
             */
            }

            
            try
            {
                using (var engine = new TesseractEngine(@"./tessdata", "pol", EngineMode.Default))
                {
                    using (var img = Pix.LoadFromFile(testImagePath))
                    {
                        //img=Pix.Create()
                        //int i = 1;
                        using (var page = engine.Process(img))
                        {
                            var text = page.GetText();
                            //System.Windows.MessageBox.Show(text);
                            resultTextBox.Text = text;
                            using (var iter = page.GetIterator())
                            {
                                iter.Begin();
                                do
                                {
                                    do
                                    {
                                        do
                                        {
                                            // MessageBox.Show("word: " + iter.GetText(PageIteratorLevel.Word));
                                        }
                                        while (iter.Next(PageIteratorLevel.TextLine, PageIteratorLevel.Word));
                                      //  MessageBox.Show("line: " + iter.GetText(PageIteratorLevel.TextLine));
                                      //  MessageBox.Show("para: " + iter.GetText(PageIteratorLevel.Para));
                                     //   MessageBox.Show("block: " + iter.GetText(PageIteratorLevel.Block));

                                    }
                                    while (iter.Next(PageIteratorLevel.Para, PageIteratorLevel.TextLine));
                                    if (iter.IsAtFinalOf(PageIteratorLevel.Block, PageIteratorLevel.Para)) ; //System.Windows.MessageBox.Show("ostatni paragraf w bloku");
                                }
                                while (iter.Next(PageIteratorLevel.Block, PageIteratorLevel.Para));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
              
                System.Windows.MessageBox.Show(e.ToString());
            }
        }

        private void sectionButton_Click(object sender, RoutedEventArgs e)
        {
          //  Section((int)begin.X, (int)begin.Y, (int)(end.X - begin.X), (int)(end.Y - begin.Y));
            Section();
  
        }

        private void sectionRemoveButton_Click(object sender, RoutedEventArgs e)
        {
            isSection = false;
        }

        private void radioOCR_Checked(object sender, RoutedEventArgs e)
        {
            processedImage.Visibility = Visibility.Hidden;
            resultTextBox.Visibility = Visibility.Visible;
            processedImageViewer.Visibility = Visibility.Hidden;
            startButton.IsEnabled = true;
            startProcessingButton.IsEnabled = false;
            validationGrid.Visibility = Visibility.Hidden;
            
        }

        private void radioProcessing_Checked(object sender, RoutedEventArgs e)
        {
            processedImage.Visibility = Visibility.Visible;
            resultTextBox.Visibility = Visibility.Hidden;
            processedImageViewer.Visibility = Visibility.Visible;
            startButton.IsEnabled = false;
            startProcessingButton.IsEnabled = true;
            validationGrid.Visibility = Visibility.Hidden;
        }

        private void radioValidation_Checked(object sender, RoutedEventArgs e)
        {
            processedImage.Visibility = Visibility.Hidden;
            resultTextBox.Visibility = Visibility.Hidden;
            processedImageViewer.Visibility = Visibility.Hidden;
            startButton.IsEnabled = false;
            startProcessingButton.IsEnabled = false;
            validationGrid.Visibility = Visibility.Visible;
        }

        private void startProcessingButton_Click(object sender, RoutedEventArgs e)
        {
            if (imageProcessingIncreaseResolutionRadioButton.IsChecked == true)
            {
                Bitmap m = new Bitmap(testImagePath);
                RenderTargetBitmap rtb = new RenderTargetBitmap((int)(m.Width * (300 / 96)), (int)(m.Height * (300 / 96)), 300d, 300d, PixelFormats.Default);
                rtb.Render(img);
                BmpBitmapEncoder encoder = new BmpBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(rtb));
                FileStream fs = File.Open(@"C:\Users\urbanowicz\Documents\image.png", FileMode.Create);
                encoder.Save(fs);

                fs.Close();
                BitmapImage imgdata;
                imgdata = new BitmapImage(new Uri(@"C:\Users\urbanowicz\Documents\image.png", UriKind.RelativeOrAbsolute));


                processedImage.Source = imgdata;
            }
            if (imageProcessingSharpening.IsChecked == true)
            {
                try
                {
                    // Utilities.Media.Image.Filter filter = new Utilities.Media.Image.Filter();
                    Bitmap m = new Bitmap(testImagePath);
                    m = Utilities.Media.Image.ExtensionMethods.BitmapExtensions.Sharpen(m);

                  //  Bitmap n = new Bitmap(m.Width, m.Height, m.PixelFormat);
                    // n.LockBits(new System.Drawing.Rectangle(0, 0, n.Width, n.Height), System.Drawing.Imaging.ImageLockMode.ReadWrite, n.PixelFormat);
                    // m.LockBits(new System.Drawing.Rectangle(0, 0, m.Width, m.Height), System.Drawing.Imaging.ImageLockMode.ReadWrite, m.PixelFormat);

                   // System.Drawing.Imaging.BitmapData mData = m.LockBits(new System.Drawing.Rectangle(0, 0, m.Width, m.Height), System.Drawing.Imaging.ImageLockMode.ReadWrite, m.PixelFormat);
                   // System.Drawing.Imaging.BitmapData nData = n.LockBits(new System.Drawing.Rectangle(0, 0, m.Width, m.Height), System.Drawing.Imaging.ImageLockMode.ReadWrite, m.PixelFormat);
                 //       IntPtr ptr = mData.Scan0;
                  //  int bytes  = Math.Abs(mData.Stride) * m.Height;
                  //  byte[] rgbValues = new byte[bytes];
                  //  System.Runtime.InteropServices.Marshal.Copy(ptr, rgbValues, 0, bytes);
                  //  int bytes2 = Math.Abs(nData.Stride) * m.Height;

                   // System.Runtime.InteropServices.Marshal.Copy(ptr, rgbValues, 0, bytes2);
                   // m.UnlockBits(mData);
                    m.Save(testImagePath1, System.Drawing.Imaging.ImageFormat.Png);
                    m.Dispose();



                    // Utilities.Media.Image.ExtensionMethods.BitmapExtensions.AdjustBrightness(m,1);
                    //  m.Save(@"C:\Users\urbanowicz\Documents\image.png");
                  //  m.Save(testImagePath1, System.Drawing.Imaging.ImageFormat.Png);
                   // m.Save(testImagePath1, System.Drawing.Imaging.ImageFormat.Png);
                    // testImagePath = @"C:\Users\urbanowicz\Documents\image.png";

                 //  m.Dispose();
                    BitmapImage imgdata;
                    imgdata = new BitmapImage(new Uri(testImagePath1, UriKind.RelativeOrAbsolute));


                    processedImage.Source = imgdata;
                }
                catch(Exception ex)
                {
                    System.Windows.MessageBox.Show(ex.ToString());
                }
                //Bitmap n=filter.ApplyFilter(m);
              
               // Save();
            }
            if (imageProcessingChangeContrast.IsChecked == true)
            {
                Bitmap m = new Bitmap(testImagePath);
                m = Utilities.Media.Image.ExtensionMethods.BitmapExtensions.AdjustContrast(m, 0);
                m.Save(@"C:\Users\urbanowicz\Documents\processedImage.png");
                testImagePath = @"C:\Users\urbanowicz\Documents\image.png";

            //    m.Dispose();
                BitmapImage imgdata;
                imgdata = new BitmapImage(new Uri(@"C:\Users\urbanowicz\Documents\image.png", UriKind.RelativeOrAbsolute));


                processedImage.Source = imgdata;
                //Bitmap n=filter.ApplyFilter(m);
            }
        }

        
        private void imgMouseDown(object sender, MouseButtonEventArgs e)
        {  /*
            begin = e.GetPosition(img);
          //  imgBegin=
           // System.Windows.MessageBox.Show(begin.X.ToString() + " " + begin.Y.ToString());
        */
        }

        private void imgMouseUp(object sender, MouseButtonEventArgs e)
        {
          /*
            end = e.GetPosition(img);
            System.Windows.MessageBox.Show(end.X.ToString() + " " + end.Y.ToString());
            Section((int)begin.X, (int)begin.Y, (int)(end.X - begin.X), (int)(end.Y - begin.Y));
            CreateARectangle((int)begin.X, (int)begin.Y, (int)(end.X - begin.X), (int)(end.Y - begin.Y));
            
        */
        }
        
        private void saveTextButton_Click(object sender, RoutedEventArgs e)
        {
            if(resultTextBox.Text!="")
            {
             using(StreamWriter sw=new StreamWriter(@"C:\Users\urbanowicz\Documents\text.txt"))
             {
                System.Windows.MessageBox.Show( resultTextBox.LineCount.ToString());
                for (int i = 0; i < resultTextBox.LineCount;i++ )
                    sw.WriteLine(resultTextBox.GetLineText(i));
             }
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if(validationTextBox.Text!="")
            {
                //Divide(validationTextBox.Text);
            //    List<string> l = Patterning.RemoveEmpltyLines(validationTextBox.Text);
             //   foreach (string s in l)
             //       validationResultTextBox.Text += s;
                
                List<string> l=new List<string>();
                for (int i = 0; i < validationTextBox.LineCount;i++ )
                    l.Add(validationTextBox.GetLineText(i));
                
                Patterning p = new Patterning(l);
                p.IterateAll();

                

                foreach(System.Windows.Controls.Control ctr in firmaGrid.Children)
                {
                    if(ctr.GetType()==typeof(System.Windows.Controls.TextBox))
                    {
                        if (ctr.Tag != null)
                        {
                           // System.Windows.MessageBox.Show(ctr.Tag.ToString());
                            System.Windows.Controls.TextBox box = (System.Windows.Controls.TextBox)ctr;
                            if(p.results.ContainsKey(ctr.Tag.ToString()))
                            box.Text = p.results[ctr.Tag.ToString()];
                        }
                    }
                }

                foreach (System.Windows.Controls.Control ctr in fakturaGrid.Children)
                {
                    if (ctr.GetType() == typeof(System.Windows.Controls.TextBox))
                    {
                        if (ctr.Tag != null)
                        {
                            // System.Windows.MessageBox.Show(ctr.Tag.ToString());
                            System.Windows.Controls.TextBox box = (System.Windows.Controls.TextBox)ctr;
                            if (p.results.ContainsKey(ctr.Tag.ToString()))
                                box.Text = p.results[ctr.Tag.ToString()];
                        }
                    }
                }

                foreach (System.Windows.Controls.Control ctr in towarGrid.Children)
                {
                    if (ctr.GetType() == typeof(System.Windows.Controls.TextBox))
                    {
                        if (ctr.Tag != null)
                        {
                            // System.Windows.MessageBox.Show(ctr.Tag.ToString());
                            System.Windows.Controls.TextBox box = (System.Windows.Controls.TextBox)ctr;
                            if (p.results.ContainsKey(ctr.Tag.ToString()))
                                box.Text = p.results[ctr.Tag.ToString()];
                        }
                    }
                }
             
                //fakturaGrid.Children.
               // p.showResults();
            }
        }
 
        
      public void Divide(String text)
        {
          /*
           List<String> l=RemoveEmpltyLines(text);
          Dictionary<string,string> patterns=new Dictionary<string,string>();
          patterns.Add("kod pocztowy", " [0-9]{2}\\-[0-9]{3}[^-]");
          patterns.Add("adres email", "[0-9|a-z|_|-]+@[0-9|a-z|_|-]+\\.[a-z]{2,3}");
          patterns.Add("NIP", "( [0-9]{10}|[0-9]{3}-[0-9]{3}-[0-9]{2}-[0-9]{2})|([0-9]{2}-[0-9]{2}-[0-9]{3}-[0-9]{3})|([0-9]{2}-[0-9]{3}-[0-9]{2}-[0-9]{3})|([0-9]{3}-[0-9]{2}-[0-9]{3}-[0-9]{2})");
          patterns.Add("numer klienta", "numer klienta[:]?[ ]*[0-9]*");
          patterns.Add("numer konta", "([0-9]{26})|([0-9]{2} [0-9]{4} [0-9]{4} [0-9]{4} [0-9]{4} [0-9]{4} [0-9]{4})");
          */
         // System.Windows.Point position=new System.Windows.Point(320,300);

         /*
         foreach(string s in l)
          {
             foreach(KeyValuePair<string,string> kvp in patterns)
             { 
                foreach(Match m in Regex.Matches(s,kvp.Value,System.Text.RegularExpressions.RegexOptions.IgnoreCase))
                {
                System.Windows.MessageBox.Show(kvp.Key+": "+m.Value.ToString()+" index: "+m.Index.ToString());
               // System.Windows.Controls.Label L
                    string temp;
                if (kvp.Key == "numer klienta") 
                {
                    // temp = m.Value.ToString().Replace("Numer klienta","");
                    temp = Regex.Replace(m.Value.ToString(), "numer klienta", "", RegexOptions.IgnoreCase);
                    temp = Regex.Replace(temp, ":", "");
                    

                     System.Windows.MessageBox.Show(temp);
                }
                else
                     temp = m.Value.ToString();
                List<System.Windows.Controls.Label> labels = new List<System.Windows.Controls.Label>();
                labels.Add(new System.Windows.Controls.Label());
                validationGrid.Children.Add(labels[labels.Count - 1]);
                labels[labels.Count-1].Content = kvp.Key + ": " + temp;

                labels[labels.Count-1].Margin = new Thickness(position.X,position.Y,0,0);
                position.Y += 30;
                }
             }
          }
          /*
          foreach(string s in l)
          {
              if(System.Text.RegularExpressions.Regex.IsMatch(s,pattern))
              {
                  System.Windows.MessageBox.Show(s);
              }
          }
           * */
           // return new string[]{};
          
        }

      private void Button_Click_2(object sender, RoutedEventArgs e)
      {
          if (resultTextBox.Text != null)
              validationTextBox.Text = resultTextBox.Text;
      }

      private void saveButton_Click(object sender, RoutedEventArgs e)
      {
          System.Windows.Controls.Button button = sender as System.Windows.Controls.Button;
         
          
          int row;
          row = Grid.GetRow(button);
          Grid g = (Grid)button.Parent;

          
          System.Windows.Controls.TextBox box = (System.Windows.Controls.TextBox)g.Children.Cast<UIElement>().First(a => Grid.GetRow(a) == row && Grid.GetColumn(a) == 1);
          if (box != null && box.Text!="")
          {
             if( System.Windows.MessageBox.Show("Zapisać do słownika wpis: "+box.Text+" ?","Potwierdzenie",MessageBoxButton.YesNo)==MessageBoxResult.Yes)
             {
                 Connection c = new Connection();
                 c.NonQuery("INSERT INTO Slownik VALUES('"+box.Text+"')");

             }
          }
          
          //button.
       //   System.Windows.Controls.Button obj = ((FrameworkElement)sender).DataContext as System.Windows.Controls.Button;
        // Object ID = ((System.Windows.Controls.Button)sender).CommandParameter;
          
       //   System.Windows.MessageBox.Show(ID.ToString());
    //      obj.
      }

      private void ClearDictionaryClick(object sender, RoutedEventArgs e)
      {
          Connection c = new Connection();
         // c.NonQuery("Insert into Slownik values('sagadfg')");
          c.NonQuery("DELETE  FROM Slownik");
          c.Dispose();
      }

      

      private void showDictionaryButton_Click(object sender, RoutedEventArgs e)
      {
          Dictionary window = new Dictionary();
          window.Show();
      }

      private void Button_Click_3(object sender, RoutedEventArgs e)
      {
          Save();
      }

      private void iterateNumerFakturyButtonClick(object sender, RoutedEventArgs e)
      {
          if (validationTextBox.Text != "")
          {
              Patterning p = new Patterning();
              foreach (ListBoxItem s in parametersListBox.SelectedItems)
              {
                  if (s.Content.ToString()=="data")
                  
                  
                  {
                      p.IterateFor(validationTextBox.Text, "data1");
                      p.IterateFor(validationTextBox.Text, "data2");

                  }
                  else if (s.Content.ToString()=="telefon")
                  {
                   //   System.Windows.MessageBox.Show("");
                      p.IterateFor(validationTextBox.Text, "telefon1");
                      p.IterateFor(validationTextBox.Text, "telefon2");
                  }
                  else
                      p.IterateFor(validationTextBox.Text, s.Content.ToString());
                 
                 // s.ToString
                  //if(parametersListBox.Items.
                  //System.Windows.MessageBox.Show(s.Content.ToString());

                 // s.GetValue(Val)
              }
              foreach (System.Windows.Controls.Control ctr in firmaGrid.Children)
             {
                 if (ctr.GetType() == typeof(System.Windows.Controls.TextBox))
                 {
                     if (ctr.Tag != null)
                     {
                        
                         System.Windows.Controls.TextBox box = (System.Windows.Controls.TextBox)ctr;
                         if (p.results.ContainsKey(ctr.Tag.ToString()))
                             box.Text = p.results[ctr.Tag.ToString()];
                     }
                 }
             }

                foreach(System.Windows.Controls.Control ctr in fakturaGrid.Children)
                {
                    if(ctr.GetType()==typeof(System.Windows.Controls.TextBox))
                    {
                        if (ctr.Tag != null)
                        {
                            System.Windows.Controls.TextBox box = (System.Windows.Controls.TextBox)ctr;
                            if (p.results.ContainsKey(box.Tag.ToString()))
                                box.Text = p.results[box.Tag.ToString()];
                        }
                  
                }
              }
                foreach (System.Windows.Controls.Control ctr in towarGrid.Children)
                {
                    if (ctr.GetType() == typeof(System.Windows.Controls.TextBox))
                    {
                        if (ctr.Tag != null)
                        {
                            System.Windows.Controls.TextBox box = (System.Windows.Controls.TextBox)ctr;
                            if (p.results.ContainsKey(box.Tag.ToString()))
                                box.Text = p.results[box.Tag.ToString()];
                        }

                    }
                }


          }
          
             
          }
      
      private void iterateSprzedawcaNabywcaButtonCLick(object sender, RoutedEventArgs e)
      {
          if (validationTextBox.Text != "")
          {
            
              Patterning p = new Patterning();
              
             Dictionary<string,string> result= p.IterateSection("nabywca",validationTextBox.Text);
             foreach (KeyValuePair<string,string> kvp in result)
                 System.Windows.MessageBox.Show(kvp.Key+" "+kvp.Value);

             foreach (System.Windows.Controls.Control ctr in firmaGrid.Children)
             {
                 if (ctr.GetType() == typeof(System.Windows.Controls.TextBox))
                 {
                     if (ctr.Tag != null)
                     {
                        
                         System.Windows.Controls.TextBox box = (System.Windows.Controls.TextBox)ctr;
                         if (result.ContainsKey(ctr.Tag.ToString()))
                             box.Text = result[ctr.Tag.ToString()];
                     }
                 }
             }

                foreach(System.Windows.Controls.Control ctr in fakturaGrid.Children)
                {
                    if(ctr.GetType()==typeof(System.Windows.Controls.TextBox))
                    {
                        if (ctr.Tag != null)
                        {
                            System.Windows.Controls.TextBox box = (System.Windows.Controls.TextBox)ctr;
                            if (result.ContainsKey(box.Tag.ToString()))
                                box.Text = result[box.Tag.ToString()];
                        }
                  
                }
              }
                foreach (System.Windows.Controls.Control ctr in towarGrid.Children)
                {
                    if (ctr.GetType() == typeof(System.Windows.Controls.TextBox))
                    {
                        if (ctr.Tag != null)
                        {
                            System.Windows.Controls.TextBox box = (System.Windows.Controls.TextBox)ctr;
                            if (result.ContainsKey(box.Tag.ToString()))
                                box.Text = result[box.Tag.ToString()];
                        }

                    }
                }
          }
      }

      private void iterateNaglowekButtonCLick(object sender, RoutedEventArgs e)
      {
          if (validationTextBox.Text != "")
          {

              Patterning p = new Patterning();

              Dictionary<string, string> result = p.IterateSection("naglowek", validationTextBox.Text);
              foreach (KeyValuePair<string, string> kvp in result)
                  System.Windows.MessageBox.Show(kvp.Key + " " + kvp.Value);

              foreach (System.Windows.Controls.Control ctr in firmaGrid.Children)
              {
                  if (ctr.GetType() == typeof(System.Windows.Controls.TextBox))
                  {
                      if (ctr.Tag != null)
                      {

                          System.Windows.Controls.TextBox box = (System.Windows.Controls.TextBox)ctr;
                          if (result.ContainsKey(ctr.Tag.ToString()))
                              box.Text = result[ctr.Tag.ToString()];
                      }
                  }
              }

              foreach (System.Windows.Controls.Control ctr in fakturaGrid.Children)
              {
                  if (ctr.GetType() == typeof(System.Windows.Controls.TextBox))
                  {
                      if (ctr.Tag != null)
                      {
                          System.Windows.Controls.TextBox box = (System.Windows.Controls.TextBox)ctr;
                          if (result.ContainsKey(box.Tag.ToString()))
                              box.Text = result[box.Tag.ToString()];
                      }

                  }
              }
              foreach (System.Windows.Controls.Control ctr in towarGrid.Children)
              {
                  if (ctr.GetType() == typeof(System.Windows.Controls.TextBox))
                  {
                      if (ctr.Tag != null)
                      {
                          System.Windows.Controls.TextBox box = (System.Windows.Controls.TextBox)ctr;
                          if (result.ContainsKey(box.Tag.ToString()))
                              box.Text = result[box.Tag.ToString()];
                      }

                  }
              }
          }
      }

      private void Button_Click_4(object sender, RoutedEventArgs e)
      {
         
          if(validationTextBox.Text!="")
          {
              string text=validationTextBox.Text;
             List<List<string>> headers= Patterning.GetHeaders(text);
             List<string> sections = Patterning.GetTextWithoutHeaders(text);
              List<string> lines = Patterning.RemoveEmpltyLines(text);
              lines.RemoveAt(0);
              Patterning p=new Patterning();
            //  foreach (string s 
            //      in lines)
               //   System.Windows.MessageBox.Show(s + "*");
              /*
              foreach (string s in sections)
                  System.Windows.MessageBox.Show("text: "+s);

              foreach(List<string> l in headers)
                  foreach(string s in l)
                      System.Windows.MessageBox.Show("parametr: "+s);

              */
             // if (sections.Count == headers.Count)
                  for (int i = 0; i < headers.Count ; i++)
                  {
                      
                      for (int j = 1; j < headers[i].Count; j++)
                      {
                        //  System.Windows.MessageBox.Show("par: " + headers[i][j]);
                        //  System.Windows.MessageBox.Show("sek: " + sections[i]);
                          p.IterateFor(sections[i], headers[i][j]);
                         // System.Windows.Forms.MessageBox.Show(p.results[headers[i][j]]);

                      }
                      if(Regex.IsMatch(headers[i][0],"tabelka1",RegexOptions.IgnoreCase))
                      {
                          
                          p.IterateTable(lines,1); //towar
                      
                      }
                      else
                          if (Regex.IsMatch(headers[i][0], "tabelka2", RegexOptions.IgnoreCase))
                          {

                              p.IterateTable(lines,2); //podsumowanie

                          }
                  }
             // else
               //   System.Windows.MessageBox.Show("");
             // */
              foreach (System.Windows.Controls.Control ctr in firmaGrid.Children)
              {
                  if (ctr.GetType() == typeof(System.Windows.Controls.TextBox))
                  {
                      if (ctr.Tag != null)
                      {

                          System.Windows.Controls.TextBox box = (System.Windows.Controls.TextBox)ctr;
                          if (p.results.ContainsKey(ctr.Tag.ToString()))
                              box.Text = p.results[ctr.Tag.ToString()];
                      }
                  }
              }

              foreach (System.Windows.Controls.Control ctr in fakturaGrid.Children)
              {
                  if (ctr.GetType() == typeof(System.Windows.Controls.TextBox))
                  {
                      if (ctr.Tag != null)
                      {
                          System.Windows.Controls.TextBox box = (System.Windows.Controls.TextBox)ctr;
                          if (p.results.ContainsKey(box.Tag.ToString()))
                              box.Text = p.results[box.Tag.ToString()];
                      }

                  }
              }
              foreach (System.Windows.Controls.Control ctr in towarGrid.Children)
              {
                  if (ctr.GetType() == typeof(System.Windows.Controls.TextBox))
                  {
                      if (ctr.Tag != null)
                      {
                          System.Windows.Controls.TextBox box = (System.Windows.Controls.TextBox)ctr;
                          if (p.results.ContainsKey(box.Tag.ToString()))
                              box.Text = p.results[box.Tag.ToString()];
                      }

                  }
              }


          }
      }

      private void buduj_Wyrazenie_Click(object sender, RoutedEventArgs e)
      {
          Patterning p = new Patterning();
          if (buildRegexTextBox.Text != "")
              buildRegexTextBox.Text = p.BuildPattern(buildRegexTextBox.Text);
          string s = buildRegexTextBox.Text;
          foreach(Match m in Regex.Matches(sprawdzanieWyrazenTextBox.Text,s))
          {
              System.Windows.MessageBox.Show("Match!:"+m.Value.ToString());
          }
      }

      private void szukaj_Click(object sender, RoutedEventArgs e)
      {
          if(wyrazenieTextBox.Text!=null)
          {
              foreach (Match m in Regex.Matches(sprawdzanieWyrazenTextBox.Text, wyrazenieTextBox.Text))
              {
                  System.Windows.MessageBox.Show("Match!:" + m.Value.ToString());
              }
          }
      }

     

  

     

    

     

     

      



        
    }
}
