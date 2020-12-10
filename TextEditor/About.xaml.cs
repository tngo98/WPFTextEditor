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

namespace TextEditor
{
    /// <summary>
    /// Interaction logic for About.xaml
    /// </summary>
    public partial class About : Window
    {
        public About()
        {
            InitializeComponent();
        }

        /*
         * Function: Close_About_Click()
         * Purpose: To close window upon button click
         * Parameters: object sender
         *             CanExecuteRoutedEventArgs e
         * Returns: NONE
         */
        private void Close_About_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
