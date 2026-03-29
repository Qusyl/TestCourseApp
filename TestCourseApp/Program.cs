using Application;
using Application.Handler.Cart;
using Application.Handler.Order;
using Application.Handler.Product;
using Application.Handler.User;
using Application.Interface;
using Application.Utills;
using Infrastructure.BackgroundService;
using Infrastructure.Message;
using Infrastructure.Persistance;
using Infrastructure.Persistance.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IOrderRepository, OrderRepository>();

builder.Services.AddScoped<IProductRepository, ProductRepository>();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<ICartRepository, CartRepository>();

builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();

builder.Services.AddHostedService<OutboxProcessService>();

builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();

builder.Services.AddScoped<CreateOrderHandler>();

builder.Services.AddScoped<CreateProductHandler>();

builder.Services.AddScoped<CheckOutCartHandler>();

builder.Services.AddScoped<CreateProductHandler>();

builder.Services.AddScoped<DeleteProductHandler>();
builder.Services.AddScoped<GetProductByIdHandler>();
builder.Services.AddScoped<GetProductsHandler>();

builder.Services.AddScoped<RegisterUserHandler>();

builder.Services.AddScoped<LoggingUserHandler>();

builder.Services.AddScoped<AddToCartHandler>();

builder.Services.AddScoped<RemoveFromCartHandler>();

builder.Services.AddScoped<IEventPublisher, EventPublisher>();

builder.Services.AddScoped<ITokenService, TokenService>();

builder.Services.AddAuthentication("Bearer").AddJwtBearer("Bearer", options =>

    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateIssuerSigningKey = true,
        ValidateLifetime = true,
        ValidateAudience = false,
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes("SUPER_SECRET_KEY_FOR_THIS_SUPER_PROJECT_YOU_KNOW_123"))
    }
);

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy =>
        policy.RequireRole("Admin")
    );
}
);

builder.Services.AddControllers();

builder.Services.AddOpenApi();

builder.Services.AddProblemDetails();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseExceptionHandler();

app.MapControllers();


app.UseDeveloperExceptionPage();
app.Run();
