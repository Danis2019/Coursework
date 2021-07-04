using System;
using System.Collections.Generic;
using System.IO;

namespace kursach
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = "";
            string path1 = @"C:\Users\Acer\Desktop\Input.txt";
            string path2 = @"C:\Users\Acer\Desktop\Output.txt";
            StreamReader sr = new StreamReader(path1);
            StreamWriter sw = new StreamWriter(path2, true);
            int lol = 4;
            input = sr.ReadToEnd();
            sr.Close();            
            string[] inp = input.Split(" ");
            int kol_vo = Convert.ToInt32(Math.Pow(lol, 2));
            int[] inputing = new int[kol_vo];
            for (int i = 0; i < inp.Length; i++)
            {
                inputing[i] = Convert.ToInt32(inp[i]);
            }
            // Создаем двумерный список / наш граф 
            var graph = new List<List<int>>();
            // Наша будущая популяция
            var population = new List<List<int>>();
            //Population population;
            Random rnd = new Random();
            //-----------------------
            for (int i = 0; i < lol; i++)
            {
                graph.Add(new List<int>());
                for (int j = 0; j < lol; j++)
                {
                    graph[i].Add(inputing[(i * lol + j)]);
                }

            }
            //----------------------------------------------
         

            /*graph[0].Add(0);
            graph[0].Add(1);
            graph[0].Add(1);
            graph[0].Add(1);

            graph.Add(new List<int>());
            graph[1].Add(1);
            graph[1].Add(2);
            graph[1].Add(3);
            graph[1].Add(2);

            graph.Add(new List<int>());
            graph[2].Add(2);
            graph[2].Add(3);
            graph[2].Add(2);
            graph[2].Add(4);

            graph.Add(new List<int>());
            graph[3].Add(2);
            graph[3].Add(3);
            graph[3].Add(2);
            graph[3].Add(0);*/

            // Двудольный граф в первую популяцию
            List<List<int>> To_Population(List<List<int>> population) 
            {
                for(int i = 0 ; i < 4; i++)
                {
                    
                    population.Add(new List<int>());
                    for (int x = 0; x < 4; x++)
                    {
                        
                    int Numb = rnd.Next(0, 4);
                    while (population[i].Contains(Numb))
                    {
                        Numb = rnd.Next(0, 4);
                    }
                    population[i].Add(Numb);
                    }
                }
                return population;
            }


            List<List<int>> pop = To_Population(population);
            int min = 0;
            Population population1 = new Population(pop);
            int r = 0;
            int min_iter = 0;
            while(min_iter < 10)
            {
                int min_last = min;
                r += 1;
                Console.WriteLine("Старая популяция");
                population1.All_Chromosomes(population1.MyPopulation[0], population1.MyPopulation[1], population1.MyPopulation[2], population1.MyPopulation[3]);

                Tuple<int,int,int,int, List<List<int>>> Min_and_Max = population1.Min_Max_Chromosome(population1,graph);
                Console.WriteLine("Итерация " + r + " Минимальное паросочетание:" + Min_and_Max.Item1);
                min = Min_and_Max.Item1;
                if (min_last == min) 
                    {
                        min_iter += 1;
                    }
                //Console.WriteLine("index_Min:" + Min_and_Max.Item3);
                //Console.WriteLine("Max:" + Min_and_Max.Item2);
                //Console.WriteLine("index_Max:" + Min_and_Max.Item4);

                //List<List<int>> index_list2 = population1.MyPopulation.GetRange(0, population1.MyPopulation.Count);

                Tuple<List<int>, List<List<int>>, Population> old_population_strong_individual = population1.Delete_Worst_and_Find_Best(population1, Min_and_Max.Item5, Min_and_Max.Item3, Min_and_Max.Item4);

                List<List<int>> new_pop = population1.Create_new_population(old_population_strong_individual.Item2, old_population_strong_individual.Item1, population1, graph);

                Population population2 = new Population(new_pop);

                Console.WriteLine("Новая популяция");

                population2.All_Chromosomes(population2.MyPopulation[0], population2.MyPopulation[1], population2.MyPopulation[2], population2.MyPopulation[3]);

                population2 = population2.Mutation(population2, graph);

                population1 = population2;
            }
            Console.WriteLine("Финальная популяция:");
            population1.All_Chromosomes(population1.MyPopulation[0], population1.MyPopulation[1], population1.MyPopulation[2], population1.MyPopulation[3]);
            Console.WriteLine("Количество итераций:" + r);
            Console.WriteLine("Минимальное паросочетание:" + min);
            sw.WriteLine("Минимальное паросочетание:" + min);
            sw.Close();

        }
    }
}
