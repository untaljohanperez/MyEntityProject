namespace Adiis.Data.Configurations
{
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.ModelConfiguration;
    using Adiis.Model;
    using System.Data.Entity.Infrastructure.Annotations;
    public class OrderEntityConfiguration: EntityTypeConfiguration<Order>
    {
        public OrderEntityConfiguration()
        {
            ToTable( "Orders" );

            HasKey( k => k.Id ).Property( order => order.Id ).HasDatabaseGeneratedOption( DatabaseGeneratedOption.Identity );
            
            Ignore( p => p.StockAssigned );
            Ignore( p => p.SalesRep );

            Property( p => p.QuickBooksId ).IsOptional().HasMaxLength( 50 );
            Property( p => p.EditSequence ).IsOptional().HasMaxLength( 50 );
            Property( p => p.CustomerId ).IsRequired();
            Property( p => p.PONumber).IsOptional().HasMaxLength( 50 );
            Property( p => p.SONumber).IsOptional().HasMaxLength( 50 );
            Property( p => p.OrderDate ).IsRequired();
            Property( p => p.SalesRepId ).IsRequired();
            Property( p => p.DeliveryDate ).IsRequired().HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IX_DeliveryDate"))); 
            Property( p => p.Discount).IsOptional();
            Property( p => p.MinPaymentPercentage).IsOptional();
            Property( p => p.QuickBooksComments).IsOptional();
            Property( p => p.WarrantyNumber).IsOptional().HasMaxLength(50);
            Property( p => p.Gift).IsOptional();
            Property( p => p.OrderType).IsOptional();
            Property( p => p.FoamDeliveryDate).IsOptional();
            Property( p => p.SalesTax).IsOptional();
            Property(p => p.IPAddress).IsRequired().HasMaxLength(20);
            Property( p => p.SalesTaxApplied).IsOptional();


            HasRequired( order => order.Customer).WithMany().HasForeignKey(order => order.CustomerId).WillCascadeOnDelete(false);
            HasOptional( order => order.Store ).WithMany().HasForeignKey( order => order.StoreId ).WillCascadeOnDelete( false );
            HasOptional( order => order.ProductionLine ).WithMany( prodLine => prodLine.Orders ).HasForeignKey( order => order.ProductionLineId ).WillCascadeOnDelete( false );
            HasMany( order => order.Details ).WithRequired().HasForeignKey( detail => detail.OrderId ).WillCascadeOnDelete( true );
            HasMany( order => order.Payments).WithRequired().HasForeignKey(payment => payment.OrderId).WillCascadeOnDelete(true);
            HasMany( order => order.Attachments).WithRequired().HasForeignKey(attachment => attachment.OrderId).WillCascadeOnDelete(true);

        }
    }
}
