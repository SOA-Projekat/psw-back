﻿using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Marketplace;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using Explorer.Tours.Core.Domain.ShoppingCarts;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.UseCases.Marketplace
{
    public class ShoppingCartService : CrudService<ShoppingCartDto, ShoppingCart>, IShoppingCartService
    {
        private readonly ICrudRepository<TourPurchaseToken> _tourPurhcaseTokenRepository;
        private readonly ICrudRepository<ShoppingCart> _shoppingCartRepository;

       
        public ShoppingCartService(ICrudRepository<ShoppingCart> repository,ICrudRepository<TourPurchaseToken> tokenRepo , IMapper mapper) : base(repository,mapper)
        {
            _shoppingCartRepository = repository;
            _tourPurhcaseTokenRepository = tokenRepo;   
        }


        public Result<ShoppingCartDto> GetByUserId(int touristId)
        {
            var cart = _shoppingCartRepository.GetPaged(1, int.MaxValue).Results.FirstOrDefault(s => s.TouristId == touristId);
            
            if (cart == null)
            {
                cart = _shoppingCartRepository.Create(new ShoppingCart(touristId));
            }
                
            return MapToDto(cart);
        }

        public Result<ShoppingCartDto> Purchase(int cartId)
        {
            var cart = _shoppingCartRepository.Get(cartId);
            var orderItems = new List<OrderItem>(cart.OrderItems);

            foreach(OrderItem item in orderItems)
            {
                TourPurchaseToken token = item.ToPurchaseToken(cart.TouristId);
                _tourPurhcaseTokenRepository.Create(token);
                
               cart.RemoveOrderItem(item);
            }
            
            _shoppingCartRepository.Update(cart);

            return MapToDto(cart);
        }

      
        public Result<ShoppingCartDto> RemoveOrderItem(long cartId,int tourId)
        {
            var cart = _shoppingCartRepository.Get(cartId);
            var item = cart.OrderItems.FirstOrDefault(x => x.IdTour == tourId);

            cart.RemoveOrderItem(item);

            _shoppingCartRepository.Update(cart);

            return MapToDto(cart);
        }

        public Result<ShoppingCartDto> Buy(ShoppingCartDto cartDto)
        {
            var cart = MapToDomain(cartDto);
            _shoppingCartRepository.Update(cart);

            return MapToDto(cart);
        }

    }
}
