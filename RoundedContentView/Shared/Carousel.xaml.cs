using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using CarouselView.CustomControls;

namespace CarouselView
{
    /// <summary>
    /// EventHandler for PositionChanged event.
    /// </summary>
    /// <param name="source">The source-carousel</param>
    /// <param name="e">The new position</param>
    public delegate void PositionChangedEventHandler(object source, PositionChangedEventArgs e);
    /// <summary>
    /// EventArgs for a PosiionChangedEvent
    /// </summary>
    public class PositionChangedEventArgs : EventArgs
    {
        /// <summary>
        /// The new Position
        /// </summary>
        public int Position;
    }
    /// <summary>
    /// A flexible and customizable Carouselview
    /// </summary>
    public partial class Carousel : ContentView
    {
        /// <summary>
        /// Event for changes of the current carousel position.
        /// </summary>
        public event PositionChangedEventHandler OnPositionChanged;
        private double carouselWidth, carouselScrollPosition, carouselContentViewSize = 0;
        private DotButtonsLayout dotLayout;
        //public TapGestureRecognizer tapGestureRecognizer = new TapGestureRecognizer();
        private int carouselPagesCount;
        #region Other Properties
        /// <summary>
        /// The current position of the Carousel
        /// </summary>
        public static readonly BindableProperty SpacingProperty =
           BindableProperty.Create(propertyName: "Spacing",
           returnType: typeof(int),
           declaringType: typeof(Carousel),
           defaultValue: 0,
           defaultBindingMode: BindingMode.TwoWay);
        /// <summary>
        /// The current position of the Carousel
        /// </summary>
        public int Spacing
        {
            get { return (int)GetValue(SpacingProperty); }
            set { SetValue(SpacingProperty, value); }
        }
        /// <summary>
        /// The current position of the Carousel
        /// </summary>
        public static readonly BindableProperty PositionProperty =
           BindableProperty.Create(propertyName: "Position",
           returnType: typeof(int),
           declaringType: typeof(Carousel),
           defaultValue: 0,
           defaultBindingMode: BindingMode.TwoWay,
           propertyChanged: OnPropertyChanged);

        private static async void OnPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (Carousel)bindable;
            var previousPosition = 1 + Math.Round((control.carouselScrollPosition - control.carouselScrollView.placeHolderOffset) / (control.carouselContentViewSize + control.carouselScrollView.spacing));

