using System;
using System.ComponentModel;

namespace GenericBinding
{
    /// <summary>
    /// A simple data object
    /// </summary>
    public class Person : INotifyPropertyChanged
    {
        #region Data
        private Int32 age = 0;
        private String firstName = String.Empty;
        private String lastName = String.Empty;
        #endregion

        #region Public Properties
        public Int32 Age
        {
            get { return this.age; }
            set
            {
                if (value != this.age)
                {
                    this.age = value;
                    NotifyPropertyChanged("Age");
                }
            }
        }

        public String FirstName
        {
            get { return this.firstName; }
            set
            {
                if (value != this.firstName)
                {
                    this.firstName = value;
                    NotifyPropertyChanged("FirstName");
                }
            }
        }

        public String LastName
        {
            get { return this.lastName; }
            set
            {
                if (value != this.lastName)
                {
                    this.lastName = value;
                    NotifyPropertyChanged("LastName");
                }
            }
        }
        #endregion

        #region Overrides
        public override Boolean Equals(object obj)
        {
            if (obj == null) return false;
            if (!obj.GetType().Equals(typeof(Person))) return false;
            if (Object.ReferenceEquals(obj, this)) return true;

            Person person = obj as Person;

            if (person.Age == this.Age &&
                person.FirstName == this.FirstName &&
                person.LastName == this.LastName) return true;

            return false;

        }

        public override string ToString()
        {
            return String.Format("{0} {1}", FirstName, LastName);
        }


        public override int GetHashCode()
        {
            return (Age ^ FirstName.Length ^ LastName.Length);
        }
        #endregion

        #region INotifyPropertyChanged implementaion
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }
        #endregion
    }
}
