using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SOA.Models
{
    public class Order
    {
        public int Id { get; set; }

        //Request for Quote
        [Display(Name = "Quantity")]
        public string Qty { get; set; }

        [Display(Name = "Status")]
        public Status Status { get; set; } = 0;

        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:C}")]
        [Display(Name = "Target Price")]
        public double? TargetPrice { get; set; }

        [Display(Name = "Manufacturer's Part Number")]
        public string MfgPn { get; set; }

        [Display(Name = "Manufacturer")]
        public string Mfg { get; set; }

        [Display(Name = "Internal Part Number")]
        public string InternalPn { get; set; }

        public string Description { get; set; }

        [Display(Name = "Date Requested")]
        public DateTime RFQDate { get; set; } = DateTime.Now;

        //Quote
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:C}")]
        [Display(Name = "Price")]
        public double? Price { get; set; }

        [Display(Name = "Date Quoted")]
        public DateTime QuoteDate { get; set; } = DateTime.Now;

      
        //Place Order
        [Display(Name = "Terms & Conditions of Purchase")]
        public string PurchaseTerms { get; set; }
       
        [Display(Name = "Date Needed")]
        public DateTime? DateNeeded { get; set; }

        [Display(Name = "Shipping Courier")]
        public string ShipCourier { get; set; }

        [Display(Name = "Shipping Method")]
        public string ShipMethod { get; set; }

        [Display(Name = "Date Order was Placed")]
        public DateTime PlaceOrderDate { get; set; } = DateTime.Now;


        //Order
        [Display(Name = "Terms & Conditions of Sale")]
        public string SaleTerms { get; set; }

        [Display(Name = "Scheduled Ship Date")]
        public DateTime? ShipDate { get; set; }

        [Display(Name = "Tracking Number")]
        public string TrackNumber { get; set; }

        [Display(Name = "Date Order Confirmed")]
        public DateTime DateConfirmed { get; set; } = DateTime.Now;

    }

    public enum Status
    {
        [Display(Name = "Active")]
        ActiveRFQ,
        [Display(Name = "InActive")]
        InactiveRFQ,
        [Display(Name = "Rejected")]
        RejectedRFQ,
        [Display(Name = "Active Quote")]
        ActiveQuote,
        [Display(Name = "Inactive Quote")]
        InactiveQuote,
        [Display(Name = "Rejected Quote")]
        RejectedQuote,
        [Display(Name = "Pending")]
        PendingOrder,
        [Display(Name = "Confirmed")]
        ConfirmedOrder,
        [Display(Name = "Shipped")]
        ShippedOrder
    }
}
