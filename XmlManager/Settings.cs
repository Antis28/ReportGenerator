using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;

namespace XmlManager
{
    [XmlRoot("Root")]
    public class Settings : IEquatable<Settings>, INotifyPropertyChanged
    {

        private string guard1 = "guard1";
        private string guard2 = "guard2";
        private string guard3 = "guard3";
        private string director = "director";
        private string deputy_chief = "deputy_chief";
        private string supervisor = "supervisor";
        private string superior = "superior";

        private List<string> holidays;

        [XmlElement]
        public string Guard1 { get => guard1; set => guard1 = value; }
        [XmlElement]
        public string Guard2 { get => guard2; set => guard2 = value; }
        [XmlElement]
        public string Guard3 { get => guard3; set => guard3 = value; }
        [XmlElement]
        public string Director
        {
            get => director;
            set
            {
                director = value;
                RaisePropertyChanged("Director");
            }
        }
        [XmlElement]
        public string Deputy_chief
        {
            get => deputy_chief;
            set
            {
                deputy_chief = value;
                RaisePropertyChanged("Deputy_chief");
            }
        }
        [XmlElement]
        public string Supervisor { get => supervisor; set => supervisor = value; }
        [XmlElement]
        public string Superior { get => superior; set => superior = value; }
        [XmlArray("Holidays")]
        [XmlArrayItem("Date")]
        public List<string> Holidays { get => holidays; set => holidays = value; }

        public event PropertyChangedEventHandler PropertyChanged;

        // Для удобства обернем событие в метод с единственным параметром - имя изменяемого свойства
        public void RaisePropertyChanged(string propertyName)
        {
            // Если кто-то на него подписан, то вызывем его
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #region сравнение в методе Contains
        public bool Equals(Settings other)
        {
            if (other == null)
                return false;

            return this == other;
        }

        public override bool Equals(Object obj)
        {
            if (obj == null)
                return false;

            Settings person = obj as Settings;
            if (person == null)
                return false;
            else
                return Equals(person);
        }
        public override int GetHashCode()
        {
            return 0;
        }

        public static bool operator ==(Settings person_1, Settings person_2)
        {
            if (((object)person_1) == null || ((object)person_2) == null)
                return Object.Equals(person_1, person_2);

            return person_1.Equals(person_2);
        }

        public static bool operator !=(Settings person_1, Settings person_2)
        {
            if (((object)person_1) == null || ((object)person_2) == null)
                return !Object.Equals(person_1, person_2);

            return !(person_1.Equals(person_2));
        }
        #endregion
    }
}
