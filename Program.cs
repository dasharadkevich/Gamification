using System.Runtime.CompilerServices;
using ProgrammingGame;

class Program
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        var questionData = new List<(string Text, string[] Options, int CorrectIndex, string Explanation)>
    {
        (
            "Що таке інкапсуляція?",
            new string[]
            {
                "Приховування деталей реалізації",
                "Можливість об'єкта набувати різних форм",
                "Створення ієрархії класів",
                "Виділення тільки суттєвих характеристик"
            },
            0,
            "Інкапсуляція — це приховування внутрішньої реалізації класу та захист даних."
        ),
        (
            "Що таке поліморфізм?",
            new string[]
            {
                "Приховування даних",
                "Можливість одного об'єкта мати кілька форм",
                "Передача відповідальності",
                "Створення нового класу на основі існуючого"
            },
            1,
            "Поліморфізм дозволяє використовувати об'єкти різних класів через один інтерфейс."
        ),
        (
            "Який принцип ООП дозволяє створювати ієрархію класів?",
            new string[] { "Інкапсуляція", "Абстракція", "Спадкування", "Поліморфізм" },
            2,
            "Спадкування (Inheritance) дозволяє дочірньому класу успадковувати властивості та методи батьківського."
        ),

        (
            "Що таке абстракція в ООП?",
            new string[]
            {
                "Приховування деталей реалізаці ї",
                "Виділення тільки суттєвих характеристик об'єкта",
                "Можливість методу мати кілька реалізацій",
                "Створення нових класів на основі старих"
            },
            1,
            "Абстракція — це приховування складності та показ лише необхідної інформації."
        ),
        (
            "Що означає ключове слово 'private' у класі?",
            new string[]
            {
                "Поле доступне тільки всередині класу",
                "Поле доступне усім класам",
                "Поле доступне тільки в поточній збірці",
                "Поле доступне в похідних класах"
            },
            0,
            "Модифікатор private забезпечує інкапсуляцію, приховуючи дані від зовнішнього доступу."
        )
    };


        AuthorIntroduction();

        var user = UserLogin();
        var quiz = new Quiz(questionData);

        var simulation = new Simulation(user, quiz);
        simulation.Run();

        Console.WriteLine("\nНатисніть будь-яку клавішу для завершення...");
        Console.ReadKey();

    }



    static void AuthorIntroduction()
    {
        Console.WriteLine("ПІБ студента: Радкевич Даша Ігорівна");
        Console.WriteLine("Курс: 1   Група: ІПЗ-11");
        Console.WriteLine("Варіант завдання: Серйозна гра для вивчення ООП");
        Console.WriteLine("Версія 1\n");
    }

    static User UserLogin()
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("Для початку зареєструйтесь будь ласка ...");

        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("Введіть ім'я: ");

        Console.ForegroundColor = ConsoleColor.White;
        string name = Console.ReadLine() ?? "";

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Введіть вік: ");

        Console.ForegroundColor = ConsoleColor.White;
        int age = int.TryParse(Console.ReadLine(), out int result) ? result : 0;

        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine($"\nВітаємо, {name}! Реєстрація успішна.");

        Console.ResetColor();

        var user = new User(name, age);

        return user;
    }
}




//   var test2 = new List<(string Text, string[] Options, int Correct, string Explanation)>
//         {
//             (
//                 "Що означає ключове слово 'void' у методі?",
//                 new[] { "Метод нічого не повертає", "Метод повертає ціле число", "Метод є асинхронним", "Метод є конструктором" },
//                 0,
//                 "void означає, що метод не повертає жодного значення."
//             ),
//             (
//                 "Як оголосити змінну типу string у C#?",
//                 new[] { "string x = 5;", "string x = \"Hello\";", "String x = Hello;", "var x = \"Hello\";" },
//                 1,
//                 "Правильний синтаксис: string x = \"Hello\";"
//             ),
//             (
//                 "Що робить оператор ++ ?",
//                 new[] { "Зменшує значення на 1", "Збільшує значення на 1", "Множить на 2", "Ділить на 2" },
//                 1,
//                 "++ — це оператор інкременту (збільшення на 1)."
//             ),
//             (
//                 "Який цикл використовується, коли кількість ітерацій відома заздалегідь?",
//                 new[] { "while", "do-while", "for", "foreach" },
//                 2,
//                 "for — найкращий вибір, коли відома кількість повторень."
//             ),
//             (
//                 "Що таке Git?",
//                 new[] { "Мова програмування", "Система контролю версій", "Фреймворк", "База даних" },
//                 1,
//                 "Git — це розподілена система контролю версій."
//             )
//             // Додайте ще питання за потребою
//         };
