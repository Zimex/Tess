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
using System.Windows.Shapes;

namespace Tess
{
    /// <summary>
    /// Interaction logic for ResultsWindow.xaml
    /// </summary>
    public partial class ResultsWindow : Window
    {
        public List<string> results;

        public string Results
        {
            get;
            set;
        }
            
        public ResultsWindow(List<string> results)
        {
            InitializeComponent();
            /*
            this.results = results;
            for (int i = 0; i < results.Count; i++)
                resultTextBox.Text += results[i];
             * */
            Patterning p=new Patterning();
            foreach(string s in p.frequentlyMistakenSequences.Keys)
            {
                 resultTextBox.Text+=s+": ";
                foreach(string ss in p.frequentlyMistakenSequences[s])
                {
                    resultTextBox.Text += ss + " ";
                }
                resultTextBox.Text += Environment.NewLine;
            }
        }
        public ResultsWindow()
        {
            InitializeComponent();
          
        }
    }
}
