using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClothForArms
{
    public partial class Material
    {
        public string MaterialLogo => Image == null ? "/Resources/picture.png" : Image;

        public string ValidSuppliers
        {
            get
            {
                string suppliers = "";
                List<Supplier> suppliersList = Supplier.ToList();
                if (suppliersList != null && suppliersList.Count() > 0)
                {
                    for (int i = 0; i < suppliersList.Count(); i++)
                    {
                        suppliers += suppliersList[i];
                        if (suppliersList.Last() == suppliersList[i]) suppliers += ".";
                        else suppliers += ", ";
                    }
                }
                else suppliers = "Отсутствуют.";
                return suppliers;
            }
        }

        public string ValidColor
        {
            get
            {
                if (CountInStock < MinCount)
                {
                    return "#f19292";
                }
                else if (CountInStock > MinCount * 4)
                {
                    return "#ffba01";
                }
                return "";
            }
        }

    }

}
