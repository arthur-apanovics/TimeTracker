﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace Core.Tests
{
    public static class MockData
    {
        private const string MockDataFilename = "mock_data.csv";

        private static readonly List<string> Buzzwords = new();
        private static readonly List<string> Slogans = new();
        private static readonly List<string> Lorems = new();
        private static readonly List<string> LoremSentences = new();
        private static readonly Random Seed = new();

        static MockData()
        {
            LoadMockData();
        }

        /// <summary>
        /// e.g. "Front-line multi-state projection"
        /// </summary>
        public static string Buzzword => GetRandom(Buzzwords);
        /// <summary>
        /// e.g. "reinvent frictionless ROI"
        /// </summary>
        public static string Slogan => GetRandom(Slogans);
        /// <summary>
        /// e.g. "turpis integer aliquet massa id lobortis convallis"
        /// </summary>
        public static string Lorem => GetRandom(Lorems);
        /// <summary>
        /// e.g. "Suspendisse potenti. Cras in purus eu magna vulputate luctus..."
        /// </summary>
        public static string LoremSentence => GetRandom(LoremSentences);

        private static string GetRandom(IList<string> list)
        {
            return list[Seed.Next(list.Count)];
        }

        private static void LoadMockData()
        {
            using var reader = new StreamReader(MockDataFilename);
            var csvParser = new Regex(",(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))");

            reader.ReadLine(); // skip headers
            while (!reader.EndOfStream)
            {
                var line = csvParser.Split(reader.ReadLine() ?? string.Empty);

                Buzzwords.Add(line[0]);
                Slogans.Add(line[1]);
                Lorems.Add(line[2]);
                LoremSentences.Add(line[3]);
            }

            reader.Close();
        }
    }
}