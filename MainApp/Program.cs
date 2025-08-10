
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
                      0. Хуруҷ
                      """);
    var choice = int.Parse(Console.ReadLine());
    switch (choice)
    {
        case 1:
            try
            {
                Console.Write("Номи муштариро ворид кунед: ");
                string firstName = Console.ReadLine();
                Console.Write("Насаби муштариро ворид кунед: ");
                string lastName = Console.ReadLine();
                Console.Write("Email-и муштариро ворид кунед: ");
                string email = Console.ReadLine();
                Console.Write("Рақами телфони муштариро ворид кунед: ");
                string phone = Console.ReadLine();
                Customer customer = new Customer()
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email,
                    Phone = phone
                };
                customerServices.AddCustomer(customer);
            }
            catch (Exception e)
            {
                Console.WriteLine("Хатоги:"+e.Message);
            }
            break;
        case 2:
            try
            {
                Console.Write("Номи фурушандаро ворид кунед: ");
                string sellerfirstName= Console.ReadLine();
                Console.Write("Насаби фурушандаро ворид кунед: ");
                string sellerlastName = Console.ReadLine();
                Console.Write("Номи мағозаро ворид куне: ");
                string sellershopName = Console.ReadLine();
                Console.Write("Email-фурушандаро ворид кунед: ");
                string selleremail = Console.ReadLine();
                Seller seller = new Seller()
                {
                    FirstName = sellerfirstName,
                    LastName = sellerlastName,
                    ShopName = sellerfirstName,
                    Email = selleremail
                };
                sellerServices.AddSeller(seller);
            }
            catch (Exception e)
            {
                Console.WriteLine("Хатоги: "+e.Message);
            }
            break;
        case 3:
            try
            {
                Console.Write("Номи категорияро ворид кунед: ");
                string category = Console.ReadLine();
                Console.Write("Тавсифро ворид кунед: ");
                string description = Console.ReadLine();
                Categorie categorie = new Categorie()
                {
                    Name = category,
                    Description = description
                };
                categorieServices.AddCategories(categorie);
            }
            catch (Exception e)
            {
                Console.WriteLine("Хатоги:"+e.Message);
            }
            break;
        case 4:
            try
            {
                Console.Write("Номи маҳсулотро ворид кунед: ");
                string name = Console.ReadLine();
                Console.Write("Нархи маҳсулотроворид кунед: ");
                decimal price = decimal.Parse(Console.ReadLine());
                Console.Write("Миқдори маҳсулотро ворид кунед: ");
                int quantity = int.Parse(Console.ReadLine());
                Console.Write("Id категорияро ворид кунед: ");
                int categoryId = int.Parse(Console.ReadLine());
                Console.Write("Id фурушандаро ворид кунед: ");
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
            try
            {
                Console.WriteLine("Id фармоишро ворид куед: ");
                int ordrID = int.Parse(Console.ReadLine());
                Console.WriteLine("ID маҳсулотро ворид кунед: ");
                int productId= int.Parse(Console.ReadLine());
                Console.WriteLine("Миқдори маҳсулотро ворид кунед: ");
                int qty = int.Parse(Console.ReadLine());
                orderServices.AddProductToOrder(ordrID, productId, qty);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                goto case 6;
            }
            break;
        case 7:
                var customers = customerServices.GetAllCustomers();
                foreach (var c in customers)
                {
                    Console.WriteLine("ID: "+ c.Id+" || Firstname: "+c.FirstName+" || Lastname: "+c.LastName+" || Email: "+c.Email+" || Phone: "+c.Phone);
                }
            break;
        case 8:
                var sellers = sellerServices.GetAllSellers();
                foreach (var s in sellers)
                {
                    Console.WriteLine("Id: "+s.Id+" || FirstNAme: "+s.FirstName+" || LastNAme: "+s.LastName+" || Email: "+s.Email);
                }
            break;
        case 9:
                var categories = categorieServices.GetAllCategories();
                foreach (var c in categories)
                {
                    Console.WriteLine("Id: " + c.Id + " || Name: " + c.Name + " || Description: " + c.Description);
                }
            break;
        case 10:
                var products=producteServices.GetAllProducts();
                foreach (var p in products)
                {
                    Console.WriteLine("ID: "+p.Id+" || Name: "+p.Name+" || Price: "+p.Price+" || Quantity: "+p.Quantity);
                }
            break;
        case 11:
                Console.Write("Номи маҳсулотро ворид кунед: ");
                string productname = Console.ReadLine();
                var foundproduct = producteServices.SearchProductByName(productname);
                foreach (var p in foundproduct)
                {
                    Console.WriteLine("ID: "+p.Id+" ||Name: " + p.Name+" ||Price: "+p.Price);
                }
            break;
        case 12:
            Console.WriteLine("ID категорияро ворид кунед: ");
            int categoryIdp = int.Parse(Console.ReadLine());
            var findCategory = producteServices.GetProductsByCategory(categoryIdp);
            foreach (var p in findCategory)
            {
                Console.Write("ID: "+p.Id+" Name: " + p.Name+" Price: "+p.Price);
            }
            break;
        case 13:
            Console.Write("ID фурушандаро ворид кунед: ");
            int SellerIDp = int.Parse(Console.ReadLine());
            var foundProductBySeller = producteServices.GetProductsBySeller(SellerIDp);
            foreach (var p in foundProductBySeller)
            {
                Console.WriteLine("ID: "+p.Id+" || Name: "+p.Name+" || Price: "+p.Price);
            }
            break;
        case 14:
            break;
        case 15:
            break;
        case 16:
            Console.Write("Id муштариро барои навсози кардан ворид кунед: ");
            int updId = int.Parse(Console.ReadLine());
            Console.Write("Номро ворид кунед: ");
            string newName = Console.ReadLine();
            Console.Write("Насабро ворид кунед: ");
            string newlastName = Console.ReadLine();
            Console.Write("Email-ро орид кунед: ");
            string newemail = Console.ReadLine();
            Console.Write("Рақами телфони муштариро ворид кунед: ");
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
            Console.WriteLine("ID фурушандаро барои навсози кардан ворид кунед: ");
            int sellerIdp = int.Parse(Console.ReadLine());
            Console.Write("Номро ворид кунед: ");
            string selName = Console.ReadLine();
            Console.Write("Насабро ворид кунед: ");
            string sellastName = Console.ReadLine();
            Console.Write("Номи мағозаро ворид кунед: ");
            string shopName = Console.ReadLine();
            Console.Write("Email-ро ворид кунед: ");
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
            Console.WriteLine("Id категорияро барои навсози кардан ворид кунед: ");
            int newid = int.Parse(Console.ReadLine());
            Console.Write("Номи категорияро ворид кунед:");
            string newname = Console.ReadLine();
            Console.Write("Тавсифро ворид кунед: ");
            string newdescription = Console.ReadLine();
            Categorie update = new Categorie()
            {
                Name = newname,
                Description = newdescription
            };
            categorieServices.UpdateCategories(newid, update);
            break;
        case 19:
            Console.Write("ID маҳсулотро барои навсози кардан ворид кунед: ");
            int updateIDpr = int.Parse(Console.ReadLine());
            Console.Write("Номи маҳсулотро ворид кунед:");
            string newnamepr = Console.ReadLine();
            Console.Write("Нархи маҳсулотро ворид кунед:  ");
            decimal newpricepr = decimal.Parse(Console.ReadLine());
            Console.Write("Миқдори маҳсулотро ворид кунед: ");
            int newqtypr = int.Parse(Console.ReadLine());
            Console.WriteLine("ID категорияро ворид кунед:");
            int newcId = int.Parse(Console.ReadLine());
            Console.WriteLine("ID фурушандаро ворид кунед: ");
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
            Console.Write("Id муштариро барои ҳазф кардан ворид кунед:");
            int id=int.Parse(Console.ReadLine());
            customerServices.DeleteCustomer(id);
            break;
        case 23:
            break;
        case 24:
            Console.Write("Id категорияро барои ҳазф кардан ворид кунед:");
            int deleteid=int.Parse(Console.ReadLine());
            customerServices.DeleteCustomer(deleteid);
            break;
        case 25:
            Console.Write("Id маҳсулотро ворид кунед:");
            int deleteidproduct=int.Parse(Console.ReadLine());
            producteServices.DeleteProduct(deleteidproduct);
            break;
        case 26:
            Console.Write("Id фармоишро барои ҳазф кардан ворид кунед: ");
            int delId=int.Parse(Console.ReadLine());
            orderServices.DeleteOrder(delId);
            break;
        case 0:
            return;
            break;
    }
}
