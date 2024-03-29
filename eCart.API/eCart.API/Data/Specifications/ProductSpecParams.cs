﻿using System;
namespace eCart.API.Data.Specifications
{
	public class ProductSpecParamsWithoutPagination
    {
		public int? brandId { get; set; }

		public int? typeId { get; set; }

		public string sort { get; set; }

		private string _search;

		public string Search
		{
			get => _search;
			set => _search = value.ToLower();
		}
	}
}

