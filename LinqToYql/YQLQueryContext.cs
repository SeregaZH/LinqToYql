﻿using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using LinqToYql.Converters;
using LinqToYql.Finders;
using LinqToYql.Interfaces;
using LinqToYql.Interfaces.YqlBaseDataLoader;

namespace LinqToYql
{
  internal static class YqlQueryContext
  {
    // Executes the expression tree that is passed to it.
    internal static object Execute<TQueryResult, TQueryData>(Expression expression, bool isEnumerable,
                                                             IQuerySettings settings, IDataLoader loader)
    {

      var whereQuery = GetPartOfWhereQuery(expression);
      var selectQuery = GetPartOfSelectQuery(expression);
      var collection = (new List<TQueryData>()).AsQueryable();

      if (whereQuery != null && selectQuery != null)
      {
        var queryBuilder = new YqlQueryBuilder(settings.BaseUri, settings.IsExtensionsTableIncluded,
                                               settings.TableName, settings.Postfix);

        var stringQuery = queryBuilder.Build(selectQuery, whereQuery);
        var executer = ExecuterFactory.CreateQueryExecuter(typeof (TQueryResult), stringQuery);
        collection = loader.LoadData(executer.Execute()).Select(x => (TQueryData) x).AsQueryable();
      }

      var modifier = new ExpressionTreeModifier<TQueryData, TQueryResult>(collection);
      var newExpression = modifier.CopyAndModify(expression);

      return isEnumerable ? collection.Provider.CreateQuery(newExpression) : collection.Provider.Execute(newExpression);
    }

    private static string GetPartOfWhereQuery(Expression expression)
    {
      var result = string.Empty;
      var whereFinder = new MethodFinder("Where");
      var whereExpression = whereFinder.GetMethod(expression);
      if (whereExpression != null)
      {
        var whereLambda = (LambdaExpression) ((UnaryExpression) whereExpression.Arguments[1]).Operand;
        var partialEvalwhereExpr = (LambdaExpression) ExpressionEvaluator.PartialEval(whereLambda);
        var whereQueryConverter = new LinqToStringQueryConverter("Where");
        result = whereQueryConverter.Convert(partialEvalwhereExpr);
      }
      return result;
    }

    private static string GetPartOfSelectQuery(Expression expression)
    {
      Expression partialEvalselectExpr = null;
      var selectFinder = new MethodFinder("Select");
      var selectExpression = selectFinder.GetMethod(expression);
      if (selectExpression != null)
      {
        var selectLambda = (LambdaExpression) ((UnaryExpression) selectExpression.Arguments[1]).Operand;
        partialEvalselectExpr = ExpressionEvaluator.PartialEval(selectLambda);
      }
      var selectQueryConverter = new LinqToStringQueryConverter("Select");
      string result = selectQueryConverter.Convert(partialEvalselectExpr);
      return result;
    }
  }
}