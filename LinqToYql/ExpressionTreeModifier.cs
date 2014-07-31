using System.Linq;
using System.Linq.Expressions;

namespace LinqToYql
{
    internal class ExpressionTreeModifier<TData, TQueryResult> : ExpressionVisitor
    {
        private readonly IQueryable<TData> queryableData;

        internal ExpressionTreeModifier(IQueryable<TData> data)
        {
            queryableData = data;
        }

        internal Expression CopyAndModify(Expression expression)
        {
            return Visit(expression);
        }

        protected override Expression VisitConstant(ConstantExpression c)
        {
           return c.Type == typeof (QuerableYQLData<TData, TQueryResult, TData>) ? Expression.Constant(queryableData) : c;
        }
    }
}
