using AutoMapper;
using EasyShopping.BusinessLogic.Models;
using System.Collections.Generic;

namespace EasyShopping.Api.Models
{
    public static class ApiTranslators
    {
        private static IMapper Mapper;

        public static void Init()
        {
            //Config Mapping
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CategoryApiModel, CategoryDTO>();
                cfg.CreateMap<CategoryDTO, CategoryApiModel>();

                cfg.CreateMap<UserDTO, UserApiModel>()
                    .ForSourceMember(
                        dto => dto.RoleID,
                        opt => opt.Ignore()
                    )
                    .ForSourceMember(
                        dto => dto.CityID,
                        opt => opt.Ignore()
                    )
                    .ForSourceMember(
                        dto => dto.StatusID,
                        opt => opt.Ignore()
                    )
                    .ForSourceMember(
                        dto => dto.DistrictID,
                        opt => opt.Ignore()
                    )
                    .ForSourceMember(
                        dto => dto.CountryID,
                        opt => opt.Ignore()
                    ).ForSourceMember(
                        dto => dto.WardID,
                        opt => opt.Ignore()
                    );

                cfg.CreateMap<AddUserModel, UserDTO>();

                cfg.CreateMap<CommentDTO, CommentApiModel>();
                cfg.CreateMap<CommentApiModel, CommentDTO>();

                cfg.CreateMap<CountryDTO, CountryApiModel>();
                cfg.CreateMap<CountryApiModel, CountryDTO>();

                cfg.CreateMap<DistrictDTO, DistrictApiModel>();
                cfg.CreateMap<DistrictApiModel, DistrictDTO>();

                cfg.CreateMap<ImageApiModel, ImageDTO>();
                cfg.CreateMap<ImageDTO, ImageApiModel>();

                cfg.CreateMap<OrderApiModel, OrderDTO>();
                cfg.CreateMap<OrderDTO, OrderApiModel>();

                cfg.CreateMap<OrderDetailApiModel, OrderDetailDTO>();
                cfg.CreateMap<OrderDetailDTO, OrderDetailApiModel>();

                cfg.CreateMap<OrderStatusApiModel, OrderStatusDTO>();
                cfg.CreateMap<OrderStatusDTO, OrderStatusApiModel>();

                cfg.CreateMap<PartnerApiModel, PartnerDTO>();
                cfg.CreateMap<PartnerDTO, PartnerApiModel>();

                cfg.CreateMap<ProducerApiModel, ProducerDTO>();
                cfg.CreateMap<ProducerDTO, ProducerApiModel>();

                cfg.CreateMap<WardApiModel, WardDTO>();
                cfg.CreateMap<WardDTO, WardApiModel>();

                cfg.CreateMap<ProductApiModel, ProductDTO>();
                cfg.CreateMap<ProductDTO, ProductApiModel>();

                cfg.CreateMap<ProductStatusApiModel, ProductStatusDTO>();
                cfg.CreateMap<ProductStatusDTO, ProductStatusApiModel>();

                cfg.CreateMap<ProductTypeApiModel, ProductTypeDTO>();
                cfg.CreateMap<ProductTypeDTO, ProductTypeApiModel>();

                cfg.CreateMap<ProvinceApiModel, ProvinceDTO>();
                cfg.CreateMap<ProvinceDTO, ProvinceApiModel>();

                cfg.CreateMap<RatingApiModel, RatingDTO>();
                cfg.CreateMap<RatingDTO, RatingApiModel>();

                cfg.CreateMap<RoleApiModel, RoleDTO>();
                cfg.CreateMap<RoleDTO, RoleApiModel>();

                cfg.CreateMap<ShipperDetailDTO, ShipperDetailApiModel>();
                cfg.CreateMap<ShipperDetailApiModel, ShipperDetailDTO>();

                cfg.CreateMap<ShipperRatingDTO, ShipperRatingApiModel>();
                cfg.CreateMap<ShipperRatingApiModel, ShipperRatingDTO>();

                cfg.CreateMap<ShipStatusApiModel, ShipStatusDTO>();
                cfg.CreateMap<ShipStatusDTO, ShipStatusApiModel>();

                cfg.CreateMap<StoreApiModel, StoreDTO>();
                cfg.CreateMap<StoreDTO, StoreApiModel>().
                ForSourceMember(
                    dto => dto.UserID,
                    opt => opt.Ignore()
                ).
                ForSourceMember(
                    dto => dto.ModifiedByID,
                    opt => opt.Ignore()    
                );

                cfg.CreateMap<StoreRatingApiModel, StoreRatingDTO>();
                cfg.CreateMap<StoreRatingDTO, StoreRatingApiModel>();

                cfg.CreateMap<StoreStatusApiModel, StoreStatusDTO>();
                cfg.CreateMap<StoreStatusDTO, StoreStatusApiModel>();

                cfg.CreateMap<UserStatusApiModel, UserStatusDTO>();
                cfg.CreateMap<UserStatusDTO, UserStatusApiModel>();

                cfg.CreateMap<WishListApiModel, WishlistDTO>();
                cfg.CreateMap<WishlistDTO, WishListApiModel>();

                //cfg.CreateMap <..., ...> ();
            });

            Mapper = config.CreateMapper();
        }

        //Map from model Tfrom to model TTo
        public static TTo Translate<TFrom, TTo>(this TFrom dto)
        {
            return Mapper.Map<TTo>(dto);
        }

        //Map list
        public static IEnumerable<TTo> Translate<TFrom, TTo>(this IList<TFrom> dto)
        {
            return Mapper.Map<IEnumerable<TTo>>(dto);
        }
    }
}