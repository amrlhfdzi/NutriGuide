using NutriGuide.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace NutriGuide.ViewModel
{
    class EyesPageViewModel
    {

        public ObservableCollection<EyesModel> FoodList { get; set; }

        public EyesPageViewModel()
        {

            FoodList = new ObservableCollection<EyesModel>();
            FoodList.Add(new EyesModel { Name = "Carrots", Image = "" });
            FoodList.Add(new EyesModel { Name = "Salmon", Image = "" });
            FoodList.Add(new EyesModel { Name = "Nuts", Image = "" });
            FoodList.Add(new EyesModel { Name = "Beef", Image = "" });
        }
    }
}
