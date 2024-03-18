using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

namespace Lab_6_Serialization_and_RAF
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const string FILE = "event.txt";

            //Creating the first object
            Event event_object = new Event
            {
                eventNumber = 1,
                Location = "Calgary"
            };

            //Serializing the object to text file
            SerializeToFile(event_object, FILE);

            Event deserializedEvent = DeserializeFromFile<Event>(FILE);
            Console.WriteLine($"{deserializedEvent.eventNumber}");
            Console.WriteLine($"{deserializedEvent.Location}");
            Console.WriteLine();

            //List of Event objects
            List<Event> eventList = new List<Event>
            {
                new Event { eventNumber = 1, Location = "Tech Competition" },
                new Event { eventNumber = 2, Location = "Calgary" },
                new Event { eventNumber = 3, Location = "Toronto" },
                new Event { eventNumber = 4, Location = "Vancover" },
                new Event { eventNumber = 5, Location = "Edmonton" }
            };

            //Diplay of the list
            Console.WriteLine($"{eventList[0].Location}");
            Console.WriteLine($"{eventList[1].eventNumber} {eventList[1].Location}");
            Console.WriteLine($"{eventList[2].eventNumber} {eventList[2].Location}");
            Console.WriteLine($"{eventList[3].eventNumber} {eventList[3].Location}");
            Console.WriteLine($"{eventList[4].eventNumber} {eventList[4].Location}");
            Console.WriteLine();

            //Serializes to JSON
            const string JSON_FILE = "events.json";
            SerializeListToJsonFile(eventList, JSON_FILE);


            //Deserializes JSON file
            static T DeserializeFromJsonFile<T>(string filePath)
            {
                using (FileStream fs = new FileStream(filePath, FileMode.Open))
                {
                    DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(T));
                    return (T)jsonSerializer.ReadObject(fs);
                }
            }


            //Displays the characters of Hackathon
            ReadFromFile();
            
            Console.ReadKey();
        }

        private static void SerializeToFile<T>(T obj, string filePath)
        {
            using (FileStream fs = new FileStream(filePath, FileMode.Create))
            {
                DataContractSerializer serializer = new DataContractSerializer(typeof(T));
                serializer.WriteObject(fs, obj);
            }
        }

        private static T DeserializeFromFile<T>(string filePath)
        {
            using (FileStream fs = new FileStream(filePath, FileMode.Open))
            {
                DataContractSerializer serializer = new DataContractSerializer(typeof(T));
                return (T)serializer.ReadObject(fs);
            }
        }

        private static void SerializeListToJsonFile<T>(List<T> list, string filePath)
        {
            using (FileStream fs = new FileStream(filePath, FileMode.Create))
            {
                DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(List<T>));
                jsonSerializer.WriteObject(fs, list);
            }
        }

        private static List<T> DeserializeFromJsonFile<T>(string filePath)
        {
            using (FileStream fs = new FileStream(filePath, FileMode.Open))
            {
                DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(List<T>));
                return (List<T>)jsonSerializer.ReadObject(fs);
            }
        }

        private static void ReadFromFile()
        {
            const string FILE = "Hackathon.txt";
            using (StreamWriter writer = new StreamWriter(FILE))
            {
                writer.Write("Hackathon");
            }

            using (StreamReader reader = new StreamReader(FILE))
            {
                string content = reader.ReadToEnd();
                int length = content.Length;
                int middleIndex = length / 2;
                Console.WriteLine("In Word: " + content);
                Console.WriteLine($"The First Character is: \"{content[0]}\"");
                Console.WriteLine($"The Middle Character is: \"{content[middleIndex]}\"");
                Console.WriteLine($"The Last Character is: \"{content[length - 1]}\"");
            }
        }
    }

    [DataContract]
    public class Event
    {
        [DataMember]
        public int eventNumber { get; set; }

        [DataMember]
        public string Location { get; set; }
    }
}
