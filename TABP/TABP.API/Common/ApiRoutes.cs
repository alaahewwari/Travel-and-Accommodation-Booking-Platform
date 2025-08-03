namespace TABP.API.Common
{
    public static class ApiRoutes
    {
        public const string Base = "api/[controller]";
        public static class Identity
        {
            public const string Login = Base + "/login";
        }
    }
}
