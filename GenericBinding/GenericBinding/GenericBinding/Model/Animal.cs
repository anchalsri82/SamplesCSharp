using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace GenericBinding
{
    /// <summary>
    /// A simple data object
    /// </summary>
    public class Animal : INotifyPropertyChanged
    {
        #region Data
        private String breed = String.Empty;
        #endregion

        #region Public Properties
        public String Breed
        {
            get { return this.breed; }
            set
            {
                if (value != this.breed)
                {
                    this.breed = value;
                    NotifyPropertyChanged("Breed");
                }
            }
        }
        #endregion

        #region Overrides
        public override Boolean Equals(object obj)
        {
            if (obj == null) return false;
            if (!obj.GetType().Equals(typeof(Animal))) return false;
            if (Object.ReferenceEquals(obj, this)) return true;

            Animal animal = obj as Animal;

            if (animal.Breed == this.Breed) return true;

            return false;

        }

        public override string ToString()
        {
            return String.Format("{0}", Breed);
        }


        public override int GetHashCode()
        {
            return Breed.GetHashCode();
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
