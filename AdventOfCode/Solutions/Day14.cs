using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace AdventOfCode.Solutions
{
    public class Day14
    {
        public static FileInfo GetInputFileInfo()
        {
            return new FileInfo("..\\..\\Inputs\\Day14input.txt");
        }

        public static void ProcessInput(StreamReader inputReader)
        {
            var regex = new Regex("(?<name>\\w+) can fly (?<flyV>\\d+) km/s for (?<flyT>\\d+) seconds, but then must rest for (?<restT>\\d+) seconds.");

            var raceTimeLimit = 2503;
            var winningDistance = 0;
            var scores = new Dictionary<string, int>();
            var deer = new List<Reindeer>();

            string inputLine;

            while (!string.IsNullOrEmpty(inputLine = inputReader.ReadLine()))
            {
                var matches = regex.Match(inputLine);

                if (matches.Groups.Count > 3)
                {
                    var name = matches.Groups["name"].Value;
                    var speed = int.Parse(matches.Groups["flyV"].Value);
                    var flyTime = int.Parse(matches.Groups["flyT"].Value);
                    var restTime = int.Parse(matches.Groups["restT"].Value);

                    var reindeer = new Reindeer(name, speed, flyTime, restTime);
                    deer.Add(reindeer);
                    scores[reindeer.Name] = 0;
                }
                else
                {
                    //Bad input.
                }
            }

            for (int t = 1; t <= raceTimeLimit; t++)
            {
                var leadingDistance = 0;

                foreach (var reindeer in deer)
                {
                    reindeer.SimulateToTime(t);
                    var distance = reindeer.GetCurrentDistance();

                    if (distance > leadingDistance)
                    {
                        leadingDistance = distance;
                    }
                }

                //Any leading or tied for the lead get one point.
                foreach (var reindeer in deer.Where(d => d.GetCurrentDistance() == leadingDistance))
                {
                    scores[reindeer.Name]++;
                }

                if (t == raceTimeLimit)
                {
                    winningDistance = leadingDistance;
                }
            }

            Console.WriteLine("Winning Distance (P1): " + winningDistance);
            Console.WriteLine("Winning Score (P2): " + scores.Values.Max());
        }

        private class Reindeer
        {
            public string Name { get; private set; }

            int Speed { get; set; }
            int FlyTime { get; set; }
            int RestTime { get; set; }

            private int curTime;
            private int curDistance;
            private int minutesBeforeRest;
            private int restMinutesNeeded;

            public Reindeer(string name, int speed, int flyTime, int restTime)
            {
                Name = name;
                Speed = speed;
                FlyTime = flyTime;
                RestTime = restTime;

                curTime = 0;
                curDistance = 0;
                minutesBeforeRest = FlyTime;
                restMinutesNeeded = 0;
            }

            public void SimulateToTime(int timeLimit)
            {
                while (curTime < timeLimit)
                {
                    if (minutesBeforeRest > 0)
                    {
                        //Still going strong
                        var timeInterval = Math.Min(timeLimit - curTime, minutesBeforeRest);

                        curDistance += timeInterval * Speed;
                        curTime += timeInterval;
                        minutesBeforeRest -= timeInterval;

                        if (minutesBeforeRest <= 0)
                        {
                            //rest again
                            restMinutesNeeded = RestTime;
                        }
                    }
                    else
                    {
                        //need to rest
                        var timeInterval = Math.Min(timeLimit - curTime, restMinutesNeeded);

                        curTime += timeInterval;
                        restMinutesNeeded -= timeInterval;

                        if (restMinutesNeeded <= 0)
                        {
                            //start again
                            minutesBeforeRest = FlyTime;
                        }
                    }
                }
            }

            public int GetCurrentDistance()
            {
                return curDistance;
            }
        }
    }
}
