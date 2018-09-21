using System;
using System.IO;
using System.Reflection;
using _2c2pAssessment.Dal;
using _2c2pAssessment.Dal.Contracts;
using _2c2pAssessment.Services;
using _2c2pAssessment.Services.Contracts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace _2c2pAssessment.WebApi
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new Info
				{
					Version = "v1",
					Title = "2c2p assessment",
					Description = "2c2p assessment",
					TermsOfService = "None",
					Contact = new Contact
					{
						Name = "Sergey Yevtushik",
						Email = "syevtushik@gmail.com",
						Url = "www.linkedin.com/in/syevtushik"
					}
				});

				// Set the comments path for the Swagger JSON and UI.
				var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
				var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
				c.IncludeXmlComments(xmlPath);

				c.DescribeAllEnumsAsStrings();
			});
			services.AddTransient<ICardStorage, FakeCardStorage>();
			services.AddTransient<INumberService, NumberService>();
			services.AddTransient<ICardTypeResolver, CardTypeResolver>();
			services.AddTransient<ICardValidator, CardValidator>();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseSwagger();

			app.UseSwaggerUI(c =>
			{
				c.SwaggerEndpoint("/swagger/v1/swagger.json", "2c2p assessment");
				c.RoutePrefix = string.Empty;
			});

			app.UseMvc();
		}
	}
}
