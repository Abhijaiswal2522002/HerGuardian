using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// ---------------- SERVICES ----------------

// Razor Pages
builder.Services.AddRazorPages();


// DB Context (EF Core)
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    )
);

// ---------------- AUTH (we will use later) ----------------
// (kept ready for next step)
builder.Services.AddScoped<AuthService>();
builder.Services.AddAuthentication();
builder.Services.AddAuthorization();

// ---------------- APP BUILD ----------------
var app = builder.Build();

// ---------------- PIPELINE ----------------
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();


app.UseAuthentication();   // login system
app.UseAuthorization();   

app.MapRazorPages();

app.Run();