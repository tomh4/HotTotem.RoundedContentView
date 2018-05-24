# HotTotem.RoundedContentView
A ContentView which allows for rounded edges

<img src="https://hot-totem.com/storage/app/media/Blogimages/Demo.png">

This control allows for several settings and options : 
* **CornerRadius** : As the name says, this will be the radius applied to the rounded corners. This is an integer and defaults to 0, so not rounded.
* **FillColor** : This will allow you to set the background color of the RoundedContentView. The difference between this and *BackgroundColor* is that the FillColor will be Rounded off in case of a rounded edge being applied, and the BackgroundColor will not. You can either use Hex-Codes or the proposed predefined Colors.
* **Circle** : This property allows you to make the edges rounded until they touch each other. This means at least 2 edges will be circular. If your ContentView has the same height as width, it will be a circle. This is a boolean. Defaults to false.
* **HasShadow** : If this boolean is set to true, the ContentView will drop a shadow, otherwise not. Defaults to no shadow.
* **BorderWidth** : You can apply a border to your RoundedContentView ( as seen in the second box ) and here you can set its width as an integer. Defaults to 0.
* **BorderColor** : Guess what, this will allow you to set the color of your Border. Of course, only if you set a *BorderWidth*. Defaults to white.

It is a Xamarin.Forms plugin and needs no platform specific code on your side.
Simply install the Nuget Package to all your projects ( be aware this is a  .NET Standard plugin! ) and you are ready to use the RoundedContentView!

For a detailed walkthrough on using this Plugin and it's settings, read this guide : https://hot-totem.com/blog/post/plugins-hottotemroundedcontentview

<img src="https://hot-totem.com/storage/app/media/Files/logohottotemfinal-270276.png"  height="200">
