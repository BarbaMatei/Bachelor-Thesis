using Bachelor_Thesis.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Bachelor_Thesis.Constants
{
    public static class CommonConstants
    {

    }

    public static class PropertyNames
    {
        public const string Timestamp = "Timestamp";
        public const string ProductId = "ProductId";
    }

    public static class StreamGenerationMillisecondsOffset
    {
        public const int MinimumOffset = 100;
        public const int OneSecondOffset = 1000;
        public const int TwoSecondsOffset = 2000;
        public const int ThreeSecondsOffset = 3000;
    }
}
