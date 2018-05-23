using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CarouselView;
using CarouselView.iOS.CustomRenderers;
using Foundation;
using UIKit;
using Xamarin.Forms;
using CarouselView.CustomControls;
using Xamarin.Forms.Platform.iOS;
using System.ComponentModel;

[assembly: ExportRenderer(typeof(CustomScrollView), typeof(CarouselRenderer))]
namespace CarouselView.iOS.CustomRenderers
{
    public class CarouselRenderer : ScrollViewRenderer
    {
        private static readonly float SlowDownThreshold = 2f;
        UIScrollView _scrollView;
        bool isCurrentlyTouched = false;
        bool hasSnapped = true;
        bool isScrolling = false;
        CustomScrollView currentScrollView;
        int scrollDirection = 0;
        int lastContentOffset = 0;

        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);
            if (e.NewElement == null) return;
            currentScrollView = (CustomScrollView)e.NewElement;
            e.NewElement.PropertyChanged += ElementPropertyChanged;
        }


        void ElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Renderer")
            {
                this.ShowsHorizontalScrollIndicator = false;
                this.ShowsVerticalScrollIndicator = false;
                this.Scrolled += CarouselRenderer_Scrolled;
                this.DecelerationEnded += CarouselRenderer_DecelerationEnded;
                this.DraggingStarted += CarouselRenderer_DraggingStarted;
                this.DraggingEnded += CarouselRenderer_DraggingEnded;
                this.DecelerationStarted += CarouselRenderer_DecelerationStarted;
            }
        }

        private async void CarouselRenderer_DecelerationStarted(object sender, EventArgs e)
        {
            isCurrentlyTouched = false;
            // Not always triggered, play around with scrolling speed
            if (currentScrollView.carouselParent.SnapMode == Carousel.SnappingMode.Instant && !hasSnapped)
            {
                hasSnapped = true;
                await ((CustomScrollView)Element).carouselParent.Snap(scrollDirection);
            }
        }

        private async void CarouselRenderer_DraggingEnded(object sender, DraggingEventArgs e)
        {
            isCurrentlyTouched = false;
            if (currentScrollView.carouselParent.SnapMode == Carousel.SnappingMode.RollOut)
            {
                if (!isScrolling && !hasSnapped)
                {
                    hasSnapped = true;
                    await ((CustomScrollView)Element).carouselParent.Snap();
                }
            }
            else
            {
                if (!Decelerating && !hasSnapped)
                {
                    if (currentScrollView.carouselParent.SnapMode == Carousel.SnappingMode.Instant)
                    {
                        hasSnapped = true;
                        await ((CustomScrollView)Element).carouselParent.Snap(scrollDirection);
                    }
                }
            }
        }
        private void CarouselRenderer_DraggingStarted(object sender, EventArgs e)
        {
            hasSnapped = false;
            isCurrentlyTouched = true;
        }

        private async void CarouselRenderer_DecelerationEnded(object sender, EventArgs e)
        {
            isScrolling = false;
            if (!isCurrentlyTouched && !hasSnapped && currentScrollView.carouselParent.SnapMode == Carousel.SnappingMode.RollOut)
            {
                hasSnapped = true;
                await ((CustomScrollView)Element).carouselParent.Snap();
            }
        }

        private void CarouselRenderer_Scrolled(object sender, EventArgs e)
        {
            isScrolling = true;
            if (lastContentOffset < (int)this.ContentOffset.X)
            {
                scrollDirection = 1;
            }
            else if (lastContentOffset > (int)this.ContentOffset.X)
            {
                scrollDirection = -1;
            }
            else
            {
                scrollDirection = 0;
            }
            lastContentOffset = (int)this.ContentOffset.X;
        }
    }
}