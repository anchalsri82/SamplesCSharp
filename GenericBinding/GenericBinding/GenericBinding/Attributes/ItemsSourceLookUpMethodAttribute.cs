using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GenericBinding
{
    /// <summary>
    /// Dummy attribute which is simply used to mark a method within the
    /// StaticData singleton. The reason this attribute is used is to allow
    /// the correct MethodInfo to be picked up using Reflection rather
    /// than checking the method name against a string. Thats just nasty.
    /// Much better to check for a known attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class ItemsSourceLookUpMethodAttribute : Attribute
    {
        #region Ctor
        public ItemsSourceLookUpMethodAttribute()
        {

        }
        #endregion
    }
}
