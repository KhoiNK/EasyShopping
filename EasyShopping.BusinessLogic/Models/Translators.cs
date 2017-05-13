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
                 .ForMember(
                    entity => entity.FirstName,
                    opt => opt.MapFrom(dto => dto.FirstName)
                )
                 .ForMember(
                    entity => entity.LastName,
                    opt => opt.MapFrom(dto => dto.LastName)
                )
                 .ForMember(
                    entity => entity.DOB,
                    opt => opt.MapFrom(dto => dto.DOB)
                )
                 .ForMember(
                    entity => entity.Email,
                    opt => opt.MapFrom(dto => dto.Email)
                ).ForMember(
                    entity => entity.RegDate,
                    opt => opt.MapFrom(dto => dto.RegDate)
                ).ForMember(
                    entity => entity.StatusID,
                    opt => opt.MapFrom(dto => dto.StatusID)
                ).ForMember(
                    entity => entity.Phone,
                    opt => opt.MapFrom(dto => dto.Phone)
                ).ForMember(
                    entity => entity.Sex,
                    opt => opt.MapFrom(dto => dto.Sex)
                ).ForMember(
                    entity => entity.CityID,
                    opt => opt.MapFrom(dto => dto.CityID)
                ).ForMember(
                    entity => entity.DistrictID,
                    opt => opt.MapFrom(dto => dto.DistrictID)
                ).ForMember(
                    entity => entity.Address,
                    opt => opt.MapFrom(dto => dto.Address)
                ).ForMember(
                    entity => entity.ImgLink,
                    opt => opt.MapFrom(dto => dto.ImgLink)
                ).ForMember(
                    entity => entity.RoleID,
                    opt => opt.MapFrom(dto => dto.RoleID)
                ).ForMember(
                    entity => entity.ModifiedDate,
                    opt => opt.MapFrom(dto => dto.ModifiedDate)
                ).ForMember(
                    entity => entity.CountryID,
                    opt => opt.MapFrom(dto => dto.CountryID)
                ).ForMember(
                    entity => entity.isSocialLogin,
                    opt => opt.MapFrom(dto => dto.isSocialLogin)
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

                cfg.CreateMap<CategoryDTO, Category>();
                cfg.CreateMap<Category, CategoryDTO>();

                cfg.CreateMap<CommentDTO, Comment>();
                cfg.CreateMap<Comment, CommentDTO>();

                cfg.CreateMap<CountryDTO, Country>();
                cfg.CreateMap<Country, CountryDTO>();

                cfg.CreateMap<DistrictDTO, District>();
                cfg.CreateMap<District, DistrictDTO>();

                cfg.CreateMap<ImageDTO, Image>();
                cfg.CreateMap<Image, ImageDTO>();

                cfg.CreateMap<OrderDetailDTO, OrderDetail>();
                cfg.CreateMap<OrderDetail, OrderStatusDTO>();

                cfg.CreateMap<OrderDTO, Order>();
                cfg.CreateMap<Order, OrderDTO>();
                
                cfg.CreateMap<OrderStatusDTO, OrderStatu>();
                cfg.CreateMap<OrderStatu, OrderStatusDTO>();

                cfg.CreateMap<PartnerDTO, Partner>();
                cfg.CreateMap<Partner, PartnerDTO>();

                cfg.CreateMap<ProducerDTO, Producer>();
                cfg.CreateMap<Producer, ProducerDTO>();

                cfg.CreateMap<ProductDTO, Product>();
                cfg.CreateMap<Product, ProductDTO>();

                cfg.CreateMap<ProductStatusDTO, ProductStatu>();
                cfg.CreateMap<ProductStatu, ProductStatusDTO>();

                cfg.CreateMap<ProductTypeDTO, ProductType>();
                cfg.CreateMap<ProductType, ProductTypeDTO>();

                cfg.CreateMap<ProvinceDTO, Province>();
                cfg.CreateMap<Province, ProvinceDTO>();

                cfg.CreateMap<RatingDTO, Rating>();
                cfg.CreateMap<Rating, RatingDTO>();

                cfg.CreateMap<RoleDTO, Role>();
                cfg.CreateMap<Role, RoleDTO>();

                cfg.CreateMap<ShipperDetailDTO, ShipperDetail>();
                cfg.CreateMap<ShipperDetail, ShipperDetailDTO>();

                cfg.CreateMap<ShipperRatingDTO, ShipperRating>();
                cfg.CreateMap<ShipperRating, ShipperRatingDTO>();

                cfg.CreateMap<ShippingDetailDTO, ShippingDetail>();
                cfg.CreateMap<ShippingDetail, ShippingDetailDTO>();

                cfg.CreateMap<ShipStatusDTO, ShippStatu>();
                cfg.CreateMap<ShippStatu, ShipStatusDTO>();

                cfg.CreateMap<StoreDTO, Store>();
                cfg.CreateMap<Store, StoreDTO>();

                cfg.CreateMap<StoreStatusDTO, StoreStatu>();
                cfg.CreateMap<StoreStatu, StoreStatusDTO>();

                cfg.CreateMap<StoreRatingDTO, StroreRating>();
                cfg.CreateMap<StroreRating, StoreRatingDTO>();

                cfg.CreateMap<UserDTO, User>();
                cfg.CreateMap<User, UserDTO>();

                cfg.CreateMap<UserStatusDTO, UserStatu>();
                cfg.CreateMap<UserStatu, UserStatusDTO>();

                cfg.CreateMap<WardDTO, Ward>();
                cfg.CreateMap<Ward, WardDTO>();

                cfg.CreateMap<WishlistDTO, Wishlist>();
                cfg.CreateMap<Wishlist, WishlistDTO>();

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