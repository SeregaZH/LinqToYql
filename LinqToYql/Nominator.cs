using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace LinqToYql
{
    /// <summary>
    /// Performs bottom-up analysis to determine which nodes can possibly
    /// be part of an evaluated sub-tree.
    /// </summary>
    internal class Nominator : ExpressionVisitor
    {
        private Func<Expression, bool> fnCanBeEvaluated;
        private HashSet<Expression> candidates;
        private bool cannotBeEvaluated;

        internal Nominator(Func<Expression, bool> fnCanBeEvaluated)
        {
            this.fnCanBeEvaluated = fnCanBeEvaluated;
        }

        internal HashSet<Expression> Nominate(Expression expression)
        {
            this.candidates = new HashSet<Expression>();
            this.Visit(expression);
            return this.candidates;
        }

        public override Expression Visit(Expression expression)
        {
            if (expression != null)
            {
                bool saveCannotBeEvaluated = this.cannotBeEvaluated;
                this.cannotBeEvaluated = false;
                base.Visit(expression);
                if (!this.cannotBeEvaluated)
                {
                    if (this.fnCanBeEvaluated(expression))
                    {
                        this.candidates.Add(expression);
                    }
                    else
                    {
                        this.cannotBeEvaluated = true;
                    }
                }
                this.cannotBeEvaluated |= saveCannotBeEvaluated;
            }
            return expression;
        }
    }
}
