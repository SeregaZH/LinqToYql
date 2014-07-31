using System;
using System.Linq.Expressions;
using LinqToYql.Interfaces;

namespace LinqToYql.Converters
{
  internal class LinqToStringQueryConverter : IExpressionConverter<string>
  {
    private readonly string _method;
    private IExpressionConverter<string> _expresssionConverter;

    public LinqToStringQueryConverter(string method)
    {
      _method = method;
      Init();
    }

    private void Init()
    {
      switch (_method)
      {
        case "Where":
          {
            _expresssionConverter = new WhereLambdaConverter();
            break;
          }
        case "Select":
          {
            _expresssionConverter = new SelectLambdaConverter();
            break;
          }
        default:
          throw new InvalidOperationException("Operation doesn't support");
      }
    }

    public string Convert(Expression expression)
    {
      return _expresssionConverter.Convert(expression);
    }
  }
}
