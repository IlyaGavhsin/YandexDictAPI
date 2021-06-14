using System;
using System.Net;
using Newtonsoft.Json;
using System.IO;

namespace YandexDict
{
    class Program
    {
        static void Main(string[] args)
        {
            var ApiKey = "dict.1.1.20210609T093927Z.888d975cd57427e7.a81c833f63a959024fa47afb7603fd71eec4747f";
            Console.WriteLine("Введите слово на русском");
            var Word = Console.ReadLine();
            var url = $"https://dictionary.yandex.net/api/v1/dicservice.json/lookup?key={ApiKey}&lang=ru-ru&text={Word}";

            var request = WebRequest.Create(url);

            var response = request.GetResponse();
            var httpStatusCode = (response as HttpWebResponse).StatusCode;

            if (httpStatusCode != HttpStatusCode.OK)
            {
                Console.WriteLine(httpStatusCode);
                return;
            }

            try
            {
                using (var streamReader = new StreamReader(response.GetResponseStream()))
                {
                    string result = streamReader.ReadToEnd();
                    var datas = JsonConvert.DeserializeObject<Root>(result);


                    foreach (var syn in datas.def[0].tr)
                    {
                        Console.WriteLine(syn.text);

                        if (syn.syn != null)
                        {
                            foreach (var s in syn.syn)
                            {
                                Console.WriteLine($"    {s.text}");
                            }
                        }   
                    }
                }
            }

            catch
            {
                Console.WriteLine("Это слово некоректно, или не имеет синонимов");
            }
        }
    }
}
