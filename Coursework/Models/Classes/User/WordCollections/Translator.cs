using System.IO;
using System.Net;
using Newtonsoft.Json;
using Coursework.Models.Classes.Enternet;

namespace Coursework.Models.Classes.User.WordCollections
{
    class Translator
    {
        public string Translate(string s, string lang)
        {
            if(s != null)
            {
                if ( s.Length > 0 && Connection.IsInternetAvailable() )
                {
                    WebRequest request = WebRequest.Create("https://translate.yandex.net/api/v1.5/tr.json/translate?"
                        + "key=" + "trnsl.1.1.20191222T000332Z.3bd22300449bd814.0737c8055a7dd5b1b510ea480802402c3eb01117"
                        + "&text=" + s
                        + "&lang=" + lang);

                    WebResponse response = request.GetResponse();
                    using ( StreamReader stream = new StreamReader(response.GetResponseStream()) )
                    {
                        string line;
                        if ( ( line = stream.ReadLine() ) != null )
                        {
                            TranslationConvert translation = JsonConvert.DeserializeObject<TranslationConvert>(line);
                            s = "";
                            foreach ( string str in translation.text )
                            {
                                s += str;
                            }
                        }
                    }
                    return s;
                }
                else
                    return "";
            }
            else
            {
                return "";
            }
        }

        class TranslationConvert
        {
            public string code { get; set; }
            public string lang { get; set; }
            public string[] text { get; set; }
        }

    }
}
