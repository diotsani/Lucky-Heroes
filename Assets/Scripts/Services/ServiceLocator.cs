using System;
using System.Collections.Generic;

namespace Services
{
    public static class ServiceLocator
    {
        private static readonly Dictionary<Type, object> Service = new Dictionary<Type, object>();

        public static void Register<T>(T service)
        {
            Service[typeof(T)] = service;
        }

        public static void Unregister<T>(T service)
        {
            Service.Remove(service.GetType());
        }

        public static T Get<T>()
        {
            return (T)Service[typeof(T)];
        }
    }
}