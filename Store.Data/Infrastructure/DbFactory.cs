namespace Store.Data.Infrastructure
{
    public class DbFactory : Disposable, IDbFactory
    {
        private StoreEntities dbContext;

        public StoreEntities Init()
        {
            return dbContext ?? (dbContext = new StoreEntities());
        }

        protected override void DisposeCore()
        {
            if (dbContext != null)
                dbContext.Dispose();
        }
    }
}