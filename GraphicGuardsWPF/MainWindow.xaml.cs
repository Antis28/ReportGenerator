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
            dp_Date.SelectedDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(2);
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
                DateTime date = dp_Date.SelectedDate.Value;
                generator = new GraphicGenerator(AppVisibility.Visible, date);
            }
            else
            {
                ;
            }

            int guardNumber = SelectGuardContinue();
            String suguardName1 = rb_Guard1.Content.ToString();
            String suguardName2 = rb_Guard2.Content.ToString();
            String suguardName3 = rb_Guard3.Content.ToString();
            try
            {
                generator.Generate(suguardName1, suguardName2, suguardName3,supervisor, guardNumber);
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

        private int SelectGuardContinue()
        {
            int guardNumber = 0;
            if (rb_Guard1.IsChecked.Value)
            {
                guardNumber = 1;
            }
            else if (rb_Guard2.IsChecked.Value)
            {
                guardNumber = 2;
            }
            else if (rb_Guard3.IsChecked.Value)
            {
                guardNumber = 3;
            }

            return guardNumber;
        }

    }
}
