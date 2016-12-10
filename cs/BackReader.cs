using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.IO;

namespace cheWeiXin
{
    public class BackReader
    {
        public static Dictionary<string, string> get(string jsonText)
        {
            JsonTextReader reader = new JsonTextReader(new StringReader(jsonText));

            Dictionary<string, string> dicback = new Dictionary<string, string>();
            string PropertyName = "";
            while (reader.Read())
            {
                //if (reader.Value != null)
                //{
                //    dicback.Add(reader.ValueType.ToString(), reader.Value.ToString());
                //}
           
                
                if (reader.Value != null)  {
                    if(reader.TokenType == JsonToken.PropertyName){
                        PropertyName = reader.Value.ToString();
                        dicback.Add(reader.Value.ToString(),"");
                        
                    }else{
                        dicback[PropertyName] = reader.Value.ToString();
                    }
                }
             
            }

            return dicback;
        }
    }
}
