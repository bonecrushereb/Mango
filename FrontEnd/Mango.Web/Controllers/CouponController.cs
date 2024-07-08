using Mango.Web.Models;
using Mango.Web.Service.IService;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Mango.Web.Controllers
{
    public class CouponController : Controller
    {
        private readonly ICouponService _couponService;
        public CouponController(ICouponService couponService)
        {
            _couponService = couponService;
        }

        //Show all Coupons
          public async Task<IActionResult> CouponIndex()
        {
            List<CouponDto> list = new();

            ResponseDto response = await _couponService.GetAllCouponsAsync();

            if (response != null && response.IsSuccess)
            {
                list= JsonConvert.DeserializeObject<List<CouponDto>>(Convert.ToString(response.Result));
            }
            else 
            {
                TempData["error"]=response.Message;
            }

            return View(list);
        }

        //Create Coupon
        public async Task<IActionResult> CouponCreate()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CouponCreate(CouponDto couponDto)
        {
            if(ModelState.IsValid)
            {
                 ResponseDto response = await _couponService.CreateCouponAsync(couponDto);

                if (response != null && response.IsSuccess)
                {
                    TempData["success"]="Coupon Created Successfully";
                    return RedirectToAction(nameof(CouponIndex));
                }
                else 
                {
                    TempData["error"]=response.Message;
                }

            }
            return View();
        }

        //Delete Coupon
        public async Task<IActionResult> CouponDelete(int couponId)
        {
                 ResponseDto response = await _couponService.GetCouponByIdAsync( couponId);

                if (response != null && response.IsSuccess)
                {
                    CouponDto couponDto = JsonConvert.DeserializeObject<CouponDto>(Convert.ToString(response.Result));
                    return View(couponDto);
                }
                else 
                {
                    TempData["error"]=response.Message;
                }
                return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> CouponDelete(CouponDto couponDto)
        {
                 ResponseDto response = await _couponService.DeleteCouponAsync(couponDto.CouponId);

                if (response != null && response.IsSuccess)
                {
                    TempData["success"]="Coupon Deleted Successfully";
                    return RedirectToAction(nameof(CouponIndex));
                }
                else 
                {
                    TempData["error"]=response.Message;
                }
                return View(couponDto);
        }
    }
}