            if (control.Position != previousPosition)
            {
                int index = control.Position;
                //Set the corresponding page as position of the carousel view
                var snapOffset = control.carouselScrollView.scrollViewWidth * control.SnapPosition - control.carouselContentViewSize / 2;
                if (snapOffset < 0)
                {
                    snapOffset = 0;
                }
                var desiredPosition = control.carouselScrollView.placeHolderOffset + (control.carouselContentViewSize + control.carouselScrollView.spacing) * index;
                desiredPosition -= snapOffset;
                await control.carouselScrollView.ScrollToAsync(desiredPosition, 0, true);
            }
        }
        /// <summary>
        /// The current position of the Carousel
        /// </summary>
        public int Position
        {
            get { return (int)GetValue(PositionProperty); }
            set { SetValue(PositionProperty, value); }
        }
        #endregion
        #region SnappingProperties
        /// <summary>
        /// Enable or disable snapping
        /// </summary>
        public static readonly BindableProperty SnappingProperty =
            BindableProperty.Create(propertyName: "Snapping",
            returnType: typeof(bool),
            declaringType: typeof(Carousel),
            defaultValue: false,
            defaultBindingMode: BindingMode.OneWay);
        /// <summary>
        /// Enable or disable snapping
        /// </summary>
        public bool Snapping
        {
            get { return (bool)GetValue(SnappingProperty); }
            set { SetValue(SnappingProperty, value); }
        }
        /// <summary>
        /// Snapping Mode
        /// </summary>
        public static readonly BindableProperty SnapModeProperty =
            BindableProperty.Create(propertyName: "SnapMode",
            returnType: typeof(SnappingMode),
            declaringType: typeof(Carousel),
            defaultValue: SnappingMode.RollOut,
            defaultBindingMode: BindingMode.OneWay);
        /// <summary>
        /// Set Snap Mode
        /// </summary>
        public SnappingMode SnapMode
        {
            get { return (SnappingMode)GetValue(SnapModeProperty); }
            set { SetValue(SnapModeProperty, value); }
        }
        public enum SnappingMode
        {
            Instant,
            RollOut
        }
        /// <summary>
        /// The relative Snap Position to which the carousel will snap
        /// relatively to the screen, vales between 0 (left) and 1 (right)
        /// </summary>
        public static readonly BindableProperty SnapPositionProperty =
           BindableProperty.Create(propertyName: "SnapPosition",
           returnType: typeof(float),
           declaringType: typeof(Carousel),
           defaultValue: 0f,
           defaultBindingMode: BindingMode.OneWay);
        /// <summary>
        /// The relative Snap Position to which the carousel will snap
        /// relatively to the screen, vales between 0 (left) and 1 (right)
        /// </summary>
        public float SnapPosition
        {
            get { return (float)GetValue(SnapPositionProperty); }
            set { SetValue(SnapPositionProperty, (value < 0) ? 0 : (value > 1f) ? 1f : value); }
        }
        #endregion
        #region IndicatorsProperties
        /// <summary>
        /// Enable or disable Indicators
        /// </summary>
        public static readonly BindableProperty IndicatorsProperty =
            BindableProperty.Create(propertyName: "Indicators",
            returnType: typeof(bool),
            declaringType: typeof(Carousel),
            defaultValue: false,
            defaultBindingMode: BindingMode.OneWay);
        /// <summary>
        /// Enable or disable Indicators
        /// </summary>
        public bool Indicators
        {
            get { return (bool)GetValue(IndicatorsProperty); }
            set { SetValue(IndicatorsProperty, value); }
        }
        /// <summary>
        /// Place indicators above or below the carousel
        /// True is above, false(default) is below
        /// </summary>
        public static readonly BindableProperty IndicatorsAboveCarouselProperty =
            BindableProperty.Create(propertyName: "IndicatorsAboveCarousel",
            returnType: typeof(bool),
            declaringType: typeof(Carousel),
            defaultValue: false,
            defaultBindingMode: BindingMode.OneWay);
        /// <summary>
        /// Place indicators above or below the carousel
        /// True is above, false(default) is below
        /// </summary>
        public bool IndicatorsAboveCarousel
        {
            get { return (bool)GetValue(IndicatorsAboveCarouselProperty); }
            set { SetValue(IndicatorsAboveCarouselProperty, value); }
        }
        /// <summary>
        /// The Color of the indicators
        /// </summary>
        public static readonly BindableProperty IndicatorsColorProperty =
            BindableProperty.Create(propertyName: "IndicatorsColor",
            returnType: typeof(Color),
            declaringType: typeof(Carousel),
            defaultValue: Color.White,
            defaultBindingMode: BindingMode.OneWay);
        /// <summary>
        /// The Color of the indicators
        /// </summary>
        public Color IndicatorsColor
        {
            get { return (Color)GetValue(IndicatorsColorProperty); }
            set { SetValue(IndicatorsColorProperty, value); }
        }
        /// <summary>
        /// The size of the indicators
        /// </summary>
        public static readonly BindableProperty IndicatorsSizeProperty =
            BindableProperty.Create(propertyName: "IndicatorsSize",
            returnType: typeof(int),
            declaringType: typeof(Carousel),
            defaultValue: 10,
            defaultBindingMode: BindingMode.OneWay);
        /// <summary>
        /// The size of the indicators
        /// </summary>
        public int IndicatorsSize
        {
            get { return (int)GetValue(IndicatorsSizeProperty); }
            set { SetValue(IndicatorsSizeProperty, value); }
        }
        #endregion
        #region ItemSourceProperties
        /// <summary>
        /// The itemsource of the carousel
        /// </summary>
        public static readonly BindableProperty ItemsSourceProperty =
            BindableProperty.Create(propertyName: "ItemsSource",
            returnType: typeof(IEnumerable),
            declaringType: typeof(Carousel),
            defaultValue: default(IEnumerable),
            defaultBindingMode: BindingMode.OneWay,
            propertyChanged: OnItemSourceChanged);

