﻿using System;
using System.Reflection;

namespace Vip.Xamarin.Base
{
    internal static class CommandHelper
    {
        internal static bool IsValidCommandParameter<T>(object o)
        {
            bool valid;
            if (o != null)
            {
                // The parameter isn't null, so we don't have to worry whether null is a valid option
                valid = o is T;

                if (!valid)
                    throw new InvalidCommandParameterException(typeof(T), o.GetType());

                return true;
            }

            var t = typeof(T);

            // The parameter is null. Is T Nullable?
            if (Nullable.GetUnderlyingType(t) != null) return true;

            // Not a Nullable, if it's a value type then null is not valid
            valid = !t.GetTypeInfo().IsValueType;

            if (!valid) throw new InvalidCommandParameterException(typeof(T));
            return true;
        }
    }
}