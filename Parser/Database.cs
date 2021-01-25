using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Parser.Models;

namespace Parser
{
    class Database
    {

        private string _connectionString;


        public Database(string connectionString)
        {
            _connectionString = connectionString;


        }


        public List<News> GetNews(string header=null)
        {
            List<News> result=new List<News>();

            string sqlExpression;

            if (header==null)
            {
                sqlExpression = "SELECT * FROM News.Posts";
            }
            else
            {
                sqlExpression = "SELECT * FROM News.Posts WHERE Header Like @header";


            }
            


            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);

                if (header!=null)
                {
                    command.Parameters.Add(new SqlParameter("@header", "%"+header+"%"));
                }

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows) // если есть данные
                {

                    while (reader.Read()) // построчно считываем данные
                    {
                        News temp = new News();

                        temp.Header = reader.GetValue(1).ToString();

                        temp.Text= reader.GetValue(2).ToString();

                        temp.Url = reader.GetValue(3).ToString();

                        temp.Date = reader.GetValue(4).ToString();

                        result.Add(temp);
                    }
                }

                reader.Close();
            }

            return result;
            
           // InsertToDb(new News() { Header = "Новый заголовок", Text = "Длинный текстттт", Url = "Адрес", Date = "13-05-1997" });
        }

        public void Insert(News news)
        {

           

            string sqlExpression = "INSERT INTO News.Posts (Header, Content, Url, CreatedAt, Uid) VALUES (@header, @content, @url, @createdat, @uid)";


            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                // создаем параметр для имени
                SqlParameter nameParam = new SqlParameter("@header", news.Header);
                // добавляем параметр к команде
                command.Parameters.Add(nameParam);
                // создаем параметр для возраста
                SqlParameter ageParam = new SqlParameter("@content", news.Text??"");
                // добавляем параметр к команде
                command.Parameters.Add(ageParam);

                command.Parameters.Add(new SqlParameter("@url", news.Url));

                command.Parameters.Add(new SqlParameter("@createdat", news.Date));

                command.Parameters.Add(new SqlParameter("@uid", Helper.MD5(news.Url)));

                int number = command.ExecuteNonQuery();


            }
        }

        public bool Contain(News news)
        {
            int count = 0;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("SELECT COUNT(Id) FROM News.Posts WHERE Uid=@uid", connection);

                command.Parameters.Add(new SqlParameter("@uid", Helper.MD5(news.Url)));

                count = Convert.ToInt32( command.ExecuteScalar());

            }

            return count > 0;
        }

    }
}
