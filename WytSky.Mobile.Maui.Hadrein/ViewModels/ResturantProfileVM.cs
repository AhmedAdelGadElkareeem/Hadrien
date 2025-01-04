

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using WytSky.Mobile.Maui.Hadrein.Helpers;
using WytSky.Mobile.Maui.Hadrein.Dtos;
using WytSky.Mobile.Maui.Hadrein.Views;
using WytSky.Mobile.Maui.Hadrein.Models;
using WytSky.Mobile.Maui.Hadrein.AppResources;
using WytSky.Mobile.Maui.Hadrein.Views.User;

namespace WytSky.Mobile.Maui.Hadrein.ViewModels
{
    public partial class ResturantProfileVM : BaseViewModel
    {
        [ObservableProperty]
        public string currencyName;

        #region Cart page properties
        [ObservableProperty]
        public int totalItems = 0;

        [ObservableProperty]
        private double totalPrice = 0;

        [ObservableProperty]
        private double subTotal = 0;

        [ObservableProperty]
        private double delivery = 20;

        [ObservableProperty]
        private double vat = 20;

        [ObservableProperty]
        private double bounsToDelivery = 20;
        /*public double BounsToDelivery
        {
            get => bounsToDelivery;
            set
            {
                value = bounsToDelivery;
                OnPropertyChanged("BounsToDelivery");
                TotalPrice = 0;
                TotalPrice = SubTotal + Vat + BounsToDelivery + Delivery;
            }
            // set => SetProperty(ref bounsToDelivery, value);
        }*/


        #endregion

        #region Checkout Page properties
        [ObservableProperty]
        private int discount = 0;

        [ObservableProperty]
        private ObservableCollection<StOrderStageBase> orderStage;
        #endregion

        #region Items page properties
        [ObservableProperty]
        private ObservableCollection<StItem> items;
        [ObservableProperty]
        private ObservableCollection<StItem> tempItems;

        [ObservableProperty]
        private ObservableCollection<StCatgeory> categories;

        [ObservableProperty]
        public string itemsSearchText;

        [ObservableProperty]
        public bool isListVisible = false;
        #endregion

        // Checkout Page
        public ResturantProfileVM()
        {
            CalculatePrices();
        }

        [RelayCommand(CanExecute = nameof(CanExecute))]
        public async Task SelectedCategory(StCatgeory obj)
        {
            try
            {
                foreach (var item in Categories)
                {
                    item.IsSelected = false;

                    //if (App.Current.Resources.TryGetValue("Gray200", out var grayColor))
                    //    item.TextColor = (Color)grayColor;
                }

                obj.IsSelected = true;
                //if (App.Current.Resources.TryGetValue("PrimaryColor", out var primaryColor))
                //    obj.TextColor = (Color)primaryColor;

                if (obj.HasSubCatgeory.Value)
                {
                    var dict = new Dictionary<string, string>()
                    {
                      { "ParentID", obj.CatgeoryID?.ToString() ?? ""}
                    };
                    var res = await APIs.ServiceApp.GetAll<StCatgeory>("StCatgeory", filter: dict);
                    if (res.Count > 0)
                    {
                        Categories = res;
                        Items = new ObservableCollection<StItem>(); // empty items when click on category
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
                        Items = res;
                    }
                }
            }
            catch (Exception ex)
            {
                ResetUserNameAndPassword();
                string ExceptionMseeage = string.Format(" Error : {0} - {1} ", ex.Message, ex.InnerException != null ? ex.InnerException.FullMessage() : "");
                ExtensionLogMethods.LogExtension(ExceptionMseeage, "", "HomeVM", "SelectedCategory");
            }
        }

        #region Checkout and Cart page methods
        [RelayCommand(CanExecute = nameof(CanExecute))]
        private void IncreaseQuantity(object obj)
        {
            var item = (StItem)obj;
            item.Quantity++;
            item.IsSelected = true;
            item.ItemTotalPrice = (double)item.Price * item.Quantity;

            if (App.CartItems.Count == 0)
            {
                App.CartItems.Add(item);
            }
            else
            {
                if (App.CartItems.Any(x => x.ItemID == item.ItemID))
                    App.CartItems.FirstOrDefault(x => x.ItemID == item.ItemID).Quantity = item.Quantity;
                else
                    App.CartItems.Add(item);
            }

            CalculatePrices();
        }

