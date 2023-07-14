using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;
using System.Xml.Serialization;

namespace Test
{
  public class Question 
    {
      
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
        }
        public string _B
        {
            get { return B; }
            set
            {
                if (value.Length != 0)
                    B = value;


            }
        }
        public string _C
        {
            get { return C; }
            set
            {
                if (value.Length != 0)
                    C = value;


            }
        }
        public string _D
        {
            get { return D; }
            set
            {
                if (value.Length != 0)
                    D = value;


            }
        }
        private string subject;
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
                    question_text = value;

                
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
        public Question() { }
        public Question(string _description = "Empty", string _correctAnswer = "Empty", string _subject = "Empty", string _a = "Empty", string _b = "Empty", string _c = "Empty", string _d = "Empty")
        {
            this.Question_text = _description;
            this.Answers = _correctAnswer;
            this.Subject = _subject;
            this._A = _a;
            this._B = _b;
            this._C = _c;
            this._D = _d;

        }
        public override string ToString()
        {
            return $"{Question_text}\n{Answers}\n{_A}\n{_B}\n{_C}\n{_D}";


        }


    }
    public class Admin 
    {
       
        private string adminName;
        public string AdminName
        {
            get { return adminName; }
            set 
            {
                if (value.Length != 0)
                    adminName = value;

                else { adminName = "admin"; }
            }
        }

        private string password;
        public string Password
        {
            get { return password; }
            set
            {
                if (value.Length != 0)
                    password = value;
            }
        }

        public Admin()
        {
            this.AdminName = "admin";
            this.Password = "admin";
;       }

        public Admin(string _adminName = "admin", string _password = "admin")
        {
            this.AdminName = _adminName;
            this.Password = _password;
        }

        public void Password_Changing(string _password) 
        {
            if (_password.Length != 0) 
            {
                Password = _password;
            }
        }

        public void Login_Changing(string _login)
        {
            if (_login.Length != 0)
            {
                this.AdminName = _login;
            }
        }
        public override string ToString()
        {
            return $"{AdminName}:{Password}";
        }
    }

  

    class Program
    {
        static void Welcome() 
        {
            Console.Write(new string(' ', 35));
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("Input your login and password to get access\n\n");
            Console.ResetColor();
        }

        public static bool Confirmation(Admin admin) 
        {
            bool isConfirmed = false;

            string _login_ = File.ReadAllText("Login.txt");
            string _password_ = File.ReadAllText("Password.txt");
            Welcome();
            do
            {


                Console.Write("Login    : ");
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                string login = Console.ReadLine();
                Console.ResetColor();
                if (login.Equals(_login_))
                {
                    Console.Write("Password : ");
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    string password = Console.ReadLine();
                    Console.ResetColor();
                    if (password.Equals(_password_))
                    {
                        Console.Write(new string(' ', 45));
                        Console.ForegroundColor = ConsoleColor.DarkYellow;

                        Console.Write("Access Confirmed. Welcome, "+login);
                        Console.WriteLine();
                        isConfirmed = true;
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.Write(new string(' ', 50));
                        Console.ForegroundColor = ConsoleColor.DarkRed;

                        Console.Write("Sad news:(");
                        Console.WriteLine();
                        Console.ResetColor();
                    }
                }
                else
                {
                    Console.Write(new string(' ', 50));
                    Console.ForegroundColor = ConsoleColor.DarkRed;

                    Console.Write("Sad news:(");
                    Console.WriteLine();
                    Console.ResetColor();
                }
            } while (!isConfirmed);
            return isConfirmed;
        }

        static void StartMenu() 
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write(new string(' ', 35));
            Console.WriteLine("Select window:\n\n");
           
            Console.ResetColor();
        }
        public static int Menu() 
        {

            StartMenu();

            Console.WriteLine("1. Login and Password changing");
            Console.WriteLine("2. Question adding");
            Console.WriteLine("3. Question removing");
            Console.WriteLine("0. _EXIT_");

            Console.WriteLine(new string('-', 80));
            Console.Write("Print: ");

            int ch=-1;
            do
            {
                try
                {
                    ch = Convert.ToInt32(Console.ReadLine());
              
                }
                catch (Exception e)
                {
                    Console.WriteLine();
                    Console.WriteLine(e.Message);
                }
            } while (ch > 3 && ch < 1);

            
            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine();
            Console.ResetColor();
            return ch;
        }
        public static void WriteIntoFile(List<Question>list,string path) 
        {
            using (var fs = new FileStream(path,FileMode.Truncate)) 
            {
                XmlSerializer xml = new XmlSerializer(typeof(List<Question>));
                xml.Serialize(fs, list);
            }
        }

        public static List<Question> FileReader(string path) 
        {
            using (var fs = new FileStream(path, FileMode.Open)) 
            {
                var list = new List<Question>();
                XmlSerializer xml = new XmlSerializer(typeof(List<Question>));
                list = xml.Deserialize(fs) as List<Question>;
                return list;

            }
        }


        static void Main(string[] args)
        {
            var admin = new Admin();

            List<Question> QuestionsList = new List<Question>();

            string questions = "Questions.xml";

            int your_choice = 0;
            if (Confirmation(admin))
            {
                Console.ReadKey();
                Console.Clear();

                do
                {
                    your_choice = Menu();

                    switch (your_choice)
                    {

                        case 1:
                            {
                                Console.Clear();
                                Console.Write("Login: ");
                                string new_login = Console.ReadLine();
                                Console.WriteLine();
                                if (String.IsNullOrWhiteSpace(new_login) || new_login.Contains(" "))
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.Write(new string(' ', 20));
                                    Console.WriteLine("Some problems! Login set as default\n");
                                    Console.ResetColor();
                                    new_login = "admin";


                                }
                                admin.Login_Changing(new_login);
                                File.WriteAllText("Login.txt", new_login);
                                Console.ForegroundColor = ConsoleColor.DarkYellow;
                                Console.Write(new string(' ', 40));
                                Console.WriteLine("Successfully changed");
                                Console.ResetColor();
                                Console.ReadKey();
                                Console.Clear();
                                Console.Write("Password: ");
                                string new_password = Console.ReadLine();
                                Console.WriteLine();
                                if (String.IsNullOrWhiteSpace(new_password) || new_password.Contains(" "))
                                {
                                    Console.WriteLine("Some problems! Password set as default\n");
                                    new_password = "admin";
                                }
                                admin.Password_Changing(new_password);
                                File.WriteAllText("Password.txt", new_password);
                                Console.ForegroundColor = ConsoleColor.DarkYellow;
                                Console.Write(new string(' ', 40));
                                Console.WriteLine("Successfully changed");
                                Console.ResetColor();
                                Console.ReadKey();
                                Console.Clear();

                            }
                            break;
                        case 2:
                            {
                                bool isAdded = false;
                                string sub = null;
                                Console.Clear();
                                Console.WriteLine("Possible subjects:\nMath\nEnglish\nGeography\n" + "Input a subject:\n");
                                while (true)
                                {
                                    sub = Console.ReadLine();
                                    if (sub == "Math" || sub == "English" || sub == "Geography")
                                    { break; }
                                }
                                Console.WriteLine();
                                Console.WriteLine("Print a question:\n");
                                string question = Console.ReadLine();
                                Console.WriteLine();
                                Console.WriteLine("Print variants:\n");
                                Console.WriteLine("Print variant A:\n");
                                string _a = Console.ReadLine();
                                Console.WriteLine();
                                Console.WriteLine("Print variant B:\n");
                                string _b = Console.ReadLine();
                                Console.WriteLine();
                                Console.WriteLine("Print variant C:\n");
                                string _c = Console.ReadLine();
                                Console.WriteLine();
                                Console.WriteLine("Print variant D:\n");
                                string _d = Console.ReadLine();
                                Console.WriteLine();
                                Console.WriteLine("Print an answer or answers:\n");
                                string answer_s = Console.ReadLine();
                                Console.WriteLine();

                                if (question.Length == 0 || answer_s.Length == 0)
                                {
                                    Console.WriteLine("Try again"); goto case 3;
                                }
                                else isAdded = true;

                                List<Question> list = FileReader(questions);
                                Question modificated_question = new Question(question, answer_s, sub, _a, _b, _c, _d);
                                list.Add(modificated_question);
                                QuestionsList.Clear();
                                QuestionsList.Capacity = list.Count;
                                for (int i = 0; i < list.Count; i++)
                                {
                                    QuestionsList.Add(list[i]);
                                }
                                WriteIntoFile(QuestionsList, questions);
                            }
                            break;

                        case 3:
                            {
                                Console.Clear();
                                int number_of_questions = 0;
                                int index_of_question = 0;
                                List<Question> list = FileReader(questions);
                                foreach (var item in list)
                                {
                                    Console.Write($"#{++number_of_questions}\n");
                                    Console.WriteLine(item);
                                    Console.WriteLine();
                                }
                                Console.Write("Number of question: ");
                                try
                                {
                                    index_of_question = Convert.ToInt32(Console.ReadLine());
                                    Console.Clear();
                                }
                                catch (Exception e)
                                {

                                    Console.WriteLine(e.Message);
                                    Console.ReadKey();
                                    Console.Clear();
                                }
                                if (index_of_question <= 0 || index_of_question > list.Count)
                                {
                                    Console.WriteLine("Problem, try again");
                                    Console.ReadKey();
                                    Console.Clear();
                                }

                                list.RemoveAt(index_of_question - 1);
                                QuestionsList.Clear();
                                QuestionsList.Capacity = list.Count;
                                for (int i = 0; i < list.Count; i++)
                                {
                                    QuestionsList.Add(list[i]);
                                }
                                WriteIntoFile(QuestionsList, questions);


                            }
                            break;
                        case 0:
                            {
                                Console.Clear();
                                Console.ForegroundColor = ConsoleColor.DarkYellow;
                                Console.Write(new string(' ', 40));
                                Console.Write($"Changes sucessfully changed. Bye!\n\n");
                                Console.ResetColor();

                                List<Question> temp = FileReader(questions);
                                var subjectQuestions = from question in temp
                                                        select question; ;


                                subjectQuestions = from question in temp
                                                    where question.Subject == "Math"
                                                    select question;
                                subjectQuestions.ToList();
                                int number_questions = subjectQuestions.Count();
                                Console.WriteLine($"Number of questions in Math: {number_questions} ");
                                if (number_questions < 20)
                                { Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("Add some math questions"); Console.ResetColor(); }
                                subjectQuestions = null;
                                subjectQuestions = from question in temp
                                                    where question.Subject == "Geography"
                                                    select question;
                                subjectQuestions.ToList();
                                number_questions = 0;
                                number_questions = subjectQuestions.Count();
                                Console.WriteLine($"Number of questions in Geography: {number_questions} ");
                                if (number_questions < 20)
                                { Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("Add some geography questions"); Console.ResetColor(); }
                                subjectQuestions = null;
                                subjectQuestions = from question in temp
                                                    where question.Subject == "English"
                                                    select question;
                                subjectQuestions.ToList();
                                number_questions = 0;
                                number_questions = subjectQuestions.Count();
                                Console.WriteLine($"Number of questions in English: {number_questions} ");
                                if (number_questions < 20)
                                { Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("Add some english questions"); Console.ResetColor(); }

                            }
                            break;
                        default:
                            {
                                Console.WriteLine("Try again!!!");
                                Console.ReadKey(); Console.Clear();
                            }
                            break;
                    }
                } while (your_choice != 0);
            }    
        }
    }
}

