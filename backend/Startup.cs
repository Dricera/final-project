using backend.Repositories;
using backend.Settings;
using Microsoft.OpenApi.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using Newtonsoft.Json.Linq;

namespace backend
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}
		public IConfiguration Configuration { get; }
		private static string projectConfigJson = File.ReadAllText("../project.json");
		static JObject projectConfig = JObject.Parse(projectConfigJson);
		private string serverTitle=projectConfig["serverTitle"].ToString();
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddCors(options =>
			{
				options.AddDefaultPolicy(
						policy =>
						{
							policy.WithOrigins("http://localhost:9000",
												"https://localhost:9001")
												.AllowAnyHeader()
												.AllowAnyMethod();
						});
			});
			// services.AddResponseCaching();


			//MongoDB services configuration block
			BsonSerializer.RegisterSerializer(new GuidSerializer(BsonType.String));
			BsonSerializer.RegisterSerializer(new DateTimeOffsetSerializer(BsonType.String));
			var mongoDbSettings = Configuration.GetSection(nameof(MongoDbSettings)).Get<MongoDbSettings>();
			services.AddSingleton<IMongoClient>(serviceProvider =>
			{
				return new MongoClient(Environment.GetEnvironmentVariable("MONGODB_CONNECTION_STRING"));
				// return new MongoClient(mongoDbSettings.ConnectionString);
	
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
					Title = serverTitle+" API",
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
			// services.AddSingleton<IRepository, InMemUserRepository>(); DELETED FOR NOW
			services.AddSingleton<TicketRepository, MongoDbTicketRepository>();
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

			app.UseCors();
			
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
