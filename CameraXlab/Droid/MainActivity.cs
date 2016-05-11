using System.IO;
using Android.App;
using Android.Content.PM;
using Android.OS;
using XLabs.Ioc;
using XLabs.Forms;
using XLabs.Platform.Device;
using XLabs.Platform.Services.Media;
using XLabs.Forms.Services;
using XLabs.Serialization;
using XLabs.Platform.Services.Email;
using XLabs.Platform.Services;
using XLabs.Platform.Mvvm;

namespace CameraXlab.Droid
{
	[Activity (Label = "CameraXlab.Droid", Icon = "@drawable/icon", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : XFormsApplicationDroid
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			if (!Resolver.IsSet)
			{
				this.SetIoc();
			}
			else
			{
				var app = Resolver.Resolve<IXFormsApp>() as IXFormsApp<XFormsApplicationDroid>;
				if (app != null) app.AppContext = this;
			}

			global::Xamarin.Forms.Forms.Init (this, bundle);

			LoadApplication (new App ());
		}

		/// <summary>
		/// Sets the IoC.
		/// </summary>
		private void SetIoc()
		{
			var resolverContainer = new SimpleContainer();

			var app = new XFormsAppDroid();

			app.Init(this);

			var documents = app.AppDataDirectory;
//			var pathToDatabase = Path.Combine(documents, "xforms.db");

			resolverContainer.Register<IDevice>(t => AndroidDevice.CurrentDevice)
				.Register<IDisplay>(t => t.Resolve<IDevice>().Display)
				.Register<IFontManager>(t => new FontManager(t.Resolve<IDisplay>()))
				//.Register<IJsonSerializer, Services.Serialization.JsonNET.JsonSerializer>()
				.Register<IEmailService, EmailService>()
				.Register<IMediaPicker, MediaPicker>()
				.Register<ITextToSpeechService, TextToSpeechService>()
				.Register<IDependencyContainer>(resolverContainer)
				.Register<IXFormsApp>(app)
				.Register<ISecureStorage>(t => new KeyVaultStorage(t.Resolve<IDevice>().Id.ToCharArray()))
				;


			Resolver.SetResolver(resolverContainer.GetResolver());
		}
	}
}

