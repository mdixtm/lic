using ASPNET_MVC_Samples.Models;
using ASPNET_MVC_Samples.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;

namespace ASPNET_MVC_Samples.Controllers
{
    public class DataCreator
    {
        public const int MAXCOLUMNS = 589;
        public const int FEATURES = 590;
        public const int MAXROWS = 1567;
        public const int ARRAYSIZE = 1700;
        public const string FILEPATH = @"C:\Users\Mada\Desktop\original_secom.csv";

   
        public static List<int> MissingValuesOnColumns()
        {
            List<int> lMissingNr = new List<int>();
            string[][] lFeatures = ReadFile();
            for (int j = 0; j < MAXCOLUMNS; j++)
            {
                int lAux = 0;
                for (int i = 1; i < lFeatures.Length; i++)
                {
                    if (lFeatures[i] != null)
                    {
                        if (lFeatures[i][j] == "NaN")
                        {
                            lAux = lAux + 1;
                        }
                    }
                }
                lMissingNr.Add(lAux);
            }
            return lMissingNr;
        }

        private static double CalculateStdDev(IEnumerable<double> values)
        {
            double ret = 0;
            if (values.Count() > 0)
            {
                //Compute the Average      
                double avg = values.Average();
                //Perform the Sum of (value-avg)_2_2      
                double sum = values.Sum(d => Math.Pow(d - avg, 2));
                //Put it all together      
                ret = Math.Sqrt((sum) / (values.Count() - 1));
            }
            return ret;
        }

        public static double StandardDeviation(double[] data)
        {
            double stdDev = 0;
            double sumAll = 0;
            double sumAllQ = 0;

            //Sum of x and sum of x²
            for (int i = 0; i < data.Length; i++)
            {
                double x = data[i];
                sumAll += x;
                sumAllQ += x * x;
            }

            //Mean (not used here)
            //double mean = 0;
            //mean = sumAll / (double)data.Length;

            //Standard deviation
            stdDev = System.Math.Sqrt(
                (sumAllQ -
                (sumAll * sumAll) / data.Length) *
                (1.0d / (data.Length - 1))
                );

            return stdDev;
        }

        public static void StDevOnColumns(ref double[]lStandardDevs, ref double[] lMins,
                                          ref double[]lMaxs, ref double[]lAveranges)
        {
            lStandardDevs = new double[FEATURES];
            lMins = new double[FEATURES];
            lMaxs = new double[FEATURES];
            lAveranges = new double[FEATURES];
            double[][] lFeatures = ReadFileInDoubles();
            string[][] lFeaturesStrings = ReadFile();
            for (int j = 0; j <= MAXCOLUMNS; j++)
            {
                List<double> lElemByColumn = new List<double>();
                for (int i = 1; i < lFeatures.Length; i++)
                {
                    if (lFeatures[i] != null)
                    {
                        if (lFeaturesStrings[i][j] != "NaN")
                        lElemByColumn.Add(lFeatures[i][j]);
                    }
                }
                lStandardDevs[j] = StandardDeviation(lElemByColumn.ToArray());
                lMins[j] = lElemByColumn.Min();
                lMaxs[j] = lElemByColumn.Max();
                lAveranges[j] = lElemByColumn.Average();
            }           
        }

        public static List<FeatureViewModel> AllFeatures()
        {
            List<FeatureViewModel> lFeatures = new List<FeatureViewModel>();
            List<int> lMissing = MissingValuesOnColumns();
            double[] stDevOnColumn = new double[FEATURES];
            double[] lMins = new double[FEATURES];
            double[] lMaxs = new double[FEATURES];
            double[] lAveranges = new double[FEATURES];
            StDevOnColumns(ref stDevOnColumn, ref lMins, ref lMaxs, ref lAveranges);

            for (int i=0; i< MAXCOLUMNS; i++)
            {
                lFeatures.Add(SetFeature(i+1,stDevOnColumn[i+1],lMins[i+1],lMaxs[i+1],lAveranges[i+1]));
            }
            return lFeatures;
        }

        public static List<PointExtensionViewModel> AllPoints()
        {
            List<PointExtensionViewModel> lPointExtension = new List<PointExtensionViewModel>();
            List<DataPoint> lPoints = new List<DataPoint>();
            List<int> lMissing = MissingValuesOnColumns();
            for (int i = 0; i < MAXCOLUMNS; i++)
            {
                lPointExtension.Add(AssignPoints(MAXROWS - lMissing[i], lMissing[i]));
            }
            return lPointExtension;
        }

        private static PointExtensionViewModel AssignPoints(int x, int y)
        {
            PointExtensionViewModel lPointExtVM = new PointExtensionViewModel();
            lPointExtVM.Points = new List<DataPoint>();
            lPointExtVM.Points.Add(new DataPoint(x, "Existing Values", "Existing Values"));
            lPointExtVM.Points.Add(new DataPoint(y, "Missing Values", "Missing Values"));
            return lPointExtVM;
        }

        private static FeatureViewModel SetFeature(int nr, double stDev, double min, double max, double averange)
        {
            FeatureViewModel feature = new FeatureViewModel();
            feature.FeatureNr = nr;
            feature.FeatureInfo = new FeatureInfo();
            feature.FeatureInfo.StDev = stDev;
            feature.FeatureInfo.Minimum = min;
            feature.FeatureInfo.Maximum = max;
            feature.FeatureInfo.Mean = averange;
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

        public static double[][] ReadFileInDoubles()
        {
            using (var fs = File.OpenRead(FILEPATH))
            using (var reader = new StreamReader(fs))
            {
                double[][] featureList = new double[ARRAYSIZE][];
                reader.ReadLine();
                var i = 0;
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');
                    i = i + 1;
                    featureList[i] = new double[FEATURES];
                    for (int j=0; j<=MAXCOLUMNS; j++)
                    {
                        double parsed = 0;
                        bool IsDoubleValue = Double.TryParse(values[j], out parsed);

                        if (IsDoubleValue)
                        {
                            parsed = Convert.ToDouble(values[j]);
                        }
                          
                        featureList[i][j] = parsed;
                    }
                    
                }
                return featureList;
            }

        }
    }
}