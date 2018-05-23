using System;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Support.V4.View;
using Android;
using HotTotem.RoundedContentView.Droid.CustomRenderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using HotTotem.RoundedContentView;

[assembly: ExportRenderer(typeof(RoundedContentView), typeof(RoundedContentViewRenderer))]
namespace HotTotem.RoundedContentView.Droid.CustomRenderers
{
    public class RoundedContentViewRenderer : ViewRenderer
    {
        public RoundedContentViewRenderer(Context context) : base(context)
        {
        }
        protected override void OnElementChanged(ElementChangedEventArgs<View> e)
        {
            base.OnElementChanged(e);
        }
        protected override bool DrawChild(Canvas canvas, global::Android.Views.View child, long drawingTime)
        {
            if (Element == null) return false;

            RoundedContentView rcv = (RoundedContentView)Element;
            this.SetClipChildren(true);

            rcv.Padding = new Thickness(0, 0, 0, 0);
            int radius = (int)(rcv.CornerRadius);
            if (rcv.Circle)
            {
                radius = Math.Min(Width, Height) / 2;
            }
            radius *= 2;

            try
            {
                var path = new Path();
                path.AddRoundRect(new RectF(0, 0, Width, Height),
                              new float[] { radius, radius, radius, radius, radius, radius, radius, radius },
                              Path.Direction.Ccw);
                if (rcv.HasShadow)
                {
                    var shadowPath = new Path();
                    shadowPath.AddRoundRect(new RectF(5, 5, Width, Height),
                                  new float[] { radius, radius, radius, radius, radius, radius, radius, radius },
                                  Path.Direction.Ccw);
                    var paint = new Paint();
                    paint.AntiAlias = true;
                    paint.StrokeWidth = 5;
                    paint.SetStyle(Paint.Style.Stroke);
                    paint.Color = Xamarin.Forms.Color.FromRgba(0, 0, 0, 0.3).ToAndroid();
                    canvas.DrawPath(shadowPath, paint);
                }
                canvas.Save();
                canvas.ClipPath(path);
                canvas.DrawColor(rcv.FillColor.ToAndroid());
                var result = base.DrawChild(canvas, child, drawingTime);

                canvas.Restore();
                if (rcv.BorderWidth > 0)
                {
                    var paint = new Paint();
                    paint.AntiAlias = true;
                    paint.StrokeWidth = rcv.BorderWidth;
                    paint.SetStyle(Paint.Style.Stroke);
                    paint.Color = rcv.BorderColor.ToAndroid();
                    canvas.DrawPath(path, paint);
                }
                path.Dispose();
                return result;
            }
            catch (Exception ex)
            {
                System.Console.Write(ex.Message);
            }

            return base.DrawChild(canvas, child, drawingTime);
        }
    }
}

