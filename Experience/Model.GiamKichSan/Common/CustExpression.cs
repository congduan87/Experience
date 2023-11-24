using System.Collections.Generic;
using System.Linq.Expressions;

namespace Model.GiamKichSan.Common
{
    public static class CustExpression
    {
        private const string _PARAM_NAME = "Param";

        public static List<string> GetKeyString(this Expression expression)
        {
            List<string> result = new List<string>();
            if (expression.NodeType == ExpressionType.MemberAccess)
            {
                result.Add(((MemberExpression)expression).Member.Name);
            }
            else if (expression.NodeType == ExpressionType.MemberInit)
            {
                foreach (var item in ((MemberInitExpression)expression).Bindings)
                {
                    result.Add(((MemberAssignment)item).Member.Name);
                }
            }
            else if (expression.NodeType == ExpressionType.New)
            {
                foreach (var item in ((NewExpression)expression).Members)
                {
                    result.Add(item.Name);
                }
            }
            return result;
        }

        public static string GetKeyValue(this Expression expression, Dictionary<string, object> parameters, string prefixParam)
        {
            switch (expression.NodeType)
            {
                //Bitwise Operations
                case ExpressionType.AndAlso:
                case ExpressionType.OrElse:
                    string left = ((BinaryExpression)expression).Left.GetKeyValue(parameters, prefixParam);
                    string right = ((BinaryExpression)expression).Right.GetKeyValue(parameters, prefixParam);
                    string con = expression.NodeType == ExpressionType.AndAlso ? "AND" : "OR";
                    if (!string.IsNullOrEmpty(left) && !string.IsNullOrEmpty(right))
                        return string.Format("({0} {1} {2})", left, con, right);
                    else if (!string.IsNullOrEmpty(left) || !string.IsNullOrEmpty(right))
                        return !string.IsNullOrEmpty(left) ? left : right;
                    break;
                //Comparison Operations
                case ExpressionType.Equal:
                case ExpressionType.NotEqual:
                case ExpressionType.LessThan:
                case ExpressionType.LessThanOrEqual:
                case ExpressionType.GreaterThan:
                case ExpressionType.GreaterThanOrEqual:
                    return ((BinaryExpression)expression).GetComparisonQuery(parameters, prefixParam);
                //Negation Operations
                case ExpressionType.Not:
                    return string.Format("NOT {0}", ((UnaryExpression)expression).Operand.GetKeyValue(parameters, prefixParam));
                //Method Operations
                case ExpressionType.Call:
                    var call = (MethodCallExpression)expression;
                    switch (call.Method.Name)
                    {
                        //Like
                        case "Contains":
                        case "StartsWith":
                        case "EndsWith":
                            return call.GetLikeQuery(parameters, prefixParam);
                        //Compare
                        case "Equals":
                            return call.GetEqualsQuery(parameters, prefixParam);
                    }
                    break;
            }
            return "";
        }

        public static string GetComparisonQuery(this BinaryExpression expression, Dictionary<string, object> parameters, string prefixParam)
        {
            string con = expression.NodeType == ExpressionType.Equal ? "=" :
                expression.NodeType == ExpressionType.NotEqual ? "<>" :
                expression.NodeType == ExpressionType.LessThan ? "<" :
                expression.NodeType == ExpressionType.LessThanOrEqual ? "<=" :
                expression.NodeType == ExpressionType.GreaterThan ? ">" :
                expression.NodeType == ExpressionType.GreaterThanOrEqual ? ">=" : "=";
            string name, nameLeft, nameRight;
            object value, valueLeft, valueRight;
            valueLeft = GetValue(expression.Left, out nameLeft);
            valueRight = GetValue(expression.Right, out nameRight);
            if (!string.IsNullOrEmpty(nameLeft) && !string.IsNullOrEmpty(nameRight))
            {
                return string.Format("{0} {1} {2}", nameLeft, con, nameRight);
            }
            else if (!string.IsNullOrEmpty(nameLeft) || !string.IsNullOrEmpty(nameRight))
            {
                name = string.IsNullOrEmpty(nameLeft) ? nameRight : nameLeft;
                value = string.IsNullOrEmpty(nameLeft) ? valueLeft : valueRight;
                int i = 0;
                while (parameters.ContainsKey(name + i))
                {
                    i++;
                }
                parameters.Add(name + i, value);
                return string.Format("{0} {1} {2}{3}", name, con, prefixParam, name + i);
            }
            else
            {
                int iLeft = 0;
                while (parameters.ContainsKey(_PARAM_NAME + iLeft))
                {
                    iLeft++;
                }
                parameters.Add(_PARAM_NAME + iLeft, valueLeft);

                int iRight = 0;
                while (parameters.ContainsKey(_PARAM_NAME + iRight))
                {
                    iRight++;
                }
                parameters.Add(_PARAM_NAME + iRight, valueRight);

                return string.Format(":{0} {1} {2}{3}", _PARAM_NAME + iLeft, con, prefixParam, _PARAM_NAME + iRight);
            }
        }

