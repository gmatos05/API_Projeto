using API_projeto.Filter;
using API_projeto.Repository;
using API_projeto.Service.Dto;
using API_projeto.Service.Interface;
using API_projeto.Service.Service;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace API_projeto
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddMvc(options =>
            {
                options.Filters.Add(typeof(ExcecaoGeralFilter));
            });

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            
            var chaveCripto = Encoding.ASCII.GetBytes(Environment.GetEnvironmentVariable("SECRET_KEY"));//Se não der certo é isso aqui 
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(chaveCripto),
                    ValidateIssuer = true,
                    ValidIssuer = "APIPessoa.com",
                    ValidateAudience = true,
                    ValidAudience = "API_projeto.com"
                };
            });
            builder.Services.AddScoped<ICityEventRepository, CityEventRepository>();
            builder.Services.AddScoped<IEventReservationRepository, EventReservationRepository>();
            builder.Services.AddScoped<ICityEventService, CityEventService>();
            builder.Services.AddScoped<IEventReservationService, EventReservationServices>();
            MapperConfiguration mapperConfig = new(mc =>
            {
                mc.CreateMap<CityEventEntity, CityEventDto>().ReverseMap();
               
                mc.CreateMap<EventReservationEntity, EventReservationDto>().ReverseMap();
            }
            );
            

            IMapper mapper = mapperConfig.CreateMapper();
            

            builder.Services.AddSingleton(mapper);
            
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}