using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace labka4
{
    internal class DaiTable
    {
        private string tableName;
        public DaiTable()
        {
            tableName = "Dai";
        }
        public Dai GetById(int id)
        {
            Dai car = null;
            SQLiteConnection conn = Singleton.GetInstance();

            using (SQLiteCommand command = new SQLiteCommand("SELECT * FROM " + tableName + " WHERE id = @Id", conn))
            {
                SQLiteParameter param = new SQLiteParameter();
                param.ParameterName = "@Id";
                param.Value = id;

                command.Parameters.Add(param);
                SQLiteDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    car = new Dai
                    {
                        id = Convert.ToInt32(reader[0].ToString()),
                        pib = reader[1].ToString(),
                        brend = reader[2].ToString(),
                        number_car = reader[3].ToString(),
                        color = reader[4].ToString(),
                    };
                }
                reader.Close();
                return car;
            }
        }

        public IEnumerable<Dai> GetAll()
        {
            SQLiteConnection conn = Singleton.GetInstance();

            using (SQLiteCommand command = new SQLiteCommand("SELECT * FROM " + tableName, conn))
            {
                SQLiteDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Dai car = new Dai
                    {
                        id = Convert.ToInt32(reader[0].ToString()),
                        pib = reader[1].ToString(),
                        brend = reader[2].ToString(),
                        number_car = reader[3].ToString(),
                        color = reader[4].ToString(),
                    };
                    yield return car;
                }
                reader.Close();
            }
        }

        public int GetNumberOfCars(string other_brend, string template)
        {
            int k = 0;
            SQLiteConnection conn = Singleton.GetInstance();

            using (SQLiteCommand command = new SQLiteCommand("SELECT * FROM " + tableName + " WHERE brend ==  " + other_brend , conn))
            {
                SQLiteDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    using (StringReader sr = new StringReader(Convert.ToString(reader["number_car"])))
                    {
                        string number = Convert.ToString(reader["number_car"]);
                        char[] b = new char[number.Length];
                        if(Convert.ToString(sr.Read(b, 0, 1)) == template)
                        {
                            // Підрахунок вже відформатованих автомобілів певної марки на певний номер автомобіля           
                            k++;
                        }
                    }
                }
                return k;
            }
        }

        public void Save(Dai car)
        {
            SQLiteConnection conn = Singleton.GetInstance();

            SQLiteCommand command = null;

            if (car.id < 1)
            {
                using (command = new SQLiteCommand("INSERT INTO " + tableName + "(pib, brend, number_car, color) " +
                    "VALUES (@pib, @brend, @number_car, @color)", conn))
                {
                    command.Parameters.AddWithValue("@pib", car.pib);
                    command.Parameters.AddWithValue("@brend", car.brend);
                    command.Parameters.AddWithValue("@number_car", car.number_car);
                    command.Parameters.AddWithValue("@color", car.color);
                    command.ExecuteNonQuery();
                    command.CommandText = "Select seq from sqlite_sequence where name = '" + tableName + "'";
                    car.id = Convert.ToInt32(command.ExecuteScalar());
                }
            }
            else
            {
                using (command = new SQLiteCommand("UPDATE " + tableName +
                    " SET pib = @pib, brend = @brend, number_car = @number_car, color = @color" +
                    " WHERE id = @id", conn))
                {
                    command.Parameters.Add(new SQLiteParameter("@pib", car.pib));
                    command.Parameters.Add(new SQLiteParameter("@brend", car.brend));
                    command.Parameters.Add(new SQLiteParameter("@number_car", car.number_car));
                    command.Parameters.Add(new SQLiteParameter("@color", car.color));
                    command.Parameters.Add(new SQLiteParameter("@id", car.id));
                    command.ExecuteNonQuery();
                }
            }

        }

        public void Remove(Dai car)
        {
            SQLiteConnection conn = Singleton.GetInstance();

            using (SQLiteCommand command = new SQLiteCommand("DELETE FROM " + tableName + " WHERE id = @id", conn))
            {
                command.Parameters.Add(new SQLiteParameter("@id", car.id));
                command.ExecuteNonQuery();
                car.id = 0;
            }
        }
    }
}
