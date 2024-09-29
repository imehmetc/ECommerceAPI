using ECommerceAPI.Application.Abstractions;
using ECommerceAPI.Application.Abstractions.Token;
using ECommerceAPI.Application.DTOs;
using ECommerceAPI.Domain.Entities.Concrete;
using ECommerceAPI.Domain.Entities.Identity;
using ECommerceAPI.Persistence.Context;
using Google.Apis.Auth;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Persistence.Concretes
{
    public class LoginService : ILoginService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IJwtTokenService _jwtTokenService;

        public LoginService(UserManager<AppUser> userManager, IJwtTokenService jwtTokenService)
        {
            _userManager = userManager;
            _jwtTokenService = jwtTokenService;
        }

        public async Task<AppUser> LoginAsync(LoginDto loginDto)
        {
            AppUser user = await _userManager.FindByEmailAsync(loginDto.Email);

            if (user == null)
                return null;

            bool result = await _userManager.CheckPasswordAsync(user, loginDto.Password);

            if (result)
            {
                var userRoles = await _userManager.GetRolesAsync(user);

                if (!userRoles.Contains(loginDto.Role))
                    return null;
            }

            return user;
        }

        public async Task<string> LoginWithGoogle(GoogleLoginDto googleLoginDto)
        {
            try
            {
                // Google Token Info API'sini kullanarak access_token'ı doğrulayın
                var httpClient = new HttpClient();
                var response = await httpClient.GetAsync($"https://www.googleapis.com/oauth2/v3/tokeninfo?access_token={googleLoginDto.IdToken}");

                if (!response.IsSuccessStatusCode)
                {
                    // Eğer access_token geçerli değilse veya doğrulama başarısızsa, hata döndürün
                    throw new Exception("Invalid access token");
                }

                var content = await response.Content.ReadAsStringAsync();
                var payload = JsonConvert.DeserializeObject<Dictionary<string, object>>(content);

                // Payload içindeki bilgileri kontrol edin
                var email = payload.ContainsKey("email") ? payload["email"].ToString() : null;
                var subject = payload.ContainsKey("sub") ? payload["sub"].ToString() : null; // User ID

                if (email == null || subject == null)
                {
                    throw new Exception("Required information not found in token");
                }

                var info = new UserLoginInfo(googleLoginDto.Provider, subject, googleLoginDto.Provider);

                // Kullanıcıyı kontrol edin
                AppUser user = await _userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);
                bool result = user != null;

                // Eğer kullanıcı yoksa, e-posta ile kontrol edin
                if (user == null)
                {
                    user = await _userManager.FindByEmailAsync(email);

                    if (user == null)
                    {
                        user = new AppUser()
                        {
                            Id = Guid.NewGuid().ToString(),
                            Email = email,
                            UserName = email,
                            FirstName = googleLoginDto.FirstName,
                            LastName = googleLoginDto.LastName,
                        };

                        var identityResult = await _userManager.CreateAsync(user);
                       
                        await _userManager.AddToRoleAsync(user, "Member");
                       
                        result = identityResult.Succeeded;
                    }
                }

                var role = await _userManager.GetRolesAsync(user);
                if (role.Contains("Member"))
                {
                    result = true;
                }

                // Kullanıcıyı giriş bilgileri ile güncelleyin
                if (result)
                {
                    await _userManager.AddLoginAsync(user, info);
                }
                else
                {
                    return null;
                }

                // JWT token'ı oluşturun ve döndürün
                string token = await _jwtTokenService.GenerateTokenAsync(user);
                return token;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }


    }
}
