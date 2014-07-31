using System;
using System.Collections.Generic;
using LinqToYql.Attributes;

namespace YQLDataProvider.Models
{
    public class YqlIndustrySector
    {
        [YqlIgnore]
        public Guid Id { get; set; }
        [YqlName("name")]
        public string Name { get; set; }
        public ICollection<YqlIndustry> Industries { get; set; }
    }
}