        [RelayCommand(CanExecute = nameof(CanExecute))]
        private void DecreaseQuantity(object obj)
        {
            CanExecute = false;
            try
            {
                var item = (StItem)obj;
                item.Quantity--;
                item.ItemTotalPrice = (double)item.Price * item.Quantity;

                if (item.Quantity <= 0)
                {
                    item.Quantity = 0;
                    item.IsSelected = false;
                    App.CartItems.Remove(item);
                }

                CalculatePrices();
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
        private void AddBounsToDelivery(object obj)
        {
            if (BounsToDelivery > 0)
            {
                TotalPrice = 0;
                TotalPrice = SubTotal + Vat + BounsToDelivery + Delivery;
            }
        }

        public void CalculatePrices()
        {
            try
            {
                TotalPrice = 0;
                SubTotal = 0;

                // used in cart and checkout pages
                if (App.CartItems.Count > 0)
                {

                    CurrencyName = App.CartItems.FirstOrDefault().CurrencyName;

                    foreach (var item in App.CartItems)
                    {
                        SubTotal += item.ItemTotalPrice;
                    }
                    CartDetailsVisibility = true;
                }
                else
                {
                    CartDetailsVisibility = false;
                }

                TotalPrice = SubTotal + Vat + BounsToDelivery + Delivery;
                TotalItems = App.CartItems.ToList().Where(x => x.IsSelected).Count();
            }
            catch (Exception ex)
            {
                string er = ex.Message;
            }
        }
        #endregion

        #region Checkout page methods
        [RelayCommand(CanExecute = nameof(CanExecute))]
        private async Task PlaceOrder(object obj)
        {
            CanExecute = false;
            try
            {
                if (Settings.IsLogedin)
                {
                    if (HasInternetConnection())
                    {
                        var order = new OrderModel()
                        {
                            AddressDetail = "",
                            Amount = SubTotal.ToString(),
                            ClientID = Settings.ClientId.ToString(),
                            Discount = Discount.ToString(),
                            Email = Settings.ClientEmail,
                            FinalAmount = TotalPrice.ToString(),
                            MapAddress = "",
                            MapLangitude = "",
                            MapLatitude = "",
                            Notes = "",
                            OrderDate = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss"),
                            Phone = Settings.ClientPhone
                        };
                        var resOrder = await APIs.ServiceOrder.SaveNew(order);
                        if (resOrder != null && resOrder.OrderID != null)
                        {
                            try
                            {
                                foreach (var item in App.CartItems)
                                {
                                    var orderItem = new OrderItemModel()
                                    {
                                        OrderID = resOrder.OrderID.ToString(),
                                        ItemID = item.ItemID.ToString(),
                                        Price = item.Price.ToString(),
                                        Discount = item.Discount.ToString(),
                                        Quantity = item.Quantity.ToString(),
                                    };
                                    var resItem = await APIs.ServiceOrderItem.SaveNew(orderItem, false);
                                }
                                if (OrderStage != null && OrderStage.Count > 0)
                                {
                                    foreach (var stage in OrderStage)
                                    {
                                        var OrderStage = new OrderStageModel()
                                        {
                                            OrderID = resOrder.OrderID.ToString(),
                                            DescriptionEn = stage.DescriptionEn.ToString(),
                                            DescriptionAr = stage.DescriptionAr.ToString(),
                                            ShowOrder = stage.ShowOrder,
                                            ProgressValue = stage.ProgressValue,
                                            AdditionalCost = stage.AdditionalCost,
                                            MustClientApprove = stage.MustClientApprove,
                                            MustProviderApprove = stage.MustProviderApprove,
                                            ClientApprove = stage.ClientApprove,
                                            ProviderApprove = stage.ProviderApprove,
                                            ClientNotes = stage.ClientNotes,
                                            ProviderNotes = stage.ProviderNotes.ToString()
                                        };
                                        await APIs.ServiceOrderStage.SaveNew(OrderStage, false);
                                    }
                                }

                                Settings.CartItems = new ObservableCollection<StItem>();
                                App.CartItems = new ObservableCollection<StItem>();

                                MainThread.BeginInvokeOnMainThread(async () =>
                                {
                                    await App.Current.MainPage.Navigation.PopModalAsync();
                                    await App.Current.MainPage.Navigation.PopModalAsync();
                                    await App.Current.MainPage.Navigation.PopModalAsync();
                                    try
                                    {
                                        await App.Current.MainPage.Navigation.PopModalAsync();
                                    }
                                    catch (Exception ex)
                                    {
                                        ExtensionLogMethods.LogExtension(ex, "", "Checkout", "PlaceOrder");
                                    }
                                    try
                                    {
                                        await App.Current.MainPage.Navigation.PopModalAsync();
                                    }
                                    catch (Exception ex)
                                    {
                                        ExtensionLogMethods.LogExtension(ex, "", "Checkout", "PlaceOrder");
                                    }
                                });

                                /*MainThread.BeginInvokeOnMainThread(() =>
                                {
                                    App.Current.MainPage = new MenuPage();
                                });*/

                                /*MainThread.BeginInvokeOnMainThread(() =>
                                {
                                    App.Current.MainPage.Navigation.PopToRootAsync();

                                });*/
                            }
                            catch (Exception ex)
                            {
                                string ExceptionMseeage = string.Format(" Error : {0} - {1} ", ex.Message, ex.InnerException != null ? ex.InnerException.FullMessage() : "");
                                System.Diagnostics.Debug.WriteLine(ExceptionMseeage);
                                ExtensionLogMethods.LogExtension(ExceptionMseeage, "", "Checkout", "PlaceOrder");
                            }
                        }
                        else
                        {
                            Toast.ShowToastError(SharedResources.Msg_Error);
                        }
                    }
                    else
                    {
                        if (Settings.CartItems.Count > 0)
                        {
                            Toast.ShowCustomToast(SharedResources.Text_ThereAreItemsInCart);
                        }
                        else
                        {
                            Settings.CartItems = App.CartItems;
                            Toast.ShowCustomToast(SharedResources.Text_CartItemsSavedLocally);
                        }
                    }
                }
                else
                {
                    MainThread.BeginInvokeOnMainThread(() =>
                    {
                        ResetUserNameAndPassword();
                        Settings.CartItems = App.CartItems;
                        Toast.ShowCustomToast(SharedResources.Text_MustLogin);
                        App.Current.MainPage = new SignInSignUpPage();
                    });
                }
            }
            catch (Exception ex)
            {
                Toast.ShowToastError(SharedResources.Msg_Error);
                string ExceptionMseeage = string.Format(" Error : {0} - {1} ", ex.Message, ex.InnerException != null ? ex.InnerException.FullMessage() : "");
                System.Diagnostics.Debug.WriteLine(ExceptionMseeage);
                ExtensionLogMethods.LogExtension(ExceptionMseeage, "", "RequestServiceVM", "SaveOrder");
            }
            finally
            {
                CanExecute = true;
            }
        }
        public async Task GetOrderStage()
        {
            CanExecute = false;
            try
            {
                var res = await APIs.ServiceOrderStageBase.GetAll();
                if (res != null && res.Count > 0)
                {
                    OrderStage = res;
                }
            }
            catch (Exception ex)
            {
                string ExceptionMseeage = string.Format(" Error : {0} - {1} ", ex.Message, ex.InnerException != null ? ex.InnerException.FullMessage() : "");
                System.Diagnostics.Debug.WriteLine(ExceptionMseeage);
                ExtensionLogMethods.LogExtension(ExceptionMseeage, "", "RequestServiceVM", "getData");
            }
            finally
            {
                CanExecute = true;
            }
        }
        #endregion

        #region Cart page methods
        [RelayCommand(CanExecute = nameof(CanExecute))]
        private async Task CheckOut(object obj)
        {
            CanExecute = false;
            try
            {
                await App.Current.MainPage.Navigation.PushModalAsync(new CheckoutPage());
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
        #endregion

        #region Items page methods
        partial void OnItemsSearchTextChanged(string value)
        {
            if (!string.IsNullOrEmpty(value))
                Items = new ObservableCollection<StItem>(TempItems.Where(x => x.Name.ToLower().Contains(value.ToLower())).ToList());
            else
                Items = TempItems;
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
    }
}
