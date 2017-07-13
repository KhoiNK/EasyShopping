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
                ).ForMember(
                    dto => dto.Ward,
                    opt => opt.MapFrom(entity => entity.Ward.Name)
                );

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
                cfg.CreateMap<Partner, PartnerDTO>()
                .ForMember(dto => dto.UserName,
                opt => opt.MapFrom(entity => entity.User.UserName));

                cfg.CreateMap<ProductViewDTO, Product>();


                cfg.CreateMap<Product, ProductViewDTO>()
                .ForMember(
                    dto => dto.Status,
                    opt => opt.MapFrom(entity => entity.ProductStatu.Description)
                )
                .ForMember(
                    dto => dto.ProductType,
                    opt => opt.MapFrom(entity => entity.ProductType.Name)
                )
                .ForMember(
                    dto => dto.Store,
                    opt => opt.MapFrom(entity => entity.Store.Name)    
                )
                .ForMember(
                    dto => dto.ManufacturedCountry,
                    opt => opt.MapFrom(entity => entity.Country.CommonName)
                );
                cfg.CreateMap<Product, ProductDTO>();
                cfg.CreateMap<ProductDTO, Product>();

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

                cfg.CreateMap<ShipStatusDTO, ShipperStatu>();
                cfg.CreateMap<ShipperStatu, ShipStatusDTO>();

                cfg.CreateMap<StoreDTO, Store>();
                cfg.CreateMap<Store, StoreDTO>();

                cfg.CreateMap<StoreDTO, Store>()
                .ForSourceMember(
                    dto => dto.UserName,
                    opt => opt.Ignore()
                ).ForSourceMember(
                    dto => dto.Status,
                    opt => opt.Ignore()
                ).ForSourceMember(
                    dto => dto.City,
                    opt => opt.Ignore()
                ).ForSourceMember(
                    dto => dto.Country,
                    opt => opt.Ignore()
                ).ForSourceMember(
                    dto => dto.Ward,
                    opt => opt.Ignore()
                ).ForSourceMember(
                    dto => dto.District,
                    opt => opt.Ignore()
                );

                cfg.CreateMap<Store, StoreDTO>()
                .ForMember(
                    dto => dto.UserName,
                    opt => opt.MapFrom(entity => entity.User.UserName)
                ).ForMember(
                    dto => dto.Status,
                    opt => opt.MapFrom(entity => entity.StoreStatu.Name)
                ).ForMember(
                    dto => dto.City,
                    opt => opt.MapFrom(entity => entity.Province.Name)
                ).ForMember(
                    dto => dto.Country,
                    opt => opt.MapFrom(entity => entity.Country.CommonName)
                ).ForMember(
                    dto => dto.District,
                    opt => opt.MapFrom(entity => entity.District.Name)
                ).ForMember(
                    dto => dto.Ward,
                    opt => opt.MapFrom(entity => entity.Ward.Name)
                ).ForMember(
                    dto => dto.Products,
                    opt => opt.Ignore()
                );

                cfg.CreateMap<StoreStatusDTO, StoreStatu>();
                cfg.CreateMap<StoreStatu, StoreStatusDTO>();

                cfg.CreateMap<StoreRatingDTO, StoreRating>();
                cfg.CreateMap<StoreRating, StoreRatingDTO>();

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
            //System.Diagnostics.Debugger.Launch();
            return Mapper.Map<TTo>(dto);
        }


        public static IEnumerable<TTo> Translate<TFrom, TTo>(this IEnumerable<TFrom> dto)
        {
            return Mapper.Map<IEnumerable<TTo>>(dto);
        }

        public static User ToUserEntity(this UserDTO user)
        {
            if (user == null) { return null; }
            return new User
            {
                Address = user.Address,
                CityID = user.CityID,
                CountryID = user.CountryID,
                DistrictID = user.DistrictID,
                DOB = user.DOB,
                Email = user.Email,
                FirstName = user.FirstName,
                ImgLink = user.ImgLink,
                LastName = user.LastName,
                ModifiedDate = user.ModifiedDate,
                PassWord = user.PassWord,
                Phone = user.Phone,
                RegDate = user.RegDate,
                RoleID = user.RoleID,
                Sex = user.Sex,
                StatusID = user.StatusID,
                UserName = user.UserName,
                isSocialLogin = user.isSocialLogin,
                WardID = user.WardID
            };
        }

        public static Store ToStoreEntity(StoreDTO dto)
        {
            var store = new Store()
            {
                Address = dto.Address,
                BankAccount = dto.BankAccount,
                CityId = dto.CityId,
                CountryId = dto.CountryId,
                CreatedDate = dto.CreatedDate,
                ModifiedDate = dto.ModifiedDate,
                Description = dto.Description,
                DistrictId = dto.DistrictId,
                ImgLink = dto.ImgLink,
                LatX = dto.LatX,
                LatY = dto.LatY,
                ModifiedByID = dto.ModifiedByID,
                Name = dto.Name,
                StatusID = dto.StatusID,
                TaxCode = dto.TaxCode,
                UserID = dto.UserID,
                WardId = dto.WardId
            };
            return store;
        }
    }
}