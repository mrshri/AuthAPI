namespace CouponWeb.Models.DTO
{
    public class CouponDTO
    {
        public int CouponId { get; set; }
        public string CouponCode { get; set; }
        public double MinAmount { get; set; }
        public double DiscountAmount { get; set; }
    }
}
