using System;
using System.Collections.Generic;
using Firebase.Auth;
using System.Text;
using Firebase;
using Firebase.Database;
using Firebase.Database.Query;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using NutriGuide.Models;
using Newtonsoft.Json;
using Firebase.Storage;
using System.IO;

namespace NutriGuide
{
    class FirebaseHelper
    {
        FirebaseClient firebase = new FirebaseClient("https://nutriguide-bec1f-default-rtdb.asia-southeast1.firebasedatabase.app/");
        string WebAPIkey = "AIzaSyBTl0uu5k1J57tPUBgpxBuCoMYzhv4E5Io";
        FirebaseStorage firebaseStorage = new FirebaseStorage("nutriguide-bec1f.appspot.com");

        FirebaseAuthProvider authProvider;

        public FirebaseHelper()
        {
            authProvider = new FirebaseAuthProvider(new FirebaseConfig(WebAPIkey));

        }

        public async Task<bool> Register(string email, string password)
        {
            var token = await authProvider.CreateUserWithEmailAndPasswordAsync(email, password);
            if (!string.IsNullOrEmpty(token.FirebaseToken))
            {
                return true;
            }
            return false;
        }

        public async Task<string> Upload(Stream img, string fileName)
        {
            //var image = await firebaseStorage.Child("Images").Child(fileName).PutAsync(img);
            //return image;
            return "";
        }


        public async Task AddRecord(Schedule schedule)
        {
            await firebase.Child("MealRecords").PostAsync(schedule);
        }

        //public async Task AddMeal(Meal meal)
        //{
        //    await firebase.Child("MealList").PostAsync(meal);
        //}

        //public async Task AddMeal(Meal meal, Stream imageStream, Stream imageStream2)
        //{
        //    // Upload image to Firebase Storage
        //    string imageName = Guid.NewGuid().ToString();
        //    var task = firebaseStorage.Child("images").Child(imageName).PutAsync(imageStream);
        //    var imageUrl = await task;


        //    // Attach image URL to meal object
        //    meal.Image = imageUrl;

        //    // Add meal object to Firebase Realtime Database
        //    await firebase.Child("MealList").PostAsync(meal);
        //}

        public async Task AddMeal(Meal meal, Stream imageStream, Stream imageStream2)
        {
            // Upload image to Firebase Storage
            string imageName = Guid.NewGuid().ToString();
            var task = firebaseStorage.Child("images").Child(imageName).PutAsync(imageStream);
            var imageUrl = await task;

            string mealImageName = Guid.NewGuid().ToString();
            var mealImageTask = firebaseStorage.Child("images").Child(mealImageName).PutAsync(imageStream2);
            var mealImageUrl = await mealImageTask;


            // Attach image URL to meal object
            meal.Image = imageUrl;
            meal.Image2 = mealImageUrl;

            // Add meal object to Firebase Realtime Database
            await firebase.Child("MealList").PostAsync(meal);
        }

        //public async Task AddMeal(Meal meal, List<Stream> imageStreams)
        //{
        //    // Upload images to Firebase Storage
        //    var imageUrls = new List<string>();
        //    foreach (var imageStream in imageStreams)
        //    {
        //        string imageName = Guid.NewGuid().ToString();
        //        var task = firebaseStorage.Child("images").Child(imageName).PutAsync(imageStream);
        //        var imageUrl = await task;
        //        imageUrls.Add(imageUrl);
        //    }

        //    // Attach image URLs to meal object
        //    meal.Images = imageUrls;

        //    // Add meal object to Firebase Realtime Database
        //    await firebase.Child("MealList").PostAsync(meal);
        //}


        public async Task<List<Meal>> GetMealsByCategory(string category)
        {
            return (await firebase
                .Child("MealList")
                .OrderBy("Category")
                .EqualTo(category)
                .OnceAsync<Meal>()).Select(item => new Meal
                {
                    Name = item.Object.Name,
                    Image = item.Object.Image,
                    Description = item.Object.Description,
                    Benefit = item.Object.Benefit,
                    Disadvantage = item.Object.Disadvantage,
                    MealName = item.Object.MealName,
                    Image2 = item.Object.Image2,
                    Ingredient = item.Object.Ingredient,
                    Instruction = item.Object.Instruction,
                    Id = item.Key

                }).ToList();
        }

        //public async Task<List<Meal>> GetAll()
        //{
        //    return (await firebase.Child(nameof(Meal)).OnceAsync<Meal>()).Select(item => new Meal
        //    {
        //        Name = item.Object.Name,
        //        Image = item.Object.Image,
        //        Description = item.Object.Description,
        //        Benefit = item.Object.Benefit,
        //        Disadvantage = item.Object.Disadvantage,
        //        MealName = item.Object.MealName,
        //        Image2 = item.Object.Image2,
        //        Ingredient = item.Object.Ingredient,
        //        Instruction = item.Object.Instruction,
        //        Id = item.Key
        //    }).ToList();
        //}

        public async Task<List<Meal>> GetAllByName(string name, string category)
        {
            return (await firebase
                 .Child("MealList")
                 .OrderBy("Category")
                 .EqualTo(category)
                 .OnceAsync<Meal>()).Select(item => new Meal
                 {
                     Name = item.Object.Name,
                     Image = item.Object.Image,
                     Description = item.Object.Description,
                     Benefit = item.Object.Benefit,
                     Disadvantage = item.Object.Disadvantage,
                     MealName = item.Object.MealName,
                     Image2 = item.Object.Image2,
                     Ingredient = item.Object.Ingredient,
                     Instruction = item.Object.Instruction,
                     Id = item.Key
                 }).Where(c => c.Name.ToLower().Contains(name.ToLower())).ToList();

        }

