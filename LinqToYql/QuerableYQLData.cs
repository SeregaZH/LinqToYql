﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using LinqToYql.Interfaces;

namespace LinqToYql
{
  public class QuerableYqlData<TData, TQueryResult, TQueryData> : IQueryable<TData>
  {
    private readonly IQuerySettings _settings;
    private readonly IDataLoader _loader;

    #region Constructors

    /// <summary>
    /// This constructor is called by the client to create the data source.
    /// </summary>
    public QuerableYqlData(IQuerySettings settings, IDataLoader loader)
    {
      _settings = settings;
      _loader = loader;
      Provider = new QuerableYqlProvider<TQueryResult, TQueryData>(_settings, _loader);
      Expression = Expression.Constant(this);
    }

    /// <summary>
    /// This constructor is called by Provider.CreateQuery().
    /// </summary>
    /// <param name="provider"></param>
    /// <param name="expression"></param>
    /// <param name="querySettings"></param>
    /// <param name="loader"></param>
    public QuerableYqlData(QuerableYqlProvider<TQueryResult, TQueryData> provider, Expression expression,
                           IQuerySettings querySettings, IDataLoader loader)
    {
      if (provider == null)
      {
        throw new ArgumentNullException("provider");
      }

      if (expression == null)
      {
        throw new ArgumentNullException("expression");
      }

      if (querySettings == null)
      {
        throw new ArgumentNullException("querySettings");
      }

      if (!typeof (IQueryable<TData>).IsAssignableFrom(expression.Type))
      {
        throw new ArgumentOutOfRangeException("expression");
      }

      Provider = provider;
      Expression = expression;
      _settings = querySettings;
      _loader = loader;

    }

    #endregion

    #region Properties

    public IQueryProvider Provider { get; private set; }
    public Expression Expression { get; private set; }

    public Type ElementType
    {
      get { return typeof (TData); }
    }

    #endregion

    #region Enumerators

    public IEnumerator<TData> GetEnumerator()
    {
      return (Provider.Execute<IEnumerable<TData>>(Expression)).GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return (Provider.Execute<IEnumerable>(Expression)).GetEnumerator();
    }

    #endregion
  }
}
