using NutriGuide.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace NutriGuide.ViewModel
{
    class MainPageViewModel
    {
        public ObservableCollection<MainPageModel> BodyPart { get; set; }

        public MainPageViewModel()
        {
            BodyPart = new ObservableCollection<MainPageModel>();
            BodyPart.Add(new MainPageModel { Id = 1, Name = "Eyes", Image = "https://img.freepik.com/free-vector/human-close-up-eyes-area_1308-52726.jpg?w=1380&t=st=1679022306~exp=1679022906~hmac=0936b7dfb459ac79e27766405234dc28120abd18034761fcf90844e13f62b5d7" });
            BodyPart.Add(new MainPageModel { Id = 2, Name = "Brain", Image = "https://img.freepik.com/free-vector/brain-organ-concept-illustration_114360-13061.jpg?w=740&t=st=1679055362~exp=1679055962~hmac=a3adb3f0cf249e6e678c3ffdd7430079d68c501c299873d3861058a3aab9c85a" });
            BodyPart.Add(new MainPageModel { Id = 3, Name = "Skin", Image = "https://img.freepik.com/free-photo/close-up-face-pores-texture_23-2149444110.jpg?w=1060&t=st=1679036684~exp=1679037284~hmac=20b415a0619e92972f4d698202f419b6bd220b9a16e10e43c84fbc641ba1816e" });
            BodyPart.Add(new MainPageModel { Id = 4, Name = "Bones", Image = "https://img.freepik.com/free-vector/xray-human-body-with-internal-organs_1308-109176.jpg?w=900&t=st=1679038841~exp=1679039441~hmac=b3fa5b4275ed11a36e02aa710c1579f09536bae55d4ffc59c2288e34848d38f4" });




        }
    }
}
