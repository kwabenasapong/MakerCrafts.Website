<<<<<<< HEAD
﻿using System.Buffers;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using Bond.IO.Unsafe;
=======
﻿using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
>>>>>>> 8e74042d426e266322783c7ffff6733aa269a6e9
using MakerCrafts.Website.Models;
using Microsoft.AspNetCore.Hosting;
using NPOI.Util;

namespace MakerCrafts.WebSite.Services
{
    public class JsonFileProductService
    {
        public JsonFileProductService(IWebHostEnvironment webHostEnvironment)
        {
            WebHostEnvironment = webHostEnvironment;
        }

        public IWebHostEnvironment WebHostEnvironment { get; }

        private string JsonFileName
        {
            get { return Path.Combine(WebHostEnvironment.WebRootPath, "data", "products.json"); }
        }

        public IEnumerable<Product> GetProducts()
        {
            using (var jsonFileReader = File.OpenText(JsonFileName))
            {
                return JsonSerializer.Deserialize<Product[]>(jsonFileReader.ReadToEnd(),
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
            }
        }

        public void AddRatings(string productId, int rating)
        {
            var products = GetProducts();
            var query = products.First(x => x.Id == productId);
            if (query.Ratings != null) 
            {
                query.Ratings = new int[] { rating };
            }
            else
            {
                var ratings = query.Ratings.ToList();
                ratings.Add(rating);
                query.Ratings = ratings.ToArray();
            }
            using (var outputstream = File.OpenText(JsonFileName))
            {
                JsonSerializer.Serialize<IEnumerable<Product>>(
                    new Utf8JsonWriter((IBufferWriter<byte>)outputstream, new JsonWriterOptions
                    {
                        SkipValidation = true,
                        Indented = true,
                    }),
                    products
                );
            }
        }
    }
}