using System;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Input;

namespace INRangeLocal
{
    internal class MainPageVM
    {
        const string COUMADIN_VALUES_FILE_NAME = "CoumadinValues.txt";


        List<float> CoumadinValues { get; set; }
        float CurrentCoumadinValue { get => CoumadinValues.First(); }


        public MainPageVM()
        {
            CoumadinValues = new List<float>();
            ReadCoumadinValueFromFile();
        }

        private void ReadCoumadinValueFromFile()
        {
            var currentDirectory = Directory.GetCurrentDirectory();
            var coumadinValuesPath = Path.Combine(currentDirectory, COUMADIN_VALUES_FILE_NAME);

            if (File.Exists(coumadinValuesPath))
            {
                using (StreamReader sr = new StreamReader(coumadinValuesPath))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        if(float.TryParse(line, out float coumadinValue))
                        {
                            CoumadinValues.Add(coumadinValue);
                        }
                    }
                }
            }

        }

        private void WriteCoumadinValuetoFile()
        {
            var currentDirectory = Directory.GetCurrentDirectory();
            var coumadinValuesPath = Path.Combine(currentDirectory, COUMADIN_VALUES_FILE_NAME);

            using (StreamWriter sr = new StreamWriter(coumadinValuesPath))
            {
                foreach (var coumadinValue in CoumadinValues)
                {
                    sr.WriteLine(coumadinValue);
                }
            }
        }

        ~MainPageVM() 
        {
            WriteCoumadinValuetoFile();
        }
    }
}