namespace Adiis.Data
{
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Validation;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Threading.Tasks;
    using Ceiba.Common.Domain;
    using Ceiba.Common.Logging;
    using Configurations;
    using Model;
    using System;
    [ExcludeFromCodeCoverage]
    public class AdiisContext: DbContext, IQueryableUnitOfWork
    {
        #region Constants

        private const string schema = "dbo";
        private const string connectionStringName = "AdiisDBConnectionString";
        private static readonly ILogger logger = LoggerFactory.Create(typeof(AdiisContext));

        #endregion

        #region Construction

        public AdiisContext() : base( connectionStringName )
        {
            Database.SetInitializer<AdiisContext>(null);
            Database.Initialize( false );
            // HACK: To ensure that EntityFramework.SqlServer.dll is copied to the destination folder.
            var x = typeof( System.Data.Entity.SqlServer.SqlProviderServices );
        }

        #endregion

        #region Overrides

        protected override void OnModelCreating( DbModelBuilder modelBuilder )
        {
            if( modelBuilder == null )
            {
                return;
            }

            modelBuilder.HasDefaultSchema( schema );

            modelBuilder.Ignore<Vendor>();
            modelBuilder.Ignore<SalesRep>();

            modelBuilder.Configurations.Add( new DetailEntityConfiguration() );
            modelBuilder.Configurations.Add( new InventoryPurchaseEntityConfiguration() );
            modelBuilder.Configurations.Add( new SaleItemEntityConfiguration() );
            modelBuilder.Configurations.Add( new InventoryItemEntityConfiguration()); 
            modelBuilder.Configurations.Add( new LeatherItemGradeEntityConfiguration() );
            modelBuilder.Configurations.Add( new LeatherItemEntityConfiguration() );
            modelBuilder.Configurations.Add( new LeatherTypeEntityConfiguration() );
            modelBuilder.Configurations.Add( new OrderEntityConfiguration() );
            modelBuilder.Configurations.Add( new StatusEntityConfiguration() );
            modelBuilder.Configurations.Add( new StoreEntityConfiguration() );
            modelBuilder.Configurations.Add( new StyleEntityConfiguration() );
            modelBuilder.Configurations.Add( new SettingEntityConfiguration() );
            modelBuilder.Configurations.Add( new ClosedDateEntityConfiguration() );
            modelBuilder.Configurations.Add( new TemplateEntityConfiguration() );
            modelBuilder.Configurations.Add( new OperationEntityConfiguration() );
            modelBuilder.Configurations.Add( new OperationRegisterEntityConfiguration() );
            modelBuilder.Configurations.Add( new ProductionLineEntityConfiguration() );
            modelBuilder.Configurations.Add( new JournalConfigEntityConfiguration() );
            modelBuilder.Configurations.Add( new SystemLogEntityConfiguration() );
            modelBuilder.Configurations.Add( new TimeRegisterEntityConfiguration());
            modelBuilder.Configurations.Add( new EmailEntityConfiguration());
            modelBuilder.Configurations.Add( new DetailPricesEntityConfiguration());
            modelBuilder.Configurations.Add( new CustomizationEntityConfiguration());
            modelBuilder.Configurations.Add( new CustomizationPartEntityConfiguration());
            modelBuilder.Configurations.Add(new DetailCustomizationEntityConfiguration());
            modelBuilder.Configurations.Add(new CustomizationPositionEntityConfiguration());
            modelBuilder.Configurations.Add(new FurnitureTypeEntityConfiguration());
            modelBuilder.Configurations.Add(new CustomerEntityConfiguration());
            modelBuilder.Configurations.Add(new AddressEntityConfiguration());
            modelBuilder.Configurations.Add(new PaymentTypeEntityConfiguration());
            modelBuilder.Configurations.Add(new PaymentEntityConfiguration());
            modelBuilder.Configurations.Add(new StyleDefaultEntityConfiguration());
            modelBuilder.Configurations.Add(new OrderHistoryEntityConfiguration());
            modelBuilder.Configurations.Add(new InventoryMovementEntityConfiguration());
            modelBuilder.Configurations.Add(new UserAdditionalInfoEntityConfiguration());
            modelBuilder.Configurations.Add(new OrderAttachmentEntityConfiguration());
            modelBuilder.Configurations.Add(new VinItemEntityConfiguration());
            modelBuilder.Configurations.Add(new TransactionHistoryEntityConfiguration());
            modelBuilder.Configurations.Add(new LabelEntityConfiguration());
            modelBuilder.Configurations.Add(new LabelConfigurationEntityConfiguration());
            modelBuilder.Configurations.Add(new GUIPropertyEntityConfiguration());
            modelBuilder.Configurations.Add(new SkinEntityConfiguration());

            base.OnModelCreating( modelBuilder );
        }

        #endregion

        #region IQueryableUnitOfWork Members

        public DbSet<TEntity> GetSet<TEntity>() where TEntity : Entity<long>
        {
            DbSet<TEntity> result = null;
            try
            {
                result = Set<TEntity>();
            }
            catch (Exception ex)
            {
                logger.Log(LogLevel.Fatal, ex.Message, ex);
            }
            return result;
        }

        public void Commit()
        {
            try
            {
                SaveChanges();
            }
            catch( DbUpdateConcurrencyException ex )
            {
                ex.Entries.Single().Reload();
            }
            catch( DbEntityValidationException ex )
            {
                var results = ex.EntityValidationErrors;

                foreach( var result in results )
                {
                    logger.Log( LogLevel.Fatal, 
                                "Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                                result.Entry.Entity.GetType().Name, result.Entry.State );

                    var errors = result.ValidationErrors;

                    foreach( var error in errors )
                    {
                        logger.Log( LogLevel.Fatal, 
                                    "- Property: \"{0}\", Error: \"{1}\"",
                                    error.PropertyName, error.ErrorMessage );
                    }
                }
            }
        }

        public async Task CommitAsync()
        {
            try
            {
                await SaveChangesAsync();
            }
            catch( DbUpdateConcurrencyException ex )
            {
                ex.Entries.Single().Reload();
            }
            catch( DbUpdateException ex )
            {
                logger.Log( LogLevel.Error, ex.Message, ex );
                throw;
            }
            catch( DbEntityValidationException ex )
            {
                foreach( var eve in ex.EntityValidationErrors )
                {
                    logger.Log( LogLevel.Fatal, "Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State );
                    foreach( var ve in eve.ValidationErrors )
                    {
                        logger.Log( LogLevel.Fatal, "- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage );
                    }
                }

                throw;
            }
        }

        public void ApplyCurrentValues<TEntity>( TEntity original, TEntity current ) where TEntity : Entity<long>
        {
            this.Entry( original ).CurrentValues.SetValues( current );
        }

        public void SetAutoDetectChanges(bool autoDetect)
        {
            this.Configuration.AutoDetectChangesEnabled = autoDetect;
        }

        #endregion
    }
}
