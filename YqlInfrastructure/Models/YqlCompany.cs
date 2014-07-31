using System;
using System.Collections.Generic;
using LinqToYql.Attributes;

namespace YQLDataProvider.Models
{
    public class YqlCompany
    {
        [YqlIgnore]
        public Guid Id { get; set; }
        [YqlName("name")]
        public string Name { get; set; }
        [YqlKey("symbol")]
        [YqlName("symbol")]
        public string Symbol { get; set; }
        public YqlIndustry Industry { get; set; }
        public ICollection<YqlQuote> Quoteis { get; set; }
    }
}
