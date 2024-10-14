
using Microsoft.Extensions.Options;

namespace StudentsCURD;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddCors(Options => {

            Options.AddPolicy("AllowLocalhost5500",
           policy =>
           {

policy.WithOrigins("http://127.0.0.1:5500")
               .AllowAnyMethod()
               .AllowAnyHeader();
               


           });
        });

// http://127.0.0.1:5500/FrontEnd/index.html


        var app = builder.Build();


       app.UseCors("AllowLocalhost5500");

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }



        app.UseHttpsRedirection();

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}
