using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TABP.Application.Common;

namespace TABP.Application.Discounts.Common
{
    public static class DiscountErrors
    {
        public static readonly Error DiscountNotFound = new(
            Code: "Discount.NotFound",
            Description: "Discount with this ID does not exist."
        );
        public static readonly Error DiscountAlreadyExists = new(
            Code: "Discount.AlreadyExists",
            Description: "Discount with this name already exists."
        );
        public static readonly Error InvalidDiscountData = new(
            Code: "Discount.InvalidData",
            Description: "The provided Discount data is invalid."
        );
    }
}
