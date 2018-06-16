using System;
using System.ComponentModel;

namespace GenericBinding
{
    /// <summary>
    /// Provides a very very simple ViewModel with a single 
    /// CurrentPerson and a CurrentAnimal property
    /// </summary>
    public class DummyViewModel : INotifyPropertyChanged
    {
        #region Data
        private Person currentPerson = null;
        private Animal currentAnimal = null;
        #endregion

        #region Ctor
        public DummyViewModel()
        {
            CurrentPerson = StaticData.Instance.GetForType<Person>()[0];
            CurrentAnimal = StaticData.Instance.GetForType<Animal>()[0];
        }
        #endregion

        #region Public Properties
        public Person CurrentPerson
        {
            get { return this.currentPerson; }
            set
            {
                if (value != this.currentPerson)
                {
                    this.currentPerson = value;
                    NotifyPropertyChanged("CurrentPerson");
                }
            }
        }

        public Animal CurrentAnimal
        {
            get { return this.currentAnimal; }
            set
            {
                if (value != this.currentAnimal)
                {
                    this.currentAnimal = value;
                    NotifyPropertyChanged("CurrentAnimal");
                }
            }
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
