using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using XmlManager;

namespace GraphicGuardsWPF
{
    /// <summary>
    /// Логика взаимодействия для Win_GuardsEdit.xaml
    /// </summary>
    public partial class Win_GuardsEdit : Window
    {
        public Win_GuardsEdit()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow win =  this.Owner as MainWindow;
            Settings settings = new Settings()
            {
                Guard1 = this.Guard1.Text,
                Guard2 = this.Guard2.Text,
                Guard3 = this.Guard3.Text,
                Supervisor = "",
                Director = this.Director.Text,
                Deputy_chief = this.Deputy_chief.Text,
                Superior = this.Superior.Text
            };
            win.settingMeneger.Save(settings);
            Close();
        }
    }
}
