
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
 
namespace ConsoleApp1
{

    class Program
    {

        static double Rand(double time, Random rnd)
        {
            double rand = (rnd.NextDouble() * (50 - 10) + 10) + time;
            //double rand = rnd.Next(5, 50) + time;
            return rand;// randomize process
        }

        static void Main(string[] args)
        {

            Random rnd = new Random();

            //buffer
            const int max = 30;
            const int min = 0;


            //band
            const int HIGH = 3;
            const int LOW = 1;
            int band = HIGH;

            //file
            const double dataCapacity = 1.5;
            const double dataFlow = 1; //file plays in 1second

            //safety
            double ratio;
            double test = 0;

            //time
            double currentTime = 0;
            double buffer = 0;
            double startTime = 0;
            double totalTime = 150;

            //checking list
            List<Event> eventList = new List<Event>();

            //two elements of list
            Event bandEvent = new Event(Rand(currentTime, rnd), "Change band");
            eventList.Add(bandEvent);
            Event bufferEvent = new Event(currentTime + (dataCapacity / band), "Change buffer");
            eventList.Add(bufferEvent);

            //writing first lines
            string pathTime = "C:/Users/293275/Desktop/Studia/AISDE/Projekt2/TXT/PlotTime.txt";
            string pathBand = "C:/Users/293275/Desktop/Studia/AISDE/Projekt2/TXT/PlotBand.txt";
            string pathBuffer = "C:/Users/293275/Desktop/Studia/AISDE/Projekt2/TXT/PlotBuffer.txt";
            System.IO.StreamWriter plotTime = new System.IO.StreamWriter(pathTime);
            System.IO.StreamWriter plotBand = new System.IO.StreamWriter(pathBand);
            System.IO.StreamWriter plotBuffer = new System.IO.StreamWriter(pathBuffer);
            plotTime.WriteLine((int)currentTime + "." + (int)((currentTime - (int)currentTime) * 100));
            plotBand.WriteLine((int)band);
            plotBuffer.WriteLine((int)buffer + "." + (int)((buffer - (int)buffer) * 100));

            //main events of project
            while (currentTime < totalTime)
            {
                //sorting our list by time
                eventList = eventList.OrderBy(item => (item.Time)).ToList();
                Event event1 = eventList.First();

                //change band event
                if (event1.Type == "Change band")
                {
                    currentTime = event1.Time;
                    ratio = (currentTime - test) / (dataCapacity / band); // ratio of part to total buffer time

                    //download file
                    buffer = buffer + (ratio * (dataFlow - (dataCapacity / band)));

                    if (buffer > max) buffer = max;
                    if (buffer < min) buffer = min;

                    plotTime.WriteLine((int)currentTime + "." + (int)((currentTime - (int)currentTime) * 100));
                    plotBand.WriteLine((int)band);
                    plotBuffer.WriteLine((int)buffer + "." + (int)((buffer - (int)buffer) * 100));

                    if (band == HIGH) band = LOW;
                    else band = HIGH;

                    bandEvent = new Event(Rand(currentTime, rnd), "Change band");
                    eventList.Remove(eventList.Last()); //deleting buffer in progress
                    eventList.Add(bandEvent); //add new band event

                    bufferEvent = new Event(currentTime + (dataCapacity / band), "Change buffer"); // add new buffer with new band
                    eventList.Add(bufferEvent);


                    //change buffor event
                }
                else if (event1.Type == "Change buffer")
                {
                    //changing time
                    startTime = currentTime;
                    currentTime = event1.Time;

                    test = event1.Time;
                    

                    //new buffer
                    bufferEvent = new Event(currentTime + (dataCapacity / band), "Change buffer");
                    eventList.Add(bufferEvent);

                    //capacity of buffer
                    buffer = buffer + (dataFlow - (dataCapacity / band));

                    //checking max or minimum
                    if (buffer > max) buffer = max;
                    if (buffer < min) buffer = min;

                }

                //writing next lines 
                plotTime.WriteLine((int)currentTime + "." + (int)((currentTime - (int)currentTime) * 100));
                plotBand.WriteLine((int)band);
                plotBuffer.WriteLine((int)buffer + "." + (int)((buffer - (int)buffer) * 100));



                //remove checked element of list
                eventList.Remove(eventList.First());
            }
            plotTime.Close();
            plotBuffer.Close();
            plotBand.Close();

            Process.Start("C:/Program Files/MATLAB/R2018b/bin/matlab.exe", "/r Plot");
        }

    }
}












