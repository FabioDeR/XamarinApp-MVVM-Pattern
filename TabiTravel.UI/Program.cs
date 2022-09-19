using FisSst.BlazorMaps.DependencyInjection;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Sotsera.Blazor.Toaster.Core.Models;
using TabiTravel.ServiceUI;
using TabiTravel.ServiceUI.Interface;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();


builder.Services.AddScoped<HttpClient>(s =>
{
    var client = new HttpClient { BaseAddress = new Uri("http://localhost:6909/") };
    return client;
});

// Toaster
builder.Services.AddToaster(config =>
{
    //Customizations
    config.PositionClass = Defaults.Classes.Position.BottomRight;
    config.PreventDuplicates = false;
    config.NewestOnTop = true;
});

//Map
builder.Services.AddBlazorLeafletMaps();

builder.Services.AddScoped<IUnitService, UnitService>();
builder.Services.AddScoped<ILanguageService, LanguageService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ICountryService, CountryService>();
builder.Services.AddScoped<IDayService, DayService>();
builder.Services.AddScoped<IInterestService, InterestService>();
builder.Services.AddScoped<ITourService, TourService>();
builder.Services.AddScoped<ITranslateService, TranslateService>();
builder.Services.AddScoped<IStepService, StepService>();
builder.Services.AddScoped<IBookingService, BookingService>();
builder.Services.AddScoped<ISearchService, SearchService>();
builder.Services.AddScoped<IAvailabilityGuideService, AvailabilityGuideService>();
builder.Services.AddScoped<IUnavailabilityGuideService, UnavailabilityGuideService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
