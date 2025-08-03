﻿namespace TABP.Infrastructure.Configurations
{
    public class StripeSettings
    {
        public const string SectionName = "Stripe";
        public string SecretKey { get; set; } = string.Empty;
        public string PublishableKey { get; set; } = string.Empty;
    }
}