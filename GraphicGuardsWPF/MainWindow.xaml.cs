using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using GraphicGuards;
using Utils;
using WordManager;
using Word = Microsoft.Office.Interop.Word;

namespace GraphicGuardsWPF
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {

            GenerateGraophic();
        }

        private void GenerateGraophic()
        {
           
            String supervisor = Helpers.GetSupervisorShort(cmb_supervisor.SelectedIndex);            
            GraphicGenerator generator = null;

            
            if (true)
            {
                DateTime date = DateTime.Now;
                generator = new GraphicGenerator(AppVisibility.Visible, date);
            }
            else
            {
                ;
            }


            try
            {
                generator.Generate(supervisor);
            }
            catch (FileLoadException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (true)
                {
                    generator.Dispose();
                }
            }
           
        }

        
    }
}
