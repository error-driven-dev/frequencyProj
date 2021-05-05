using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;





namespace frequenyProj
{
    class Program
    {
        static void Main(string[] args)
        {
            var filename = @"sites.json";
            var filepath = Path.Combine(Environment.CurrentDirectory + filename);    
            Dictionary<string, int> frequencyCounts = new System.Collections.Generic.Dictionary<string, int>();
            
            var rawData = File.ReadAllText(filename).ToString();
            var allCameraListObjects = JsonConvert.DeserializeObject<List<CameraList>>(rawData);

            foreach(var cameraList in allCameraListObjects)
            {
                foreach (var camera in cameraList.cameras)
                {
                    if (frequencyCounts.ContainsKey(camera))
                    {
                        frequencyCounts[camera] = ++frequencyCounts[camera];
                    }
                    else
                    {
                        frequencyCounts.Add(camera, 1);
                    }
                }
            }
            var sortedFrequencyCounts = from v in frequencyCounts  orderby v.Key orderby v.Value descending select v;
            var top10 = sortedFrequencyCounts.Take(10);

            Console.WriteLine("***********  TOP 10 ********************");
            foreach (KeyValuePair<string, int> total in top10)
            {
                Console.WriteLine("Camera: {0} Count: {1}", total.Key, total.Value);
            }


            Console.WriteLine("\n\n*********** ALL RESULTS IN DECENDING ORDER ********************");
            foreach (KeyValuePair<string, int> total in sortedFrequencyCounts)
            {
                Console.WriteLine("Camera: {0} Count: {1}", total.Key, total.Value);
            }


       



        }
    }
}
