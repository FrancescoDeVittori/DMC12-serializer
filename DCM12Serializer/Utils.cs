/*
 * Copyright (c) 2006-2012 Francesco De Vittori, Board International SA
 *
 * Distributed under the MIT license.
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies
 * of the Software, and to permit persons to whom the Software is furnished to do
 * so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in all
 * copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED,
 * INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A
 * PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
 * HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF
 * CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE
 * OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
 * 
 */

using System;
using System.Reflection;
using System.Runtime.Serialization;

namespace DMC12Serializer
{
    internal static class Utils
    {
        internal static bool IsSimpleType(this Type type)
        {
            return type.IsValueType || type == typeof(string) || type == typeof(object);
        }

        internal static bool IsSystemClass(this Type type)
        {
            // somewhat arbitrary...
            return type.AssemblyQualifiedName.StartsWith("System.");
        }

        internal static object CreateInstance(this PropertyInfo property)
        {
            return property.CreateInstance(0);
        }

        internal static object CreateInstance(this PropertyInfo property, int arrayLength)
        {
            if (property == null)
                return null;

            return property.PropertyType.CreateInstance(
                arrayLength,
                string.Format("Cannot create a default value for property '{0}.{1}'. Please supply a value, or add a public parameterless constructor to Type '{2}'.",
                    property.DeclaringType.Name,
                    property.Name,
                    property.PropertyType.Name));
        }

        internal static object CreateElementInstance(this Array array)
        {
            if (array == null)
                return null;

            var elementType = array.GetType().GetElementType();
            return elementType.CreateInstance(0,
                string.Format("Please supply a value, or add a public parameterless constructor to Type '{0}'", elementType.Name));
        }

        internal static object CreateInstance(this Type type)
        {
            return type.CreateInstance(0);
        }

        internal static object CreateInstance(this Type type, int arrayLength)
        {
            if (type == null)
                return null;

            return type.CreateInstance(arrayLength, string.Format("Type '{0}' must expose a public parameterless constructor.", type.Name));
        }

        internal static object CreateInstance(this Type type, int arrayLength, string errorMessage)
        {
            if (type == null)
                return null;

            if (type == typeof(string))
                return string.Empty;

            if (type.IsArray && type.GetArrayRank() == 1)
                return Array.CreateInstance(type.GetElementType(), arrayLength);

            try
            {
                return Activator.CreateInstance(type);
            }
            catch (MissingMethodException ex)
            {
                throw new SerializationException(errorMessage, ex);
            }
        }

        internal static bool MustIgnore(this PropertyInfo property)
        {
            var att = property.GetCustomAttributes(typeof(DoNotSerializeAttribute), false);
            return att != null && att.Length > 0;
        }

        internal static ArrayDefinition GetArrayDefinition(this PropertyInfo property)
        {
            var isFixedLength = false;
            var fixedLength = -1;

            var fixedLenAttribute = property.GetCustomAttributes(typeof(FixedLengthAttribute), false);
            if (fixedLenAttribute != null && fixedLenAttribute.Length > 0)
            {
                isFixedLength = true;
                fixedLength = ((FixedLengthAttribute)fixedLenAttribute[0]).Length;

                if (fixedLength < 0)
                {
                    throw new SerializationException(string.Format(
                        "Invalid fixed array length ({0}) in '{1}.{2}'. Array length must be non-negative.",
                        fixedLength,
                        property.DeclaringType.Name,
                        property.Name));
                }
            }

            var isDynamic = false;
            var dynamicFrom = -1;

            var dynamicLengthAttribute = property.GetCustomAttributes(typeof(DynamicLengthAttribute), false);
            if (dynamicLengthAttribute != null && dynamicLengthAttribute.Length > 0)
            {
                isDynamic = true;
                dynamicFrom = ((DynamicLengthAttribute)dynamicLengthAttribute[0]).From;
            }

            if (isFixedLength && isDynamic)
            {
                throw new SerializationException(
                    string.Format("Property '{0}.{1}' is decorated with DynamicLengthAttribute and FixedLengthAttribute. You cannot use both on the same property.",
                    property.DeclaringType.Name,
                    property.Name));
            }

            if (isFixedLength)
            {
                return ArrayDefinition.Fixed(fixedLength);
            }
            else if (isDynamic)
            {
                return ArrayDefinition.Dynamic(dynamicFrom);
            }
            else
            {
                return ArrayDefinition.Default;
            }
        }

        internal static int GetConstantLengthString(this PropertyInfo property)
        {
            var att = property.GetCustomAttributes(typeof(ConstantLengthString), false);
            if (att != null && att.Length > 0)
            {
                var len = ((ConstantLengthString)att[0]).Length;
                if (len < 0)
                {
                    throw new SerializationException(
                        string.Format("Invalid constant string length ({0}) in property '{0}.{1}'. Constant string lengths must be non negative.",
                            len,
                            property.DeclaringType.Name,
                            property.Name));
                }
                else
                {
                    return len;
                }
            }

            return -1;
        }
    }
}
