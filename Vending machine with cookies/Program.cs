using System;

namespace Vending_machine_with_cookies {
    class Program  {

        struct item {
            public string name;
            public int cost;
            public int number;
        }

        static item[] items;

        static int[] money_automat = new int[4];

        static int[] coins = { 10, 5, 2, 1 };

        static int balance = 0;

        static void Init() {
            items = new item[3];

            items[0].name = "Кексы";
            items[0].cost = 50;
            items[0].number = 4;

            items[1].name = "Печенье";
            items[1].cost = 10;
            items[1].number = 3;

            items[2].name = "Вафли";
            items[2].cost = 30;
            items[2].number = 10;

            //money_automat = ...
        }

        static void ShowMenu() {
            Console.WriteLine("В продаже имеются:");
            for (int i = 0; i <= 2; i++ )
                Console.WriteLine("{0}) {1} {2} р.", i+1, items[i].name, items[i].cost);
        }

        static void Selecting() {
            int item = -1;
            int count;
            Console.WriteLine("Выберите пункт меню и количество товара, чтобы получить сдачу, нажмите 0");
            while (true) {
                string[] str = Console.ReadLine().Split(new[] { ' ' }, StringSplitOptions.None);
                item = Convert.ToInt32(str[0]);
                if (item == 0)
                    break;
                count = Convert.ToInt32(str[1]);
                if (item > items.Length)
                    Console.WriteLine("Такого пункта меню нет");
                else if (items[item - 1].number >= count) {
                    int cost = items[item - 1].cost * count;
                    if (cost <= balance) {
                        balance -= cost;
                        items[item - 1].number -= count;
                        Console.WriteLine("*автомат выдает товар*");
                    } 
                    else {
                        Console.WriteLine("Не хватает {0} рублей, добавьте еще", cost - balance);
                        pay();
                    }
                } 
                else
                    Console.WriteLine("Тут столько нет");
            }
        }

        static void pay() {
            Console.WriteLine("Вставьте по очереди монеты по 10, 5, 2 и 1 р.");
            string[] str = Console.ReadLine().Split(new[] { ' ' }, StringSplitOptions.None);
            for (int i = 0; i < 4; i++) {
                money_automat[i] += Int32.Parse(str[i]);
                balance += Int32.Parse(str[i]) * coins[i];
            }
            Console.WriteLine("Ваш баланс {0}", balance);
        }

        static bool delivery()
        {
            int[] d = new int[4];
            for( int i = 0; i < 4; i++) {
                d[i] = Math.Min(money_automat[i], balance / coins[i]);
                balance -= d[i] * coins[i];
                money_automat[i] -= d[i];
            }
            if (balance == 0) { 
                Console.WriteLine("Ваша сдача: {0}, {1}, {2} и {3} монет по 10, 5, 2 и 1 рублю соответственно", d[0], d[1], d[2], d[3]);
                return true;
            }
            Console.WriteLine("У автомата нет денег на сдачу, вы можете купить что-то еще");
            return false;

        }

        static void Main(string[] args) {
            Init();
            while (true) {
                ShowMenu();

                pay();

                do
                    Selecting();
                while (!delivery());
                Console.WriteLine("Приходи еще");
            }

        }
    }
}
