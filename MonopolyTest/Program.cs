// See https://aka.ms/new-console-template for more information
using MonopolyTest;
using System.Text.Json;
using System.Text.Json.Nodes;

internal class Program
{
    private static void Main(string[] args)
    {
        Warehouse warehouse = new Warehouse();
        string jsonPath = Path.Combine(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName, "warehouse.json");
        using (FileStream fs = new FileStream(jsonPath, FileMode.OpenOrCreate))
        {
            var json = JsonNode.Parse(fs);
            var pallets = json["pallets"].AsArray();
            for (int i = 0; i < pallets.Count; i++)
            {
                int id = (int)pallets[i]["id"];
                double length = (double)pallets[i]["length"];
                double width = (double)pallets[i]["width"];
                double height = (double)pallets[i]["height"];
                Pallet pallet = new Pallet(id, length, width, height);
                warehouse.AddPallet(pallet);
            }
            var boxes = json["boxes"].AsArray();
            for (int i = 0; i < boxes.Count; i++)
            {
                int id = (int)boxes[i]["id"];
                double length = (double)boxes[i]["length"];
                double width = (double)boxes[i]["width"];
                double height = (double)boxes[i]["height"];
                double weight = (double)boxes[i]["weight"];
                string dateString = (string)boxes[i]["manufacture_date"];
                Pallet pallet = warehouse.GetPalletById((int)boxes[i]["pallet"]);
                Box box = new Box(id, length, width, height, weight, dateString, pallet);
            }
        }

        try
        {
            Console.WriteLine(warehouse.GetOrderedPallets());
            Console.WriteLine(warehouse.GetPalletesWithMostExpDate());
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}