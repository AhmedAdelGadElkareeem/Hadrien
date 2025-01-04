using System;
using System.Collections.Generic;
using System.ComponentModel;
using WytSky.Mobile.Maui.Hadrein.Helpers;

namespace WytSky.Mobile.Maui.Hadrein.Dtos
{
    public class CategoryModel : INotifyPropertyChanged
    {
        public int Id { get; set; }

        private int _Quantity = 0;
        public int Quantity
        {
            get => _Quantity;
            set => SetProperty(ref _Quantity, value);
        }

        private int _Price = 10;
        public int Price
        {
            get => _Price;
            set => SetProperty(ref _Price, value);
        }

        private bool _IsSelected = false;
        public bool IsSelected
        {
            get => _IsSelected;
            set => SetProperty(ref _IsSelected, value);
        }

        private int _ItemTotalPrice = 0;
        public int ItemTotalPrice
        {
            get => _ItemTotalPrice;
            set => SetProperty(ref _ItemTotalPrice, value);
        }


        public Nullable<decimal> DiscountAmount { get; set; }
        public Nullable<decimal> DiscountPercentage { get; set; }
        public string ParentPicStockControllername { get; set; }
        public string PicStockControllername { get; set; }
        public string PicStockPictureFileName { get; set; }
        public string ParentNameAr { get; set; }
        public Nullable<long> CatgeoryID { get; set; }
        public Nullable<long> PicStockID { get; set; }
        public Nullable<long> ParentID { get; set; }
        public Nullable<bool> HasSubCatgeory { get; set; }
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public string DescriptionEn { get; set; }
        public string DescriptionAr { get; set; }
        public string Picurl1 { get; set; }
        public Nullable<int> ShowOrder { get; set; }
        public Nullable<double> Discount { get; set; }
        public System.Collections.ObjectModel.ObservableCollection<Dtos.StItem> Items { get; set; }
        public string Name { get { return Settings.Language == "ar" ? NameAr : NameEn; } }
        public string Description { get { return Settings.Language == "ar" ? DescriptionAr : DescriptionEn; } }
        public string CatgeoryPicurl1 { get { return PicStockControllername + "-" + PicStockID + "-" + PicStockPictureFileName; } }
        public string CatgeoryImageUrl { get { return string.IsNullOrEmpty(CatgeoryPicurl1) || CatgeoryPicurl1 == "--" ? "default_image.png" : Services.ApiServices.BaseImage + CatgeoryPicurl1; } }
        public string ImageUrl { get { return string.IsNullOrEmpty(Picurl1) || Picurl1 == "--" ? "default_image.png" : Services.ApiServices.BaseImage + Picurl1; } }

        #region SetProperty
        protected bool SetProperty<T>(ref T backingStore, T value,
            [System.Runtime.CompilerServices.CallerMemberName] string propertyName = "",
            Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
