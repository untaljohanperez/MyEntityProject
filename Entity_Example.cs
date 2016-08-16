namespace Adiis.Model
{
    using System;
    using System.Collections.Generic;
    using Ceiba.Common.Domain;
    using System.Linq;
    using System.Diagnostics.CodeAnalysis;
    using Enum;
    public class Order : Entity<long>
    {
        public Order()
        {
            Details = new List<Detail>();
            Payments = new List<Payment>();
        }

        public string QuickBooksId { get; set; }

        public string EditSequence { get; set; }

        public long CustomerId { get; set; }

        public virtual Customer Customer { get; set; }

        public string SalesRepId { get; set; }

        public SalesRep SalesRep { get; set; }

        public DateTime OrderDate { get; set; }

        public long? StoreId { get; set; }

        public virtual Store Store { get; set; }

        public long StatusId { get; set; }

        public virtual Status Status { get; set; }

        public DateTime DeliveryDate { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? FoamDeliveryDate { get; set; }

        public decimal Seats { get; set; }

        public decimal Discount { get; set; }

        public virtual ICollection<Detail> Details { get; set; }

        private string pONumber;
        public string PONumber
        {
            get
            {
                if (pONumber == null)
                {
                    return Id.ToString();
                }
                return pONumber;
            }
            set
            {
                if (value != null && value != "0")
                {
                    pONumber = value;
                }
            }
        }

        private string sONumber;
        public string SONumber
        {
            get
            {
                if (sONumber == null)
                {
                    return Id.ToString();
                }
                return sONumber;
            }
            set
            {
                if (value != null && value != "0")
                {
                    sONumber = value;
                }
            }
        }

        public long? ProductionLineId { get; set; }

        public virtual ProductionLine ProductionLine { get; set; }

        public bool StockAssigned
        {
            get
            {
                if (Details == null || Details.Count() == 0)
                {
                    return false;
                }
                else
                {
                    return Details.Aggregate(true, (ele1, ele2) => ele1 && ele2.StockAssigned);
                }
            }
        }

        public override long Id { get; set; }

        public string Comments { get; set; }

        public string WarrantyNumber { get; set; }

        public bool? Gift { get; set; }

        public OrderType? OrderType { get; set; }

        public virtual ICollection<Payment> Payments { get; set; }

        public decimal? MinPaymentPercentage { get; set; }

        public string QuickBooksComments { get; set; }

        public bool? SalesTax { get; set; }

        public decimal? SalesTaxApplied { get; set; }

        public virtual ICollection<OrderAttachment> Attachments { get; set; }
        
        public string IPAddress { get; set; }

        [ExcludeFromCodeCoverage]
        public override void GenerateIdentity()
        {
            throw new NotImplementedException();
        }
    }
}
