using System;
using System.Collections.Generic;

namespace YQLDataProvider.Models
{
    public class YqlPair
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string PairId { get; set; }
        public ICollection<YqlXchangeRate> Rateses { get; set; }
    }
}