        private static void OnItemSourceChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (Carousel)bindable;
            control.Render(oldValue, newValue);
        }
        /// <summary>
        /// The itemsource of the carousel
        /// </summary>
        public IEnumerable ItemsSource
        {
            get { return (IEnumerable)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }
        /// <summary>
        /// The itemtemplate of the carousel
        /// </summary>
        public static readonly BindableProperty ItemTemplateProperty =
            BindableProperty.Create(propertyName: "ItemTemplate",
            returnType: typeof(DataTemplate),
            declaringType: typeof(Carousel),
            defaultValue: default(DataTemplate),
            defaultBindingMode: BindingMode.OneWay,
            propertyChanged: OnItemTemplateChanged);
        private static void OnItemTemplateChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (Carousel)bindable;
            control.Render(null, null, true);
        }
        /// <summary>
        /// The itemtemplate of the carousel
        /// </summary>
        public DataTemplate ItemTemplate
        {
            get { return (DataTemplate)GetValue(ItemTemplateProperty); }
            set { SetValue(ItemTemplateProperty, value); }
        }
        #endregion
        /// <summary>
        /// The constructor of the carousel
        /// </summary>
        public Carousel()
        {
            InitializeComponent();
            carouselScrollView.carouselParent = this;
            //tapGestureRecognizer.Tapped += TapGestureRecognizer_Tapped;
        }
        protected override void OnChildAdded(Element child)
        {
            base.OnChildAdded(child);
            if (child is View childview)
            {
                //childview.GestureRecognizers.Add(tapGestureRecognizer);
            }
        }
        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
        }
        /// <summary>
        /// Not to be called
        /// </summary>
        public void Render(object oldItemsSource, object newItemsSource, bool completeReRender = false)
        {
            if (this.ItemTemplate == null || this.ItemsSource == null)
                return;
            var layout = new CustomStackLayout();
            layout.carouselParent = this;
            layout.Orientation = StackOrientation.Horizontal;
            layout.Spacing = Spacing;
            carouselPagesCount = 0;
            carouselWidth = 0;
            carouselScrollPosition = 0;
            foreach (var item in this.ItemsSource)
            {
                carouselPagesCount++;
                var viewCell = this.ItemTemplate.CreateContent() as ViewCell;
                viewCell.View.BindingContext = item;
                layout.Children.Add(viewCell.View);
            }
            carouselScrollView.Content = layout;
            carouselScrollView.Scrolled += CarouselScrollView_Scrolled;
            //Create indicators and place them abover or below the carousel
            if (Indicators)
            {
                var buttonList = carouselMainLayout.Children.Where(x => x.GetType() == typeof(DotButtonsLayout)).ToList();
                if (buttonList.Count > 0)
                {
                    for (int i = 0; i < buttonList.Count; i++)
                    {
                        carouselMainLayout.Children.Remove(buttonList[i]);
                    }
                }
                dotLayout = new DotButtonsLayout(carouselPagesCount, IndicatorsColor, IndicatorsSize);
                foreach (DotButton dot in dotLayout.dots)
                    dot.Clicked += DotClicked;
                if (IndicatorsAboveCarousel)
                {
                    carouselMainLayout.Children.Insert(0, dotLayout);
                }
                else
                {
                    carouselMainLayout.Children.Add(dotLayout);
                }
            }
        }
        //The function called by the buttons clicked event
        private async void DotClicked(object sender)
        {
            var button = (DotButton)sender;
            //Get the selected buttons index
            int index = button.index;
            //Set the corresponding page as position of the carousel view
            var snapOffset = carouselScrollView.scrollViewWidth * SnapPosition - carouselContentViewSize / 2;
            if (snapOffset < 0)
            {
                snapOffset = 0;
            }
            var desiredPosition = carouselScrollView.placeHolderOffset + (carouselContentViewSize + carouselScrollView.spacing) * index;
            desiredPosition -= snapOffset;
            await carouselScrollView.ScrollToAsync(desiredPosition, 0, true);
        }
        /// <summary>
        /// Not to be called
        /// </summary>
        public async Task Snap(int snapDirection = 0)
        {
            if (carouselScrollPosition < carouselScrollView.placeHolderOffset)
            {
                var startPosition = carouselScrollView.scrollViewWidth * SnapPosition - carouselContentViewSize / 2;
                if (startPosition < 0)
                {
                    startPosition = 0;
                }
                await carouselScrollView.ScrollToAsync(carouselScrollView.placeHolderOffset - startPosition, 0, true);
            }
            else
            {
                var endPosition = carouselScrollView.placeHolderOffset;
                endPosition += (carouselPagesCount - 1) * (carouselContentViewSize + carouselScrollView.spacing);
                var snapOffset = carouselScrollView.scrollViewWidth * SnapPosition - carouselContentViewSize / 2;
                if (snapOffset < 0)
                {
                    snapOffset = 0;
                }
                endPosition -= snapOffset;
                if (carouselScrollPosition > endPosition)
                {
                    await carouselScrollView.ScrollToAsync(endPosition, 0, true);
                }
                else
                {
                    if (Snapping)
                    {
                        if (snapDirection == 0)
                        {
                            snapOffset = carouselScrollView.scrollViewWidth * SnapPosition - carouselContentViewSize / 2;
                            if (snapOffset < 0)
                            {
                                snapOffset = 0;
                            }
                            var desiredPosition = carouselScrollPosition - carouselScrollView.placeHolderOffset + snapOffset;
                            desiredPosition = (carouselContentViewSize + carouselScrollView.spacing) * Math.Round((desiredPosition / (carouselContentViewSize + carouselScrollView.spacing)));
                            desiredPosition -= snapOffset;
                            await carouselScrollView.ScrollToAsync(carouselScrollView.placeHolderOffset + desiredPosition, 0, true);
                        }
                        else
                        {
                            var currentScrollPosition = carouselScrollPosition - carouselScrollView.placeHolderOffset;
                            var desiredCarouselPosition = 0d;
                            if (snapDirection > 0)
                            {
                                desiredCarouselPosition = Math.Floor(currentScrollPosition / (carouselContentViewSize + carouselScrollView.spacing)) + snapDirection;
                            }
                            else
                            {
                                desiredCarouselPosition = Math.Ceiling(currentScrollPosition / (carouselContentViewSize + carouselScrollView.spacing)) + snapDirection;
                            }
                            if(desiredCarouselPosition < 0)
                            {
                                desiredCarouselPosition = 0;
                            }
                            else if(desiredCarouselPosition > carouselPagesCount)
                            {
                                desiredCarouselPosition = carouselPagesCount;
                            }
                            snapOffset = carouselScrollView.scrollViewWidth * SnapPosition - carouselContentViewSize / 2;
                            if (snapOffset < 0)
                            {
                                snapOffset = 0;
                            }
                            var desiredPosition = (carouselContentViewSize + carouselScrollView.spacing) * desiredCarouselPosition;
                            desiredPosition -= snapOffset;
                            await carouselScrollView.ScrollToAsync(carouselScrollView.placeHolderOffset + desiredPosition, 0, true);
                        }
                    }
                }
            }
        }
        private void CarouselScrollView_Scrolled(object sender, ScrolledEventArgs e)
        {
            if (carouselWidth != carouselScrollView.ContentSize.Width)
            {
                carouselWidth = carouselScrollView.ContentSize.Width;
                carouselContentViewSize = ((carouselWidth - 2 * carouselScrollView.placeHolderOffset - ((carouselPagesCount - 1) * carouselScrollView.spacing)) / (carouselPagesCount));
            }
            if (Indicators)
            {
                var previousPosition = 1 + Math.Round((carouselScrollPosition - carouselScrollView.placeHolderOffset) / (carouselContentViewSize + carouselScrollView.spacing));
                var currentPosition = 1 + Math.Round((e.ScrollX - carouselScrollView.placeHolderOffset) / (carouselContentViewSize + carouselScrollView.spacing));
                if (previousPosition != currentPosition)
                {
                    for (int i = 0; i < dotLayout.dots.Length; i++)
                        if (currentPosition == i)
                            dotLayout.dots[i].Opacity = 1;
                        else
                            dotLayout.dots[i].Opacity = 0.5;
                    Position = (int)currentPosition;
                    OnPositionChanged?.Invoke(this, new PositionChangedEventArgs() { Position = (int)currentPosition });
                }
            }
            carouselScrollPosition = e.ScrollX;
        }
    }
}

