using System;
using System.Runtime.InteropServices;

namespace WindowsFirewallHelper.InternalHelpers
{
    // ReSharper disable once HollowTypeName
    internal static class ComHelper
    {
        public static T CreateInstance<T>() where T : class, new()
        {
            if (!IsSupported<T>())
            {
                throw new NotSupportedException("This type is not supported in current environment.");
            }

            try
            {
                return new T();
                //var progId = ComClassProgIdAttribute.GetClassProgId<T>();

                //if (!string.IsNullOrEmpty(progId?.Trim()))
                //{
                //    var typeByProgId = Type.GetTypeFromProgID(progId, false);

                //    if (typeByProgId != null)
                //    {
                //        return (T) Activator.CreateInstance(typeByProgId);
                //    }
                //}

                //var typeByClassId = Type.GetTypeFromCLSID(typeof(T).GUID, false);

                //if (typeByClassId != null)
                //{
                //    return (T) Activator.CreateInstance(typeByClassId);
                //}
            }
            catch (COMException e)
            {
                throw new NotSupportedException(
                    "Can not create a new instance of this interface in current environment.", e);
            }

            throw new NotSupportedException("Can not create a new instance of this interface in current environment.");
        }

        public static bool IsSupported<T>()
        {
            if (!typeof(T).IsInterface)
            {
                throw new ArgumentException("Invalid generic type passed.", nameof(T));
            }

            if (Type.GetTypeFromCLSID(typeof(T).GUID, false) == null)
            {
                return false;
            }

            //var progId = ComClassProgIdAttribute.GetClassProgId<T>();

            //if (!string.IsNullOrEmpty(progId?.Trim()) &&
            //    Type.GetTypeFromProgID(progId, false) == null)
            //{
            //    return false;
            //}

            return true;
        }
    }
}