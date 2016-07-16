using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Xml.Linq;
using YQLDataProvider.Models;
using HelperTools;
using AutoMapper;

namespace YQLDataProvider.Mappings
{
    internal static class XDocumentToYqlDataMappings
    {
        public static void CreateIndustryMap()
        {
            Mapper.Initialize(cfg => {
                cfg.CreateMap<string, int>().ConvertUsing((source) => Convert.ToInt32(source));
                cfg.CreateMap<XElement, YqlIndustry>()
                   .ForMember(dest => dest.Id, opt => opt.ResolveUsing(x => Guid.NewGuid()))
                   .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.With(f => f.Attribute("name")).With(x => x.Value)))
                   .ForMember(dest => dest.Key, opt => opt.MapFrom(src => src.With(f => f.Attribute("id")).With(x => x.Value)))
                   .ForMember(dest => dest.Companies, opt => opt.ResolveUsing(x => Mapper.Map<IEnumerable<XElement>, IEnumerable<YqlCompany>>(x.With(z => z.Elements("company")))));

                cfg.CreateMap<XElement, YqlCompany>()
                  .ForMember(dest => dest.Id, opt => opt.ResolveUsing(x => Guid.NewGuid()))
                  .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Attribute("name").With(x => x.Value)))
                  .ForMember(dest => dest.Symbol, opt => opt.MapFrom(src => src.Attribute("symbol").With(x => x.Value)))
                  .ForMember(dest => dest.Quoteis, opt => opt.ResolveUsing(x => new List<YqlQuote>()));
            });
        }

        public static void CreateSectorsMap()
        {
            Mapper.Initialize(cfg => {
                cfg.CreateMap<string, int>().ConvertUsing((source) => Convert.ToInt32(source));
                cfg.CreateMap<XElement, YqlIndustrySector>()
                  .ForMember(src => src.Id, opt => opt.ResolveUsing(x => Guid.NewGuid()))
              .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Attribute("name").With(x => x.Value)))
              .ForMember(dest => dest.Industries, opt => opt.
                  ResolveUsing(x => Mapper.Map<IEnumerable<XElement>, IEnumerable<YqlIndustry>>(x.With(z => z.Elements("industry")))));

                cfg.CreateMap<XElement, YqlIndustry>()
               .ForMember(src => src.Id, opt => opt.ResolveUsing(x => Guid.NewGuid()))
               .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Attribute("name").With(x => x.Value)))
               .ForMember(dest => dest.Key, opt => opt.MapFrom(src => src.Attribute("id").With(x => x.Value)));
            });
        }

        public static void CreateQuoteMap()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<string, float?>().ConvertUsing<StringToNullableFloat>();
                cfg.CreateMap<string, DateTime?>().ConvertUsing<StringToNullableDateTime>();
                cfg.CreateMap<string, long?>().ConvertUsing<StringToNullableLong>();
                var mapping = cfg.CreateMap<XElement, YqlQuote>()
                  .ForMember(src => src.Id, opt => opt.ResolveUsing(x => Guid.NewGuid()));

                var properties = typeof(YqlQuote).GetProperties();
                foreach (var propertyInfo in properties)
                {

                    var lambdaParameter = Expression.Parameter(typeof(YqlQuote));
                    var sourcePropExp = Expression.Lambda<Func<YqlQuote, object>>(Expression.Convert(Expression.Property(lambdaParameter, propertyInfo), typeof(object)), new[] { lambdaParameter });
                    mapping = mapping.ForMember(sourcePropExp, opt => opt.MapFrom(src => src.With(x => x.Element(propertyInfo.Name)).With(y => y.Value)));
                }
            });

        }

        public static void CreateXChangeRateMap()
        {
            Mapper.Initialize(cfg => {
                cfg.CreateMap<string, float?>().ConvertUsing<StringToNullableFloat>();
                cfg.CreateMap<string, DateTime?>().ConvertUsing<StringToNullableDateTime>();
                cfg.CreateMap<XElement, YqlPair>()
                  .ForMember(src => src.Id, opt => opt.ResolveUsing(x => Guid.NewGuid()))
                  .ForMember(src => src.Rateses, opt => opt.ResolveUsing(x => new List<YqlXchangeRate>()))
                  .ForMember(src => src.Name, opt => opt.MapFrom(dst => dst.With(x => x.Element("Name")).With(y => y.Value)))
                  .ForMember(src => src.PairId, opt => opt.MapFrom(dst => dst.With(x => x.Attribute("id")).With(y => y.Value)));

                cfg.CreateMap<XElement, YqlXchangeRate>()
                .ForMember(src => src.Id, opt => opt.ResolveUsing(x => Guid.NewGuid()))
                .ForMember(src => src.Ask, opt => opt.MapFrom(dst => dst.With(x => x.Element("Ask").With(y => y.Value))))
                .ForMember(src => src.Bid, opt => opt.MapFrom(dst => dst.With(x => x.Element("Bid").With(y => y.Value))))
                .ForMember(src => src.TradeDate, opt => opt.MapFrom(dst => dst.With(x => x.Element("Date").With(y => y.Value))))
                .ForMember(src => src.Rate, opt => opt.MapFrom(dst => dst.With(x => x.Element("Rate").With(y => y.Value))))
                .ForMember(src => src.Pair, opt => opt.ResolveUsing(Mapper.Map<YqlPair>));

            });
        }
    }
}
