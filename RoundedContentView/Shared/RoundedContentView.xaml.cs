using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace HotTotem.RoundedContentView
{
    public partial class RoundedContentView : StackLayout
    {
        /// <summary>
        /// The background color of the rounded ContentView
        /// </summary>
        public Color FillColor  
        {  
            get { return (Color)GetValue(FillColorProperty); }  
            set { SetValue(FillColorProperty, value); }
        }
        /// <summary>
        /// The background color of the rounded ContentView
        /// </summary>
        public static readonly BindableProperty FillColorProperty = BindableProperty.Create(
            propertyName: "FillColor",
            returnType: typeof(Color),
            declaringType: typeof(RoundedContentView),
            defaultValue: Color.White,
            defaultBindingMode: BindingMode.OneWay
        ); 
         /// <summary>
         /// The Corner Radius of the ContentView
         /// Defaults to 0 which is not rounded.
         /// </summary>
        public double CornerRadius  
        {  
            get { return (double)GetValue(CornerRadiusProperty); }  
            set { SetValue(CornerRadiusProperty, value); }  
        }  
        public static readonly BindableProperty CornerRadiusProperty = BindableProperty.Create(
            propertyName: "CornerRadius",
            returnType: typeof(double),
            declaringType: typeof(RoundedContentView),
            defaultValue: 0d,
            defaultBindingMode: BindingMode.OneWay
        );  
        /// <summary>
        /// If set to true, the ContentView will be circular
        /// This means the edges will be rounded as much as necessary 
        /// to form a circle
        /// </summary>
        public bool Circle  
        {  
            get { return (bool)GetValue(CircleProperty); }  
            set { SetValue(CircleProperty, value); }  
        }  
        public static readonly BindableProperty CircleProperty =  BindableProperty.Create(
            propertyName: "Circle",
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
            BackgroundColor = Color.Transparent;
            
        }
    }
}
