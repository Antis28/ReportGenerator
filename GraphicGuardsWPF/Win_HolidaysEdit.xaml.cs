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

namespace GraphicGuardsWPF
{
    /// <summary>
    /// Логика взаимодействия для Win_HolidaysEdit.xaml
    /// </summary>
    public partial class Win_HolidaysEdit : Window
    {
        MainViewModel viewModel;

        public Win_HolidaysEdit()
        {
            InitializeComponent();
            viewModel = new MainViewModel();            
        }
    }
}
