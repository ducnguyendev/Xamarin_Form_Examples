using System;
using System.Collections.Generic;
using Xamarin.Forms;
using XLabs.Forms.Mvvm;


namespace CameraXlab
{
	/// <summary>
	/// Class CameraPage.
	/// </summary>
	public partial class CameraPage : ContentPage
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="CameraPage"/> class.
		/// </summary>
		public CameraPage()
		{
			InitializeComponent();
			BindingContext = new CameraViewModel ();
		}
	}
}

