using System;
using System.Net.Http;
using System.Net.Http.Json;


namespace DemoApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7160");
                PostStudent(client).Wait();
                GetALlStudents(client).Wait();
             
            }
        }

        private static async Task PostStudent(HttpClient client)
        {
            HttpResponseMessage httpResponseMessage =  await client.PostAsJsonAsync<Student>("Student", new Student { Id = 1, Name = "XYZ" });
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                string errorMsg = await httpResponseMessage.Content.ReadAsStringAsync();

                Console.WriteLine(errorMsg);
            }
        }

        private static async Task GetALlStudents(HttpClient client)
        {
            HttpResponseMessage httpResponseMessage = await client.GetAsync("student");
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                List<Student>? students = await httpResponseMessage.Content.ReadFromJsonAsync<List<Student>>();
                foreach(var item in students)
                {
                    Console.WriteLine(item.Name);
                }
            }
        }

    }
}
