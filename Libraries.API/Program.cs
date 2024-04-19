using Microsoft.EntityFrameworkCore;
using Libraries.Data;
using Libraries.Models;
using Libraries.Services;
using Libraries.DTOs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//connection service
builder.Services.AddDbContext<LibrariesDbContext>(option => option.UseSqlServer(builder.Configuration["dbconnectionstr"]));

builder.Services.AddScoped<IBooksRepository, BooksRepository>();
builder.Services.AddScoped<IBookService, BookService>();

builder.Services.AddScoped<IAuthorsRepository, AuthorsRepository>();
builder.Services.AddScoped<IAuthorService, AuthorService>();

builder.Services.AddScoped<IGenresRepository, GenresRepository>();
builder.Services.AddScoped<IGenreService, GenreService>();

builder.Services.AddControllers()
.AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
        });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//new comment line

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
