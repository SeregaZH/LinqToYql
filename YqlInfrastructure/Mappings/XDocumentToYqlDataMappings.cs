using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Xml.Linq;
using AutoMapper;
using AutoMapper.Internal;
using YQLDataProvider.Models;

namespace YQLDataProvider.Mappings
{
  internal static class XDocumentToYqlDataMappings
  {
    public static void CreateIndustryMap()
    {
      Mapper.Reset();
      Mapper.CreateMap<string, int>().ConvertUsing(Convert.ToInt32);
      Mapper.CreateMap<XElement, YqlIndustry>()
            .ForMember(dest => dest.Id, opt => opt.ResolveUsing(x => Guid.NewGuid()))
            .ForMember(dest => dest.Name,
                       opt => opt.MapFrom(src => src.With(f => f.Attribute("name")).With(x => x.Value)))
            .ForMember(dest => dest.Key, opt => opt.MapFrom(src => src.With(f => f.Attribute("id")).With(x => x.Value)))
            .ForMember(dest => dest.Companies,
                       opt =>
                       opt.ResolveUsing(
                         x =>
                         Mapper.Map<IEnumerable<XElement>, IEnumerable<YqlCompany>>(x.With(z => z.Elements("company")))))
            .ConstructUsing((Func<ResolutionContext, YqlIndustry>) (obj => new YqlIndustry()));
      Mapper.CreateMap<XElement, YqlCompany>()
            .ForMember(dest => dest.Id, opt => opt.ResolveUsing(x => Guid.NewGuid()))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Attribute("name").With(x => x.Value)))
            .ForMember(dest => dest.Symbol, opt => opt.MapFrom(src => src.Attribute("symbol").With(x => x.Value)))
            .ForMember(dest => dest.Quoteis, opt => opt.ResolveUsing(x => new List<YqlQuote>()))
            .ConstructUsing((Func<ResolutionContext, YqlCompany>) (obj => new YqlCompany()));
    }

    public static void CreateSectorsMap()
    {
      Mapper.Reset();
      Mapper.CreateMap<string, int>().ConvertUsing(Convert.ToInt32);
      Mapper.CreateMap<XElement, YqlIndustrySector>()
            .ForMember(src => src.Id, opt => opt.ResolveUsing(x => Guid.NewGuid()))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Attribute("name").With(x => x.Value)))
            .ForMember(dest => dest.Industries, opt => opt.
                                                         ResolveUsing(
                                                           x =>
                                                           Mapper.Map<IEnumerable<XElement>, IEnumerable<YqlIndustry>>(
                                                             x.With(z => z.Elements("industry")))))
            .ConstructUsing((Func<ResolutionContext, YqlIndustrySector>) (obj => new YqlIndustrySector()));
      Mapper.CreateMap<XElement, YqlIndustry>()
            .ForMember(src => src.Id, opt => opt.ResolveUsing(x => Guid.NewGuid()))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Attribute("name").With(x => x.Value)))
            .ForMember(dest => dest.Key, opt => opt.MapFrom(src => src.Attribute("id").With(x => x.Value)))
            .ConstructUsing((Func<ResolutionContext, YqlIndustry>) (obj => new YqlIndustry()));

    }

    public static void CreateQuoteMap()
    {
      Mapper.Reset();
      Mapper.CreateMap<string, float?>().ConvertUsing<StringToNullableFloat>();
      Mapper.CreateMap<string, DateTime?>().ConvertUsing<StringToNullableDateTime>();
      Mapper.CreateMap<string, long?>().ConvertUsing<StringToNullableLong>();
      var mapping = Mapper.CreateMap<XElement, YqlQuote>()
                          .ForMember(src => src.Id, opt => opt.ResolveUsing(x => Guid.NewGuid()))
                          .ConstructUsing((Func<ResolutionContext, YqlQuote>) (obj => new YqlQuote()));

      var properties = typeof (YqlQuote).GetProperties();
      foreach (var propertyInfo in properties)
      {
        if (!propertyInfo.PropertyType.IsValueType || propertyInfo.PropertyType.IsNullableType())
        {
          var lambdaParameter = Expression.Parameter(typeof (YqlQuote));
          var sourcePropExp =
            Expression.Lambda<Func<YqlQuote, object>>(
              Expression.Convert(Expression.Property(lambdaParameter, propertyInfo), typeof (object)),
              new[] {lambdaParameter});
          mapping = mapping.ForMember(sourcePropExp,
                                      opt =>
                                      opt.MapFrom(src => src.With(x => x.Element(propertyInfo.Name)).With(y => y.Value)));
        }

      }

    }

    public static void CreateXChangeRateMap()
    {
      Mapper.Reset();
      Mapper.CreateMap<string, float?>().ConvertUsing<StringToNullableFloat>();
      Mapper.CreateMap<string, DateTime?>().ConvertUsing<StringToNullableDateTime>();
      Mapper.CreateMap<XElement, YqlPair>()
            .ForMember(src => src.Id, opt => opt.ResolveUsing(x => Guid.NewGuid()))
            .ForMember(src => src.Rateses, opt => opt.ResolveUsing(x => new List<YqlXchangeRate>()))
            .ForMember(src => src.Name, opt => opt.MapFrom(dst => dst.With(x => x.Element("Name")).With(y => y.Value)))
            .ForMember(src => src.PairId, opt => opt.MapFrom(dst => dst.With(x => x.Attribute("id")).With(y => y.Value)))
            .ConstructUsing((Func<ResolutionContext, YqlPair>) (obj => new YqlPair()));
      Mapper.CreateMap<XElement, YqlXchangeRate>()
            .ForMember(src => src.Id, opt => opt.ResolveUsing(x => Guid.NewGuid()))
            .ForMember(src => src.Ask, opt => opt.MapFrom(dst => dst.With(x => x.Element("Ask").With(y => y.Value))))
            .ForMember(src => src.Bid, opt => opt.MapFrom(dst => dst.With(x => x.Element("Bid").With(y => y.Value))))
            .ForMember(src => src.TradeDate,
                       opt => opt.MapFrom(dst => dst.With(x => x.Element("Date").With(y => y.Value))))
            .ForMember(src => src.Rate, opt => opt.MapFrom(dst => dst.With(x => x.Element("Rate").With(y => y.Value))))
            .ForMember(src => src.Pair, opt => opt.ResolveUsing(Mapper.Map<YqlPair>))
            .ConstructUsing((Func<ResolutionContext, YqlXchangeRate>) (obj => new YqlXchangeRate()));
    }
  }
}
