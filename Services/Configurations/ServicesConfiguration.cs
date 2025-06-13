using Firebase.Storage;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using MusicBackend.Interfaces;
using MusicBackend.Interfaces.Auth;
using MusicBackend.Services.Auth;

namespace MusicBackend.Services.Configurations;

public static class ServicesConfiguration
{
    
    public static void AddServices(this IServiceCollection services, IConfiguration configuration)
    {
;
    
        var googleCredentials = GoogleCredential.FromFile(configuration["Firebase:CredentialPath"]);
        FirebaseApp.Create(new AppOptions()
        {
            Credential = googleCredentials
        });
        
        services.AddAuthentication()
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme,options =>
            {
                options.Authority = configuration["Firebase:ValidIssuer"];
                options.Audience = configuration["Firebase:ProjectId"];
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = configuration["Firebase:ValidIssuer"],
                };
            });
        
        services.AddScoped<IAuthenticationService, AuthenticationService>();
        services.AddHttpClient<IJwtProvider, JwtProvider>(client =>
        {
            client.BaseAddress = new Uri(configuration["Firebase:TokenUri"]);
            
        });

        services.AddSingleton<IFileSystemStorage, FileSystemStorage>();
        services.AddSingleton<IFirebaseStorageService, FirebaseStorageService>();
    }
    
}