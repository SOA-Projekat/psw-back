using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Dtos.ShoppingCartDtos;
using Explorer.Payments.API.Public.ShoppingCart;
using Explorer.Payments.Core.Domain.ShoppingCarts;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using Explorer.Stakeholders.Core.UseCases;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.Core.UseCases.ShoppingCarts
{
    public class ShoppingCartService : CrudService<ShoppingCartDto, ShoppingCart>, IShoppingCartService
    {
        private readonly ICrudRepository<TourPurchaseToken> _tourPurhcaseTokenRepository;
        private readonly ICrudRepository<ShoppingCart> _shoppingCartRepository;
        //private readonly QRCodeService _qrCodeService;
        

        public ShoppingCartService(ICrudRepository<ShoppingCart> repository, ICrudRepository<TourPurchaseToken> tokenRepo, IMapper mapper/*, QRCodeService qrCodeService*/) : base(repository, mapper)
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

            //var accountManagementService = new AccountManagementService(_userRepository);
            //var qrCodeService = new QRCodeService();
            var cart = _shoppingCartRepository.Get(cartId);
            //qrCodeService.SendReceiptViaEmail(cart);
            var orderItems = new List<OrderItem>(cart.OrderItems);

            foreach (OrderItem item in orderItems)
            {
                TourPurchaseToken token = item.ToPurchaseToken(cart.TouristId);
                _tourPurhcaseTokenRepository.Create(token);

                cart.RemoveOrderItem(item);
            }

            _shoppingCartRepository.Update(cart);

            return MapToDto(cart);
        }


        public Result<ShoppingCartDto> RemoveOrderItem(long cartId, int tourId)
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
