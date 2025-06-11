using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using EnglishLearning.Web;
using EnglishLearning.Web.Services;
using EnglishLearning.Web.Services.Contracts;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using EnglishLearning.Models.Dtos;

namespace EnglishLearning.Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");

            builder.Services.AddOptions();
            builder.Services.AddAuthorizationCore();

            builder.Services.AddScoped<AuthenticationStateProvider, TokenAuthenticationStateProvider>();
            builder.Services.AddScoped<IMyLocalStorageService, MyLocalStorageService>();
            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7127") });

            builder.Services.AddScoped<ITopicService, TopicService>();
            builder.Services.AddScoped<IResourceProfileService, ResourceProfileService>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IUserProfileActivitiesService, UserProfileActivitiesService>();

            builder.Services.AddBlazoredLocalStorage();

            builder.Services.AddScoped<IManageTopicsLocalStorageService, ManageTopicsLocalStorageService>();
            builder.Services.AddScoped<IManageResourceProfile_ItemsLocalStorageService, ManageResourceProfile_ItemsLocalStorageService>();
            builder.Services.AddScoped<IManageUsersLocalStorageService, ManageUsersLocalStorageService>();
            builder.Services.AddScoped<IManageUserProfileActivitiessLocalStorageService, ManageUserProfileActivitiessLocalStorageService>();

            await builder.Build().RunAsync();
        }
    }

    public class TokenAuthenticationStateProvider : AuthenticationStateProvider
    {
        public readonly IMyLocalStorageService _myLocalStorageService;

        public TokenAuthenticationStateProvider(IMyLocalStorageService myLocalStorageService)
        {
            _myLocalStorageService = myLocalStorageService;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            AuthenticationState CreateAnonymous()
            {
                var anonymousIdentity = new ClaimsIdentity();
                var anonymousPrincipal = new ClaimsPrincipal(anonymousIdentity);
                return new AuthenticationState(anonymousPrincipal);
            }

            var token = await _myLocalStorageService.GetAsync<SecurityTokenDto>(nameof(SecurityTokenDto));

            if (token == null)
            {
                return CreateAnonymous();
            }

            if (string.IsNullOrEmpty(token.AccessToken) || token.ExpiredAt < DateTime.UtcNow)
            {
                return CreateAnonymous();
            }

            //Create real user state
            var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Country, "Ukraine"),
                    new Claim(ClaimTypes.Name, token.UserName),
                    new Claim(ClaimTypes.Expired, token.ExpiredAt.ToLongDateString()),
                    new Claim(ClaimTypes.Role, "Administrator"),
                    new Claim(ClaimTypes.Role, "Manager"),
                    new Claim("Blazor", "Rulezzz")
                };

            var identity = new ClaimsIdentity(claims, "Token");
            var principal = new ClaimsPrincipal(identity);
            return new AuthenticationState(principal);
        }

        public void MakeUserAnonymous()
        {
            _myLocalStorageService.RemoveAsync(nameof(SecurityTokenDto));

            var anonymousIdentity = new ClaimsIdentity();
            var anonymousPrincipal = new ClaimsPrincipal(anonymousIdentity);
            var aurhState = Task.FromResult(new AuthenticationState(anonymousPrincipal));
            NotifyAuthenticationStateChanged(aurhState);
        }
    }
}