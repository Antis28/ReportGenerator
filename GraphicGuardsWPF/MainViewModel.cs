using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace GraphicGuardsWPF
{
    class MainViewModel
    {
        public MainViewModel()
        {
            AddDate = new RelayCommand(obj => AddNewHoliday());
            RemoveSelectedDate = new RelayCommand(obj =>
            {
                if (obj != null)
                    People.Remove((String)obj);
            });

        }

        private void AddNewHoliday()
        {
            Win_HolidaysEdit win_HolidaysEdit;
           
            // TODO: возможна ошбка по индексу. Переделать.
            win_HolidaysEdit = App.Current.MainWindow.OwnedWindows[0] as Win_HolidaysEdit;
            if (win_HolidaysEdit != null)
            {
                DateTime date = win_HolidaysEdit.dp_Date.SelectedDate.Value;
                People.Add(date.ToString("yyyy.MM.dd"));
            }
        }

        public ObservableCollection<String> People
        {
            get;
        } = new ObservableCollection<String>();

        public ICommand AddDate
        {
            get;
        }
        public ICommand RemoveSelectedDate
        {
            get;
        }
    }
}
