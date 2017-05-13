using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using AutoMapper;
using EasyShopping.Repository.Models.Entity;

namespace EasyShopping.BusinessLogic.Models
{
    public static class MappingExpressionExtensions
    {
        public static IMappingExpression<TSource, TDest> IgnoreAllUnmapped<TSource, TDest>(this IMappingExpression<TSource, TDest> expression)
        {
            expression.ForAllMembers(opt => opt.Ignore());
            return expression;
        }
    }
    public static class BusinessTranslators
    {
        private static IMapper Mapper;
        
        public static void Init()
        {
            var config = new MapperConfiguration(cfg =>
            {
                //cfg.CreateMap<CategoryDTO, Category>();
                //cfg.CreateMap<Category, CategoryDTO>();

                cfg.CreateMap<UserDTO, User>().IgnoreAllUnmapped()
                .ForMember(
                    entity => entity.ID,
                    opt => opt.MapFrom(dto => dto.ID)
                )
                .ForMember(
                    entity => entity.UserName,
                    opt => opt.MapFrom(dto => dto.UserName)    
                )
                .ForMember(
                    entity => entity.PassWord,
                    opt => opt.MapFrom(dto => dto.PassWord)
                )
                ;

                cfg.CreateMap<User, UserDTO>()
                    .ForMember(
                        dto => dto.Role,
                        opt => opt.MapFrom(entity => entity.Role.Name)
                    )
                    .ForMember(
                        dto => dto.Country,
                        opt => opt.MapFrom(entity => entity.Country.CommonName)
                    ).ForMember(
                        dto => dto.District,
                        opt => opt.MapFrom(entity => entity.District.Name)
                    ).ForMember(
                    dto => dto.City,
                    opt => opt.MapFrom(entity => entity.Province.Name)
                    ).ForMember(
                        dto => dto.Status,
                        opt => opt.MapFrom(entity => entity.UserStatu.Name)
                    );

                cfg.CreateMap<CommentDTO, Comment>();
                cfg.CreateMap<Comment, CommentDTO>();

                cfg.CreateMap<CountryDTO, Country>();
                cfg.CreateMap<Country, CountryDTO>();

                cfg.CreateMap<DistrictDTO, District>();
                cfg.CreateMap<District, DistrictDTO>();

                cfg.CreateMap<ImageDTO, Image>();
                cfg.CreateMap<Image, ImageDTO>();

                cfg.CreateMap<OrderDTO, Order>();
                cfg.CreateMap<Order, OrderDTO>();

                cfg.CreateMap<Order, Order>();
                cfg.CreateMap<Order, OrderDTO>();
                //cfg.CreateMap<..., ...>();
            });

            Mapper = config.CreateMapper();
        }

        
        public static TTo Translate<TFrom, TTo>(this TFrom dto)
        {
            System.Diagnostics.Debugger.Launch();
            return Mapper.Map<TTo>(dto);
        }


        public static IList<TTo> Translate<TFrom, TTo>(this IEnumerable<TFrom> dto)
        {
            return Mapper.Map<IList<TTo>>(dto);
        }

        //public static IMappingExpression<TSource, TDestination> IgnoreAllNonExisting<TSource, TDestination>(this IMappingExpression<TSource, TDestination> expression)
        //{
        //    var sourceType = typeof(TSource);
        //    Mapper.
        //    var destinationType = typeof(TDestination);
        //    var existingMaps = Mapper.GetAllTypeMaps().First(x => x.SourceType.Equals(sourceType) && x.DestinationType.Equals(destinationType));
        //    foreach (var property in existingMaps.GetUnmappedPropertyNames())
        //    {
        //        expression.ForMember(property, opt => opt.Ignore());
        //    }
        //    return expression;
        //}
        //public static void IgnoreIfSourceIsNull<T>(this IMemberConfigurationExpression<T> expression)
        //{
        //    expression.Condition(IgnoreIfSourceIsNull);
        //}

        //static bool IgnoreIfSourceIsNull(ResolutionContext context)
        //{
        //    if (!context.IsSourceValueNull)
        //    {
        //        return true;
        //    }
        //    var result = context.GetContextPropertyMap().ResolveValue(context.Parent);
        //    return result.Value != null;
        //}

        
    }
}