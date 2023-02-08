using Chapter.Contexts;
using Chapter.Interfaces;
using Chapter.Repositories;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<ChapterContext, ChapterContext>();
builder.Services.AddTransient<ILivroRepository, LivroRepository>();
builder.Services.AddTransient<IUsuarioRepository, UsuarioRepository>();

builder.Services.AddControllers();

//Adicionado servi�os de CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder =>
    {
        builder.WithOrigins("http://localhost:3000")
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});

//Adicionado servi�o de JwtBearer, que � uma forma de autentifica��o
builder.Services.AddAuthentication(options =>
{
    options.DefaultChallengeScheme = "JwtBearer";
    options.DefaultAuthenticateScheme = "JwtBearer";
})
    //Define os par�metros de valida��o do Token
    .AddJwtBearer("JwtBearer", options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            //Valida quem est� solicitando o acesso
            ValidateIssuer = true,
            //Valida quem est� recebendo 
            ValidateAudience = true,
            //Define se o termo de expira��o ser� validada
            ValidateLifetime = true,
            //Forma de criptografia e ainda valida a chave de autentifica��o
            IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("chapter-chave-autenticacao")),
            //Valida tempo de expira��o do Token
            ClockSkew = TimeSpan.FromMinutes(30),
            //Nome do Issuer, de onde est� vindo
            ValidIssuer = "Chapter",
            //Nome do audience, para onde est� indo
            ValidAudience = "Chapter"
        };
    });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Chapter API",
        Description = "API de livros desenvolvida na UC de Desenvolvimento de API",
        Contact = new OpenApiContact
        {
            Name = "Benigno Batista",
        },
        License = new OpenApiLicense
        {
            Name = "Chapter License"
        }
    });



    //Trecho options.AddSecurityDefinition copiado resolu��o GitHub
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
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


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseCors("CorsPolicy");

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();