using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CFB.BlazorWebApp.ThanhDT.Services
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly AuthenticationService _authService;

        public CustomAuthenticationStateProvider(AuthenticationService authService)
        {
            _authService = authService;
            _authService.AuthenticationStateChanged += OnAuthenticationStateChanged;
        }

        public override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var claimsPrincipal = _authService.GetClaimsPrincipal();
            return Task.FromResult(new AuthenticationState(claimsPrincipal));
        }

        private void OnAuthenticationStateChanged()
        {
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        public void NotifyAuthenticationStateChangedManually()
        {
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }
    }
}
