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
using System.IO;
using System.Runtime.InteropServices.WindowsRuntime;
using Microsoft.Win32;

namespace TextEditor
{
    /*
     * Name: MainWindow()
     * Purpose: This class represents a typical text editor that includes a new, save, open, and close menu buttons, a textblock that allows users to type, and a character count for the words typed.
     */
    public partial class MainWindow : Window
    {
        private bool isSaved = false;

        public MainWindow()
        {
            InitializeComponent();
        }

        /*
         * Function: Open_CanExecuted()
         * Purpose: To allow Open button to work
         * Parameters: object sender
         *             CanExecuteRoutedEventArgs e
         * Returns: NONE
         */
        private void Open_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }



        /*
         * Function: Open_Executed()
         * Purpose: The execution of the Open button functionality
         * Parameters: object sender
         *             CanExecuteRoutedEventArgs e
         * Returns: NONE
         */
        private void Open_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (isSaved == false) // if file has been changed, prompt user for save and then open new file
            {
                bool retValue = unsavedMessage();

                if (retValue == true)
                {
                    OpenFileDialog oDlg = new OpenFileDialog();

                    oDlg.ShowDialog();


                    if (oDlg.FileName.Trim() != "")
                    {
                        TextBody.Text = File.ReadAllText(oDlg.FileName);
                    }
                }
            }
            else // if file hasn't been changed, then open new file
            {
                OpenFileDialog oDlg = new OpenFileDialog();

                oDlg.ShowDialog();


                if (oDlg.FileName.Trim() != "")
                {
                    TextBody.Text = File.ReadAllText(oDlg.FileName);
                }

                isSaved = true;
            }
        }



        /*
         * Function: New_CanExecuted()
         * Purpose: To allow New button to work
         * Parameters: object sender
         *             CanExecuteRoutedEventArgs e
         * Returns: NONE
         */
        private void New_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }



        /*
         * Function: New_Executed()
         * Purpose: The execution of the New button functionality
         * Parameters: object sender
         *             CanExecuteRoutedEventArgs e
         * Returns: NONE
         */
        private void New_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (isSaved == false) // if file has been changed, prompt user for save and then create new document
            {
                bool retValue = unsavedMessage();

                if (retValue == true)
                {
                    TextBody.Text = "";
                    this.Title = "Untitled";
                }
            }
            else // if file hasn't been changed, then create new document
            {
                TextBody.Text = "";
                this.Title = "Untitled";
                isSaved = true;
            }


        }



        /*
         * Function: Save_CanExecuted()
         * Purpose: To allow Save button to work
         * Parameters: object sender
         *             CanExecuteRoutedEventArgs e
         * Returns: NONE
         */
        private void Save_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }



        /*
         * Function: Save_Executed()
         * Purpose: The execution of the Save button functionality
         * Parameters: object sender
         *             CanExecuteRoutedEventArgs e
         * Returns: NONE
         */
        private void Save_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            SaveFileDialog sDlg = new SaveFileDialog();
            sDlg.Filter = "Text Document (*.txt)|*.txt|All files (*.*)|*.*";

            sDlg.ShowDialog();

            if (sDlg.FileName.Trim() != "")
            {
                File.WriteAllText(sDlg.FileName, TextBody.Text);
            }

            isSaved = true;
        }


        /*
         * Function: Close_CanExecuted()
         * Purpose: To allow Close button to work
         * Parameters: object sender
         *             CanExecuteRoutedEventArgs e
         * Returns: NONE
         */
        private void Close_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        /*
         * Function: Close_Executed()
         * Purpose: The execution of the Close button functionality
         * Parameters: object sender
         *             CanExecuteRoutedEventArgs e
         * Returns: NONE
         */
        private void Close_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (isSaved == false) // if file has been changed, then prompt user for save and then close window
            {
                bool retValue = unsavedMessage();

                if (retValue == true)
                {
                    System.Windows.Application.Current.Shutdown();
                }
            }
            else // if file hasn't been changed, then close window
            {
                System.Windows.Application.Current.Shutdown();
            }
        }



        /*
         * Function: About_Click()
         * Purpose: The execution of the About button
         * Parameters: object sender
         *             CanExecuteRoutedEventArgs e
         * Returns: NONE
         */
        private void About_Click(object sender, RoutedEventArgs e)
        {
            About aboutBox = new About();
            aboutBox.Owner = this;

            aboutBox.ShowDialog();
        }



        /*
         * Function: charCount()
         * Purpose: To update the character count text
         * Parameters: object sender
         *             CanExecuteRoutedEventArgs e
         * Returns: NONE
         */
        private void charCount(object sender, TextChangedEventArgs e)
        {
            int count = TextBody.Text.Length;
            string charCount = "Character Count: " + count.ToString();
            CharCount.Text = charCount;

            isSaved = false;
        }



        /*
         * Function: unsavedMessage()
         * Purpose: To prompt unsaved progress message box and it's following executions
         * Parameters: object sender
         *             CanExecuteRoutedEventArgs e
         * Returns: NONE
         */
        private bool unsavedMessage()
        {
            MessageBoxResult unsaved = new MessageBoxResult();

            unsaved = System.Windows.MessageBox.Show("You have unsaved progress. Would you like to save?", "Save", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);

            bool retCode = false;

            if (unsaved == MessageBoxResult.Yes) // if user clicks yes, then let them save file and return true
            {
                SaveFileDialog sDlg = new SaveFileDialog();
                sDlg.Filter = "Text Document (*.txt)|*.txt|All files (*.*)|*.*";

                sDlg.ShowDialog();

                if (sDlg.FileName.Trim() != "")
                {
                    File.WriteAllText(sDlg.FileName, TextBody.Text);
                }

                retCode = true;
            }
            else if (unsaved == MessageBoxResult.No) // if user clicks no, then continue with code and return true
            {
                retCode = true;
            }
            else if (unsaved == MessageBoxResult.Cancel) // if user click cancel, then continue with code and return false
            {
                retCode = false;
            }

            return retCode;
        }
    }
}
