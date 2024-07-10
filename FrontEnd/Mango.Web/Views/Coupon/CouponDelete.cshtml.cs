using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Mango.Web.Views.Coupon
{
    public class CouponDelete : PageModel
    {
        private readonly ILogger<CouponDelete> _logger;

        public CouponDelete(ILogger<CouponDelete> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}