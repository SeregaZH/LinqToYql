using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using LinqToYql.Exceptions;
using LinqToYql.Interfaces;

namespace LinqToYql
{
    internal class WhereLambdaConverter : ExpressionVisitor, IExpressionConverter<string>
    {
        private readonly StringBuilder _result;
        private readonly Stack<string> _operators;
        private readonly StringBuilder _values;
        private bool _isOperatorQuery;
        private bool _isKeyFounded;
        private bool _isSuccessConvert;
        
        private const string Keyword = " where";
        private const string ValuesKeyword = " in ";

        public WhereLambdaConverter()
        {
            _result = new StringBuilder(Keyword);
            _values = new StringBuilder(ValuesKeyword);
            _operators = new Stack<string>();
            _isOperatorQuery = false;
            _isKeyFounded = false;
            _isSuccessConvert = true;
        }

        public string Convert(Expression expression)
        {
            Visit(expression);
            if(!_isKeyFounded)
                throw new YqlKeyNotFoundedException(string.Format("Key for type {0} not founded", expression.Type));
            return _isSuccessConvert ? _result.ToString() : null;
        }

        public override Expression Visit(Expression node)
        {
            if(node!=null)
            {
                if (node.NodeType == ExpressionType.Equal)
                {
                    _operators.Push("=");
                    _isOperatorQuery = true;
                }
            }
            return base.Visit(node);
        }
            
        protected override Expression VisitConstant(ConstantExpression node)
        {
            if(_isOperatorQuery)
                 _result.Append(string.Format("\"{0}\"", node.Value));
            return base.VisitConstant(node);
        }

        protected override Expression VisitMethodCall(MethodCallExpression node)
        {
            var callObj = ((ConstantExpression)node.Arguments[0]).Value;
            switch (node.Method.Name)
            {
                case "Contains" :
                    {
                        _isOperatorQuery = false;
                        _isSuccessConvert = CreateContainsStatement(callObj);
                        break;
                    }
            }
            return base.VisitMethodCall(node);
        }

        protected override Expression VisitMember(MemberExpression node)
        {
            if (KeyFinder.PropertyIsKey(node.Member.Name, node.Expression.Type))
            {
                _isKeyFounded = true;
                var keyName = KeyFinder.GetKeyNameForProperty(node.Member.Name, node.Expression.Type);
                if (_isOperatorQuery)
                    _result.Append(string.Format(" {0}{1}", keyName, _operators.Pop()));
                else
                    _result.Append(string.Format(" {0}{1}", keyName, _values));
            }
            return base.VisitMember(node);
        }

        private bool CreateContainsStatement(object callObject)
        {
            var callObj = callObject as IEnumerable;
            if(callObj == null)
                throw new InvalidOperationException("For perform this operation object should be enumerable");
            
            _values.Append("(");

            if (!callObj.Cast<object>().Any())
                return false;
            
            foreach (var var in callObj)
                _values.Append(string.Format("\"{0}\",", var));
            
            _values.Remove(_values.Length - 1, 1);
            _values.Append(")");

            return true;
        }
    }
}
