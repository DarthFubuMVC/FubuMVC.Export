using System;

namespace FubuMVC.Export
{
    public static class ExportTypeExtensions
    {
        private static readonly Predicate<Type> DefaultPredicate = (type) => type.BaseType == typeof (object);

        public static Type GetBaseType<T>()
        {
            return GetBaseType<T>(DefaultPredicate);
        }

        public static Type GetBaseType<T>(Predicate<Type> matches)
        {
            return GetBaseType(typeof (T), matches);
        }

        public static Type GetBaseType(this Type type)
        {
            return GetBaseType(type, DefaultPredicate);
        }

        public static Type GetBaseType(this Type type, Predicate<Type> matches)
        {
            while (type != null)
            {
                if (matches(type))
                    break;
                type = type.BaseType;
            }

            return type;
        }
    }
}
