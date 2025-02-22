using AutoMapper;
using CouponWeb.DATA;
using CouponWeb.Models;
using CouponWeb.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

namespace CouponWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CouponController : Controller
    {
        private readonly CouponContext _context;
        private IMapper _mapper;
        public CouponController(CouponContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAllCoupon()
        {
            IEnumerable<Coupon> obj = _context.Coupons.ToList();
            if (obj == null)
            {
                return NotFound();
            }
            IEnumerable<CouponDTO> couponDTO = _mapper.Map<IEnumerable<CouponDTO>>(obj);
            return Ok(couponDTO);
        }

        [HttpGet("GetById/{id}")]
        public IActionResult GetCouponById(int id)
        {
            var couponbyId = _context.Coupons.FirstOrDefault(u => u.CouponId == id);
            if (couponbyId == null)
            {
                return NotFound();
            }
            CouponDTO couponDTO = _mapper.Map<CouponDTO>(couponbyId);
            return Ok(couponDTO);
        }

        [HttpGet("GetByCode/{code}")]
        public IActionResult GetCouponByCode(string code)
        {
            var couponbyCode = _context.Coupons.FirstOrDefault(u => u.CouponCode.ToLower() == code.ToLower());
            if (couponbyCode == null)
            {
                return NotFound();
            }
            CouponDTO couponDTO = _mapper.Map<CouponDTO>(couponbyCode);
            return Ok(couponDTO);
        }

        [HttpPost]
        public IActionResult CreateCoupon([FromBody] CouponDTO coupondto )
        {
            Coupon newCoupon = _mapper.Map<Coupon>(coupondto);
             _context.Coupons.Add(newCoupon);
            if (newCoupon == null)
            {
                return NotFound();
            }
            _context.SaveChanges();
            CouponDTO couponDTO = _mapper.Map<CouponDTO>(newCoupon);

            return Ok(couponDTO);
        }


        [HttpPut]
        public IActionResult updateCoupon([FromBody] CouponDTO coupondto)
        {
            Coupon newCoupon = _mapper.Map<Coupon>(coupondto);
            _context.Coupons.Update(newCoupon);
            if (newCoupon == null)
            {
                return NotFound();
            }
            _context.SaveChanges();
            CouponDTO couponDTO = _mapper.Map<CouponDTO>(newCoupon);

            return Ok(couponDTO);
        }

        [HttpDelete]
        public IActionResult DeleteCoupon(int id)
        {

            var obj = _context.Coupons.First(u => u.CouponId == id);
            if (obj == null)
            {
                return NotFound();
            }
            _context.Coupons.Remove(obj);
            _context.SaveChanges();
            return Ok(new { message = "Coupon successfully removed." });
        }
    }
}
