
public class Product
{
    public string Name { get; set; }
    public double Price { get; set; }
    public int Quantity { get; set; }

    // Конструктор
    public Product(string name, double price, int quantity)
    {
        Name = name;
        Price = price;
        Quantity = quantity;
    }

    // Метод для вывода информации о товаре
    public void PrintInfo()
    {
        Console.WriteLine($"{Name} - Цена: {Price}, Количество: {Quantity}");
    }
}

// Класс Store
public class Store
{
    private List<Product> products;

    // Конструктор
    public Store()
    {
        products = new List<Product>();
    }

    // Метод для добавления нового товара
    public void AddProduct(Product product)
    {
        products.Add(product);
    }

    // Метод для вывода списка всех товаров
    public void PrintProducts()
    {
        for (int i = 0; i < products.Count; i++)
        {
            Console.Write($"{i + 1}. ");
            products[i].PrintInfo();
        }
    }

    // Метод для поиска товара по названию
    public Product FindProduct(string name)
    {
        return products.FirstOrDefault(p => p.Name.ToLower() == name.ToLower());
    }

    // Метод для продажи товара
    public void SellProduct(string name, int quantity)
    {
        Product product = FindProduct(name);
        if (product != null)
        {
            if (product.Quantity >= quantity)
            {
                product.Quantity -= quantity;
                Console.WriteLine($"Товар продан! Осталось {product.Quantity} штук.");
            }
            else
            {
                Console.WriteLine("Недостаточно товара на складе.");
            }
        }
        else
        {
            Console.WriteLine("Товар не найден.");
        }
    }

    // Метод для сохранения данных о товарах в файл
    public void SaveProductsToFile(string filename)
    {
        using (StreamWriter writer = new StreamWriter(filename))
        {
            foreach (Product product in products)
            {
                writer.WriteLine($"{product.Name};{product.Price};{product.Quantity}");
            }
        }
    }

   
    public void LoadProductsFromFile(string filename)
    {
        if (File.Exists(filename))
        {
            using (StreamReader reader = new StreamReader(filename))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] parts = line.Split(';');
                    if (parts.Length == 3)
                    {
                        string name = parts[0];
                        double price = double.Parse(parts[1]);
                        int quantity = int.Parse(parts[2]);
                        AddProduct(new Product(name, price, quantity));
                    }
                }
            }
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        Store store = new Store();
        string filename = "products.txt";
        store.LoadProductsFromFile(filename);

        while (true)
        {
            Console.WriteLine("Добро пожаловать в магазин!");
            Console.WriteLine("1. Добавить товар");
            Console.WriteLine("2. Просмотреть список товаров");
            Console.WriteLine("3. Найти товар");
            Console.WriteLine("4. Продать товар");
            Console.WriteLine("5. Выйти");
            Console.Write("Введите номер команды: ");
            Console.WriteLine();
            int command;
            if (int.TryParse(Console.ReadLine(), out command))
            {
                switch (command)
                {
                    case 1:
                        AddProduct(store);
                        break;
                    case 2:
                        store.PrintProducts();
                        break;
                    case 3:
                        FindProduct(store);
                        break;
                    case 4:
                        SellProduct(store);
                        break;
                    case 5:
                        store.SaveProductsToFile(filename);
                        return;
                    default:
                        Console.WriteLine("Недопустимая команда.");
                        Console.WriteLine();
                        break;
                }
            }
            else
            {
                Console.WriteLine("Недопустимая команда.");
                Console.WriteLine();
            }
        }
    }

    static void AddProduct(Store store)
    {
        Console.Write("Введите название товара: ");
        string name = Console.ReadLine();
        Console.Write("Введите цену товара: ");
        double price;
        while (!double.TryParse(Console.ReadLine(), out price) || price <= 0)
        {
            Console.Write("Недопустимая цена. Введите цену товара: ");
        }
        Console.Write("Введите количество товара: ");
        int quantity;
        while (!int.TryParse(Console.ReadLine(), out quantity) || quantity < 0)
        {
        }
        Product product = new Product(name, price, quantity);
        store.AddProduct(product);
        Console.WriteLine("Товар успешно добавлен!");
        Console.WriteLine();
    }

    static void FindProduct(Store store)
    {
        Console.Write("Введите название товара для поиска: ");
        string name = Console.ReadLine();
        Product product = store.FindProduct(name);
        if (product != null)
        {
            product.PrintInfo();
        }
        else
        {
            Console.WriteLine("Товар не найден.");
            Console.WriteLine();
        }
    }

    static void SellProduct(Store store)
    {
        Console.Write("Введите название товара для продажи: ");
        string name = Console.ReadLine();
        Console.Write("Введите количество для продажи: ");
        int quantity;
        while (!int.TryParse(Console.ReadLine(), out quantity) || quantity <= 0)
        {
            Console.Write("Недопустимое количество. Введите количество для продажи: ");
            Console.WriteLine();
        }
        store.SellProduct(name, quantity);
    }
}

