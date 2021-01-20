using ProgMob.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace ProgMob.ViewModel
{
    public class MyDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate UserTemplate { get; set; }
        public DataTemplate AdminTemplate { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            if (Application.Current.Properties["Admin"].ToString().Equals("true"))
            {
                return AdminTemplate;
            }
            else
            {
                return UserTemplate;
            }
        }
    }
}
