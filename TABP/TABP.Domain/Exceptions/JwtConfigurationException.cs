namespace TABP.Domain.Exceptions
{
    public class JwtConfigurationException : Exception
    {
        public static readonly string MissingSigningKey = "JWT signing key is not configured.";
        public static readonly string InvalidExpiration = "Invalid JWT expiration value.";
        public static readonly string MissingJwtSection = "JWT configuration section is missing in the appsettings.";
        public static readonly string MissingIssuerOrAudience = "JWT issuer or audience is not configured.";
        public JwtConfigurationException(string message) : base(message) { }
    }
}