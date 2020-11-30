using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Google.Cloud.Firestore;
using blackstore_firebase_api.Entity;
using System.Linq;

namespace blackstore_firebase_api.Configuration
{
    public class FirebaseConfiguration
    {
        private readonly static FirebaseConfiguration _instance = new FirebaseConfiguration();
        private FirestoreDb _db;

        public FirebaseConfiguration()
        {
            String path = AppDomain.CurrentDomain.BaseDirectory + @"blackstore-d57ff-firebase-adminsdk-gh8e1-0d65bd3b5d.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);
            _db = FirestoreDb.Create("blackstore-d57ff");

        }

        public static FirebaseConfiguration Instance
        {
            get
            {
                return _instance;
            }
        }

        public async Task<Product> AddProduct(Product product)
        {
            DocumentReference docRef = _db.Collection("products").Document(product.id);
            await docRef.SetAsync(product);

            return product;
        }

        public async Task<Product> GetProductById(string id)
        {
            DocumentReference docRef = _db.Collection("products").Document(id);
            DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();
            Product product = new Product();
            if (snapshot.Exists)
            {
                product = snapshot.ConvertTo<Product>();
            }

            return product;
        }
        public async Task<Result> GetProductWhereName(string name)
        {
            Query capitalQuery = _db.Collection("products").WhereEqualTo("name", name);
            QuerySnapshot capitalQuerySnapshot = await capitalQuery.GetSnapshotAsync();
            List<Item> items = new List<Item>();


            Seller seller = new Seller();
            seller.name = "BlackStore";
            seller.id = "BST13579ALV";
            seller.logo = "";
            Result result = new Result();
            result.query = name;
            result.total = capitalQuerySnapshot.Count;
            result.seller = seller;

                foreach (DocumentSnapshot documentSnapshot in capitalQuerySnapshot.Documents)
            {
                Dictionary<String, Object> prod = documentSnapshot.ToDictionary();
                Item item = new Item();

                foreach (var product in prod)
                {

                    if (product.Key == "city")
                    {
                        City _city = new City();
                        Dictionary<string, object> cities = (Dictionary<string, object>)product.Value;
                        foreach (var city in cities)
                        {
                            if (city.Key == "name")
                            {
                                _city.name = (string)city.Value;
                            }
                            if (city.Key == "code")
                            {
                                _city.code = (string)city.Value;
                            }
                        }
                        item.city = _city;
                    }
                    else if (product.Key == "currency")
                    {
                        item.currency = (string)product.Value;
                    }
                    else if (product.Key == "id")
                    {
                        item.id = (string)product.Value;
                    }
                    else if (product.Key == "name")
                    {
                        item.name = (string)product.Value;
                    }
                    else if (product.Key == "price")
                    {
                        item.price = (int)(long)product.Value;
                    }
                    else if (product.Key == "rating")
                    {
                        item.rating = (int)(long)product.Value;
                    }
                    else if (product.Key == "thumbnail")
                    {
                        item.thumbnail = (string)product.Value;
                    }
                    else if (product.Key == "brand")
                    {
                        item.brand = (string)product.Value;
                    }
                }
                items.Add(item);
                result.items = items;
            }
            return result;
        }

        public async Task<List<Product>> GetAllProducts()
        {
            Query allProductsQuery = _db.Collection("products");
            QuerySnapshot allProductsQuerySnapshot = await allProductsQuery.GetSnapshotAsync();
            List<Product> Listproducts = new List<Product>();

            foreach (DocumentSnapshot documentSnapshot in allProductsQuerySnapshot.Documents)
            {
                Dictionary<String, Object> prod = documentSnapshot.ToDictionary();
                Product item = new Product();
                foreach (var product in prod)
                {
                    if (product.Key == "city")
                    {
                        City _city = new City();
                        Dictionary<string, object> cities = (Dictionary<string, object>)product.Value;
                        foreach (var city in cities)
                        {
                            if (city.Key == "name") 
                            {
                                _city.name = (string)city.Value;
                            }
                            if (city.Key == "code")
                            {
                                _city.code = (string)city.Value;
                            }
                        }
                        item.city = _city;
                    }
                    else if (product.Key == "currency")
                    {
                        item.currency = (string)product.Value;
                    }
                    else if (product.Key == "id")
                    {
                        item.id = (string)product.Value;
                    }
                    else if (product.Key == "name")
                    {
                        item.name = (string)product.Value;
                    }
                    else if (product.Key == "price")
                    {
                        item.price = (int)(long)product.Value;
                    }
                    else if (product.Key == "rating")
                    {
                        item.rating = (int)(long)product.Value;
                    }
                    else if (product.Key == "thumbnail")
                    {
                        item.thumbnail = (string)product.Value;
                    }
                    else if (product.Key == "pictures")
                    {
                        List<Object> lista = (List<Object>)product.Value;
                        item.pictures = lista.Select(s => (string)s).ToList();

                    }
                    else if (product.Key == "seller")
                    {
                        Seller _seller = new Seller();
                        Dictionary<string, object> sellers = (Dictionary<string, object>)product.Value;
                        foreach (var seller in sellers)
                        {
                            if (seller.Key == "name")
                            {
                                _seller.name = (string)seller.Value;
                            }
                            if (seller.Key == "logo")
                            {
                                _seller.logo = (string)seller.Value;
                            }
                            if (seller.Key == "id")
                            {
                                _seller.id = (string)seller.Value;
                            }

                        }
                        item.seller = _seller;

                    }
                    else if (product.Key == "brand")
                    {
                        item.brand = (string)product.Value;
                    }
                    else if (product.Key == "description")
                    {
                        item.description = (string)product.Value;
                    }
                }
                Listproducts.Add(item);
                
            }

            return Listproducts;
        }

        public async Task<String> DeleteProductById(String id)
        {
            DocumentReference cityRef = _db.Collection("products").Document(id);
            await cityRef.DeleteAsync();
            return "Usuario con Id: " + id + " eliminado correctamente";
        }
    }
}
