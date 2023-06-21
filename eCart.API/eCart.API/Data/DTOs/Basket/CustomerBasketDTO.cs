using System;
using System.ComponentModel.DataAnnotations;
using eCart.API.Data.Models;

namespace eCart.API.Data.DTOs.Basket
{
	public class CustomerBasketDTO
	{
        [Required]
        public string Id { get; set; }
        
        public List<BasketItemDTO> Items { get; set; }
    }
}

