using System.Linq.Expressions;

namespace LinqToYql.Interfaces
{
    public interface IExpressionConverter<out TResult>
    {
        TResult Convert(Expression expression);
    }
}
