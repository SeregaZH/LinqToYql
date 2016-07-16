using System;
using LinqToYql.Attributes;

namespace YQLDataProvider.Models
{
    public class YqlXchangeRate
    {
        [YqlIgnore]
        public Guid Id { get; set; }
        [YqlKey("pair")]
        public YqlPair Pair { get; set; }
        public float? Rate { get; set; }
        [YqlName("Date")]
        public DateTime? TradeDate { get; set; }
        public float? Ask { get; set; }
        public float? Bid { get; set; }
    }
}
