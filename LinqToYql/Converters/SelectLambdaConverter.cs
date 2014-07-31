using System;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using LinqToYql.Finders;
using LinqToYql.Helpers;
using LinqToYql.Interfaces;

namespace LinqToYql.Converters
{
  internal class SelectLambdaConverter : ExpressionVisitor, IExpressionConverter<string>
  {
    private readonly StringBuilder _result;
    private readonly StringBuilder _projection;
    private readonly StringBuilder _nameProjection;
    private StringBuilder _keyProjection;
    
    private const string Keyword = "select * ";

    public SelectLambdaConverter()
    {
      _result = new StringBuilder(Keyword);
      _projection = new StringBuilder();
      _keyProjection = new StringBuilder();
      _nameProjection = new StringBuilder();
    }

    public string Convert(Expression expression)
    {
      Visit(expression);
      _projection.Append(ConcatProjection(_nameProjection, _keyProjection));
      if (_projection.Length != 0)
        _result.Replace("*", _projection.ToString());
      return _result.ToString();
    }

    protected override Expression VisitNew(NewExpression node)
    {
      var argument = (MemberExpression) node.Arguments.FirstOrDefault();
      var expType = argument == null ? node.Type : argument.Expression.Type;

      _keyProjection = CreateKeysForSelectProjection(expType);

      return base.VisitNew(node);
    }

    protected override Expression VisitMember(MemberExpression node)
    {
      AppendNameForSelectProjection(node, node.Expression.Type);
      return base.VisitMember(node);
    }

    private void AppendNameForSelectProjection(MemberExpression members, Type expType)
    {
      if (!NameFinder.IsIgnored(members.Member.Name, expType))
        _nameProjection.Append(string.Format("{0},",
                                             NameFinder.GetPropertyName(members.Member.Name, expType)));
    }

    private static StringBuilder CreateKeysForSelectProjection(Type expType)
    {
      var projection = new StringBuilder();
      var keyNames = KeyFinder.GetKeysForType(expType);
      foreach (var key in keyNames)
        projection.Append(string.Format("{0},", key));
      return projection;
    }

    /// <summary>
    /// Concat name and keys projection string and remove final comma
    /// </summary>
    /// <param name="nameProj">name projection</param>
    /// <param name="keyProj">projection keys</param>
    /// <returns>result string</returns>
    private static string ConcatProjection(StringBuilder nameProj, StringBuilder keyProj)
    {
      keyProj.Append(nameProj);
      if (keyProj.Length != 0)
        keyProj.Remove(keyProj.Length - 1, 1);
      return keyProj.ToString();
    }
  }
}
