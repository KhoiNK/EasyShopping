﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using AutoMapper;
using Easyshopping.DataAccess.Models.Entity;

namespace EasyShopping.BusinessLogic.Models
{
    public static class BusinessTranslators
    {
        private static IMapper Mapper;

        public static void Init()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CategoryDTO, Category>();
                cfg.CreateMap<Category, CategoryDTO>();

                cfg.CreateMap<UserDTO, User>();
                cfg.CreateMap<User, UserDTO>();

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

                //cfg.CreateMap<Order, Order>();
                //cfg.CreateMap<Order, OrderDTO>();
                //cfg.CreateMap<..., ...>();
            });

            Mapper = config.CreateMapper();
        }

        public static TTo Translate<TFrom, TTo>(this TFrom dto)
        {
            return Mapper.Map<TTo>(dto);
        }


        public static IList<TTo> Translate<TFrom, TTo>(this IEnumerable<TFrom> dto)
        {
            return Mapper.Map<IList<TTo>>(dto);
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