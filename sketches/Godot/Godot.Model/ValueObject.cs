using System;
using System.Collections.Generic;
using System.Reflection;

namespace Godot.Model
{
    public abstract class ValueObject<TValue> : IEquatable<TValue> where TValue : ValueObject<TValue>
    {
        public bool Equals(TValue other)
        {
            if (other == null)
                return false;

            var leftType = GetType();
            var otherType = other.GetType();
            if (leftType != otherType)
                return false;

            var fields = GetFieldsFromTypeHierarchy();
            foreach (var field in fields)
            {
                var value1 = field.GetValue(this);
                var value2 = field.GetValue(other);
                if (value1 == null)
                {
                    if (value2 != null)
                        return false;
                }
                else if (!value1.Equals(value2))
                    return false;
            }
            return true;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            return Equals(obj as TValue);
        }

        public override int GetHashCode()
        {
            var fields = GetFieldsFromTypeHierarchy();
            const int startValue = 17;
            const int multiplier = 59;

            var hashcode = startValue;
            foreach (var field in fields)
            {
                var value = field.GetValue(this);
                hashcode = (hashcode * multiplier) + (value != null ? value.GetHashCode() : startValue);
            }
            return hashcode;
        }

        IEnumerable<FieldInfo> GetFieldsFromTypeHierarchy()
        {
            var t = GetType();
            var fields = new List<FieldInfo>();
            while (t != typeof(object))
            {
                fields.AddRange(t.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public));
                t = t.BaseType;
            }
            return fields;
        }

        public static bool operator ==(ValueObject<TValue> left, ValueObject<TValue> right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(ValueObject<TValue> left, ValueObject<TValue> right)
        {
            return !Equals(left, right);
        }
    }
}