
using Domain.Models;
using Infrastructure.Services;

CategorieServices categorieServices = new CategorieServices();
CustomerServices customerServices = new CustomerServices();
OrderServices orderServices = new OrderServices();
ProducteServices producteServices = new ProducteServices();
SellerServices sellerServices = new SellerServices();
while (true)
{
    Console.WriteLine("""
                      ========== SHOPNET MARKETPLACE ==========
                      1.  Иловаи мизоҷ
                      2.  Иловаи фурӯшанда
                      3.  Иловаи категория
                      4.  Иловаи маҳсулот
                      5.  Эҷоди фармоиш
                      6.  Иловаи маҳсулот ба фармоиш
                      7.  Намоиши ҳамаи мизоҷон
                      8.  Намоиши ҳамаи фурӯшандаҳо
                      9.  Намоиши ҳамаи категорияҳо
                      10. Намоиши ҳамаи маҳсулот
                      11. Ҷустуҷӯи маҳсулот
                      12. Намоиши маҳсулот аз рӯи категория
                      13. Намоиши маҳсулот аз рӯи фурӯшанда
                      14. Маҳсулоти кам
                      15. Маҳсулоти бештар фурӯхташуда
                      16. Тағйири маълумоти мизоҷ
                      17. Тағйири маълумоти фурӯшанда
                      18. Тағйири маълумоти категория
                      19. Тағйири маълумоти маҳсулот
                      20. Намоиши фармоишҳои мизоҷ
                      21. Тафсилоти фармоиш
                      22. Ҳазфи мизоҷ
                      23. Ҳазфи фурӯшанда
                      24. Ҳазфи категория
                      25. Ҳазфи маҳсулот
                      26. Ҳазфи фармоиш
                      27. Хуруҷ
                      """);
    var choice = int.Parse(Console.ReadLine());
    switch (choice)
    {
        case 1:
            Console.Write("Nom: ");
            string firstName = Console.ReadLine();
            Console.Write("nasab: ");
            string lastName = Console.ReadLine();
            Console.Write("Email: ");
            string email = Console.ReadLine();
            Console.Write("Phone: ");
            string phone = Console.ReadLine();
            Customer customer = new Customer()
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                Phone = phone
            };
            customerServices.AddCustomer(customer);
            break;
        case 2:
            Console.Write("Nomi seller : ");
            string sellerfirstName= Console.ReadLine();
            Console.Write("Nasab: ");
            string sellerlastName = Console.ReadLine();
            Console.Write("ShopNAme: ");
            string sellershopName = Console.ReadLine();
            Console.Write("Email: ");
            string selleremail = Console.ReadLine();
            Seller seller = new Seller()
            {
                FirstName = sellerfirstName,
                LastName = sellerlastName,
                ShopName = sellerfirstName,
                Email = selleremail
            };
            sellerServices.AddSeller(seller);
            break;
        case 3:
            Console.Write("Nomi kategorya: ");
            string category = Console.ReadLine();
            Console.Write("Description: ");
            string description = Console.ReadLine();
            Categorie categorie = new Categorie()
            {
                Name = category,
                Description = description
            };
            categorieServices.AddCategories(categorie);
            break;
        case 4:
            try
            {
                Console.Write("Nomi mahsulot: ");
                string name = Console.ReadLine();
                Console.Write("Narkh: ");
                decimal price = decimal.Parse(Console.ReadLine());
                Console.Write("Miqdor: ");
                int quantity = int.Parse(Console.ReadLine());
                Console.Write("Id kategorya: ");
                int categoryId = int.Parse(Console.ReadLine());
                Console.Write("Id furushanda: ");
                int sellerId = int.Parse(Console.ReadLine());
                Product product = new Product()
                {
                    Name = name,
                    Price = price,
                    Quantity = quantity,
                    CategoryId = categoryId,
                    SellerId = sellerId
                };
                producteServices.AddProduct(product);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            break;
        case 5:
            break;
        case 6:
            Console.WriteLine("Id farmoishro vorid kuned: ");
            int ordrID = int.Parse(Console.ReadLine());
            Console.WriteLine("ID mahsulotro vorid kuned: ");
            int productId= int.Parse(Console.ReadLine());
            Console.WriteLine("Miqdori mahsulotro vorid kuned: ");
            int qty = int.Parse(Console.ReadLine());
            orderServices.AddProductToOrder(ordrID, productId, qty);
            break;
        case 7:
            var customers = customerServices.GetAllCustomers();
            foreach (var c in customers)
            {
                Console.WriteLine("ID: "+ c.Id+" Firstname: "+c.FirstName+" Lastname: "+c.LastName+" Email: "+c.Email+" Phone: "+c.Phone);
            }
            break;
        case 8:
            var sellers = sellerServices.GetAllSellers();
            foreach (var s in sellers)
            {
                Console.WriteLine("Id: "+s.Id+" FirstNAme: "+s.FirstName+" LastNAme: "+s.LastName+" Email: "+s.Email);
            }
            break;
        case 9:
            var categories = categorieServices.GetAllCategories();
            foreach (var c in categories)
            {
                Console.WriteLine("Id: " + c.Id + " Name: " + c.Name + " description: " + c.Description);
            }
            break;
        case 10:
            var products=producteServices.GetAllProducts();
            foreach (var p in products)
            {
                Console.WriteLine("ID: "+p.Id+" Name: "+p.Name+" Price: "+p.Price+" Quantity: "+p.Quantity);
            }
            break;
        case 11:
            Console.Write("Nomi mahsulotro vorid kuned: ");
            string productname = Console.ReadLine();
            var foundproduct = producteServices.SearchProductByName(productname);
            foreach (var p in foundproduct)
            {
                Console.WriteLine("ID: "+p.Id+" Name: " + p.Name+" Price: "+p.Price);
            }
            break;
        case 12:
            Console.WriteLine("ID kategoriyaro vorid kuned: ");
            int categoryIdp = int.Parse(Console.ReadLine());
            var findCategory = producteServices.GetProductsByCategory(categoryIdp);
            foreach (var p in findCategory)
            {
                Console.Write("ID: "+p.Id+" Name: " + p.Name+" Price: "+p.Price);
            }
            break;
        case 13:
            Console.Write("ID furushandaro vorid kuned: ");
            int SellerIDp = int.Parse(Console.ReadLine());
            var foundProductBySeller = producteServices.GetProductsBySeller(SellerIDp);
            foreach (var p in foundProductBySeller)
            {
                Console.WriteLine("ID"+p.Id+" Name: "+p.Name+" Price: "+p.Price);
            }
            break;
        case 14:
            break;
        case 15:
            break;
        case 16:
            Console.Write("Id mizojro baroi update kardan vorid kuned: ");
            int updId = int.Parse(Console.ReadLine());
            Console.Write("nomi navi mizojro vorid kuned: ");
            string newName = Console.ReadLine();
            Console.Write("Nasabi navro vorid kuned: ");
            string newlastName = Console.ReadLine();
            Console.Write("Emaili navro voridkuned: ");
            string newemail = Console.ReadLine();
            Console.Write("Phone navro voridkuned: ");
            string newphone = Console.ReadLine();
            Customer updcustomer = new Customer()
            {
                FirstName = newName,
                LastName = newlastName,
                Email = newemail,
                Phone = newphone
            };
            customerServices.UpdateCustomer(updId, updcustomer);
            break;
        case 17:
            Console.WriteLine("ID sellerro baroi update kardan vorid kuned: ");
            int sellerIdp = int.Parse(Console.ReadLine());
            Console.Write("Nomi selleri nav: ");
            string selName = Console.ReadLine();
            Console.Write("Nasabi selleri nav: ");
            string sellastName = Console.ReadLine();
            Console.Write("Shopname: ");
            string shopName = Console.ReadLine();
            Console.Write("Email: ");
            string newemailsellr = Console.ReadLine();
            Seller updateseller = new Seller()
            {
                FirstName = selName,
                LastName = sellastName,
                ShopName = shopName,
                Email = newemailsellr
            };
            sellerServices.UpdateSeller(sellerIdp, updateseller);
            break;
        case 18:
            Console.WriteLine("Id kategorya baroi update kardan: ");
            int newid = int.Parse(Console.ReadLine());
            Console.Write("Nomi nav:");
            string newname = Console.ReadLine();
            Console.Write("descriptioni nav: ");
            string newdescription = Console.ReadLine();
            Categorie update = new Categorie()
            {
                Name = newname,
                Description = newdescription
            };
            categorieServices.UpdateCategories(newid, update);
            break;
        case 19:
            Console.Write("ID mahsulotro baroi update kardan: ");
            int updateIDpr = int.Parse(Console.ReadLine());
            Console.Write("Nomi nav:");
            string newnamepr = Console.ReadLine();
            Console.Write("new Price:  ");
            decimal newpricepr = decimal.Parse(Console.ReadLine());
            Console.Write("new Quantity: ");
            int newqtypr = int.Parse(Console.ReadLine());
            Console.WriteLine("new id kategorya:");
            int newcId = int.Parse(Console.ReadLine());
            Console.WriteLine("NewSellerId: ");
            int newsellerId = int.Parse(Console.ReadLine());
            Product newProduct = new Product()
            {
                Name = newnamepr,
                Quantity = newqtypr,
                Price = newpricepr,
                CategoryId = newcId,
                SellerId = newsellerId
            };
            producteServices.UpdateProduct(updateIDpr,newProduct);
            break;
        case 20:
            break;
        case 21:
            break;
        case 22:
            Console.Write("Id mizojro barou nest kardan vorid kuned:");
            int id=int.Parse(Console.ReadLine());
            customerServices.DeleteCustomer(id);
            break;
        case 23:
            break;
        case 24:
            Console.Write("Id kategorya baroi nest kardan vorid kuned:");
            int deleteid=int.Parse(Console.ReadLine());
            customerServices.DeleteCustomer(deleteid);
            break;
        case 25:
            Console.Write("Id mahsulotro baroi nest kardan vorid kuned:");
            int deleteidproduct=int.Parse(Console.ReadLine());
            producteServices.DeleteProduct(deleteidproduct);
            break;
        case 26:
            Console.Write("Id farmoishro baroi nest kardan vorid kuned: ");
            int delId=int.Parse(Console.ReadLine());
            customerServices.DeleteCustomer(delId);
            break;
        case 27:
            return;
            break;
    }
}
