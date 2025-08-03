namespace TABP.API.Common
{
    public static class ApiRoutes
    {
        public const string Base = "api/[controller]";
        public static class Identity
        {
            public const string Login = Base;
        }
        public static class Cities
        {
            public const string GetById = Base + "/{id}";
            public const string GetAll = Base;
            public const string Create = Base;
            public const string Update = Base + "/{id}";
            public const string Delete = Base + "/{id}";
            public const string GetTrending = Base + "/trending";
        }
        public static class Hotels
        {
            public const string GetById = Base + "/{id}";
            public const string GetAll = Base;
            public const string Create = Base;
            public const string Update = Base + "/{id}";
            public const string Delete = Base + "/{id}";
            public const string GetFeaturedDeals = Base + "/featured-deals";
        }
    }
}
