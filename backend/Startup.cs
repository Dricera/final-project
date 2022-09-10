using backend.Repositories;
using backend.Settings;
using Catalog.Api.Repositories;
using Microsoft.OpenApi.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;

namespace backend
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}
		public IConfiguration Configuration { get; }
		public void ConfigureServices(IServiceCollection services)
		{
			//MongoDB services configuration block
			BsonSerializer.RegisterSerializer(new GuidSerializer(BsonType.String));
			BsonSerializer.RegisterSerializer(new DateTimeOffsetSerializer(BsonType.String));
			var mongoDbSettings = Configuration.GetSection(nameof(MongoDbSettings)).Get<MongoDbSettings>();

			services.AddSingleton<IMongoClient>(serviceProvider =>
			{
				return new MongoClient(mongoDbSettings.ConnectionString);
			});
			// END MongoDB service configuration

			services.AddSpaStaticFiles(options => { options.RootPath = "wwwroot"; });
			services.AddControllers(options =>
			{
				options.SuppressAsyncSuffixInActionNames = false;
			});
			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo
				{
					Version = "v1",
					Title = "Backend API",
					Description = "Initial deployment of api backend",
				});
				c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
				{
					Description = "Standard Authorization header using the Bearer scheme (\"bearer {token}\")",
					In = ParameterLocation.Header,
					Name = "Authorization",
					Type = SecuritySchemeType.ApiKey
				});
			});
			services.AddSingleton<IRepository, InMemItemsRepository>();
			services.AddSingleton<TicketRepository, InMemTicketRepository>();
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				// Enable middleware to serve generated Swagger as a JSON endpoint.

			}
			app.UseSwagger();
			// Enable middleware to serve swagger-ui (static page to test the API)
			app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Backend v1"));

			// app.UseSpaStaticFiles();

			app.UseRouting();
			// app.UseAuthorization();
			app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

			/*             app.UseSpa(spa =>
						{
							spa.Options.SourcePath = "wwwroot";
							if (env.IsDevelopment())
							{
								spa.UseProxyToSpaDevelopmentServer("http://localhost:8080");
							}
						}); */

			/*
			 Currently we are hosting both static files and api in same server.
			 This can be changed by uncommenting above UseSpa function and removing the below mapping.
			 This will proxy the local 5002 server to the index of the main server in case user calls the api url without any api route or parameters.
            */


			app.Map("",
			root =>
			{
				var sfo = new StaticFileOptions()
				{
					OnPrepareResponse = ctx =>
					{
						var resp = ctx.Context.Response;
					}
				};
				root.UseSpaStaticFiles(sfo);
				root.UseSpa(spa =>
				{
					spa.Options.SourcePath = "http://localhost:8080";
					// spa.Options.SourcePath = "wwwroot";
					spa.Options.DefaultPageStaticFileOptions = sfo;
				});
			});

		}
	}
}
