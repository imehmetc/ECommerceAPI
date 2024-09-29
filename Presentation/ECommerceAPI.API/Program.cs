using ECommerceAPI.Persistence;
using ECommerceAPI.Application;
using ECommerceAPI.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using ECommerceAPI.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using ECommerceAPI.Application.Abstractions;
using ECommerceAPI.Persistence.Concretes;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using ECommerceAPI.Application.Abstractions.Token;
using ECommerceAPI.Infrastructure.Concretes.Jwt;
using ECommerceAPI.Application.Repositories;
using ECommerceAPI.Domain.Entities.Concrete;
using ECommerceAPI.Infrastructure.Repository;
using ECommerceAPI.Application.Abstractions.Comment;
using ECommerceAPI.Persistence.Concretes.Comment;
using Microsoft.OpenApi.Models;
using ECommerceAPI.Infrastructure.Concretes;
using System.Security.Claims;
using Serilog.Events;
using Serilog.Sinks.MSSqlServer;
using Serilog;
using ECommerceAPI.API.ActionFilters;
using ECommerceAPI.API.Middleware;
using ECommerceAPI.Infrastructure.Repositories;

namespace ECommerceAPI.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddPersistenceServices();
            builder.Services.AddApplicationServices();

            builder.Services.AddDbContext<ECommerceDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            builder.Services.AddDbContext<ECommerceLogDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("LogConnection"))); // Log veritabanı

            // Loglama için tablo
            var connectionString = builder.Configuration.GetConnectionString("LogConnection");
            
            Log.Logger = new LoggerConfiguration()
               .MinimumLevel.Information()
               .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
               .Enrich.FromLogContext()
               .WriteTo.MSSqlServer(
                   connectionString: connectionString,
                   sinkOptions: new MSSqlServerSinkOptions
                   {
                       TableName = "Logs", // Tablo adı

                       AutoCreateSqlTable = true
                   },
                   restrictedToMinimumLevel: LogEventLevel.Information)
               .CreateLogger();

            builder.Services.AddControllers(config =>
            {
                config.Filters.Add<PerformanceLoggingFilter>(); // Global filtre kaydı
            });

            // Services
            builder.Services.AddScoped<ILoginService, LoginService>();
			builder.Services.AddScoped<IAdminService, AdminService>();
			builder.Services.AddScoped<IAccountService, AccountService>();
            builder.Services.AddScoped<IMailService, MailService>();
            builder.Services.AddScoped<ICategoryService, CategoryService>();
            builder.Services.AddScoped<ICommentService, CommentService>();
            builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.AddScoped<ISellerService, SellerService>();
            builder.Services.AddScoped<ICustomerService, CustomerService>();
            builder.Services.AddScoped<ICartItemService, CartItemService>();
            builder.Services.AddScoped<ICartService, CartService>();
            builder.Services.AddScoped<IAddressService, AddressService>();
            builder.Services.AddScoped<IOrderService, OrderService>();

            builder.Services.AddScoped<ILogService, LogService>();

            // Repositories
            builder.Services.AddScoped<IRepository<HelpCenter>, Repository<HelpCenter>>();
			builder.Services.AddScoped<IRepository<Category>, Repository<Category>>();
			builder.Services.AddScoped<IRepository<Product>, Repository<Product>>();
			builder.Services.AddScoped<IRepository<Contact>, Repository<Contact>>();
            builder.Services.AddScoped<IRepository<AboutUs>, Repository<AboutUs>>();
            builder.Services.AddScoped<IRepository<UserComment>, Repository<UserComment>>();
            builder.Services.AddScoped<IRepository<Order>, Repository<Order>>();
            builder.Services.AddScoped<IRepository<CartItem>, Repository<CartItem>>();
            builder.Services.AddScoped<IRepository<Cart>, Repository<Cart>>();
            builder.Services.AddScoped<IRepository<Address>, Repository<Address>>();

            builder.Services.AddScoped<IRepository<SellerReply>, Repository<SellerReply>>();
            builder.Services.AddScoped<ILogRepository<LogEntry>, LogRepository<LogEntry>>(); // Log repository

            builder.Services.AddControllers();

            builder.Services.AddCors(options => options.AddDefaultPolicy(p => p.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin())); // Ajax istekleri için

            builder.Services.AddIdentity<AppUser, AppRole>()
				.AddEntityFrameworkStores<ECommerceDbContext>()
				.AddDefaultTokenProviders();

            // JWT Eklenmesi
            builder.Services.AddScoped<IJwtTokenService, JwtTokenService>();

            var key = Encoding.ASCII.GetBytes(builder.Configuration["Jwt:SecurityKey"]);
            builder.Services.AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {
                o.RequireHttpsMetadata = false;
                o.SaveToken = true;
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    ValidAudience = builder.Configuration["Jwt:Audience"],
                    ValidateLifetime = true,
                    RoleClaimType = ClaimTypes.Role,
                    ClockSkew = TimeSpan.Zero
                };
            });

            // Güvenlik şeması oluşturalım. Token'imiz ile ilgili bir şemadır.
            var securityScheme = new OpenApiSecurityScheme()
            {
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer", // Güvenlik şemasının adı
                BearerFormat = "JWT",
                In = ParameterLocation.Header, // Token'i neresinde göndereceğiz (Header'da gönderilir genellikle)
                Description = "JSON Web Token based security"
            };

            // Güvenlik şeması gereksinimi oluşturalım.
            var securityRequirement = new OpenApiSecurityRequirement()
            {
                {
                     new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme, // yukarıda yazdığımız SecurityScheme olduğundan böyle yazdık.
                                Id = "Bearer"
                            }
                        },
                     // Gereksinim ile ilgili ekstra bilgi yazmamızı ister. Ancak vermeye gerek yoktur. Boş bir string dizisi yazalım.
                     new string[] {}
                }

            };

            // Gönderenin bilgilieri
            var contact = new OpenApiContact()
            {
                Name = "ANK17 Sınıfı",
                Email = "ank17@gmail.com",
                Url = new Uri("http://www.bilgeadam.com")
            };

            // Lisans bilgileri
            var license = new OpenApiLicense()
            {
                Name = "Free License",
                Url = new Uri("http://www.bilgeadam.com")
            };

            var info = new OpenApiInfo()
            {
                Version = "V1.0",
                Title = "Jwt Token Uygulaması",
                Description = "BilgeAdam ANK17 Grubu Jwt Token Uygulaması",
                Contact = contact,
                License = license,
                TermsOfService = new Uri("http://www.bilgeadam.com")
            };

            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", info); // Swagger'a contact, license ve info bilgilerini gönderir.
                options.AddSecurityDefinition("Bearer", securityScheme); // yazdığımız securityScheme'i gönderir
                options.AddSecurityRequirement(securityRequirement); // yazdığımız securityRequirement'ı gönderir
            });


            // Swagger için güvenlik tanımı ekle
            builder.Services.AddEndpointsApiExplorer();

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var userManager = services.GetRequiredService<UserManager<AppUser>>();
                    var roleManager = services.GetRequiredService<RoleManager<AppRole>>();

                    await SeedData.Initialize(services, userManager, roleManager);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred while seeding the database: {ex.Message}");
                }
            }

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseMiddleware<ExceptionHandlingMiddleware>(); // Hataların loglanması
            app.UseMiddleware<RequestTimingMiddleware>(); // API yanıt sürelerinin loglanması

            app.UseHttpsRedirection();

            app.UseCors(); // CORS'un kullanılması

            app.UseAuthentication();  // Authentication'ı kullan
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
