using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;
using System.Linq;
public class User 
{
    private string login;
    public string _Login
    {
        get { return login; }
        set 
        {
            if (value.Length != 0)
            {
                login = value;

            }
            else { login = "Log"; }
        }
    }

    private string password;
    public string _Password
    {
        get { return  password; }
        set 
        {
            if (value.Length != 0)
            {
                password = value;
            }

            else { password = "Pass"; }
        }
    }

    public DateTime BirthDay{ get; set; }

    private int mathScore;
    public int MathScore
    {
        get { return mathScore; }
        set { mathScore = value; }
    }

    private int geographyScore;
    public int GeographyScore
    {
        get { return geographyScore; }
        set { geographyScore = value; }
    }

    private int englishScore;
    public int EnglishScore
    {
        get { return englishScore; }
        set { englishScore = value; }
    }

    private int mixtureScore;
    public int MixtureScore
    {
        get { return mixtureScore; }
        set { mixtureScore = value; }
    }

    public User()
    {
        _Login = "Log";
        _Password = "Pass";
        MathScore = 0;
        GeographyScore = 0;
        EnglishScore = 0;
        MixtureScore = 0;
        BirthDay =  DateTime.Now;
    }
    public User(string _login = "Log", string _password = "Pass", int _math = 0, int _geography = 0, int _eng = 0, int _all = 0, string birth_date ="2001-01-01") 
    {
        _Login = _login;
        _Password = _password;
        MathScore = _math;
        GeographyScore = _geography;
        EnglishScore = _eng;
        MixtureScore = _all;
        BirthDay = DateTime.ParseExact(birth_date, "dd-MM-yyyy",
                                       System.Globalization.CultureInfo.InvariantCulture);
    }

    public override string ToString()
    {
        return $"{_Login}'s results:\n" +$"Best in Math: {MathScore}\n" +$"Best in Geography: {GeographyScore}\n" + $"Best in English: {EnglishScore}\n" +$"Best in All:{MixtureScore}\n" + $"BirthDay {BirthDay.ToShortDateString()}";
    }

}
public class Question
{private string subject;
    public string Subject
    {
        get { return subject; }
        set
        {

            if (value.Length != 0)
                subject = value;
        }
    }
    private string question_text;
    public string Question_text
    {
        get { return question_text; }
        set
        {
            if (value.Length != 0)
            { question_text = value; }


        }
    }
    private string A;
    private string B;
    private string C;
    private string D;
    public string _A
    {
        get { return A; }
        set
        {
            if (value.Length != 0)
                A = value;


        }
    }public string _B
    {
        get { return B; }
        set
        {
            if (value.Length != 0)
                B = value;


        }
    }public string _C
    {
        get { return C; }
        set
        {
            if (value.Length != 0)
                C = value;


        }
    }public string _D
    {
        get { return D; }
        set
        {
            if (value.Length != 0)
                D = value;


        }
    }
    private string answers;
    public string Answers
    {
        get { return answers; }
        set
        {
            if (value.Length != 0)
                answers = value;


        }
    }    
    public override string ToString()
    {
        return $"{Question_text}\n";
    }
    public Question() { }
    public Question(string _text = "Empty", string _Answer = "Empty", string a = "Empty", string b = "Empty", string c = "Empty", string d = "Empty")
    {
        this.Question_text = _text;
        this.Answers = _Answer;
        this.A = a;
        this.B = b;
        this.C = c;
        this.D = d;

    }

    
  }


namespace Testing_Code_User
{
    class Program
    {
        
        public static void Write_User(List<User> list, string path)
        {
            using (var fs = new FileStream(path, FileMode.Truncate))
            {
                XmlSerializer xml = new XmlSerializer(typeof(List<User>));
                xml.Serialize(fs, list);
            }
        }

        public static List<Question> Read_Questions(string path)
        {
            using (var fs = new FileStream(path, FileMode.Open))
            {
                var list = new List<Question>();
                XmlSerializer xml = new XmlSerializer(typeof(List<Question>));
                list = xml.Deserialize(fs) as List<Question>;
                return list;

            }
        }

