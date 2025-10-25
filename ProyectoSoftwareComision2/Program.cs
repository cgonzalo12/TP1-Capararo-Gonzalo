using Application.DTOs;
using Application.Interfaces;
using Application.Services;
using Infrastructure.Commands;
using Infrastructure.Persistence.Configurations;
using Infrastructure.Queries;
using Microsoft.EntityFrameworkCore;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy =>
        {
            // 🔸 Cambiá el puerto si Live Server usa otro (5500 o 5501 normalmente)
            policy.WithOrigins("http://127.0.0.1:5500")
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

//custom
//dbcontext
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(opt => opt.UseSqlServer(connectionString));

//interfaces
//--dishes
builder.Services.AddScoped<ICreateDishService,CreateDishService>();
builder.Services.AddScoped<IGetDishByIdService, GetDishByIdService>();
builder.Services.AddScoped<IGetAllDishesService, GetAllDishService>();
builder.Services.AddScoped<IUpdateDishService, UpdateDishService>();
builder.Services.AddScoped<IDeleteDishService, DeleteDishService>();
builder.Services.AddScoped<IDishCommand,DishCommand>();
builder.Services.AddScoped<IDishQuery, DishQuery>();
//--categories
builder.Services.AddScoped<ICategoryQuery, CategoryQuery>();
builder.Services.AddScoped<ICreateCategoryService, CreateCategoryService>();
builder.Services.AddScoped<ICategoryCommand, CategoryCommand>();
builder.Services.AddScoped<IGetAllCategoryService, GetAllCategoryService>();
//--deliveryType
builder.Services.AddScoped<IDeliveryTypeCommand,DeliveryTypeCommand >();
builder.Services.AddScoped<ICreateDeliveryTypeService, CreateDeliveryTypeService>();
builder.Services.AddScoped<IGetAllDeliveryTypeService, GetAllDeliveryTypeService>();
builder.Services.AddScoped<IDeliveryTypeQuery, DeliveryTypeQuery>();
builder.Services.AddScoped<IGetDeliveryTypeByIdService, GetDeliveryTypeByIdService>();
//--status
builder.Services.AddScoped<IStatusCommand,StatusCommand >();
builder.Services.AddScoped<ICreateStatusService, CreateStatusService>();
builder.Services.AddScoped<IGetAllStatusService, GetAllStatusService>();
builder.Services.AddScoped<IStatusQuery, StatusQuery>();
//--orders
builder.Services.AddScoped<IOrderCommand, OrderCommand>();
builder.Services.AddScoped<IGetAllOrdersService, GetAllOrdersService>();
builder.Services.AddScoped<IOrderQuery, OrderQuery>();
builder.Services.AddScoped<ICreateOrderService, CreateOrderService>();
builder.Services.AddScoped<IGetOrderByIdService, GetOrderByIdService>();
builder.Services.AddScoped<IUpdateOrderService, UpdateOrderService>();
builder.Services.AddScoped<IOrderItemUpdateService, OrderItemUpdateService>();

//--orderItems
builder.Services.AddScoped<IOrderItemCommand, OrderItemCommand>();
builder.Services.AddScoped<IOrderItemQuery, OrderItemQuery>();
builder.Services.AddScoped<IGetAllOrderItemService, GetAllOrderItemService>();
builder.Services.AddScoped<ICreateOrderItemService, CreateOrderItemService>();

var app = builder.Build();

app.UseCors("AllowFrontend");

//Ejecutar la precarga de datos

//using (var scope = app.Services.CreateScope())
//{
//    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
//    try
//    {
//        dbContext.Database.Migrate();

//        await ApplicationDbContext.SeedAsync(dbContext);
//    }
//    catch (Exception ex)
//    {
//        Console.WriteLine($"Error al precargar datos: {ex.Message}");
//    }
//}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(swagger =>
    {
        swagger.SwaggerEndpoint("/swagger/v1/swagger.json", "Restaurant API v1");
        swagger.RoutePrefix = "swagger";
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
