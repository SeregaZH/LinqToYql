using System.Linq;
using System.Linq.Expressions;

namespace LinqToYql
{
  internal class ExpressionTreeModifier<TData, TQueryResult> : ExpressionVisitor
  {
    private readonly IQueryable<TData> _queryableData;

    internal ExpressionTreeModifier(IQueryable<TData> data)
    {
      _queryableData = data;
    }

    internal Expression CopyAndModify(Expression expression)
    {
      return Visit(expression);
    }

    protected override Expression VisitConstant(ConstantExpression c)
    {
      return c.Type == typeof (QuerableYqlData<TData, TQueryResult, TData>) ? Expression.Constant(_queryableData) : c;
    }
  }
}
