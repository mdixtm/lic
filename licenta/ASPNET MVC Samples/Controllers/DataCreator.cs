using ASPNET_MVC_Samples.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;

namespace ASPNET_MVC_Samples.Controllers
{
    public  class DataCreator
    {
        public const int MAXCOLUMNS = 598;
        public const int MAXROWS = 1567;
        public const int ARRAYSIZE = 1700;
        public const string FILEPATH = @"C:\Users\Mada\Desktop\secom.csv";

        public static List<int> MissingValuesOnColumns()
        {
            List<int> lMissingNr = new List<int>();
            string[][] lFeatures = ReadFile();
            for (int j = 1; j <= MAXCOLUMNS; j++)
            {
                int lAux = 0;
                for (int i = 1; i < lFeatures.Length; i++)
                {
                    if (lFeatures[i] != null)
                    {
                        if (lFeatures[i][j] == "")
                        {
                            lAux = lAux + 1;
                        }
                    }
                }
                lMissingNr.Add(lAux);
            }
            return lMissingNr;
        }

        public static List<FeatureViewModel> AllFeatures()
        {
            List<FeatureViewModel> lFeatures = new List<FeatureViewModel>();
            List<int> lMissing = MissingValuesOnColumns();
            for (int i=0; i<MAXCOLUMNS; i++)
            {
                lFeatures.Add(SetFeature(MAXROWS - lMissing[i], lMissing[i], i+1));
            }
            return lFeatures;
        }

        public static List<List<DataPoint>> AllPoints()
        {
            List<List<DataPoint>> lPoints = new List<List<DataPoint>>();
            List<FeatureViewModel> lFeatures = new List<FeatureViewModel>();
            List<int> lMissing = MissingValuesOnColumns();
            for (int i = 0; i < MAXCOLUMNS; i++)
            {
                lPoints.Add(AssignPoints(MAXROWS - lMissing[i], lMissing[i]));
            }
            return lPoints;
        }

        private static List<DataPoint> AssignPoints(int x, int y)
        {
            List<DataPoint> lPoints = new List<DataPoint>();
            lPoints.Add(new DataPoint(x, "Existing Values", "Existing Values"));
            lPoints.Add(new DataPoint(y, "Missing Values", "Missing Values"));
            return lPoints;
        }

        private static FeatureViewModel SetFeature(int x, int y, int nr)
        {
            FeatureViewModel feature = new FeatureViewModel();
            feature.FeatureNr = nr;
            return feature;
        }


        public static string[][] ReadFile()
        {
            using (var fs = File.OpenRead(FILEPATH))
            using (var reader = new StreamReader(fs))
            {
                string[][] featureList = new string[ARRAYSIZE][];
                reader.ReadLine();
                var i = 1;
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');
                    featureList[i] = (string[])values.Clone();
                    i = i + 1;
                }
                return featureList;
            }
        }
    }
}