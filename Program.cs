using CozyHaven.Context;
using CozyHaven.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using System.Text.Json.Serialization;

namespace CozyHaven
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers().AddJsonOptions(opts =>
            {
                opts.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                opts.JsonSerializerOptions.WriteIndented = true;
            });
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            //builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(opt =>
            {
                opt.SwaggerDoc("v1", new OpenApiInfo { Title = "CozyHaven", Version = "v1" });
                opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "bearer"
                });

                opt.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type=ReferenceType.SecurityScheme,
                                    Id="Bearer"
                                }
                            },
                            new string[]{}
                        }
                    });
            });
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["SecretKey"])),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });

            #region contexts
            builder.Services.AddDbContext<RequestTrackerContext>(opts =>
            {
                opts.UseSqlServer(builder.Configuration.GetConnectionString("requestTrackerConnection"));
            });
            #endregion

            #region RepositoryInjection
            //builder.Services.AddScoped<IRepository<int, Admin>, AdminRepository>();
            //builder.Services.AddScoped<IRepository<int, Amenity>, AmenityRepository>();
            //builder.Services.AddScoped<IRepository<int, HotelOwner>, HotelOwnerRepository>();
            //builder.Services.AddScoped<IRepository<int, Hotel>, HotelRepository>();
            //builder.Services.AddScoped<IRepository<int, Payment>, PaymentRepository>();
            //builder.Services.AddScoped<IRepository<int, Reservation>, ReservationRepository>();
            //builder.Services.AddScoped<IRepository<int, Review>, ReviewRepository>();
            //builder.Services.AddScoped<IRepository<int, Room>, RoomRepository>();
            //builder.Services.AddScoped<IRepository<string, User>, UserRepository>();
            //#endregion

            //#region ServiceInjection
            //builder.Services.AddScoped<ITokenService, TokenService>();
            //builder.Services.AddScoped<IAuthService, AuthService>();
            //builder.Services.AddScoped<IAmenityAdminService, AmenityService>();
            //builder.Services.AddScoped<IAmenityCustomerService, AmenityService>();
            //builder.Services.AddScoped<IAmenityOwnerService, AmenityService>();
            //builder.Services.AddScoped<IHotelAdminService, HotelService>();
            //builder.Services.AddScoped<IHotelCustomerService, HotelService>();
            //builder.Services.AddScoped<IHotelOwnerService, HotelService>();
            //builder.Services.AddScoped<IPaymentAdminService, PaymentService>();
            //builder.Services.AddScoped<IPaymentCustomerService, PaymentService>();
            //builder.Services.AddScoped<IPaymentOwnerService, PaymentService>();
            //builder.Services.AddScoped<IReservationAdminService, ReservationService>();
            //builder.Services.AddScoped<IReservationCustomerService, ReservationService>();
            //builder.Services.AddScoped<IReservationHotelService, ReservationService>();
            //builder.Services.AddScoped<IReviewAdminService, ReviewService>();
            //builder.Services.AddScoped<IReviewCustomerService, ReviewService>();
            //builder.Services.AddScoped<IReviewHotelService, ReviewService>();
            //builder.Services.AddScoped<IRoomAdminService, RoomService>();
            //builder.Services.AddScoped<IRoomCustomerService, RoomService>();
            //builder.Services.AddScoped<IRoomHotelOwnerService, RoomService>();
            ////builder.Services.AddScoped<IUserAdminService, UserService>();
            ////builder.Services.AddScoped<IUserCustomerService, UserService>();
            ////builder.Services.AddScoped<IUserHotelOwnerService, UserService>();
            #endregion

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }


            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
