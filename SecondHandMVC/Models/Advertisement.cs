using System;
using System.ComponentModel;

namespace SecondHandMVC.Models
{
	public class Advertisement
	{
        
        [Key]
        public int Id { get; set; }

        [StringLength(maximumLength: 100, MinimumLength = 2)]
        public string Title { get; set; }

        [StringLength(maximumLength: 100, MinimumLength = 2)]
        public string Description { get; set; }

        [StringLength(maximumLength: 100, MinimumLength = 2)]
        public string Tag { get; set; }

        [StringLength(maximumLength: 100, MinimumLength = 2)]
        public int Price { get; set; }

        [DisplayName("Image")]
        public string Image{ get; set; }


        public User Seller { get; set; }
        public User Buyer { get; set; }

        public int SellerUserID { get; set; }
        public int BuyerUserID { get; set; }


    }
}

