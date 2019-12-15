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
using XmlManager;
using Word = Microsoft.Office.Interop.Word;

namespace GraphicGuardsWPF
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public Settings settings;
        public MainWindow()
        {
            InitializeComponent();
            dp_Date.SelectedDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(2);

            settings = SettingManeger.Load();

            gridGuards.DataContext = settings;
            //UpdateSupervisorList();
        }

        private void UpdateSupervisorList()
        {
            var items = cmb_supervisor.Items;
            items.Clear();
            items.Add(settings.Director);
            items.Add(settings.Deputy_chief);
            cmb_supervisor.SelectedIndex = 0;
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            settings = SettingManeger.Load();
            GenerateGraophic();
        }

        private void GenerateGraophic()
        {
            settings.Supervisor = Helpers.GetSupervisorShort(cmb_supervisor.SelectedIndex, settings);
            GraphicGenerator generator = null;

            DateTime date = dp_Date.SelectedDate.Value;
            generator = new GraphicGenerator(AppVisibility.Visible, date);

            int guardNumber = SelectGuardContinue();
            try
            {
                generator.Generate(settings, guardNumber);
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

        private void btn_openDirectory_Click(object sender, RoutedEventArgs e)
        {
            String newFolderPath = GraphicGenerator.GetSaveCatalog();

            System.Diagnostics.Process Proc = new System.Diagnostics.Process();
            Proc.StartInfo.FileName = "explorer";
            Proc.StartInfo.Arguments = newFolderPath;
            Proc.Start();
            Proc.Close();
        }

        private void btn_editGuards_Click(object sender, RoutedEventArgs e)
        {
            settings = SettingManeger.Load();
            Win_GuardsEdit win_GuardsEdit = new Win_GuardsEdit
            {
                Owner = this
            };
            win_GuardsEdit.gridGuardsDetails.DataContext = settings;
            win_GuardsEdit.gridGuardsDetails.UpdateLayout();
            win_GuardsEdit.Show();
        }

        private void btn_editHolidays_Click(object sender, RoutedEventArgs e)
        {
            settings = SettingManeger.Load();
            Win_HolidaysEdit win_GuardsEdit = new Win_HolidaysEdit()
            {
                Owner = this
            };
            win_GuardsEdit.Show();
        }
    }
}
