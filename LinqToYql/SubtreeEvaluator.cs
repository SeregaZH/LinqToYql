using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace LinqToYql
{
  /// <summary>
  /// Evaluates & replaces sub-trees when first candidate is reached (top-down)
  /// </summary>
  internal class SubtreeEvaluator : ExpressionVisitor
  {
    private readonly HashSet<Expression> _candidates;

    internal SubtreeEvaluator(HashSet<Expression> candidates)
    {
      _candidates = candidates;
    }

    internal Expression Eval(Expression exp)
    {
      return Visit(exp);
    }

    public override Expression Visit(Expression exp)
    {
      if (exp == null)
        return null;
      return _candidates.Contains(exp) ? Evaluate(exp) : base.Visit(exp);
    }

    private Expression Evaluate(Expression e)
    {
      if (e.NodeType == ExpressionType.Constant)
        return e;
      var lambda = Expression.Lambda(e);
      Delegate fn = lambda.Compile();
      return Expression.Constant(fn.DynamicInvoke(null), e.Type);
    }
  }
}
    

