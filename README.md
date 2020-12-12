Code contains two applications
1) Web application 
2) API application

From web application is consuming one post api as per given requirement.
Api application is saving data to normal CSV file as there is no DB.

-----------------------------------------------------------------------------------------------

Integration of AD :

1) First register both application in azure from portal.
2) you will get the application Id and Client Id from portal.
3) Go to startup file and add below code that is used to validate the request.

services.AddAuthorization(options =>
            {
                var defaultAuthorizationPolicyBuilder = new AuthorizationPolicyBuilder(
                    JwtBearerDefaults.AuthenticationScheme,
                    "AzureAd");
                defaultAuthorizationPolicyBuilder =
                    defaultAuthorizationPolicyBuilder.RequireAuthenticatedUser();
                options.DefaultPolicy = defaultAuthorizationPolicyBuilder.Build();
            });

            services.AddAuthentication(sharedOptions =>
            {
                sharedOptions.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                sharedOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                sharedOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer("AzureAd", options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = this.Configuration["AzureAd:TenantId"],
                    ValidateAudience = true,
                    ValidAudience = this.Configuration["AzureAd:ClientId"],
                };
                options.Audience = this.Configuration["AzureAd:ClientId"];
                options.Authority = this.Configuration["AzureAd:Authority"];
            })
4) Put Authorize attribute in API.
