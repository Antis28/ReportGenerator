using System;
using System.Windows;
using System.Windows.Input;
using System.Threading;

using Word = Microsoft.Office.Interop.Word;
using System.Reflection;
using System.IO;
using System.Globalization;
using Reports;
using WordManager;
using Utils;

namespace ReportGenerator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
            this.tb_photoCount.PreviewTextInput += new TextCompositionEventHandler(textBox_PreviewTextInput);
        }

        //фильтр ввода только цифр
        private void textBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            char ch = e.Text[0];
            if (InputFilter.CheckDigit(ch)) e.Handled = true;
        }

        private void FormDocument_click(object sender, RoutedEventArgs e)
        {
            GenThur();
        }

        private void GenThur()
        {
            int photoCount = int.Parse(tb_photoCount.Text, CultureInfo.CurrentCulture);
            String supervisor = GetSupervisorByIndex(cmb_supervisor.SelectedIndex);
            String works = CombineListWorks();
            ReportThursday report = null;

            if (cb_WordVisible.IsChecked.Value)
            {
                report = new ReportThursday(AppVisibility.Visible);                
            }
            else
            {
                report = new ReportThursday(AppVisibility.Hide);
            }


            try
            {
                report.GenerateThursday(works, supervisor, photoCount);
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
                if (!cb_WordVisible.IsChecked.Value)
                {
                    report.Dispose();
                }
            }
            if (cb_CloseByEnd.IsChecked.Value)
            {
                Application.Current.MainWindow.Close();
            }

            /*
            else
                MessageBox.Show("Работа генератора завершена");
                */
        }

        private static String GetSupervisorByIndex(int index)
        {
            String position = String.Empty;
            switch (index)
            {
                case 0:
                    position = "Начальник управления" + new String(' ', 70) + "М.В. Городкова";
                    break;
                case 1:
                    position = "И.о. начальника управления" + new String(' ', 70) + "Т.Н. Хомич";
                    break;
                default:
                    break;
            }
            return position;
        }
        private string CombineListWorks()
        {
            String work = String.Empty;
            //Зима
            String cleanSnow = "уборка снега на тротуарах;";
            String sprinklingSidewalk = "посыпка тротуара противогололедными материалами;";

            //Весна-осень:
            String landscaping = "облагораживание территории;";
            string harvestingFoliage = "уборка листвы;";
            String sweepingPavements = "подметание тротуаров;";

            //Лето:
            String cutGrass = "покос травы;";

            //Всегда
            String lightGarbageCollection = "уборка легкого мусора.";


            //Зима            
            if (cb_cleanSnow.IsChecked == true)
            {
                work += cleanSnow + Environment.NewLine;
            }
            if (cb_sprinklingSidewalk.IsChecked == true)
            {
                work += sprinklingSidewalk + Environment.NewLine;
            }
            //Весна-осень:
            if (cb_landscaping.IsChecked == true)
            {
                work += landscaping + Environment.NewLine;
            }
            if (cb_harvestingFoliage.IsChecked == true)
            {
                work += harvestingFoliage + Environment.NewLine;
            }
            if (cb_sweepingPavements.IsChecked == true)
            {
                work += sweepingPavements + Environment.NewLine;
            }
            //Лето:
            if (cb_cutGrass.IsChecked == true)
            {
                work += cutGrass + Environment.NewLine;
            }

            //Всегда в конце, потому что тут после предложения поставил точку.
            if (cb_lightGarbageCollection.IsChecked == true)
            {
                work += lightGarbageCollection;// + Environment.NewLine;
            }

            return work;
        }
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Middle && e.ButtonState == MouseButtonState.Pressed)
            {
                //Делаем при нажатии на колесико мыши
                cb_lightGarbageCollection.IsEnabled = !cb_lightGarbageCollection.IsEnabled;
            }
        }
    }
}
