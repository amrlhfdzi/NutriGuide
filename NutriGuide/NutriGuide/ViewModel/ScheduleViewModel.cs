using NutriGuide.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace NutriGuide.ViewModel
{
    class ScheduleViewModel
    {

        public ObservableCollection<Schedule> schedule { get; set; }
        public Command<DateTime> ViewMealPlanCommand { get; internal set; }
    }
}
