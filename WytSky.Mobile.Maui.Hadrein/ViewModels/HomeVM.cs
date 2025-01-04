using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using WytSky.Mobile.Maui.Hadrein.AppResources;
using WytSky.Mobile.Maui.Hadrein.Dtos;
using WytSky.Mobile.Maui.Hadrein.Views;
using WytSky.Mobile.Maui.Hadrein.Views.SubCategories;

namespace WytSky.Mobile.Maui.Hadrein.ViewModels
{
    public partial class HomeVM : BaseViewModel
    {
        #region Properties

        [ObservableProperty]
        public ObservableCollection<StCatgeory> mainCategories;

        [ObservableProperty]
        public ObservableCollection<StCatgeory> subCategories;

        [ObservableProperty]
        public ObservableCollection<StCatgeory> tempSubCategories;

        #region SubSubCategoriesPage
        [ObservableProperty]
        public ObservableCollection<StCatgeory> subSubCategories;

        [ObservableProperty]
        public ObservableCollection<StCatgeory> tempSubSubCategories;
        #endregion

        [ObservableProperty]
        public ObservableCollection<NearestDto> nearestsList;

        [ObservableProperty]
        public ObservableCollection<StCatgeory> featuredResturants;

        #region Main sub categories page properties
        [ObservableProperty]
        public string searchText;

        [ObservableProperty]
        public bool isListVisible = false;
        #endregion

        #region Sub sub categories page properties
        [ObservableProperty]
        public string subSubCategorySearchText;
        #endregion

        #endregion

        #region Home Page Methods

        public async Task GetCategories()
        {
            try
            {
                SetUserNameAndPassword();
                var dict = new Dictionary<string, string>()
                {
                    { "ParentID","null"}
                };
                var response = await APIs.ServiceApp.GetAll<StCatgeory>("StCatgeory", filter: dict);
                if (response != null)
                {
                    MainCategories = new ObservableCollection<StCatgeory>(response);
                    foreach (var item in MainCategories)
                    {
                        item.IsSelected = false;
                    }
                }
            }
            catch (Exception ex)
            {
                string ExceptionMseeage = string.Format(" Error : {0} - {1} ", ex.Message, ex.InnerException != null ? ex.InnerException.FullMessage() : "");
                System.Diagnostics.Debug.WriteLine(ExceptionMseeage);
                ExtensionLogMethods.LogExtension(ExceptionMseeage, "", "HomeVM", "GetCategories");
            }
        }

        [RelayCommand(CanExecute = nameof(CanExecute))]
        private async Task SelectedMainCategory(StCatgeory obj)
        {
            CanExecute = false;
            try
            {
                var dict = new Dictionary<string, string>()
                {
                   { "ParentID", obj.CatgeoryID?.ToString() ?? ""}
                };
                var response = await APIs.ServiceApp.GetAll<StCatgeory>("StCatgeory", filter: dict);
                if (response != null)
                {
                    SubCategories = new ObservableCollection<StCatgeory>(response);
                    TempSubCategories = SubCategories;
                }
                foreach (var item in MainCategories) { item.isSelected = false; }
                obj.IsSelected = true;

                await App.Current.MainPage.Navigation.PushModalAsync(new MainSubcategoriesPage());
            }
            catch (Exception ex)
            {
                string ExceptionMseeage = string.Format(" Error : {0} - {1} ", ex.Message, ex.InnerException != null ? ex.InnerException.FullMessage() : "");
                ExtensionLogMethods.LogExtension(ExceptionMseeage, "", "", "");
            }
            finally
            {
                CanExecute = true;
            }

        }

