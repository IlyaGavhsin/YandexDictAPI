using System;
using System.Net;
using Newtonsoft.Json;
using System.IO;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;

namespace YandexDict
{
    class Program
    {
        static void Main(string[] args)
        {
            TelegramBotClient bot = new TelegramBotClient("1848825408:AAHpxH4ZgBQFHItNUOIR5-vCIrccPSzF2D0");




            var ApiKey = "dict.1.1.20210609T093927Z.888d975cd57427e7.a81c833f63a959024fa47afb7603fd71eec4747f";

           

            bot.OnMessage += (s, arg) =>
            {
                var Word = arg.Message.Text;
                var url = $"https://dictionary.yandex.net/api/v1/dicservice.json/lookup?key={ApiKey}&lang=ru-ru&text={Word}";
                Console.WriteLine($"{arg.Message.Chat.FirstName}: {Word}");
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
                        result = null;

                        foreach (var syn in datas.def[0].tr)
                        {
                            Console.WriteLine(syn.text);
                            result += $"*{syn.text}*\n";

                            if (syn.syn != null)
                            {
                                foreach (var M in syn.syn)
                                {
                                    result += $"    {M.text}" + Environment.NewLine ;
                                }
                            }
                        }

                        bot.SendTextMessageAsync(arg.Message.Chat.Id, $"Результат по запросу слова: {Word}\n{result}", ParseMode.Markdown);
                    }

                }

                catch
                {
                    Console.WriteLine("Это слово некоректно, или не имеет синонимов");
                }
            };

            bot.StartReceiving();

            Console.ReadKey();
        }
    }
}
