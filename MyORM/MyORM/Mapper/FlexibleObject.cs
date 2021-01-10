using System;
using System.Reflection;

namespace MyORM.Mapper
{
    public class FlexibleObject<T> where T : class, new()
    {
        private T core;

        public FlexibleObject(T core)
        {
            this.core = core;
        }

        public void set(string propertyName, object targetValue)
        {
            PropertyInfo prop = core.GetType().GetProperty("Name", BindingFlags.Public | BindingFlags.Instance);
            if (null != prop && prop.CanWrite)
            {
                prop.SetValue(core, targetValue, null);
            }
        }

        public Type getType(string propertyName)
        {
            return core.GetType().GetProperty(propertyName)?.GetType();
        }
    }
}