using ECommerceAPI.Application.DTOs;
using ECommerceAPI.Domain.Entities.Concrete;
using ECommerceAPI.Domain.Entities.Identity;
using Mapster;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ECommerceAPI.Application.Mappings
{
    public static class MapsterConfig
    {
        public static void RegisterMappings()
        {
            TypeAdapterConfig<HelpCenter, HelpCenterDto>.NewConfig()
                .Map(dest => dest.PopularQuestion, src => src.PopularQuestion)
                .Map(dest => dest.PopularAnswer, src => src.PopularAnswer);

            TypeAdapterConfig<AboutUs, AboutUsDto>.NewConfig()
                .Map(dest => dest.BusinessInfo, src => src.BusinessInfo)
                .Map(dest => dest.WhatWeDo, src => src.WhatWeDo);

            TypeAdapterConfig<Contact, ContactDto>.NewConfig()
                .Map(dest => dest.Address, src => src.Address)
                .Map(dest => dest.Title, src => src.Title)
                .Map(dest => dest.Email, src => src.Email)
                .Map(dest => dest.Phone, src => src.Phone);

            TypeAdapterConfig<AppUser, AdminDto>.NewConfig()
                .Map(dest => dest.Id, src => src.Id)
                .Map(dest => dest.FirstName, src => src.FirstName)
                .Map(dest => dest.SecondName, src => src.SecondName)
                .Map(dest => dest.LastName, src => src.LastName)
                .Map(dest => dest.SecondLastName, src => src.SecondLastName)
                .Map(dest => dest.Email, src => src.Email)
                .Map(dest => dest.PhoneNumber, src => src.PhoneNumber)
                .Map(dest => dest.Photo, src => src.Photo);

            // RegisterDto config

            TypeAdapterConfig<RegisterDto, AppUser>.NewConfig()
            .Map(dest => dest.FirstName, src => src.FirstName)
            .Map(dest => dest.SecondName, src => src.SecondName)
            .Map(dest => dest.LastName, src => src.LastName)
            .Map(dest => dest.SecondLastName, src => src.SecondLastName)
            .Map(dest => dest.Email, src => src.Email)
            .Map(dest => dest.UserName, src => src.Email)
            .Map(dest => dest.Photo, src => src.Photo)
            .Map(dest => dest.CreatedDate, src => src.CreatedDate)
            .Map(dest => dest.DeletedDate, src => src.DeletedDate)
            .Map(dest => dest.ModifiedDate, src => src.ModifiedDate)
            .Map(dest => dest.IsDeleted, src => src.IsDeleted);

            // RegisterSellerDto config

            TypeAdapterConfig<RegisterSellerDto, AppUser>.NewConfig()
                .Map(dest => dest.FirstName, src => src.FirstName)
                .Map(dest => dest.SecondName, src => src.SecondName)
                .Map(dest => dest.LastName, src => src.LastName)
                .Map(dest => dest.SecondLastName, src => src.SecondLastName)
                .Map(dest => dest.Email, src => src.Email)
                .Map(dest => dest.UserName, src => src.Email)
                .Map(dest => dest.Photo, src => src.Photo)
                .Map(dest => dest.CompanyName, src => src.CompanyName)
                .Map(dest => dest.ContactInformation, src => src.ContactInformation)
                .Map(dest => dest.IsApproved, src => src.IsApproved ?? false)
                .Map(dest => dest.CreatedDate, src => src.CreatedDate)
                .Map(dest => dest.DeletedDate, src => src.DeletedDate)
                .Map(dest => dest.ModifiedDate, src => src.ModifiedDate)
                .Map(dest => dest.IsDeleted, src => src.IsDeleted);

            // PendingSellerDto config

            TypeAdapterConfig<PendingSellerDto, AppUser>.NewConfig()
                .Map(dest => dest.Id, src => src.Id)
                .Map(dest => dest.FirstName, src => src.FirstName)
                .Map(dest => dest.LastName, src => src.LastName)
                .Map(dest => dest.CompanyName, src => src.CompanyName)
                .Map(dest => dest.ContactInformation, src => src.ContactInformation)
                .Map(dest => dest.IsApproved, src => src.IsApproved ?? false)
                .Map(dest => dest.CreatedDate, src => src.CreatedDate)
                .Map(dest => dest.DeletedDate, src => src.DeletedDate)
                .Map(dest => dest.ModifiedDate, src => src.ModifiedDate)
                .Map(dest => dest.IsDeleted, src => src.IsDeleted);

            TypeAdapterConfig<ProductDto, Product>.NewConfig()
                .Map(dest => dest.Name, src => src.Name)
                .Map(dest => dest.Description, src => src.Description)
                .Map(dest => dest.Price, src => src.Price)
                .Map(dest => dest.StockQuantity, src => src.StockQuantity)
                .Map(dest => dest.Photo, src => src.Photo)
                .Map(dest => dest.CategoryId, src => src.CategoryId)
                .Map(dest => dest.SellerId, src => src.SellerId)
                .Map(dest => dest.Category, src => src.CategoryName)  // İlişkisel veriyi de haritalıyoruz
                .Map(dest => dest.Seller, src => src.SellerName)          // Seller ile ilişkili veriyi haritalıyoruz
                .Map(dest => dest.UserComments, src => src.UserComments.Adapt<List<UserCommentDto>>());  // Yorumlar listesi

            TypeAdapterConfig<UserCommentDto, UserComment>.NewConfig()
                .Map(dest => dest.Id, src => src.Id)
                .Map(dest => dest.AppUserId, src => src.AppUserId)
                .Map(dest => dest.ProductId, src => src.ProductId)
                .Map(dest => dest.Content, src => src.Content)
                .Map(dest => dest.Rate, src => src.Rate);
            //TypeAdapterConfig<CartItem, CartItemDto>
            //.NewConfig()
            //.Map(dest => dest.Cart, src => src.Cart); // Cart'ı doğrudan haritalayın
          

            //TypeAdapterConfig<Cart, CartDto>
            //    .NewConfig()
            //    .Map(dest => dest.Order, src => src.Order); // CartItems'ı haritalayın

            //TypeAdapterConfig<Order, OrderDto>
            //    .NewConfig();
           
        }
    }
}
