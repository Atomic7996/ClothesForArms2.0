using System;

namespace WSUniversalLib
{
    public class Calculation
    {
        public static int GetQuantityForProduct(int productType, int materialType, int count, float width, float length)
        {
            if (count <= 0 || width <= 0 || length <= 0)
            {
                return -1;
            }

            float prodTypeCoef = 0;
            float matTypeCoef = 0;

            switch (productType)
            {
                case 1:
                    prodTypeCoef = 1.1f;
                    break;
                case 2:
                    prodTypeCoef = 2.5f;
                    break;
                case 3:
                    prodTypeCoef = 8.43f;
                    break;
                default:
                    return -1;
            }

            switch (materialType)
            {
                case 1:
                    matTypeCoef = 1 - 0.3f / 100;
                    break;
                case 2:
                    matTypeCoef = 1 - 0.12f / 100;
                    break;
                default:
                    return -1;
            }

            return (int)Math.Ceiling(count * width * length * prodTypeCoef / matTypeCoef);
        }
    }
}
