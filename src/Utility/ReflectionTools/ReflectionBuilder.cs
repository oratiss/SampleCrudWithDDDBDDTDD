using System.Linq.Expressions;
using System.Reflection;

namespace Utility.ReflectionTools
{
    public abstract class ReflectionBuilder<TReflection, TBuilder>
          where TReflection : class
          where TBuilder : ReflectionBuilder<TReflection, TBuilder>
    {
        private readonly TBuilder? _builderInstance = null;

        public ReflectionBuilder()
        {
            //store the concrete builder instance
            _builderInstance = (TBuilder)this;
        }

        private class PropertyWrapper
        {
            public PropertyInfo Property { get; }
            public object Value { get; set; }

            internal PropertyWrapper(PropertyInfo property, object value)
            {
                Property = property;
                Value = value;
            }
        }

        public TBuilder With<TValue>(Expression<Func<TReflection, TValue>> exp, TValue value)
        {
            var body = exp.Body as MemberExpression;
            if (body == null)
            {
                throw new InvalidOperationException("Improperly formatted expression");
            }

            var propertyName = body.Member.Name;

            var property = GetType().GetRuntimeField(propertyName);

            if (property != null)
            {
                property.SetValue(_builderInstance, value);
            }
            else
            {
                GetType().GetField(propertyName, BindingFlags.NonPublic | BindingFlags.Instance)
                    ?.SetValue(_builderInstance, value);
            }

            return _builderInstance!;
        }


        public abstract TReflection Build();

    }
}