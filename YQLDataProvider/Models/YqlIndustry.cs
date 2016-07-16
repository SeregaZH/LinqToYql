using System;
using System.Collections.Generic;
using LinqToYql.Attributes;

namespace YQLDataProvider.Models
{
    public class YqlIndustry
    {
        [YqlKeyAttribute("id")]
        [YqlName("id")]
        public int Key { get; set; }

        [YqlIgnore]
        public Guid Id { get; set; }

        [YqlName("name")]
        public string Name { get; set; }

        public YqlIndustrySector Sector { get; set; }

        public ICollection<YqlCompany> Companies { get; set; }
    }
}
