using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Foodhorn.Controls
{
    public partial class RoundedContentView : StackLayout
    {
        public Color FillColor  
        {  
            get { return (Color)GetValue(FillColorProperty); }  
            set { SetValue(FillColorProperty, value); }  
        } 
        public static readonly BindableProperty FillColorProperty = BindableProperty.Create(
            propertyName: "FillColor",
            returnType: typeof(Color),
            declaringType: typeof(RoundedContentView),
            defaultValue: Color.White,
            defaultBindingMode: BindingMode.OneWay
        ); 
         
        public double RoundedCornerRadius  
        {  
            get { return (double)GetValue(RoundedCornerRadiusProperty); }  
            set { SetValue(RoundedCornerRadiusProperty, value); }  
        }  
        public static readonly BindableProperty RoundedCornerRadiusProperty = BindableProperty.Create(
            propertyName: "RoundedCornerRadius",
            returnType: typeof(double),
            declaringType: typeof(RoundedContentView),
            defaultValue: 3d,
            defaultBindingMode: BindingMode.OneWay
        );  
        
        public bool MakeCircle  
        {  
            get { return (bool)GetValue(MakeCircleProperty); }  
            set { SetValue(MakeCircleProperty, value); }  
        }  
        public static readonly BindableProperty MakeCircleProperty =  BindableProperty.Create(
            propertyName: "MakeCircle",
            returnType: typeof(bool),
            declaringType: typeof(RoundedContentView),
            defaultValue: false,
            defaultBindingMode: BindingMode.OneWay
        );   
        
        public bool HasShadow  
        {  
            get { return (bool)GetValue(HasShadowProperty); }  
            set { SetValue(HasShadowProperty, value); }  
        }  
        public static readonly BindableProperty HasShadowProperty =  BindableProperty.Create(
            propertyName: "HasShadow",
            returnType: typeof(bool),
            declaringType: typeof(RoundedContentView),
            defaultValue: false,
            defaultBindingMode: BindingMode.OneWay
        );  
        
        public Color BorderColor  
        {  
            get { return (Color)GetValue(BorderColorProperty); }  
            set { SetValue(BorderColorProperty, value); }  
        } 
        public static readonly BindableProperty BorderColorProperty =   BindableProperty.Create(
            propertyName: "BorderColor",
            returnType: typeof(Color),
            declaringType: typeof(RoundedContentView),
            defaultValue: Color.Transparent,
            defaultBindingMode: BindingMode.OneWay
        );  
         
        public int BorderWidth  
        {  
            get { return (int)GetValue(BorderWidthProperty); }  
            set { SetValue(BorderWidthProperty, value); }  
        }  
        public static readonly BindableProperty BorderWidthProperty =   BindableProperty.Create(
            propertyName: "BorderWidth",
            returnType: typeof(int),
            declaringType: typeof(RoundedContentView),
            defaultValue: 1,
            defaultBindingMode: BindingMode.OneWay
        );   

        public Thickness CustomMargin  
        {  
            get { return (Thickness)GetValue(CustomMarginProperty); }  
            set { SetValue(CustomMarginProperty, value); }  
        }  
        public static readonly BindableProperty CustomMarginProperty =   BindableProperty.Create(
            propertyName: "CustomMargin",
            returnType: typeof(Thickness),
            declaringType: typeof(RoundedContentView),
            defaultValue: new Thickness(0),
            defaultBindingMode: BindingMode.OneWay
        );  
        public RoundedContentView()
        {
            InitializeComponent();
        }
    }
}
