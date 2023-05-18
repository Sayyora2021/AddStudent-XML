using System.ComponentModel;
using System.Net.Http.Headers;
using System.Xml.Linq;

namespace ConsoleApp7
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string filePath = AppDomain.CurrentDomain.BaseDirectory + "XML\\Sample.xml";
            XDocument xmldoc = XDocument.Load(filePath);

            int affiche = 1;

            while (affiche!=0)
            {
                Console.WriteLine("Saisissez un choix: 1 afficher, 2 ajouter, 3 mettre à jour, 4 supprimer, 0 quitter ");
                

                 affiche = int.Parse(Console.ReadLine());
                switch(affiche){
                    case 1: //Afficher
                        var affichage = from customer in xmldoc.Descendants("Customer")
                                        select new
                                        {
                                            id = customer.Element("Id").Value,
                                            name = customer.Element("Name").Value,
                                            gender = customer.Element("Gender").Value,
                                            phone = customer.Element("Phone").Value,
                                            street = customer.Element("Address").Element("Street").Value,
                                            city = customer.Element("Address").Element("City").Value,
                                            state = customer.Element("Address").Element("State").Value,
                                            country = customer.Element("Address").Element("Country").Value,
                                        };
                        Console.WriteLine(String.Join("\n", affichage));
                        break;
                    case 2:  //Ajouter un nouveau Id
                        Console.WriteLine("Saisir name, gender, phone, street, city, state, country");

                        var c = new XElement("Customer",
                             new XElement("Id", int.Parse(Console.ReadLine())),
                             new XElement("Name", Console.ReadLine()),
                             new XElement("Gender", Console.ReadLine()),
                             new XElement("Phone", Console.ReadLine()),
                             new XElement("Address",
                             new XElement("Street", Console.ReadLine()),
                             new XElement("City", Console.ReadLine()),
                             new XElement("State", Console.ReadLine()),
                             new XElement("Country", Console.ReadLine())
                             )
                             );
                        xmldoc.Root.Add( c );
                        xmldoc.Save(filePath);
                        break;
                    case 3: // Modifier
                        Console.WriteLine("Choisissez id pour mettre à jour");
                        var selected = int.Parse(Console.ReadLine());
                        Console.WriteLine("Saisir name, gender, phone, street, city, state, country");
                        XElement update = xmldoc.Descendants("Customer")
                         .FirstOrDefault(x => x.Element("Id").Value == selected.ToString());
                        if(update != null)
                        {
                            update.Element("Name").Value = Console.ReadLine();
                            update.Element("Gender").Value = Console.ReadLine();
                            update.Element("Phone").Value = Console.ReadLine();
                            update.Element("Address").Element("Street").Value = Console.ReadLine();
                            update.Element("Address").Element("City").Value = Console.ReadLine();
                            update.Element("Address").Element("State").Value = Console.ReadLine();
                            update.Element("Address").Element("Country").Value = Console.ReadLine();
                        }
                        xmldoc.Save(filePath);

                        break;
                    case 4: //supprimer
                        Console.WriteLine("Supprimer id");
                        var delete = int.Parse(Console.ReadLine());
                        XElement deleted = xmldoc.Descendants("Customer")
                             .FirstOrDefault(x => x.Element("Id").Value == delete.ToString());
                        if (deleted != null)
                            deleted.Remove();
                        xmldoc.Save(filePath);
                        break;

                   
                        
                    default: 
                        break;
                }
            }

        }
    }
}