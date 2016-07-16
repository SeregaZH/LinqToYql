using System.Configuration;
using System.Linq;
using HelperTools;

namespace YQLDataProvider.Configuration
{
    public class TablesNameCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new TableNameElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((TableNameElement)element).Key;
        }

        public TableNameElement this[int index]
        {
            get { return (TableNameElement)BaseGet(index); }
        }

        public new TableNameElement this[string key]
        {
            get
            {
                return
                    (from TableNameElement element in this select element).FirstOrDefault(
                        el => el.With(x => x.Key).Returns(y => y.Equals(key), false));
            }
        }
    }
}
