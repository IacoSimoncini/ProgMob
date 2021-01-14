using ProgMob.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace ProgMob.ViewModel
{
    public class MyDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate TrueTemplate { get; set; }
        public DataTemplate FalseTemplate { get; set; }
        public DataTemplate TrueAdminTemplate { get; set; }
        public DataTemplate FalseAdminTemplate { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            if (Application.Current.Properties["Admin"].Equals("true"))
            {
                var aux = item as Day;
                if (aux.ifSet)
                {
                    return TrueAdminTemplate;
                }
                else
                {
                    return FalseAdminTemplate;
                }
            }
            else
            {
                var aux = item as Day;
                if (aux.ifSet)
                {
                    return TrueTemplate;
                }
                else
                {
                    return FalseTemplate;
                }
            }
        }
    }
}
