﻿using BlazorEcommerce.Server.Data;
using BlazorEcommerce.Server.Migrations;
using BlazorEcommerce.Server.Services.Interfaces;
using BlazorEcommerce.Shared.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Security.Claims;

namespace BlazorEcommerce.Server.Services
{
    public class CartService : ICartService
    {
        private readonly DataContext _context;
        private readonly IAuthService _authService;

        public CartService(DataContext context, IAuthService authService)
        {
            _context = context;
            _authService = authService;
        }


        public async Task<ServiceResponse<List<CartProductResponse>>> GetCartProducts(List<CartItem> cartItems)
        {
            var result = new ServiceResponse<List<CartProductResponse>>
            {
                Data = new List<CartProductResponse>()
            };

            foreach(var item in cartItems)
            {
                var product = await _context.Products.Where(p => p.Id == item.ProductId)
                                                     .FirstOrDefaultAsync();

                if(product == null)
                {
                    continue;
                }

                var productVariant = await _context.ProductVariants
                    .Where(v => v.ProductId == item.ProductId && v.ProductTypeId == item.ProductTypeId)
                    .Include(v => v.ProductType)
                    .FirstOrDefaultAsync();

                if(productVariant == null)
                {
                    continue;
                }

                var cartProduct = new CartProductResponse
                {
                    ProductId = product.Id,
                    Title = product.Title,
                    ImageUrl = product.ImageUrl,
                    Price = productVariant.Price,
                    ProductType = productVariant.ProductType.Name,
                    ProductTypeId = productVariant.ProductTypeId,
                    Quantity = item.Quantity
                };

                result.Data.Add(cartProduct);
            }

            return result;
        }

        public async Task<ServiceResponse<List<CartProductResponse>>> StoreCartItems(List<CartItem> cartItems)
        {
            cartItems.ForEach(cartItem => cartItem.UserId = _authService.GetUserId());
            _context.CartItems.AddRange(cartItems);

            await _context.SaveChangesAsync();

            return await GetDbCartProducts();
        }

        public async Task<ServiceResponse<int>> GetCartItemsCount()
        {
            var list = await _context.CartItems.Where(ci => ci.UserId == _authService.GetUserId()).ToListAsync();

            return new ServiceResponse<int> { Data = list.Count };
        }

        public async Task<ServiceResponse<List<CartProductResponse>>> GetDbCartProducts(int? userId = null)
        {
            if(userId == null)
            {
                userId = _authService.GetUserId();
            }

            var cartItemsList = await _context.CartItems.Where(ci => ci.UserId == userId).ToListAsync();

            return await GetCartProducts(cartItemsList);
        }

        public async Task<ServiceResponse<bool>> AddToCart(CartItem cartItem)
        {
            cartItem.UserId = _authService.GetUserId();

            var sameItem = await _context.CartItems
                .FirstOrDefaultAsync(ci => ci.ProductId == cartItem.ProductId
                    && ci.ProductTypeId == cartItem.ProductTypeId 
                    && ci.UserId == cartItem.UserId);

            if(sameItem == null)
            {
                _context.CartItems.Add(cartItem);
            }
            else
            {
                sameItem.Quantity += cartItem.Quantity;
            }

            await _context.SaveChangesAsync();

            return new ServiceResponse<bool> { Data = true };
        }

        public async Task<ServiceResponse<bool>> UpdateQuantity(CartItem cartItem)
        {
            var dbCartItem = await _context.CartItems
                .FirstOrDefaultAsync(ci => ci.ProductId == cartItem.ProductId
                    && ci.ProductTypeId == cartItem.ProductTypeId
                    && ci.UserId == _authService.GetUserId());

            if(dbCartItem == null)
            {
                return new ServiceResponse<bool>
                {
                    Data = false,
                    Success = false,
                    Message = "Cart item does not exist"
                };
            }

            dbCartItem.Quantity = cartItem.Quantity;

            await _context.SaveChangesAsync();

            return new ServiceResponse<bool> { Data = true };
        }

        public async Task<ServiceResponse<bool>> RemoveItemFromCart(int productId, int productTypeId)
        {
            var dbCartItem = await _context.CartItems
                .FirstOrDefaultAsync(ci => ci.ProductId == productId
                    && ci.ProductTypeId == productTypeId
                    && ci.UserId == _authService.GetUserId());

            if (dbCartItem == null)
            {
                return new ServiceResponse<bool>
                {
                    Data = false,
                    Success = false,
                    Message = "Cart item does not exist"
                };
            }

            _context.CartItems.Remove(dbCartItem);
            await _context.SaveChangesAsync();

            return new ServiceResponse<bool> { Data = true };
        }
    }
}
