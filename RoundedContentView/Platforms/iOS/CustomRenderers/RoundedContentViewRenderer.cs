using System;
using System.Diagnostics;
using CoreGraphics;
using HotTotem.RoundedContentView;
using HotTotem.RoundedContentView.iOS.CustomRenderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(RoundedContentView), typeof(RoundedContentViewRenderer))]
namespace HotTotem.RoundedContentView.iOS.CustomRenderers
{
    public class RoundedContentViewRenderer : ViewRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<View> e)
        {
            base.OnElementChanged(e);

            if (this.Element == null) return;

            this.Element.PropertyChanged += (sender, e1) =>
            {
                try
                {
                    if (NativeView != null)
                    {
                        NativeView.SetNeedsDisplay();
                        NativeView.SetNeedsLayout();
                    }
                }
                catch (Exception exp)
                {
                    Debug.WriteLine("Handled Exception in RoundedCornerViewDemoRenderer. Just warngin : " + exp.Message);
                }
            };
        }

        public override void Draw(CoreGraphics.CGRect rect)
        {
            base.Draw(rect);

            this.LayoutIfNeeded();

            RoundedContentView rcv = (RoundedContentView)Element;
            //rcv.HasShadow = false;  
            if (rcv == null)
                return;
            //rcv.Margin = rcv.CustomMargin;
            //this.BackgroundColor = rcv.FillColor.ToUIColor();  
            this.ClipsToBounds = true;
            this.Layer.BackgroundColor = rcv.FillColor.ToCGColor();
            this.Layer.MasksToBounds = true;
            this.Layer.CornerRadius = (nfloat)rcv.RoundedCornerRadius;
            if (rcv.HasShadow)
            {
                this.Layer.ShadowRadius = 3.0f;
                this.Layer.ShadowColor = UIColor.Gray.CGColor;
                this.Layer.ShadowOffset = new CGSize(1, 1);
                this.Layer.ShadowOpacity = 0.60f;
                this.Layer.ShadowPath = UIBezierPath.FromRect(Layer.Bounds).CGPath;
                this.Layer.MasksToBounds = false;
            }
            if (rcv.MakeCircle)
            {
                this.Layer.CornerRadius = (int)(Math.Min(Element.Width, Element.Height) / 2);
            }
            this.Layer.BorderWidth = 0;

            if (rcv.BorderWidth > 0 && rcv.BorderColor.A > 0.0)
            {
                this.Layer.BorderWidth = rcv.BorderWidth;
                this.Layer.BorderColor =
                    new UIKit.UIColor(
                    (nfloat)rcv.BorderColor.R,
                    (nfloat)rcv.BorderColor.G,
                    (nfloat)rcv.BorderColor.B,
                        (nfloat)rcv.BorderColor.A).CGColor;
            }
        }
    }
}


