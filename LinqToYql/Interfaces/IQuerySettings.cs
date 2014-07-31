namespace LinqToYql.Interfaces
{
    public interface IQuerySettings
    {
        string BaseUri { get; }
        bool IsExtensionsTableIncluded { get;  }
        string TableName { get;  }
        string Postfix { get; }
    }
}
