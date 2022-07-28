namespace Infrastructure
{
    public static class ServiceLocator
    {
        public static void RegisterSingle<TService>(TService implementation)
        {
            new Implementation<TService>(implementation);
        }

        public static TService Single<TService>()
        {
            return Implementation<TService>.Instance;
        }

        private class Implementation<TService>
        {
            public static TService Instance { get; private set; }

            public Implementation(TService instance)
            {
                Instance = instance;
            }
        }
    }
}