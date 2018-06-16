using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace GenericBinding
{
    /// <summary>
    /// A small singleton class to demonstrate one possible way that
    /// WPF can support binding to generic methods within XAML
    /// </summary>
    public sealed class StaticData
    {
        #region Data
        static readonly StaticData instance = new StaticData();
        private ObservableCollection<Person> people = null;
        private ObservableCollection<Animal> animals = null;
        private Dictionary<Type, Object> typeToCollectionLookup = 
            new Dictionary<Type, Object>();
        #endregion

        #region Ctor
        static StaticData()
        {
        }

        private StaticData()
        {
            //create some dummy data
            people = new ObservableCollection<Person>
            {
                new Person { Age=10, FirstName="Sam", LastName="Davis" },
                new Person { Age=34, FirstName="Dan", LastName="Smith" },
                new Person { Age=17, FirstName="Joe", LastName="Boynes" },
                new Person { Age=12, FirstName="Max", LastName="Jones" }
            };

            animals = new ObservableCollection<Animal>
            {
                new Animal { Breed="German Shephard" },
                new Animal { Breed="Pitbull" },
                new Animal { Breed="Great Dane" },
                new Animal { Breed="Rottweiler" },
                new Animal { Breed="Bull Dog" }
            };

            typeToCollectionLookup.Add(typeof(Person), people);
            typeToCollectionLookup.Add(typeof(Animal), animals);


        }
        #endregion

        #region Public Properties/Methods
        /// <summary>
        /// Singleton instance
        /// </summary>
        public static StaticData Instance
        {
            get
            {
                return instance;
            }
        }

        /// <summary>
        /// This method is called via the ComboBoxProps.BoundCollectionType attached
        /// property, and it demonstrates how we can get round the current WPF limitations
        /// for generics support 
        /// </summary>
        /// <typeparam name="T">The type of collection to bind to</typeparam>
        /// <returns>A collection of static data. Which for this example is used as an 
        /// ItemsSource for the calling ComboBox</returns>
        [ItemsSourceLookUpMethodAttribute()]
        public ObservableCollection<T> GetForType<T>()
        {
            return (ObservableCollection<T>)typeToCollectionLookup[typeof(T)];
        }
        #endregion
    }

}
