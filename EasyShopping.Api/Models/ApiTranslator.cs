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

                cfg.CreateMap<UserDTO, UserApiModel>();
                cfg.CreateMap<UserApiModel, UserDTO>();

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
                //cfg.CreateMap<..., ...>();
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

        /*
        public static Category ToEntity(this CategoryDTO dto)
        {
            return Mapper.Map<Category>(dto);
        }

        public static IList<Category> ToEntities(this IEnumerable<CategoryDTO> dto)
        {
            return Mapper.Map<IList<Category>>(dto);
        }

        public static CategoryDTO ToDTO(this Category entity)
        {
            return Mapper.Map<CategoryDTO>(entity);
        }

        public static IList<CategoryDTO> ToDTOs(this IEnumerable<Category> entities)
        {
            return Mapper.Map<IList<CategoryDTO>>(entities);
        }
        //*/
    }
}