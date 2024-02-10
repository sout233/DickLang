using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DickLang
{
    public static class DickValueProcess
    {
        public static object Add(object? left, object? right)
        {
            if (left is int l && right is int r)
                return l + r;
            if (left is float lf && right is float rf)
                return lf + rf;
            if (left is int li2 && right is float rf2)
                return li2 + rf2;
            if (left is float lf3 && right is int ri3)
                return lf3 + ri3;
            if (left is string ls && right is string rs)
                return ls + rs;
            if (left is string ls2 && right is int ri4)
                return ls2 + ri4.ToString();
            if (left is int li && right is string rs2)
                return li.ToString() + rs2;
            if (left is float lf4 && right is string rs3)
                return lf4.ToString() + rs3;
            if (left is string ls3 && right is float rf4)
                return ls3 + rf4.ToString();

            throw new Exception($"Cannot add dicks of types {left?.GetType()} and {right?.GetType()}");
        }

        public static object Subtract(object? left, object? right)
        {
            if (left is int l && right is int r)
                return l - r;
            if (left is float lf && right is float rf)
                return lf - rf;
            if (left is int li2 && right is float rf2)
                return li2 - rf2;
            if (left is float lf3 && right is int ri3)
                return lf3 - ri3;

            throw new Exception($"Cannot subtract dicks of types {left?.GetType()} and {right?.GetType()}");
        }

        public static object Multiply(object? left, object? right)
        {
            if (left is int l && right is int r)
                return l * r;
            if (left is float lf && right is float rf)
                return lf * rf;
            if (left is int li2 && right is float rf2)
                return li2 * rf2;
            if (left is float lf3 && right is int ri3)
                return lf3 * ri3;

            throw new Exception($"Cannot multiply dicks of types {left?.GetType()} and {right?.GetType()}");
        }

        public static object Divide(object? left, object? right)
        {
            if (left is int l && right is int r)
                return l / r;
            if (left is float lf && right is float rf)
                return lf / rf;
            if (left is int li2 && right is float rf2)
                return li2 / rf2;
            if (left is float lf3 && right is int ri3)
                return lf3 / ri3;

            throw new Exception($"Cannot divide dicks of types {left?.GetType()} and {right?.GetType()}");
        }

        public static object Modulo(object? left, object? right)
        {
            if (left is int l && right is int r)
                return l % r;
            if (left is float lf && right is float rf)
                return lf % rf;
            if (left is int li2 && right is float rf2)
                return li2 % (int)rf2;
            if (left is float lf3 && right is int ri3)
                return (int)lf3 % ri3;

            throw new Exception($"Cannot perform modulo operation on dicks of types {left?.GetType()} and {right?.GetType()}");
        }

        public static bool IsEqual(object? left, object? right)
        {
            return Equals(left, right);
        }

        public static bool LessThan(object? left, object? right)
        {
            if (left is int l && right is int r)
                return l < r;
            if (left is float lf && right is float rf)
                return lf < rf;
            if (left is float lf2 && right is int ri2)
                return lf2 < ri2;
            if (left is int li2 && right is float rf2)
                return li2 < rf2;
            if (left is string ls && right is string rs)
                return ls.CompareTo(rs) < 0;

            throw new Exception($"Cannot compare {left?.GetType().Name} and {right?.GetType().Name}");
        }

        public static bool LessThanOrEqual(object? left, object? right)
        {
            return IsEqual(left, right) || LessThan(left, right);
        }

        public static bool GreaterThan(object? left, object? right)
        {
            return !IsEqual(left, right) && !LessThan(left, right);
        }

        public static bool GreaterThanOrEqual(object? left, object? right)
        {
            return IsEqual(left, right) || GreaterThan(left, right);
        }

        public static bool NotEqual(object? left, object? right)
        {
            return !IsEqual(left, right);
        }

        public static bool IsTrue(object? value)
        {
            if (value is bool b)
            {
                return b;
            }
            if (value is int i)
            {
                return i != 0;
            }
            if (value is null)
            {
                return false;
            }
            throw new Exception($"Expected a boolean value, got {value?.GetType()}");
        }

        public static bool IsFalse(object? value)
        {
            return !IsTrue(value);
        }
    }
}