        public static List<User> Read_User(string path)
        {
            using (var fs = new FileStream(path, FileMode.Open))
            {
                var list = new List<User>();
                XmlSerializer xml = new XmlSerializer(typeof(List<User>));
                list = xml.Deserialize(fs) as List<User>;
                return list;
            }
        }
        static User Registration() 
        {
            Console.Write("Registration\n");
            bool confirmation = false;
            string login = default;
            string password = default;
            string birthday = default;
            List<User> users = Read_User("Users.xml");
            do
            {
                confirmation = false;
                Console.Write("Your login: ");
                login = Console.ReadLine();

                if (login.Contains(' ') || login.Length == 0) { Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("Some problems! Pay attention"); Console.ResetColor(); Console.ReadKey(); Console.Clear(); }

                else confirmation = true;

                foreach (var item in users)
                {
                    if (login == item._Login) { confirmation = false;Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("This login exists. Rename!"); Console.ResetColor(); Console.ReadKey(); Console.Clear(); }
                }

            } while (confirmation == false);

            do
            {
                Console.Write("Your password: ");
                password = Console.ReadLine();
                if (password.Contains(' ')  || password.Length == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Some problems! Be smarter!");
                    Console.ResetColor(); Console.ReadKey(); Console.Clear();
                }
                else break;
            } while (true);
            

            bool catcher;
            do
            {
                catcher = false;

                Console.Write("Birthday date (dd-MM-yyyy): ");
                birthday = Console.ReadLine();
                try
                {
                    DateTime.ParseExact(birthday, "dd-MM-yyyy",
                                        System.Globalization.CultureInfo.InvariantCulture);
                }
                catch (Exception e)
                {

                    Console.WriteLine(e.Message);
                    catcher = true;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Exception");
                    Console.ResetColor();

                }

            } while (catcher);

            User new_user = new User(login, password, 0, 0, 0, 0, birthday);
            return new_user;
        }
        static int LogIn() 
        {
            int Position = 0;
            bool isConfirmed = false;
            string isCorrect = default;
            bool isPasswordConfirmed = false;
            do
            {
                isConfirmed = false;

                List<User> useri = Read_User("Users.xml");
                
                Console.Write("Your login: ");
                var login = Console.ReadLine();

                for (int i = 0; i < useri.Count; i++)
                {
                    if (useri[i]._Login == login) { isConfirmed = true; Console.ForegroundColor = ConsoleColor.DarkYellow; Console.WriteLine("Confirmed successfully"); Console.ResetColor(); Position = i; isCorrect = useri[i]._Password; break; }
                }

            } while (isConfirmed == false);

            do
            {
                isPasswordConfirmed = false;
                Console.Write("Your password: ");
                string user_password = Console.ReadLine();
                if (user_password != isCorrect) { Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("Wrong password!!!\nWrong password!!!\nWrong password!!!"); Console.ResetColor(); Console.ReadKey(); Console.Clear(); isPasswordConfirmed = false; }
                else isPasswordConfirmed = true;
            } while (isPasswordConfirmed == false);

            return Position;
        }
        static void Starting_Menu()
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("Hi! Register or log in to continue\n\n");
            Console.ResetColor();
            Console.WriteLine("\t\t|1| Registration\n\n" + "\t\t|2| Log in\n\n" + "\t\t|3| Leaderboars on Math\n\n" + "\t\t|4| Leaderboars on English\n\n" + "\t\t|5| Leaderboars on Geography\n\n" + "\t\t|6| Leaderboars on Mixed\n\n" + "\t\t|7| Leaderboars of All time\n\n" + "\t\t|0| Exit\n\n");
        }
        public static int GetQuestions(string sub)
        {
            string path = @"C:\Users\lenovo\source\repos\Testing Code Users\bin\Debug\net5.0\Questions.xml";
            var list = new List<Question>();
            list = Read_Questions(path);
            
            Console.WriteLine(list);
            var selectedQuestions = from question in list
                                    select question;
            if (sub != "") { 
                selectedQuestions = from question in list
                                        where question.Subject == sub
                                        select question;
         
           }
            List<Question> _questions = selectedQuestions.ToList();
            List<int> _nums = Randomizer_Positions(_questions.Count);
            int num = 1;
            string c_answer = default;
            string _answer = default;
            int _score = default;
            for (int i = 0; i < 20; i++)
            {
                Console.WriteLine("№ " + num);
                Console.WriteLine(_nums[i]);

                Console.WriteLine("\t"+  _questions[_nums[i]]+ "A)" + _questions[_nums[i]]._A + "\nB)" + _questions[_nums[i]]._B + "\nC)" + _questions[_nums[i]]._C + "\nD)" + _questions[_nums[i]]._D);
                c_answer = _questions[_nums[i]].Answers.ToUpper();
                Console.WriteLine();
                Console.Write("Your variant:");
                _answer = Console.ReadLine().ToUpper();
                Console.WriteLine();
                if (c_answer == _answer) { _score++; }
                num++;
            }
            return _score;
        }
        static public List<int> Randomizer_Positions(int size) 
        {
            var random = new Random();
            int count = 20;

            List<int> SecretNumbers = new List<int>();

            for (int i = 0; i < count; i++)
            {
                int number;

                do number = random.Next(0, size);
                while (SecretNumbers.Contains(number));

                SecretNumbers.Add(number);
            }
            return SecretNumbers;
        }
        public static void Show_Subject() 
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("Choose subject:\n");
            Console.ResetColor();
            Console.WriteLine("1. Math\n\n" + "2.Geography\n\n"+ "3.English\n\n" + "4.All Subjects\n\n"+ "0.Exit\n\n");
        }
        static int Input_Scope(int a)
        {

            int choice = -1;
            do
            {
                if (a == 7)
                { Starting_Menu(); }
                Console.Write("Your choice: ");
                try
                {
                    choice = Convert.ToInt32(Console.ReadLine());

                }
                catch (Exception e)
                {
                    Console.WriteLine();
                    Console.WriteLine(e.Message);
                    Console.WriteLine();

                }

                if (choice > a || choice < 0) { Console.WriteLine("Why? Try again"); Console.ReadKey(); Console.Clear(); }
            } while (choice > a || choice < 0);


            return choice;
        }

       
       
        static void Main(string[] args)
        {
       
            Console.WriteLine("Salam!");
            List<User> users_list = new();
            bool isPlay = true;
            do
            {
                int scope = Input_Scope(7);
                switch (scope)
                {
                    case 1:
                        {
                            Console.Clear();
                            var new_user = new User();
                            new_user = Registration();
                            List<User> users_from_xml = Read_User("Users.xml");
                            users_from_xml.Add(new_user);
                            users_list.Clear();
                            for (int i = 0; i < users_from_xml.Count; i++)
                            {
                                users_list.Add(users_from_xml[i]);
                            }

                            Write_User(users_list, "Users.xml"); goto case 2;
                            
                        }
                        break;

                    case 2:
                        {
                            Console.Clear();
                            int user_index = LogIn();
                            users_list = Read_User("Users.xml");
                            Show_Subject();
                            int choice = Input_Scope(4);
                            int score = default;

                            switch (choice)
                            {

                                case 1:
                                    {
                                        Console.Clear();
                                        score = GetQuestions("Math");

                                        Console.Write(new string(' ', 50));
                                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                                        Console.Write("Your score is " + score); Console.WriteLine();
                                        Console.ResetColor();

                                        List<User> users = Read_User("Users.xml");
                                        int currect_score = users[user_index].MathScore;
                                        if (currect_score < score) { users[user_index].MathScore = score; }
                                        Write_User(users, "Users.xml");

                                        Console.ReadKey();
                                        Console.Clear();

                                    }
                                    break;
                                case 2:
                                    {
                                        Console.Clear();
                                        score = GetQuestions("Geography");
                                        Console.Write(new string(' ', 50));
                                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                                        Console.Write("Score: " + score); Console.WriteLine();
                                        Console.ResetColor();

                                        List<User> users = Read_User("Users.xml");
                                        int currect_score = users[user_index].GeographyScore;
                                        if (currect_score < score) { users[user_index].GeographyScore = score; }
                                        Write_User(users, "Users.xml");
                                        Console.ReadKey();
                                        Console.Clear();
                                    }
                                    break;
                                case 3:
                                    {
                                        Console.Clear();
                                        score = GetQuestions("English");

                                        Console.Write(new string(' ', 50));
                                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                                        Console.Write("Score: " + score); Console.WriteLine();
                                        Console.ResetColor();

                                        List<User> users = Read_User("Users.xml");
                                        int currect_score = users[user_index].EnglishScore;
                                        if (currect_score < score) { users[user_index].EnglishScore = score; }
                                        Write_User(users, "Users.xml");
                                        Console.ReadKey();
                                        Console.Clear();
                                    }
                                    break;
                                case 4:
                                    {
                                        Console.Clear();
                                        score = GetQuestions("");

                                        Console.Write(new string(' ', 50));
                                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                                        Console.Write("Score: " + score); Console.WriteLine( );
                                        Console.ResetColor();

                                        List<User> users = Read_User("Users.xml");
                                        int currect_score = users[user_index].MixtureScore;
                                        if (currect_score < score) { users[user_index].MixtureScore = score; }
                                        Write_User(users, "Users.xml");
                                        Console.ReadKey();
                                        Console.Clear();
                                    }
                                    break;

                                case 0: 
                                    { Console.Write(new string(' ', 50));
                                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                                        Console.WriteLine("Try next time! Thanks for play\n"); 
                                        Console.ResetColor(); 
                                    }
                                    break;
                            }

                        }
                        break;

                    case 3:
                        {
                            Console.Clear();
                            List<User> user = Read_User("Users.xml");
                            List<User> sorted = user.OrderByDescending(x => x.MathScore).ToList();

                            Console.Write(new string(' ', 50));
                            Console.WriteLine("Top in Math\n");
                            var count = sorted.Count;
                            for (int i = 0; i < count; i++)
                            {
                                Console.Write($"{sorted[i]._Login}: ");
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.Write($"{sorted[i].MathScore}");
                                Console.ResetColor();
                                Console.WriteLine();
                                Console.WriteLine(new string('-', 50));
                            }
                            Console.WriteLine();
                            Console.Write(new string (' ',45));
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.WriteLine("To continue press\n");
                            Console.ReadKey();
                            Console.Clear();
                        }
                        break;

                    case 5:
                        {
                            Console.Clear();
                            List<User> user = Read_User("Users.xml");
                            List<User> sorted = user.OrderByDescending(x => x.GeographyScore).ToList();

                            Console.Write(new string(' ', 50));
                            Console.WriteLine("Top on Geography\n");
                            var count = sorted.Count;
                            for (int i = 0; i < count; i++)
                            {
                                Console.Write($"{sorted[i]._Login}: ");
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.Write($"{sorted[i].GeographyScore}");
                                Console.ResetColor();
                                Console.WriteLine();
                                Console.WriteLine(new string('-', 50));
                            }
                            Console.WriteLine();
                            Console.Write(new string(' ', 45));
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.WriteLine("To continue press\n");
                            Console.ReadKey();
                            Console.Clear();

                        }
                        break;

                    case 4:
                        {
                            Console.Clear();
                            List<User> user = Read_User("Users.xml");
                            List<User> sorted = user.OrderByDescending(x => x.EnglishScore).ToList();

                            Console.Write(new string(' ', 50));
                            Console.WriteLine("Top on English\n");
                            var count = sorted.Count;
                            for (int i = 0; i < count; i++)
                            {
                                Console.Write($"{sorted[i]._Login}: ");
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.Write($"{sorted[i].EnglishScore}");
                                Console.ResetColor();
                                Console.WriteLine();
                                Console.WriteLine(new string('-', 50));
                            }
                            Console.WriteLine();
                            Console.Write(new string(' ', 45));
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.WriteLine("To continue press\n");
                            Console.ReadKey();
                            Console.Clear();
                        }
                        break;
                    case 6:
                        {
                            Console.Clear();
                            List<User> user = Read_User("Users.xml");
                            List<User> sorted = user.OrderByDescending(x => x.MixtureScore).ToList();

                            Console.Write(new string(' ', 50));
                            Console.WriteLine("Top on All\n");
                            var count = sorted.Count;
                            for (int i = 0; i < count; i++)
                            {
                                Console.Write($"{sorted[i]._Login}: ");
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.Write($"{sorted[i].MixtureScore}");
                                Console.ResetColor();
                                Console.WriteLine();
                                Console.WriteLine(new string('_', 50));
                            }

                            Console.WriteLine();
                            Console.Write(new string(' ', 45));
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.WriteLine("To continue press\n");
                            Console.ReadKey();
                            Console.Clear();
                        }
                        break;

                       case 7:
                        {
                            Console.Clear();
                            List<User> user = Read_User("Users.xml");
                            List<User> sorted = user.OrderByDescending(x => x.MathScore).ToList();

                            Console.Write(new string(' ', 50));
                            Console.WriteLine("Top of All time\n");
                            var count = sorted.Count;
                            for (int i = 0; i < count; i++)
                            {
                                
                                Console.Write($"{sorted[i]._Login}: ");
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.Write($"{Math.Max(Math.Max(sorted[i].MixtureScore,sorted[i].EnglishScore), Math.Max(sorted[i].GeographyScore, sorted[i].MathScore))}");
                                Console.ResetColor();
                                Console.WriteLine();
                                Console.WriteLine(new string('-', 50));
                            }

                            Console.WriteLine();
                            Console.Write(new string(' ', 45));
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.WriteLine("To continue press\n");
                            Console.ReadKey();
                            Console.Clear();
                        }
                        break;

                        
                    case 0:
                        { 
                            Console.Write(new string(' ', 24)); 
                            Console.ForegroundColor = ConsoleColor.Green; 
                            Console.WriteLine("Thanks for playing. Bye\n"); 
                            Console.ResetColor();
                            isPlay = false;
                        } break;
                }
            } while (isPlay != false);
        }

    }
}
