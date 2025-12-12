using CFB.Entities.ThanhDT.Models;
using CFB.Services.ThanhDT;
using Microsoft.JSInterop;
using System.Security.Claims;

namespace CFB.BlazorWebApp.ThanhDT.Services
{
    public class AuthenticationService
    {
        private readonly IJSRuntime _jsRuntime;
        private SystemUserAccount? _currentUser;
        private readonly SystemUserAccountService _service;
        public AuthenticationService(IJSRuntime jsRuntime, SystemUserAccountService service)
        {
            _jsRuntime = jsRuntime;
            _service = service;
        }

        public SystemUserAccount? CurrentUser => _currentUser;
        public bool IsAuthenticated => _currentUser != null && _currentUser.IsActive;
        public event Action? AuthenticationStateChanged;

        public async Task<bool> LoginAsync(string username, string password)
        {
            try
            {
                var user = await _service.GetUserAccount(username, password);

                if (user != null && user.IsActive)
                {
                    _currentUser = user;

                    await _jsRuntime.InvokeVoidAsync("sessionStorage.setItem", "isAuthenticated", "true");
                    await _jsRuntime.InvokeVoidAsync("sessionStorage.setItem", "username", user.UserName);
                    await _jsRuntime.InvokeVoidAsync("sessionStorage.setItem", "fullName", user.FullName ?? user.UserName);
                    await _jsRuntime.InvokeVoidAsync("sessionStorage.setItem", "email", user.Email ?? "");
                    await _jsRuntime.InvokeVoidAsync("sessionStorage.setItem", "roleId", user.RoleId.ToString());
                    Console.WriteLine($"[DEBUG] DB query: {username} / {password}");
                    AuthenticationStateChanged?.Invoke();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Login error: {ex.Message}");
                return false;
            }
        }

        public async Task LogoutAsync()
        {
            _currentUser = null;
            await _jsRuntime.InvokeVoidAsync("sessionStorage.clear");
            AuthenticationStateChanged?.Invoke();
        }

        public async Task LoadUserFromSessionStorageAsync(bool isPrerendering = false)
        {
            if (isPrerendering)
            {
                Console.WriteLine("[INFO] Skipped loading user during prerender.");
                return;
            }

            try
            {
                var isAuthenticated = await _jsRuntime.InvokeAsync<string>("sessionStorage.getItem", "isAuthenticated");
                if (isAuthenticated == "true")
                {
                    var username = await _jsRuntime.InvokeAsync<string>("sessionStorage.getItem", "username");
                    var fullName = await _jsRuntime.InvokeAsync<string>("sessionStorage.getItem", "fullName");
                    var email = await _jsRuntime.InvokeAsync<string>("sessionStorage.getItem", "email");
                    var roleIdStr = await _jsRuntime.InvokeAsync<string>("sessionStorage.getItem", "roleId");

                    if (!string.IsNullOrEmpty(username) && int.TryParse(roleIdStr, out int roleId))
                    {
                        _currentUser = new SystemUserAccount
                        {
                            UserName = username,
                            FullName = fullName ?? username,
                            Email = email ?? "",
                            RoleId = roleId,
                            IsActive = true
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading user from session storage: {ex.Message}");
                _currentUser = null;
            }
        }
        public ClaimsPrincipal GetClaimsPrincipal()
        {
            if (!IsAuthenticated || _currentUser == null)
                return new ClaimsPrincipal();

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, _currentUser.UserAccountId.ToString()),
                new Claim(ClaimTypes.Name, _currentUser.UserName),
                new Claim(ClaimTypes.Email, _currentUser.Email ?? ""),
                new Claim("FullName", _currentUser.FullName ?? _currentUser.UserName),
                new Claim("RoleId", _currentUser.RoleId.ToString())
            };

            var identity = new ClaimsIdentity(claims, "CustomAuth");
            return new ClaimsPrincipal(identity);
        }
    }
}