        public async Task<List<Meal>> GetAll()
        {
            return (await firebase
                .Child("MealList")
                .OnceAsync<Meal>()).Select(item => new Meal
                {
                    Name = item.Object.Name,
                    Image = item.Object.Image,
                    Description = item.Object.Description,
                    Benefit = item.Object.Benefit,
                    Disadvantage = item.Object.Disadvantage,
                    MealName = item.Object.MealName,
                    Image2 = item.Object.Image2,
                    Ingredient = item.Object.Ingredient,
                    Instruction = item.Object.Instruction,
                    Id = item.Key
                }).ToList();
        }





        public async Task<List<Schedule>> GetAllSchedule()
        {
            return (await firebase
                .Child("MealRecords")
                .OnceAsync<Schedule>()).Select(item => new Schedule
                {
                    DateRecorded = item.Object.DateRecorded,
                    Title = item.Object.Title,
                    Description = item.Object.Description,
                    Hours = item.Object.Hours,
                    Nutrient = item.Object.Nutrient,
                    LunchTitle = item.Object.LunchTitle,
                    LunchDescription = item.Object.LunchDescription,
                    LunchHours = item.Object.LunchHours,
                    LunchNutrient = item.Object.LunchNutrient,
                    DinnerTitle = item.Object.DinnerTitle,
                    DinnerDescription = item.Object.DinnerDescription,
                    DinnerHours = item.Object.DinnerHours,
                    DinnerNutrient = item.Object.DinnerNutrient,
                    Id = item.Key

                }).ToList();
        }

        public async Task<Schedule> GetById(string id)
        {
            var schedule = await firebase.Child("MealRecords").Child(id).OnceSingleAsync<Schedule>();
            return schedule;
        }

        public async Task<Meal> GetByIdMeal(string id)
        {
            var lists = await firebase.Child("MealList").Child(id).OnceSingleAsync<Meal>();
            return lists;
        }






        // Add a new meal
        //public async Task<bool> AddMeal(Schedule meal)
        //{
        //    try
        //    {
        //        await firebase
        //            .Child("Meals")
        //            .PostAsync(meal);

        //        return true;
        //    }
        //    catch
        //    {
        //        return false;
        //    }
        //}

        //Update an existing meal
        public async Task<bool> Update(Meal meal)
        {
            try
            {
                await firebase
                    .Child("MealList")
                    .Child(meal.Id)
                    .PutAsync(meal);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> Updates(Meal meal, Stream newImageStream2)
        {
            if (newImageStream2 != null)
            {
                // Upload the new image to Firebase Storage
                string newImageName = Guid.NewGuid().ToString();
                var task = firebaseStorage.Child("images").Child(newImageName).PutAsync(newImageStream2);
                var newImageUrl = await task;



                // Replace the URL of the old image with the URL of the new image in the meal object
                meal.Image2 = newImageUrl;
            }

            try
            {
                await firebase
                    .Child("MealList")
                    .Child(meal.Id)
                    .PutAsync(meal);

                return true;
            }
            catch
            {
                return false;
            }
        }
        public async Task<bool> UpdateMeal(Schedule schedule)
        {
            try
            {
                await firebase
                    .Child("MealRecords")
                    .Child(schedule.Id)
                    .PutAsync(schedule);

                return true;
            }
            catch
            {
                return false;
            }
        }


        //public async Task<bool> Update(Meal meal)
        //{
        //    try
        //    {
        //        // Get a reference to the meal to update
        //        var mealToUpdate = firebase.Child("MealList").Child(meal.Id);

        //        // Update the fields of the meal
        //        await mealToUpdate.Child("Name").PutAsync(meal.Name);
        //        await mealToUpdate.Child("Description").PutAsync(meal.Description);
        //        await mealToUpdate.Child("Image").PutAsync(meal.Image);

        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //        return false;
        //    }
        //}


        //public async Task<bool> Update(Meal meal)
        //{
        //    await firebase.Child(nameof(Meal) + "/" + meal.Id).PutAsync(JsonConvert.SerializeObject(meal));
        //    return true;
        //}



        // Delete an existing meal
        //public async Task<bool> DeleteMeal(string id)
        //{
        //    try
        //    {
        //        await firebase
        //            .Child("Meals")
        //            .Child(id)
        //            .DeleteAsync();

        //        return true;
        //    }
        //    catch
        //    {
        //        return false;
        //    }
        //}


        //public async Task<bool> DeleteMeal(string id)
        //{
        //    await firebase.Child(nameof(Meal) + "/" + id).DeleteAsync();
        //    return true;
        //}

        public async Task<bool> DeleteMeal(string mealId)
        {
            try
            {
                var toDeleteMeal = (await firebase.Child("MealList").OnceAsync<Meal>()).FirstOrDefault(a => a.Object.Id == mealId);
                await firebase.Child("MealList").Child(toDeleteMeal.Key).DeleteAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return false;
            }
        }

        public async Task<bool> DeleteMealPlan(string scheduleId)
        {
            try
            {
                var toDeleteMeal = (await firebase.Child("MealRecords").OnceAsync<Schedule>()).FirstOrDefault(a => a.Object.Id == scheduleId);
                await firebase.Child("MealRecords").Child(toDeleteMeal.Key).DeleteAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return false;
            }
        }






    }


}
