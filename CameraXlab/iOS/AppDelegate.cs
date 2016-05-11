using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;
using XLabs.Forms;
using XLabs.Ioc;
using System.IO;
using XLabs.Platform.Device;
using XLabs.Forms.Services;
using XLabs.Serialization;
using XLabs.Platform.Services;
using XLabs.Platform.Services.Email;
using XLabs.Platform.Services.Media;
using XLabs.Platform.Mvvm;

namespace CameraXlab.iOS
{
	[Register ("AppDelegate")]
	public partial class AppDelegate : XFormsApplicationDelegate
	{
		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
			this.SetIoc();

			global::Xamarin.Forms.Forms.Init ();

			LoadApplication (new App ());

			return base.FinishedLaunching (app, options);
		}

		/// <summary>
		/// Sets the IoC.
		/// </summary>
		private void SetIoc()
		{
			var resolverContainer = new SimpleContainer();

			var app = new XFormsAppiOS();
			app.Init(this);

			var documents = app.AppDataDirectory;
//			var pathToDatabase = Path.Combine(documents, "xforms.db");

			resolverContainer.Register<IDevice>(t => AppleDevice.CurrentDevice)
				.Register<IDisplay>(t => t.Resolve<IDevice>().Display)
				.Register<IFontManager>(t => new FontManager(t.Resolve<IDisplay>()))
//				.Register<IJsonSerializer, XLabs.Serialization.ServiceStack.JsonSerializer>()
				//.Register<IJsonSerializer, Services.Serialization.SystemJsonSerializer>()
				.Register<ITextToSpeechService, TextToSpeechService>()
				.Register<IEmailService, EmailService>()
				.Register<IMediaPicker, MediaPicker>()
				.Register<IXFormsApp>(app)
				.Register<ISecureStorage, SecureStorage>()
				.Register<IDependencyContainer>(t => resolverContainer);

			Resolver.SetResolver(resolverContainer.GetResolver());
		}
	}
}

