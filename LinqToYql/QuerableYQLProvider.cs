﻿using System;
using System.Linq;
using System.Linq.Expressions;
using LinqToYql.Interfaces;

namespace LinqToYql
{
    public class QuerableYQLProvider<TQueryResult,TQueryData> : IQueryProvider
    {
        private readonly IQuerySettings _settings;
        private readonly IDataLoader _loader;
     
        public QuerableYQLProvider(IQuerySettings settings, IDataLoader loader)
        {
            _settings = settings;
            _loader = loader;
        }
        
        public IQueryable CreateQuery(Expression expression)
        {
            Type elementType = TypeSystem.GetElementType(expression.Type);
            try
            {
                return
                    (IQueryable)
                    Activator.CreateInstance(typeof(QuerableYQLData<,,>).MakeGenericType(elementType),
                                             new object[] {this, expression});
            }
            catch (System.Reflection.TargetInvocationException tie)
            {
                throw tie.InnerException;
            }
        }

        // Queryable's collection-returning standard query operators call this method.
        public IQueryable<TResult> CreateQuery<TResult>(Expression expression)
        {
            return new QuerableYQLData<TResult, TQueryResult,TQueryData>(this, expression, _settings,_loader);
        }

        public object Execute(Expression expression)
        {
            return YQLQueryContext.Execute<TQueryResult,TQueryData>(expression, false, _settings, _loader);
        }

        // Queryable's "single value" standard query operators call this method.
        // It is also called from QueryableTerraServerData.GetEnumerator().
        public TResult Execute<TResult>(Expression expression)
        {
            bool IsEnumerable = (typeof (TResult).Name == "IEnumerable`1");

            return (TResult) YQLQueryContext.Execute<TQueryResult,TQueryData>(expression, IsEnumerable, _settings,_loader);
        }
    }
}


