using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Ocph.DAL
{
    public interface IProvierHelper
    {
        object ConverConstant(object value);
        object AnotherValue(object value);
        object CreateParameter(object fieldName, PropertyInfo p, object source);
    }
}
