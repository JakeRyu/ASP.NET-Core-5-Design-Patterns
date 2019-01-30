﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace OperationResult
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRouting();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouter(builder =>
            {
                builder.MapGet("/simplest-form", SimplestFormHandler);
                builder.MapGet("/single-error", SingleErrorHandler);
                builder.MapGet("/single-error-with-value", SingleErrorWithValueHandler);
            });
        }

        private async Task SimplestFormHandler(HttpRequest request, HttpResponse response, RouteData data)
        {
            // Create an instance of the class that contains the operation
            var executor = new SimplestForm.Executor();

            // Execute the operation and handle its result
            var result = executor.Operation();
            if (result.Succeeded)
            {
                // Handle the success
                await response.WriteAsync("Operation succeeded");
            }
            else
            {
                // Handle the failure
                await response.WriteAsync("Operation failed");
            }
        }

        private async Task SingleErrorHandler(HttpRequest request, HttpResponse response, RouteData data)
        {
            // Create an instance of the class that contains the operation
            var executor = new SingleError.Executor();

            // Execute the operation and handle its result
            var result = executor.Operation();
            if (result.Succeeded)
            {
                // Handle the success
                await response.WriteAsync("Operation succeeded");
            }
            else
            {
                // Handle the failure
                await response.WriteAsync(result.ErrorMessage);
            }
        }

        private async Task SingleErrorWithValueHandler(HttpRequest request, HttpResponse response, RouteData data)
        {
            // Create an instance of the class that contains the operation
            var executor = new SingleErrorWithValue.Executor();

            // Execute the operation and handle its result
            var result = executor.Operation();
            if (result.Succeeded)
            {
                // Handle the success
                await response.WriteAsync($"Operation succeeded with a value of '{result.Value}'.");
            }
            else
            {
                // Handle the failure
                await response.WriteAsync(result.ErrorMessage);
            }
        }
    }
}
