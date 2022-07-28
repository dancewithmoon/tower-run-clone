using System;
using System.Collections.Generic;

namespace Infrastructure.SignalBus
{
    public static class SignalBus 
    {
        private static Dictionary<Type, Action<BaseSignal>> bindings = new Dictionary<Type, Action<BaseSignal>>();

        public static void AddListener<T>(Action<BaseSignal> method) where T : BaseSignal
        {
            if (bindings.ContainsKey(typeof(T)))
            {
                bindings[typeof(T)] += method;
            }
            else
            {
                bindings.Add(typeof(T), method);
            }
        }

        public static void RemoveListener<T>(Action<BaseSignal> method) where T : BaseSignal
        {
            if (bindings.ContainsKey(typeof(T)))
            {
                bindings[typeof(T)] -= method;
            }
        }

        public static void Dispatch<T>(T data) where T : BaseSignal
        {
            if (bindings.ContainsKey(typeof(T)))
            {
                bindings[typeof(T)]?.Invoke(data);
            }
        }
    }
}