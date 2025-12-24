    using Microsoft.EntityFrameworkCore;
    using System;
    using easy_trip.Models.Siniflar;
    using Microsoft.AspNetCore.Authentication.Cookies; // <-- kendi namespace’in

    var builder = WebApplication.CreateBuilder(args);

    builder.Services.AddSession(options => {
        options.IdleTimeout = TimeSpan.FromMinutes(30); // Oturum süresi
        options.Cookie.HttpOnly = true;
        options.Cookie.IsEssential = true;
    });

    AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    // DbContext’i en üste koymalýsýn
    builder.Services.AddDbContext<Context>(options =>
        options.UseNpgsql(builder.Configuration.GetConnectionString("PostgresConnection")));

    builder.Services.AddControllersWithViews();

    builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
        .AddCookie(options =>
        {
            options.LoginPath = "/GirisYap/Login/"; // Giriþ sayfasý yolu
            options.LogoutPath = "/Login/Logout"; // Çýkýþ sayfasý yolu
          //  options.ExpireTimeSpan = TimeSpan.FromMinutes(60); // Çerez süresi
        });

    var app = builder.Build();

    using (var scope = app.Services.CreateScope())//ileride silinecek
    {
        var db = scope.ServiceProvider.GetRequiredService<Context>();
        db.Database.Migrate(); 
    }

    // Configure the HTTP request pipeline.
    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Home/Error");
        app.UseHsts();
    }

    app.UseHttpsRedirection();
    app.UseStaticFiles();
    app.UseRouting();


    app.UseSession();

    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Blog}/{action=Index}/{id?}");

    app.Run();
