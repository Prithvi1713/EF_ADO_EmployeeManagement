
using CollegeRecord.DataContext;
using EF_EmployeeRecordMgt.IRepository;
using EF_EmployeeRecordMgt.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AppDbContext>(options => 
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddControllersWithViews().AddJsonOptions(

    options => options.JsonSerializerOptions.PropertyNamingPolicy = null);


builder.Services.AddScoped<IDepartmentMaster, DepartmentMasterRepository>();
builder.Services.AddScoped<IDesignationMaster, DesignationMasterRepository>();
builder.Services.AddScoped<IEmployeeMaster, EmployeeMasterRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=EmployeeMaster}/{action=Index}/{id?}");

app.Run();
