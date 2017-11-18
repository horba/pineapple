using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace DBMigration
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] files = Directory.GetFiles("Databases");
            List<Migrator> migrators = new List<Migrator>();

            foreach (string file in files)
            {
                Console.WriteLine(file);
                string query = File.ReadAllText(file);
                string fileNumber = file.Split('\\')[1].Split('.')[0];

                if (fileNumber != "Baseline")
                {
                    migrators.Add(new Migrator(fileNumber, query));
                }
            }

            foreach (Migrator mig in migrators) {
                if (mig.Apply())
                {
                    Console.WriteLine("Изменение " + mig.FileNumber + " применено успешно.");
                }
                else {
                    Console.WriteLine("Изменение " + mig.FileNumber + " не удалось применить. Ошибка: " + mig.Error);
                }
            }

            Console.ReadLine();
        }
    }
}
