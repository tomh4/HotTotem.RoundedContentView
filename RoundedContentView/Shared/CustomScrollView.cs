using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;


namespace CarouselView.CustomControls
{
    public class CustomScrollView : ScrollView
    {
        public Carousel carouselParent;
        public double placeHolderOffset, spacing;
        public double scrollViewWidth;
        private bool initialised = false;
        private void InitScrollView()
        {
            var layout = (CustomStackLayout)Content;
            var childViewSize = layout.Children[0].Width;
            spacing = layout.Spacing;
            placeHolderOffset = 0;
            var placeHolderCount = (int)Math.Ceiling(scrollViewWidth / childViewSize);
            for (int i = 0; i < placeHolderCount; i++)
            {
                placeHolderOffset += (childViewSize + spacing);
                var startPlaceHolder = new ViewCell
                {
                    View = new BoxView() { WidthRequest = childViewSize, Color = Color.Transparent }
                };
                startPlaceHolder.View.Opacity = 0;
                startPlaceHolder.View.InputTransparent = true;
                var endPlaceHolder = new ViewCell
                {
                    View = new BoxView() { WidthRequest = childViewSize, Color = Color.Transparent }
                };
                endPlaceHolder.View.Opacity = 0;
                endPlaceHolder.View.InputTransparent = true;
                layout.Children.Insert(0, startPlaceHolder.View);
                layout.Children.Add(endPlaceHolder.View);
            }
            var startPosition = scrollViewWidth * carouselParent.SnapPosition - childViewSize / 2;
            if (startPosition < 0)
            {
                startPosition = 0;
            }
            Device.BeginInvokeOnMainThread(async () => await ScrollToAsync(placeHolderOffset - startPosition, 0, false));
        }
        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);
            if (width > 0 && !initialised)
            {
                initialised = true;
                scrollViewWidth = width;
                InitScrollView();
            }
        }
    }
    public class CustomStackLayout : StackLayout
    {
        public Carousel carouselParent;
        protected override void OnChildAdded(Element child)
        {
            base.OnChildAdded(child);
            if (child is View childview)
            {
               //childview.GestureRecognizers.Add(carouselParent.tapGestureRecognizer);
            }
        }
    }
    public class DotButton : BoxView
    {
        public int index;
        public DotButtonsLayout layout;
        public event ClickHandler Clicked;
        public delegate void ClickHandler(DotButton sender);
        public DotButton()
        {
            var clickCheck = new TapGestureRecognizer()
            {
                Command = new Command(() =>
                {
                    if (Clicked != null)
                    {
                        Clicked(this);
                    }
                })
            };
            GestureRecognizers.Add(clickCheck);
        }
    }
    public class DotButtonsLayout : StackLayout
    {
        //This array will hold the buttons
        public DotButton[] dots;
        public DotButtonsLayout(int dotCount, Color dotColor, int dotSize)
        {
            //Create as many buttons as desired.
            dots = new DotButton[dotCount];
            //This class inherits from a StackLayout, so we can stack
            //the buttons together from left to right.
            Orientation = StackOrientation.Horizontal;
            VerticalOptions = LayoutOptions.Center;
            HorizontalOptions = LayoutOptions.Center;
            //Here we create the buttons.
            for (int i = 0; i < dotCount; i++)
            {
                dots[i] = new DotButton
                {
                    HeightRequest = dotSize,
                    WidthRequest = dotSize,
                    Color = dotColor,
                    //All buttons except the first one will get an opacity
                    //of 0.5 to visualize the first one is selected.
                    Opacity = 0.5
                };
                dots[i].index = i;
                dots[i].layout = this;
                Children.Add(dots[i]);
            }
            dots[0].Opacity = 1;
        }
    }
}