        public static string GetEqualsQuery(this MethodCallExpression call, Dictionary<string, object> parameters, string prefixParam)
        {
            string con = "=";
            string name, nameLeft, nameRight;
            object value, valueLeft, valueRight;
            valueLeft = GetValue(call.Object, out nameLeft);
            valueRight = GetValue(call.Arguments[0], out nameRight);
            if (!string.IsNullOrEmpty(nameLeft) && !string.IsNullOrEmpty(nameRight))
            {
                return string.Format("{0} {1} {2}", nameLeft, con, nameRight);
            }
            else if (!string.IsNullOrEmpty(nameLeft) || !string.IsNullOrEmpty(nameRight))
            {
                name = string.IsNullOrEmpty(nameLeft) ? nameRight : nameLeft;
                value = string.IsNullOrEmpty(nameLeft) ? valueLeft : valueRight;
                int i = 0;
                while (parameters.ContainsKey(name + i))
                {
                    i++;
                }
                parameters.Add(name + i, value);
                return string.Format("{0} {1} {2}{3}", name, con, prefixParam, name + i);
            }
            else
            {
                int iLeft = 0;
                while (parameters.ContainsKey(_PARAM_NAME + iLeft))
                {
                    iLeft++;
                }
                parameters.Add(_PARAM_NAME + iLeft, valueLeft);

                int iRight = 0;
                while (parameters.ContainsKey(_PARAM_NAME + iRight))
                {
                    iRight++;
                }
                parameters.Add(_PARAM_NAME + iRight, valueRight);

                return string.Format("{0}{1} {2} {0}{3}", prefixParam, _PARAM_NAME + iLeft, con, _PARAM_NAME + iRight);
            }
        }

        public static string GetLikeQuery(this MethodCallExpression call, Dictionary<string, object> parameters, string prefixParam)
        {
            string nameLeft, nameRight;
            object valueLeft, valueRight;

            valueLeft = GetValue(call.Object, out nameLeft);
            if (string.IsNullOrEmpty(nameLeft))
            {
                int i = 0;
                while (parameters.ContainsKey(_PARAM_NAME + i))
                {
                    i++;
                }
                parameters.Add(_PARAM_NAME + i, valueLeft);
                nameLeft = string.Format("{0}{1}", prefixParam, _PARAM_NAME + i);
            }

            string start = "'%' || ", end = " || '%'";
            switch (call.Method.Name)
            {
                case "StartsWith": start = ""; break;
                case "EndsWith": end = ""; break;
            }
            valueRight = GetValue(call.Arguments[0], out nameRight);
            if (string.IsNullOrEmpty(nameRight))
            {
                int i = 0;
                while (parameters.ContainsKey(_PARAM_NAME + i))
                {
                    i++;
                }
                parameters.Add(_PARAM_NAME + i, valueRight);
                nameRight = string.Format("{0}{1}{2}{3}", start, prefixParam, _PARAM_NAME + i, end);
            }
            else
            {
                nameRight = string.Format("{0}{1}{2}", start, nameRight, end);
            }

            return string.Format("{0} LIKE {1}", nameLeft, nameRight);
        }

        private static object GetValue(this Expression expression, out string name)
        {
            name = "";
            if (expression.NodeType == ExpressionType.MemberAccess
                && ((MemberExpression)expression).Expression.NodeType == ExpressionType.Parameter)
            {
                name = ((MemberExpression)expression).Member.Name;
            }
            else if (expression.NodeType == ExpressionType.Convert)
            {
                return GetValue(((UnaryExpression)expression).Operand, out name);
            }
            else
            {
                return Expression.Lambda(expression).Compile().DynamicInvoke();
            }
            return null;
        }
    }
}
