﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.Talabat.Core.Entities.Basket
{
    public class CustomerBasket
    {
        public string Id { get; set; }
        public List<BasketItem> Items { get; set; }
    }
}
