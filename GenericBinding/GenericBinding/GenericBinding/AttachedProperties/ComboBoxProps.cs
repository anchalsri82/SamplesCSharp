using System;
using System.Windows;
using System.Windows.Controls;
using System.Reflection;
using System.Collections;

namespace GenericBinding
{
    /// <summary>
    /// Provides a mechanism for binding a ComboBox.ItemSource against
    /// a generic method within the StaticData singleton. Where the generic
    /// method has a signature as follows:
    /// 
    /// public ObservableCollection<T> GetForType<T>()
    /// </summary>
    public class ComboBoxProps
    {
        #region BoundCollectionType

        /// <summary>
        /// BoundCollectionType Attached Dependency Property
        /// </summary>
        public static readonly DependencyProperty BoundCollectionTypeProperty =
            DependencyProperty.RegisterAttached("BoundCollectionType", 
            typeof(Type), typeof(ComboBoxProps), new FrameworkPropertyMetadata(null, new PropertyChangedCallback(OnBoundCollectionTypeChanged)));

        /// <summary>
        /// Gets the BoundCollectionType property.  
        /// </summary>
        public static Type GetBoundCollectionType(DependencyObject d)
        {
            return (Type)d.GetValue(BoundCollectionTypeProperty);
        }

        /// <summary>
        /// Sets the BoundCollectionType property.  
        /// </summary>
        public static void SetBoundCollectionType(DependencyObject d, Type value)
        {
            d.SetValue(BoundCollectionTypeProperty, value);
        }

        /// <summary>
        /// Handles changes to the BoundCollectionType property.
        /// Uses Reflection to obtain the method within the StaticData singelton class
        /// that has the generic method that we need to use to get the values from.
        /// The method will be marked with a custom ItemsSourceLookUpMethodAttribute
        /// to indicate which method is to be used, to create a Dynamic call to using the correct generic parameter.
        /// </summary>
        private static void OnBoundCollectionTypeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ComboBox cbSource = d as ComboBox;
            Type t = (Type)e.NewValue;
            Type[] types = new Type[] { t };

            MethodInfo[] methods = typeof(StaticData).GetMethods(BindingFlags.Public | BindingFlags.Instance);

            foreach (MethodInfo method in methods)
            {
                //Didnt like looking up MethodInfo.Name based on a string as it could
                //change, so use a custom attribute to look for on the method instead

                ItemsSourceLookUpMethodAttribute[] attribs = (ItemsSourceLookUpMethodAttribute[])method.GetCustomAttributes(
                            typeof(ItemsSourceLookUpMethodAttribute), true);
                
                //is this the correct MethodInfo to invoke
                if (attribs.Length > 0)
                {
                    // create the generic method
                    MethodInfo genericMethod = method.MakeGenericMethod(types);
                    cbSource.ItemsSource = 
                        (IEnumerable)genericMethod.Invoke(StaticData.Instance, 
                        BindingFlags.Instance, null, null, null);
                }
            }
        }
        #endregion
    }
}
