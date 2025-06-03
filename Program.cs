using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WebApi29.Context;
using WebApi29.Services.IServices;
using WebApi29.Services.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
	c.SwaggerDoc("v1", new() { Title = "WebApi29", Version = "v1" });
	c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
	{
		Description = "JWT Authorization usando el esquema Bearer. Ejemplo: 'Bearer {token}'",
		Name = "Authorization",
		In = Microsoft.OpenApi.Models.ParameterLocation.Header,
		Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
		Scheme = "Bearer"
	});
	c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
	{
		{
			new Microsoft.OpenApi.Models.OpenApiSecurityScheme
			{
				Reference = new Microsoft.OpenApi.Models.OpenApiReference
				{
					Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
					Id = "Bearer"
				}
			},
			Array.Empty<string>()
		}
	});
});
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(
	builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddTransient<IUsuarioServices, UsuarioServices>();
builder.Services.AddTransient<IRolServices, RolServices>();
//para el jwt
builder.Services.AddAuthentication();
builder.Services.AddAuthorization();

builder.Services.AddAuthentication("Bearer")
	.AddJwtBearer("Bearer", options =>
	{
		options.TokenValidationParameters = new TokenValidationParameters
		{
			ValidateIssuer = true,
			ValidateAudience = true,
			ValidateLifetime = true,
			ValidateIssuerSigningKey = true,
			ValidIssuer = builder.Configuration["Jwt:Issuer"],
			ValidAudience = builder.Configuration["Jwt:Audience"],
			IssuerSigningKey = new SymmetricSecurityKey(
				Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
		};
	});

builder.Services.AddAuthorization();
builder.Services.AddScoped<IAuthServices, AuthServices>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//para el jwt
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
