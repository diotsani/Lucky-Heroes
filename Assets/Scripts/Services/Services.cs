using System;
using System.Collections.Generic;

namespace Services
{
    public static class Services
    {
        private static readonly Dictionary<Type, object> Service = new Dictionary<Type, object>();

        public static void Register<T>(T service)
        {
            Service[typeof(T)] = service;
        }

        public static T Get<T>()
        {
            return (T)Service[typeof(T)];
        }
    }
}