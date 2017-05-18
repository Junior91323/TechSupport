using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using TechSupport.BLL.DTO;
using TechSupport.BLL.Infrastructure;
using TechSupport.BLL.Interfaces;
using TechSupport.BLL.Services;

namespace TechSupport.RequestGenerator
{
    class Program
    {
        static HttpClient client = new HttpClient();
        static void Main(string[] args)
        {
            client.BaseAddress = new Uri("http://localhost:50740/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            //IKernel kernal = new StandardKernel(new ServiceModule("DefaultConnection"));
            //IUnitOfService DB = kernal.Get<UnitOfService>();
            //var p = DB.EmployeeService.GetEmployees().ToList();

             GetProductAsync("api/request").Wait();
        }
        static async Task<Uri> CreateProductAsync(object product)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync("api/products", product);
            response.EnsureSuccessStatusCode();

            // Return the URI of the created resource.
            return response.Headers.Location;
        }
        static async Task<object> GetProductAsync(string path)
        {
            object product = null;
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                product = await response.Content.ReadAsAsync<object>();
            }
            return product;
        }
    }
}
