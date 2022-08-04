using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Ordering;
using Ordering.Repositories;
using Ordering.Services;
using Ordering.Commands;
using Ordering.Queries;

namespace Stroopwafels
{
	public class Startup
	{
		public IConfiguration Configuration { get; }

		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public void ConfigureServices(IServiceCollection services)
		{
			var validSuppliers = new ISupplier[]
			{
				new SupplierA(),
				new SupplierB(),
				new SupplierC()
			};

			services.AddControllersWithViews();
			services.AddSingleton<IHttpClientWrapper, HttpClientWrapper>();

			services
				.AddOrderingInfrastructure(
					Configuration.GetSection("Stroopwafels:supplierA"),
					Configuration.GetSection("Stroopwafels:supplierB"),
					Configuration.GetSection("Stroopwafels:supplierC"))
				.AddOrderingRepositories(validSuppliers)
				.AddOrderingCommands()
				.AddOrderingQueries();
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseHsts();
			}
			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllerRoute(
					name: "default",
					pattern: $"{{controller={nameof(Stroopwafel)}}}/{{action=Index}}/{{id?}}");
			});
		}
	}
}