        public async Task GetNearestForAllMainCategoryParentID()
        {
            try
            {
                NearestsList = new ObservableCollection<NearestDto>();
                ObservableCollection<StItem> res;
                if (MainCategories != null)
                {
                    foreach (var item in MainCategories)
                    {
                        var dict = new Dictionary<string, string>()
                        {
                            { "ParentID", item.CatgeoryID?.ToString() ?? ""},
                            { "pageSize", "5" } // to get only five items in nearest
                        };
                        var response = await APIs.ServiceApp.GetAll<StCatgeory>("StCatgeory", filter: dict, isLoading: false);
                        if (response != null)
                        {
                            var nearestModel = new NearestDto();
                            var nearest = new ObservableCollection<StCatgeory>(response);

                            nearestModel.CategoryName = $"{AppResources.SharedResources.Text_Nearest}{" "}{item.Name}";
                            nearestModel.Nearests = nearest;
                            NearestsList.Add(nearestModel);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string ExceptionMseeage = string.Format(" Error : {0} - {1} ", ex.Message, ex.InnerException != null ? ex.InnerException.FullMessage() : "");
                ExtensionLogMethods.LogExtension(ExceptionMseeage, "", "HomeVM", "GetItemsByCategoryId");
            }
        }

        [RelayCommand(CanExecute = nameof(CanExecute))]
        public async Task OpenOffers()
        {
            CanExecute = false;
            try
            {
                await App.Current.MainPage.Navigation.PushModalAsync(new OffersPage());
            }
            catch (Exception ex)
            {
                string ExceptionMseeage = string.Format(" Error : {0} - {1} ", ex.Message, ex.InnerException != null ? ex.InnerException.FullMessage() : "");
                ExtensionLogMethods.LogExtension(ExceptionMseeage, "", "HomeVM", "SelectedSubCategory");
            }
            finally
            {
                CanExecute = true;
            }
        }

        #endregion

        #region Main SubCategories Page Methods

        [RelayCommand(CanExecute = nameof(CanExecute))]
        public async Task SelectedMainCategoryFromMainSubCateories(StCatgeory obj)
        {
            CanExecute = false;
            try
            {
                if (obj.HasSubCatgeory != null && obj.HasSubCatgeory.Value)
                {
                    foreach (var item in MainCategories)
                    {
                        item.IsSelected = false;
                    }

                    obj.IsSelected = true;

                    var dict = new Dictionary<string, string>()
                    {
                       { "ParentID", obj.CatgeoryID?.ToString() ?? ""}
                    };
                    var response = await APIs.ServiceApp.GetAll<StCatgeory>("StCatgeory", filter: dict);
                    if (response.Count > 0)
                    {
                        SubCategories = response;
                        TempSubCategories = response;
                    }
                    else
                    {
                        Helpers.Toast.ShowCustomToast(SharedResources.Msg_NoDataFound);
                    }
                }
                else
                {
                    var dictionary = new Dictionary<string, string>()
                    {
                       { "CatgeoryID", obj.CatgeoryID?.ToString() ??""}
                    };
                    var res = await APIs.ServiceItem.GetAll(filter: dictionary);
                    if (res.Count > 0)
                    {
                        var item = SubCategories.FirstOrDefault(x => x.Name.ToLower() == obj.Name);
                        if (item != null) { item.IsSelected = true; }
                        await App.Current.MainPage.Navigation.PushModalAsync(new ItemsPage(res, SubCategories));
                    }
                    else
                    {
                        Helpers.Toast.ShowCustomToast(SharedResources.Msg_NoDataFound);
                    }
                }
            }
            catch (Exception ex)
            {
                string ExceptionMseeage = string.Format(" Error : {0} - {1} ", ex.Message, ex.InnerException != null ? ex.InnerException.FullMessage() : "");
                ExtensionLogMethods.LogExtension(ExceptionMseeage, "", "", "");
            }
            finally
            {
                CanExecute = true;
            }
        }

        [RelayCommand(CanExecute = nameof(CanExecute))]
        public async Task SelectedSubCategory(StCatgeory obj)
        {
            CanExecute = false;
            try
            {
                await NavigateToPage(obj);
            }
            catch (Exception ex)
            {
                string ExceptionMseeage = string.Format(" Error : {0} - {1} ", ex.Message, ex.InnerException != null ? ex.InnerException.FullMessage() : "");
                ExtensionLogMethods.LogExtension(ExceptionMseeage, "", "HomeVM", "SelectedSubCategory");
            }
            finally
            {
                CanExecute = true;
            }
        }

        public async Task NavigateToPage(StCatgeory item, ObservableCollection<StCatgeory> catgeories = null)
        {
            CanExecute = false;
            try
            {
                if (item.HasSubCatgeory != null && item.HasSubCatgeory.Value)
                {
                    var dict = new Dictionary<string, string>()
                    {
                      { "ParentID", item.CatgeoryID?.ToString() ?? ""}
                    };
                    var res = await APIs.ServiceApp.GetAll<StCatgeory>("StCatgeory", filter: dict);
                    if (res.Count > 0)
                    {
                        SubSubCategories = res;
                        TempSubSubCategories = res;
                        if (catgeories != null && catgeories.Count > 0)
                        {
                            SubCategories = catgeories;
                        }
                        if (SubCategories != null && SubCategories.Count > 0)
                        {
                            foreach (var subCategory in SubCategories)
                            {
                                subCategory.IsSelected = false;
                            }
                            var item_ = SubCategories.FirstOrDefault(x => x.Name.ToLower() == item.Name.ToLower());
                            if (item_ != null) { item_.IsSelected = true; }
                        }

                        await App.Current.MainPage.Navigation.PushModalAsync(new SubSubCategoriesPage());
                    }
                    else
                    {
                        Helpers.Toast.ShowCustomToast(SharedResources.Msg_NoDataFound);
                    }
                }
                else
                {
                    var dictionary = new Dictionary<string, string>()
                    {
                       { "CatgeoryID", item.CatgeoryID?.ToString() ?? ""}
                    };

                    foreach (var subCategory in SubSubCategories)
                    {
                        subCategory.IsSelected = false;
                    }

                    var item_ = SubSubCategories.FirstOrDefault(x => x.Name.ToLower() == item.Name.ToLower());
                    if (item_ != null) { item.isSelected = true; }

                    var res = await APIs.ServiceItem.GetAll(filter: dictionary);
                    if (res.Count > 0)
                    {
                        await App.Current.MainPage.Navigation.PushModalAsync(new ItemsPage(res, SubCategories));
                    }
                    else
                    {
                        Helpers.Toast.ShowCustomToast(SharedResources.Msg_NoDataFound);
                    }
                }
            }
            catch (Exception ex)
            {
                string ExceptionMseeage = string.Format(" Error : {0} - {1} ", ex.Message, ex.InnerException != null ? ex.InnerException.FullMessage() : "");
                ExtensionLogMethods.LogExtension(ExceptionMseeage, "", "HomeVM", "NavigateToPage");
            }
            finally
            {
                CanExecute = true;
            }
        }

        [RelayCommand(CanExecute = nameof(CanExecute))]
        public void ChangeToList()
        {
            IsListVisible = true;
        }

        [RelayCommand(CanExecute = nameof(CanExecute))]
        public void ChangeToGrid()
        {
            IsListVisible = false;
        }

        #endregion

        #region Sub sub categories page methods

        [RelayCommand(CanExecute = nameof(CanExecute))]
        public async Task SelectedSubCategoryInSubSubPage(StCatgeory obj)
        {
            CanExecute = false;
            try
            {
                foreach (var subCategory in SubCategories)
                {
                    subCategory.IsSelected = false;
                }

                var item_ = SubCategories.FirstOrDefault(x => x.Name.ToLower() == obj.Name.ToLower());
                if (item_ != null) { item_.IsSelected = true; }


                if (obj.HasSubCatgeory != null && obj.HasSubCatgeory.Value)
                {
                    var dict = new Dictionary<string, string>()
                    {
                      { "ParentID", obj.CatgeoryID?.ToString() ?? ""}
                    };
                    var res = await APIs.ServiceApp.GetAll<StCatgeory>("StCatgeory", filter: dict);
                    if (res.Count > 0)
                    {
                        SubSubCategories = res;
                        TempSubSubCategories = res;
                    }
                    else
                    {
                        Helpers.Toast.ShowCustomToast(SharedResources.Msg_NoDataFound);
                    }
                }
                else
                {
                    var dictionary = new Dictionary<string, string>()
                    {
                       { "CatgeoryID", obj.CatgeoryID?.ToString() ?? ""}
                    };
                    var res = await APIs.ServiceItem.GetAll(filter: dictionary);
                    if (res.Count > 0)
                    {
                        await App.Current.MainPage.Navigation.PushModalAsync(new ItemsPage(res, SubCategories));
                    }
                    else
                    {
                        Helpers.Toast.ShowCustomToast(SharedResources.Msg_NoDataFound);
                    }
                }
            }
            catch (Exception ex)
            {
                string ExceptionMseeage = string.Format(" Error : {0} - {1} ", ex.Message, ex.InnerException != null ? ex.InnerException.FullMessage() : "");
                ExtensionLogMethods.LogExtension(ExceptionMseeage, "", "HomeVM", "SelectedSubSubCategory");
            }
            finally
            {
                CanExecute = true;
            }
        }

        [RelayCommand(CanExecute = nameof(CanExecute))]
        public async Task SelectedSubSubCategory(StCatgeory obj)
        {
            CanExecute = false;
            try
            {
                if (obj.HasSubCatgeory != null && obj.HasSubCatgeory.Value)
                {
                    var dict = new Dictionary<string, string>()
                    {
                      { "ParentID", obj.CatgeoryID?.ToString() ?? ""}
                    };
                    var res = await APIs.ServiceApp.GetAll<StCatgeory>("StCatgeory", filter: dict);
                    if (res.Count > 0)
                    {
                        SubSubCategories = res;
                        TempSubSubCategories = res;
                    }
                    else
                    {
                        Helpers.Toast.ShowCustomToast(SharedResources.Msg_NoDataFound);
                    }
                }
                else
                {
                    foreach (var item in SubSubCategories)
                    {
                        item.IsSelected = false;
                    }

                    var item_ = SubSubCategories.FirstOrDefault(x => x.Name.ToLower() == obj.Name.ToLower());
                    if (item_ != null) { item_.IsSelected = true; }

                    var dictionary = new Dictionary<string, string>()
                    {
                       { "CatgeoryID", obj.CatgeoryID?.ToString() ?? ""}
                    };
                    var res = await APIs.ServiceItem.GetAll(filter: dictionary);
                    if (res.Count > 0)
                    {
                        await App.Current.MainPage.Navigation.PushModalAsync(new ItemsPage(res, SubSubCategories));
                    }
                    else
                    {
                        Helpers.Toast.ShowCustomToast(SharedResources.Msg_NoDataFound);
                    }
                }
            }
            catch (Exception ex)
            {
                string ExceptionMseeage = string.Format(" Error : {0} - {1} ", ex.Message, ex.InnerException != null ? ex.InnerException.FullMessage() : "");
                ExtensionLogMethods.LogExtension(ExceptionMseeage, "", "HomeVM", "SelectedSubSubCategory");
            }
            finally
            {
                CanExecute = true;
            }
        }

        #endregion

        #region Search in main sub categories page
        partial void OnSearchTextChanged(string value)
        {
            if (!string.IsNullOrEmpty(value))
                SubCategories = new ObservableCollection<StCatgeory>(
                    TempSubCategories.Where(x => x.Name.ToLower().Contains(value.ToLower())).ToList());
            else
                SubCategories = TempSubCategories;
        }
        #endregion

        #region Search in sub sub categories page
        partial void OnSubSubCategorySearchTextChanged(string value)
        {
            if (!string.IsNullOrEmpty(value))
                SubSubCategories = new ObservableCollection<StCatgeory>(
                    TempSubSubCategories.Where(x => x.Name.ToLower().Contains(value.ToLower())).ToList());
            else
                SubSubCategories = TempSubSubCategories;
        }
        #endregion
    }
}