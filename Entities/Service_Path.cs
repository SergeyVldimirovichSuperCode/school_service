using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace school_service.Entities
{
    public partial class Service
    {
        public string DiscountText
        {
            get
            {
                if (Discount == null || Discount == 0)
                {
                    return "";
                }
                else
                {
                    return $"* скидка {Discount * 100}%";
                }
            }
        }
        public Visibility DiscountVisibility
        {
            get
            {
                if (Discount == 0 || Discount == null)
                    return Visibility.Collapsed;
                else
                    return Visibility.Visible;
            }
        }
        public string Stri
        {
            get
            {
                if (Discount == 0 || Discount == null)
                    return "";
                else
                    return "Strikethrough";
            }
        }
        public string Cost_s
        {
            get
            {
                return $"{Math.Round(Double.Parse(Cost.ToString()), 2)}";
            }
        }
        public string TotalCost
        {
            get
            {
                if (Discount == 0 || Discount == null)
                {
                    return $"{Cost:N2} рублей за {DurationInSeconds / 60} Минут";
                }
                else
                {

                    return $"{Double.Parse(Cost.ToString()) - (Double.Parse(Cost.ToString()) * Discount) } рублей за {DurationInSeconds / 60} минут";
                }
            }
        }
    }
}
